using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CuttingMan : MonoBehaviour
{
    public float minForce = 5f;  // ��С���Ĵ�С
    public float maxForce = 10f; // ������Ĵ�С
    public float interval = 2f;  // ÿ��������ʩ��һ����
    public Image image1;
    public TextMeshProUGUI textMeshProUGUI1;

    private Rigidbody rb;  // ����� Rigidbody ���

    void Start()
    {
        // ��ȡ����� Rigidbody ���
        rb = GetComponent<Rigidbody>();

        // ÿ��һ��ʱ����� ApplyRandomForce ����
        InvokeRepeating("ApplyRandomForce", 0f, interval);
    }

    void ApplyRandomForce()
    {
        // ����һ������ķ��򣨵�λ������
        Vector3 randomDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;

        // ����һ����������Ĵ�С
        float randomForce = Random.Range(minForce, maxForce);

        // �ڸ����������ʩ����
        rb.AddForce(randomDirection * randomForce, ForceMode.Impulse);

        // ���ʩ�ӵ������������
        Debug.Log("Applied Random Force: " + randomDirection * randomForce);
    }
    private void OnTriggerEnter(Collider other)
    {
        // �����ײ���Ƿ��������ײ��
        if (other.CompareTag("Player"))  // ���������ײ�������� Tag Ϊ "Fish"
        {
            // ����������ײ�壬��ִ�� Bye ����
            Bye();
        }
    }

    // Bye ����
    private void Bye()
    {
        image1.DOFade(1f, 0.2f).OnComplete(() =>
        {
            // ������ɺ󣬵��� EnableFollowLine ����
            textMeshProUGUI1.DOFade(1f, 1f);
            Debug.Log("�����л�����");
        });;
        
    }
}
