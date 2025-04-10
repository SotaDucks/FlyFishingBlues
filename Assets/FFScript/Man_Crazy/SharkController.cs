using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public class SharkController : MonoBehaviour
{
    public Transform objectToMove; // Ҫ�ƶ�������
    public Transform targetPosition; // Ŀ��λ��
    public float moveDuration = 2f; // �ƶ�����ʱ��
    public Ease moveEaseType = Ease.InOutSine; // ��������
    public GameObject Hook;
   public  Rigidbody rb;
    private void Start()
    {
     rb=Hook.GetComponent<Rigidbody>();
    }
    public void MoveFish()
    {
        objectToMove.DOMove(targetPosition.position, moveDuration)
                    .SetEase(moveEaseType)
                    .OnStart(() => Debug.Log("�ƶ���ʼ"))
                    .OnComplete(() => Debug.Log("�ƶ����"));
        HookKinematic();
    }
    private void HookKinematic()
    { 
    rb.isKinematic = true;
    }

}
