using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Shark_Shake : MonoBehaviour
{


    [Header("��ת����")]
    public float minAngle = -45f; // ��С��ת�Ƕ�
    public float maxAngle = 45f; // �����ת�Ƕ�
    public float duration = 2f;  // ÿ�� PingPong �˶��ĳ���ʱ��

    private float targetRotation;
    private float currentRotation;
    private float nextRotationTime;
    private bool isIncreasing = true; // ���ڿ�����ת����

    void Start()
    {
        // ��ʼ����ǰ��ת�Ƕ�
        currentRotation = transform.localEulerAngles.y;
        nextRotationTime = Time.time + duration;

        // ��ʼʹ�� DOTween ������ת����
        
    }

    void Update()
    {
        // ����ʱ����������ÿ����ת�ļӼ�����
        if (Time.time >= nextRotationTime)
        {
            // �������һ����ת�Ƕ�
            float randomAngle = Random.Range(minAngle, maxAngle);

            if (isIncreasing)
            {
                // ��������Ƕ�
                targetRotation = currentRotation + randomAngle;
            }
            else
            {
                // ��ȥ����Ƕ�
                targetRotation = currentRotation - randomAngle;
            }

            // ������ת����
            RotateToTarget(targetRotation);

            // �л���ת����
            isIncreasing = !isIncreasing;

            // ������һ�α仯��ʱ��
            nextRotationTime = Time.time + duration;
        }
    }

    private void RotateToTarget(float target)
    {
        // ʹ�� DOTween ��������ת��ƽ���Ľ��нǶȱ任
        DOTween.To(() => transform.localEulerAngles.y, x => transform.localEulerAngles = new Vector3(0f, x, 0f), target, duration)
            .SetEase(Ease.InOutSine);
    }
}
