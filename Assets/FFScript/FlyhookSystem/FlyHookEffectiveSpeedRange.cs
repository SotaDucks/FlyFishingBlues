using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class FlyHookEffectiveSpeedRange : MonoBehaviour
{
    [Header("速度区间 (单位：m/s)")]
    [Tooltip("当速度大于等于此值时，才可能激活 Collider")]
    public float minEffectiveSpeed = 1f;
    [Tooltip("当速度小于等于此值时，才可能激活 Collider")]
    public float maxEffectiveSpeed = 10f;

    private SphereCollider sphereCollider;
    private Vector3 previousPosition;

    void Awake()
    {
        sphereCollider = GetComponent<SphereCollider>();
        previousPosition = transform.position;
        // 初始时先禁用
        sphereCollider.enabled = false;
    }

    void Update()
    {
        // 计算速度 = 距离 / 时间
        Vector3 delta = transform.position - previousPosition;
        float speed = delta.magnitude / Time.deltaTime;
        previousPosition = transform.position;

        // 在控制台输出当前速度，保留两位小数
        Debug.Log($"[FlyHook] 当前速度: {speed:F2} m/s");

        // 根据速度区间启用 / 禁用 Collider
        bool inRange = speed >= minEffectiveSpeed && speed <= maxEffectiveSpeed;
        if (sphereCollider.enabled != inRange)
            sphereCollider.enabled = inRange;
    }
}
