using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ManStru : MonoBehaviour
{
    public float range = 60f;  // ��ת�ķ�Χ
    public float speed = 1f;   // ��ת���ٶ�

    private void Update()
    {
        // ������ת�Ƕȣ���Χ�� -60 �� 60
        float angle = Mathf.PingPong(Time.time * speed, range * 2) - range;

        // �����������ת����������� X ����ת��
        transform.rotation = Quaternion.Euler(angle, angle*0.1f, transform.rotation.eulerAngles.z);
    }
}
