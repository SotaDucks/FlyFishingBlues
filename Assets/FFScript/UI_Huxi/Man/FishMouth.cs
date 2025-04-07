using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMouth : MonoBehaviour
{
    public float angleOffset = 5f;      // ��5��
    public float cycleDuration = 2f;    // һ�����ص�ʱ�䣨�룩

    private float startYAngle;

    void Start()
    {
        // ��¼��ʼ�ı��� Y ��Ƕ�
        startYAngle = transform.localEulerAngles.x;
    }

    void Update()
    {
        // ���ٰڶ��ĺ��ģ�PingPong ���Է���һ��ֵ
        float t = Mathf.PingPong(Time.time / cycleDuration, 1f); // t: 0~1~0

        // ���㵱ǰ�ڶ��Ƕȣ�-5 �� +5��
        float offset = Mathf.Lerp(-angleOffset, angleOffset, t);

        // �����µ���ת��ֻ�޸� Y������ X/Z ���䣩
        Vector3 currentEuler = transform.localEulerAngles;
        currentEuler.x = startYAngle + offset;
        transform.localEulerAngles = currentEuler;
    }
}
