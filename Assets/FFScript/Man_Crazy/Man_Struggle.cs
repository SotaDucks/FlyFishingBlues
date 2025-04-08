using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Man_Struggle : MonoBehaviour
{
    [Header("挣扎力度")]
    public float minForce = 1f; // 最小力
    public float maxForce = 2f; // 最大力

    [Header("挣扎间隔时间范围")]
    public float minInterval = 0.3f; // 最小间隔
    public float maxInterval = 1.2f; // 最大间隔

    [Header("头部旋转速度范围 (秒)")]
    public float minRotateDuration = 0.2f; // 最小旋转时间
    public float maxRotateDuration = 0.6f; // 最大旋转时间

    [Header("头部旋转角度范围")]
    public float angleRange = 90f; // 旋转角度范围

    public GameObject eelHead; // 鳗鱼头部

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }

        rb.useGravity = true;

        // 启动挣扎过程
        StartNextStruggle();
    }

    private void StartNextStruggle()
    {
        // 设置一个随机时间间隔，控制挣扎的节奏
        float interval = Random.Range(minInterval, maxInterval);
        Invoke(nameof(PerformStruggle), interval);
    }

    private void PerformStruggle()
    {
        if (eelHead == null) return;

        // 随机目标角度（Z轴旋转）
        float randomAngle = Random.Range(-angleRange, angleRange);
        Vector3 currentEuler = eelHead.transform.localEulerAngles;
        Vector3 targetEuler = new Vector3(currentEuler.x, currentEuler.y, randomAngle);

        // 随机旋转时间
        float rotateDuration = Random.Range(minRotateDuration, maxRotateDuration);

        // 使用 DOTween 旋转头部
        eelHead.transform.DOLocalRotate(targetEuler, rotateDuration, RotateMode.FastBeyond360)
            .SetEase(Ease.InOutSine)
            .OnComplete(() =>
            {
                // 旋转完成后施加随机方向的力
                ApplyRandomForce();
                StartNextStruggle(); // 完成一次挣扎后，启动下一次
            });
    }

    private void ApplyRandomForce()
    {
        // 生成一个随机的力方向，方向是随机的
        Vector3 randomDirection = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f)).normalized;

        // 随机设置力的大小
        float forceMagnitude = Random.Range(minForce, maxForce);

        // 向身体施加随机方向的力
        rb.AddForce(randomDirection * forceMagnitude, ForceMode.Impulse);
    }
}
