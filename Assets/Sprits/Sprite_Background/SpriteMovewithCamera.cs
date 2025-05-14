using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SpriteMovewithCamera : MonoBehaviour
{
    [Header("References")]
    public CinemachineVirtualCamera vcam;                    // 拖入 PlayerFollowCamera 虚机位
    private CinemachineFramingTransposer transposer;

    [Header("Baseline")]
    public float baseCameraDistance = 16.8f;                 // “不抛竿”距离
    [Range(-2f, 2f)]
    public float parallaxFactor = 1f;                        // 1=完全跟随

    [Header("Smoothing")]
    [Tooltip("越小越紧跟，越大越柔和（单位：秒）")]
    public float smoothTime = 0.25f;                         // 0.2~0.3 比较自然

    private Vector3 initialPos;
    private float zVelocity;                                 // SmoothDamp 的内部速度缓存

    void Awake()
    {
        transposer = vcam.GetCinemachineComponent<CinemachineFramingTransposer>();
        initialPos = transform.position;
    }

    void LateUpdate()
    {
        // 目标 Z 位置
        float delta = (transposer.m_CameraDistance - baseCameraDistance) * parallaxFactor;
        float targetZ = initialPos.z + delta;

        // 平滑插值到目标 Z
        Vector3 pos = transform.position;
        pos.z = Mathf.SmoothDamp(pos.z, targetZ, ref zVelocity, smoothTime);
        transform.position = pos;
    }
}
