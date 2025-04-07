using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMouth : MonoBehaviour
{
    public float angleOffset = 5f;      // ±5度
    public float cycleDuration = 2f;    // 一次来回的时间（秒）

    private float startYAngle;

    void Start()
    {
        // 记录初始的本地 Y 轴角度
        startYAngle = transform.localEulerAngles.x;
    }

    void Update()
    {
        // 匀速摆动的核心：PingPong 线性返回一个值
        float t = Mathf.PingPong(Time.time / cycleDuration, 1f); // t: 0~1~0

        // 计算当前摆动角度（-5 到 +5）
        float offset = Mathf.Lerp(-angleOffset, angleOffset, t);

        // 设置新的旋转（只修改 Y，保持 X/Z 不变）
        Vector3 currentEuler = transform.localEulerAngles;
        currentEuler.x = startYAngle + offset;
        transform.localEulerAngles = currentEuler;
    }
}
