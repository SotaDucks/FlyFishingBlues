using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Splines;

public class Attract_Shark : MonoBehaviour
{
    public Transform shark;         // Reference to the shark object
    public Transform fishingHook;   // Reference to the fishing hook object
    public float moveDistance = 5f; // Distance the shark moves forward when pressing "S"
    public float Max;
    public float Min;
    private Vector3 recordedPosition;
    private Vector3 recordedRotation;
    bool Working;
    private void Start()
    {
        Working = true;
    }
    void Update()
    {
        // 检测按下"S"键
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (fishingHook.position.y < -4f )
            {
                Debug.LogWarning("鱼钩够底了");
            }

            // 获取鱼钩的Y位置，确保小于-4
            if ( shark.eulerAngles.y < Max && shark.transform.eulerAngles.y > Min)
            {
                
              
                
                    Working = true;
                    BanFollowLine();
                    MoveTowardsObject(moveDistance);
                    Debug.LogWarning("移动了");
                
               
                   
    
            }
        }
    }

   

    public void BanFollowLine()
    {
        SplineAnimate splineScript = GetComponent<SplineAnimate>();
        splineScript.enabled = false;
        recordedPosition = transform.position;
        recordedRotation = transform.eulerAngles;

    }
    public void MoveTowardsObject( float distance)
    {
        // 计算目标位置（targetObject的方向 + 目标距离）
        Vector3 directionToTarget = (fishingHook.position - shark.position).normalized;
        Vector3 targetPosition = shark.position + directionToTarget * distance;

        // 使用 DOTween 让物体移动到目标位置
        shark.DOMove(targetPosition, 1f).OnComplete(() => { MoveToTargetAndRotate(recordedPosition,recordedRotation); });  // 1f 是移动的时间，可以根据需要调整
    }
    public void EnableFollowLine()
    {
        SplineAnimate splineScript = GetComponent<SplineAnimate>();
        splineScript.enabled = true;

    }
    public void MoveToTargetAndRotate(Vector3 targetPosition, Vector3 targetRotation)
    {
        // 移动物体到目标位置，并设置移动时间（例如 2 秒）
        transform.DOMove(targetPosition, 1f).OnComplete(() =>
        {
            // 动画完成后，调用 EnableFollowLine 方法
            EnableFollowLine();
            Working = false;
        });

        // 旋转物体到目标角度（欧拉角）
        transform.DORotate(targetRotation, 2f);
    }
}
