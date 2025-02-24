using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class fishstruggling : MonoBehaviour
{
    [Header("��������")]
    public float struggleForce = 5f; // ��ֱ��Ծ����
    public float horizontalMoveForce = 2f; // ˮƽ�ƶ�����
    public float struggleFrequency = 1f; // ����Ƶ��
    public float groundLevel = 0f; // ����߶�
    public float maxHorizontalMovement = 5f; // ˮƽ�ƶ���Χ
    public float maxRotationZ = 80f; // Z ����ת�Ƕ�����
    public float maxJumpHeight = 2f; // �����Ծ�߶�
    public float escapeJumpForce = 10f; // ����ˮ�е���������
    public float zMoveForce = 1f; // Z ��ǰ���ƶ�������

    private Vector3 initialPosition;
    private Rigidbody ParentRb;
    private bool isOnGround = true;
    private float timer;
    private float zRotation;
    public bool isEscaping = false; // ���ڱ�����Ƿ��Ѿ�����ˮ��

    void Start()
    {
        // ��ȡ�������ϵ� Rigidbody
        ParentRb = GetComponentInParent<Rigidbody>();

        // ��ʼ��λ�úͼ�ʱ��
        initialPosition = ParentRb.transform.position;
        timer = 0f;

        // ���һ���Ŀ�����������ֹ�����ۻ��ٶ�
        ParentRb.drag = 1f;
        ParentRb.angularDrag = 1f;
    }

    void Update()
    {
        // ����ʱ��
        timer += Time.deltaTime;

        // ������ڵ����ϲ���û�н�������״̬
        if (isOnGround && !isEscaping)
        {
            if (timer >= struggleFrequency)
            {
                Struggle();
                timer = 0f; // ���ü�ʱ��
                struggleFrequency = UnityEngine.Random.Range(0.2f, 1.5f);
            }
        }

        // ������ĸ߶�
        Vector3 clampedPosition = ParentRb.transform.position;
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, groundLevel, groundLevel + maxJumpHeight);
        ParentRb.transform.position = clampedPosition;
    }

    // ������Ϊ
    void Struggle()
    {
        // ���һ������Ĵ�ֱ����������Ծ��
        ParentRb.AddForce(Vector3.up * UnityEngine.Random.Range(0.5f, struggleForce), ForceMode.Impulse);

        // ���һ�������ˮƽ�������������ƶ���
        float randomDirection = UnityEngine.Random.Range(-0.2f, 0.9f);
        float randomZdirection= UnityEngine.Random.Range(-0.1f, 0.1f);
        Vector3 horizontalForce = new Vector3(randomDirection * horizontalMoveForce, 0, randomZdirection * horizontalMoveForce);
        ParentRb.AddForce(horizontalForce, ForceMode.Impulse);

        // �������ˮƽ�ƶ���Χ
        Vector3 clampedPosition = ParentRb.transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, initialPosition.x - maxHorizontalMovement, initialPosition.x + maxHorizontalMovement);
        ParentRb.transform.position = clampedPosition;
        float randomNum = UnityEngine.Random.Range(15f, 120f);
        // ��ʼ������ -90f �� 90f ������
        float[] Orientationside = { -90f, 90f };
        System.Random random = new System.Random();

        // ���ѡ�� -90f �� 90f
        float randomOrientation = Orientationside[random.Next(0, 2)];
        ParentRb.transform.rotation = Quaternion.Euler(0, randomNum, randomOrientation);
    }

    // ������ˮ�е���Ϊ
    public void EscapeJump()
    {
        // ������Ѿ���������״̬
        isEscaping = true;

        // ���ϡ����ҡ�����ǰʩ����
        ParentRb.AddForce((Vector3.up * escapeJumpForce) + (Vector3.right * escapeJumpForce) + (Vector3.forward * zMoveForce), ForceMode.Impulse);

        // �����ͷ������ X ���򣨵�����ת��
        float randomNum = UnityEngine.Random.Range(45f, 120f);
        ParentRb.transform.rotation = Quaternion.Euler(0, randomNum, 0); // �� Z ����ת����Ϊ 0
        Collider parentCollider = GetComponentInParent<Collider>();

        // ����������ϴ��� Collider����ɾ����
 
        // ����ϵͳ�ӹ�ʣ�µ��˶�
    }

    // ��ײ��⣬����Ƿ��ŵ�
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground" && !isEscaping)
        {
            isOnGround = true;
            ParentRb.velocity = Vector3.zero; // �����ٶȣ���ֹ�ۻ�������
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag != "Ground")
        {
            isOnGround = false;
        }
    }
}
