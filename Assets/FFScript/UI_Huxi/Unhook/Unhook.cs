using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unhook : MonoBehaviour
{

    public float maxRotation = 40f; // �����ת�Ƕ�
    public float maxSpeed = 100f; // �����ת�ٶ�
    private float screenCenterX;
    public float moveSpeed = 5f; // �ƶ��ٶ�
    void Start()
    {
        screenCenterX = Screen.width / 2; // ��ȡ��Ļ���� X ����
    }
    void Update()
    {
        // ��ȡˮƽ��A/D���ʹ�ֱ��W/S������
        float moveX = 0f;
        float moveY = 0f;

        if (Input.GetKey(KeyCode.A)) moveX = -1f; // ���� A ����
        if (Input.GetKey(KeyCode.D)) moveX = 1f;  // ���� D ����
        if (Input.GetKey(KeyCode.W)) moveY = 1f;  // ���� W ����
        if (Input.GetKey(KeyCode.S)) moveY = -1f; // ���� S ����

        // �����ƶ�����
        Vector2 moveDirection = new Vector2(moveX, moveY).normalized;

        // �ƶ�����
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);

        float mouseX = Input.mousePosition.x; // ��ȡ��� X λ��
        float distanceFromCenter = mouseX - screenCenterX; // �������ƫ����
        float percentFromCenter = Mathf.Clamp(distanceFromCenter / screenCenterX, -1f, 1f); // ��һ���� [-1, 1]

        // ������ת�Ƕȣ���� ��40�㣩
        float targetRotation = percentFromCenter * maxRotation;

        // ������ת�ٶȣ����ԽԶ����תԽ�죨��� maxSpeed��
        float rotationSpeed = Mathf.Abs(percentFromCenter) * maxSpeed;

        // ƽ����ת
        transform.rotation = Quaternion.RotateTowards(
            transform.rotation,
            Quaternion.Euler(0, 0, targetRotation),
            rotationSpeed * Time.deltaTime
        );
    }
}





