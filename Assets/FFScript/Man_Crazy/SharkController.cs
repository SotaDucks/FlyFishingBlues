using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public class SharkController : MonoBehaviour
{
    public Transform objectToMove; // 要移动的物体
    public Transform targetPosition; // 目标位置
    public float moveDuration = 2f; // 移动持续时间
    public Ease moveEaseType = Ease.InOutSine; // 缓动类型
    public GameObject Hook;
   public  Rigidbody rb;
    private void Start()
    {
     rb=Hook.GetComponent<Rigidbody>();
    }
    public void MoveFish()
    {
        objectToMove.DOMove(targetPosition.position, moveDuration)
                    .SetEase(moveEaseType)
                    .OnStart(() => Debug.Log("移动开始"))
                    .OnComplete(() => Debug.Log("移动完成"));
        HookKinematic();
    }
    private void HookKinematic()
    { 
    rb.isKinematic = true;
    }

}
