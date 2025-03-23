using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.Mathematics;
using UnityEngine;

public class Unhook : MonoBehaviour
{
    public bool Fishfreehook;
    public quaternion hookPosition;
    public float maxRotation = 40f; // �����ת�Ƕ�
    public float maxSpeed = 100f; // �����ת�ٶ�
    private float screenCenterX;
    public float moveSpeed = 5f; // �ƶ��ٶ�
    private bool Fisdown;
   
    void Start()
    {
    
        //  transform.rotation = hookPosition;
        screenCenterX = Screen.width / 2; // ��ȡ��Ļ���� X ����
       
    }
    void Update()
    {
         HookPosition(); 
        if (Input.GetKeyDown(KeyCode.F))
        { Fisdown = true; }

       
            if (Fisdown)
        {

       // ��ȡˮƽ��A/D���ʹ�ֱ��W/S������
        float moveX = 0f;
        float moveY = 0f;

        if (Input.GetKey(KeyCode.A)) moveX = -1f; // ���� A ����
        if (Input.GetKey(KeyCode.D)) moveX = 1f;  // ���� D ����
        if (Input.GetKey(KeyCode.W)) moveY = 1f;  // ���� W ����
        if (Input.GetKey(KeyCode.S)) moveY = -1f; // ���� S ����

        // �����ƶ�����
        Vector3 moveDirection = new Vector3(moveX, moveY, 0).normalized;

        // ʹ��������������ƶ�
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);

        if (Input.GetKey(KeyCode.Space)) return;
        float mouseX = Input.mousePosition.x; // ��ȡ��� X λ��
        float distanceFromCenter = mouseX - screenCenterX; // �������ƫ����
        float percentFromCenter = Mathf.Clamp(distanceFromCenter / screenCenterX, -1f, 1f); // ��һ���� [-1, 1]

        // ����Ŀ����ת�Ƕȣ���� ��40�㣩
        float targetRotation = percentFromCenter * maxRotation;

        // ������ת�ٶȣ����ԽԶ����תԽ�죨��� maxSpeed��
        float rotationSpeed = Mathf.Abs(percentFromCenter) * maxSpeed * Time.deltaTime;

        // ƽ����ֵ��ת�������� Z �ᣩ
        float newZRotation = Mathf.LerpAngle(transform.eulerAngles.z, targetRotation, rotationSpeed);

        // Ӧ����ת��ֻ���� Z ����ת
        transform.rotation = Quaternion.Euler(0, 180, newZRotation);
        }
    }
    private void HookPosition()
    {

        if(Fishfreehook)
        {
            GetComponentInParent<FishFree>().enabled = true;

        }


    }
}





