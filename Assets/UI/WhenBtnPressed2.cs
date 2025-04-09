using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhenBtnPressed2 : MonoBehaviour
{
    [Header("A ����Ӧ��״̬")]
    public GameObject A1; // ����״̬��ʾ�Ķ��󣬴�����˸�ű�
    public GameObject A2; // ����״̬��ʾ�Ķ���

    [Header("W ����Ӧ��״̬")]
    public GameObject W1; // ����״̬��ʾ�Ķ��󣬴�����˸�ű�
    public GameObject W2; // ����״̬��ʾ�Ķ���

    [Header("���״̬")]
    [Tooltip("�������������Ϊ true������Ϊ false��")]
    public static bool isFishTired = false;

    // ��ȡ A1 �� W1 �ϵ���˸�ű��������������Ϊ BlinkingScript��
    private BlinkingImageButton blinkingA;
    private BlinkingImageButton blinkingW;

    void Start()
    {
        // ��ʼʱ��ʾ����״̬�����ذ���״̬
        if (A1 != null) A1.SetActive(true);
        if (A2 != null) A2.SetActive(false);
        if (W1 != null) W1.SetActive(true);
        if (W2 != null) W2.SetActive(false);

        // ���Ի�ȡ��˸�ű����
        if (A1 != null)
            blinkingA = A1.GetComponent<BlinkingImageButton>();
        if (W1 != null)
            blinkingW = W1.GetComponent<BlinkingImageButton>();
    }

    void Update()
    {
        // ��� A ��
        if (Input.GetKey(KeyCode.A))
        {
            // ���� A ������ʾ A2������ A1
            if (A1 != null) A1.SetActive(false);
            if (A2 != null) A2.SetActive(true);
        }
        else
        {
            // A ��δ���£���ʾ A1������ A2
            if (A1 != null) A1.SetActive(true);
            if (A2 != null) A2.SetActive(false);

            // �����������������ˣ�A ������˸������ر���˸����ʾ��̬��
            if (blinkingA != null)
            {
                blinkingA.enabled = isFishTired;
            }
        }

        // ��� W ��
        if (Input.GetKey(KeyCode.W))
        {
            // ���� W ������ʾ W2������ W1
            if (W1 != null) W1.SetActive(false);
            if (W2 != null) W2.SetActive(true);
        }
        else
        {
            // W ��δ���£���ʾ W1������ W2
            if (W1 != null) W1.SetActive(true);
            if (W2 != null) W2.SetActive(false);

            // �������������㲻�ۣ�W ������˸������ر���˸����ʾ��̬��
            if (blinkingW != null)
            {
                blinkingW.enabled = !isFishTired;
            }
        }
    }
}
