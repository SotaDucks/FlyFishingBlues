using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Man_Struggle : MonoBehaviour
{
    [Header("��������")]
    public float minForce = 1f; // ��С��
    public float maxForce = 2f; // �����

    [Header("�������ʱ�䷶Χ")]
    public float minInterval = 0.3f; // ��С���
    public float maxInterval = 1.2f; // �����

    [Header("ͷ����ת�ٶȷ�Χ (��)")]
    public float minRotateDuration = 0.2f; // ��С��תʱ��
    public float maxRotateDuration = 0.6f; // �����תʱ��

    [Header("ͷ����ת�Ƕȷ�Χ")]
    public float angleRange = 90f; // ��ת�Ƕȷ�Χ

    public GameObject eelHead; // ����ͷ��

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }

        rb.useGravity = true;

        // ������������
        StartNextStruggle();
    }

    private void StartNextStruggle()
    {
        // ����һ�����ʱ���������������Ľ���
        float interval = Random.Range(minInterval, maxInterval);
        Invoke(nameof(PerformStruggle), interval);
    }

    private void PerformStruggle()
    {
        if (eelHead == null) return;

        // ���Ŀ��Ƕȣ�Z����ת��
        float randomAngle = Random.Range(-angleRange, angleRange);
        Vector3 currentEuler = eelHead.transform.localEulerAngles;
        Vector3 targetEuler = new Vector3(currentEuler.x, currentEuler.y, randomAngle);

        // �����תʱ��
        float rotateDuration = Random.Range(minRotateDuration, maxRotateDuration);

        // ʹ�� DOTween ��תͷ��
        eelHead.transform.DOLocalRotate(targetEuler, rotateDuration, RotateMode.FastBeyond360)
            .SetEase(Ease.InOutSine)
            .OnComplete(() =>
            {
                // ��ת��ɺ�ʩ������������
                ApplyRandomForce();
                StartNextStruggle(); // ���һ��������������һ��
            });
    }

    private void ApplyRandomForce()
    {
        // ����һ������������򣬷����������
        Vector3 randomDirection = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f)).normalized;

        // ����������Ĵ�С
        float forceMagnitude = Random.Range(minForce, maxForce);

        // ������ʩ������������
        rb.AddForce(randomDirection * forceMagnitude, ForceMode.Impulse);
    }
}
