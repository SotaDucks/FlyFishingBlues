using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ManStru : MonoBehaviour
{
    public float range = 60f;  // 旋转的范围
    public float speed = 1f;   // 旋转的速度

    private void Update()
    {
        // 计算旋转角度，范围从 -60 到 60
        float angle = Mathf.PingPong(Time.time * speed, range * 2) - range;

        // 更新物体的旋转（沿着自身的 X 轴旋转）
        transform.rotation = Quaternion.Euler(angle, angle*0.1f, transform.rotation.eulerAngles.z);
    }
}
