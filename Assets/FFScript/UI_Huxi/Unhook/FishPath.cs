using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FishPath : MonoBehaviour
{
    public GameObject Armature;
    [Header("·������")]
    public Vector3[] pathPoints = new Vector3[4]; // �ĸ�·����
    public float moveSpeed = 2f; // �ƶ��ٶ�
    public float reachThreshold = 0.1f; // �����ľ�����ֵ

    [Header("�յ����")]
    public Vector3 finalForce = new Vector3(5f, 2f, 0f); // �յ�ʩ�ӵ���
    public float rotationSpeed = 5f; // ��ת�����ٶ�

    private Rigidbody rb;
    private int currentPointIndex = 0;
    private bool isPathComplete = false;
    private Vector3 initialPosition;

    void Start()
    {
        rb = GetComponentInParent<Rigidbody>();
        initialPosition = rb.transform.position;

        // ���û������·���㣬��ʼ��Ĭ��ֵ
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
            // ��������ģ���������
            AdjustOrientation();
        }
    }

    void MoveAlongPath()
    {
        // ��ȡĿ���
        Vector3 targetPoint = pathPoints[currentPointIndex];

        // �����ƶ�����;���
        Vector3 direction = (targetPoint - rb.position).normalized;
        float distance = Vector3.Distance(rb.position, targetPoint);

        // �ƶ���
        rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);

        // �����ƶ�����
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime));

        // ����Ƿ񵽴ﵱǰ��
        if (distance <= reachThreshold)
        {
            currentPointIndex++;

            // ����������һ����
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
        // ʩ��������
        rb.AddForce(finalForce, ForceMode.Impulse);

        // ������ͷ��������һ��
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
        // ��ʩ��������������������ٶȷ���һ��
        if (rb.velocity.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(rb.velocity.normalized);
            rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime));
        }
    }

    // �ڱ༭���п��ӻ�·��
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

        // ��ʾ�յ�������
        if (pathPoints.Length > 0)
        {
            Gizmos.color = Color.red;
            Vector3 lastPoint = pathPoints[pathPoints.Length - 1];
            Gizmos.DrawLine(lastPoint, lastPoint + finalForce.normalized * 2f);
        }
    }
}