using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManGoWithShark : MonoBehaviour
{
    public GameObject objectA; // ����A
    public GameObject objectB; // ����B
    public float moveSpeed = 2f; // ��������A������B�ƶ����ٶ�

    private bool isFollowing = false;

    void Update()
    {
        // �����¿ո��ʱ������A��ʼ������B�ƶ�
        if (Input.GetKeyDown(KeyCode.V))
        {
            isFollowing = true;
        }

        // ������ڸ�������B������A�����ƶ�������B��λ��
        if (isFollowing)
        {
            // ʹ��Lerp����ƽ�����ƶ�
            objectA.transform.position = Vector3.Lerp(objectA.transform.position, objectB.transform.position, moveSpeed * Time.deltaTime);
        }
    }
}
