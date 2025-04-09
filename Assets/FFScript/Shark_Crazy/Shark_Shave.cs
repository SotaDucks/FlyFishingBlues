using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Shark_Shake : MonoBehaviour
{


    [Header("旋转控制")]
    public float minAngle = -45f; // 最小旋转角度
    public float maxAngle = 45f; // 最大旋转角度
    public float duration = 2f;  // 每次 PingPong 运动的持续时间

    private float targetRotation;
    private float currentRotation;
    private float nextRotationTime;
    private bool isIncreasing = true; // 用于控制旋转方向

    void Start()
    {
        // 初始化当前旋转角度
        currentRotation = transform.localEulerAngles.y;
        nextRotationTime = Time.time + duration;

        // 开始使用 DOTween 进行旋转动画
        
    }

    void Update()
    {
        // 根据时间间隔来控制每次旋转的加减幅度
        if (Time.time >= nextRotationTime)
        {
            // 随机生成一个旋转角度
            float randomAngle = Random.Range(minAngle, maxAngle);

            if (isIncreasing)
            {
                // 加上随机角度
                targetRotation = currentRotation + randomAngle;
            }
            else
            {
                // 减去随机角度
                targetRotation = currentRotation - randomAngle;
            }

            // 启动旋转动画
            RotateToTarget(targetRotation);

            // 切换旋转方向
            isIncreasing = !isIncreasing;

            // 计算下一次变化的时间
            nextRotationTime = Time.time + duration;
        }
    }

    private void RotateToTarget(float target)
    {
        // 使用 DOTween 来进行旋转，平滑的进行角度变换
        DOTween.To(() => transform.localEulerAngles.y, x => transform.localEulerAngles = new Vector3(0f, x, 0f), target, duration)
            .SetEase(Ease.InOutSine);
    }
}
