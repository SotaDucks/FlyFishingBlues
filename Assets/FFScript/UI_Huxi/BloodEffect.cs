using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodEffect : MonoBehaviour
{
    [Header("Ŀ���������")]
    [Tooltip("������Ҫ���Ƶ�GameObject")]
    public GameObject targetObject;  // ����Inspector��ָ������

    void Start()
    {
        // ��ʼ��ʱ�ر�����[3](@ref)
        if (targetObject != null)
            targetObject.SetActive(false);
    }

    void Update()
    {
        // ���ո�������¼�[1,2](@ref)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // �л����弤��״̬[3](@ref)
            if (targetObject.activeSelf == false)
            {
                targetObject.SetActive(true);
                Debug.Log("�����Ѽ���"); // ������Ϣ���[3](@ref)
            }
        }
    }
}
