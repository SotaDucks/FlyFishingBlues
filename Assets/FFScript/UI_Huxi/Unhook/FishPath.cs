using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FishPath : MonoBehaviour
{
    public GameObject Armature;
    [Header("路径参数")]
    public Vector3[] pathPoints = new Vector3[4]; // 四个路径点
    public float moveSpeed = 2f; // 移动速度
    public float reachThreshold = 0.1f; // 到达点的距离阈值

    [Header("终点参数")]
    public Vector3 finalForce = new Vector3(5f, 2f, 0f); // 终点施加的力
    public float rotationSpeed = 5f; // 旋转调整速度

    private Rigidbody rb;
    private int currentPointIndex = 0;
    private bool isPathComplete = false;
    private Vector3 initialPosition;

    void Start()
    {
        rb = GetComponentInParent<Rigidbody>();
        initialPosition = rb.transform.position;

        // 如果没有设置路径点，初始化默认值
        if (pathPoints.Length != 4)
        {
            pathPoints = new Vector3[4];
            pathPoints[0] = initialPosition;
            pathPoints[1] = initialPosition + new Vector3(2f, 1f, 0f);
            pathPoints[2] = initialPosition + new Vector3(4f, 0f, 0f);
            pathPoints[3] = initialPosition + new Vector3(6f, -1f, 0f);
        }

        rb.drag = 1f;
        rb.angularDrag = 2f;
    }

    void FixedUpdate()
    {
        Debug.Log("123");
        if (!isPathComplete)
        {
            MoveAlongPath();
        }
        else
        {
            // 保持物理模拟继续进行
            AdjustOrientation();
        }
    }

    void MoveAlongPath()
    {
        // 获取目标点
        Vector3 targetPoint = pathPoints[currentPointIndex];

        // 计算移动方向和距离
        Vector3 direction = (targetPoint - rb.position).normalized;
        float distance = Vector3.Distance(rb.position, targetPoint);

        // 移动鱼
        rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);

        // 朝向移动方向
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime));

        // 检查是否到达当前点
        if (distance <= reachThreshold)
        {
            currentPointIndex++;

            // 如果到达最后一个点
            if (currentPointIndex >= pathPoints.Length)
            {
                isPathComplete = true;
                ApplyFinalForce();
                AddRigidbody();
            }
        }
    }

    void ApplyFinalForce()
    {
        // 施加最后的力
        rb.AddForce(finalForce, ForceMode.Impulse);

        // 设置鱼头朝向与力一致
        Vector3 forceDirection = finalForce.normalized;
        Quaternion targetRotation = Quaternion.LookRotation(forceDirection);
        rb.MoveRotation(targetRotation);
    }
    void AddRigidbody()
    { 
    Armature.gameObject.AddComponent<Rigidbody>();
    }

    void AdjustOrientation()
    {
        // 在施加力后持续调整朝向与速度方向一致
        if (rb.velocity.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(rb.velocity.normalized);
            rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime));
        }
    }

    // 在编辑器中可视化路径
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        for (int i = 0; i < pathPoints.Length; i++)
        {
            Gizmos.DrawSphere(pathPoints[i], 0.1f);
            if (i > 0)
            {
                Gizmos.DrawLine(pathPoints[i - 1], pathPoints[i]);
            }
        }

        // 显示终点力方向
        if (pathPoints.Length > 0)
        {
            Gizmos.color = Color.red;
            Vector3 lastPoint = pathPoints[pathPoints.Length - 1];
            Gizmos.DrawLine(lastPoint, lastPoint + finalForce.normalized * 2f);
        }
    }
}