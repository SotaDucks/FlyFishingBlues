using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Splines;

public class Attract_Shark : MonoBehaviour
{
 
    // ��������Ƿ�����ײ����
    private bool isInsideCollider = false;
    public GameObject targetObject;
    public float moveSpeed = 5f;
   
    // �����������ײ��ʱ����
    private void OnCollisionEnter(Collision collision)
    {
        // �����ײ����������Ƿ�Ϊ "flyhook"
        if (collision.gameObject.name == "flyhook")
        {
            isInsideCollider = true;  // ������ײ����
            Debug.Log("������ײ��: flyhook");
        }
    }

    // �������뿪��ײ��ʱ����
    private void OnCollisionExit(Collision collision)
    {
        // �����ײ����������Ƿ�Ϊ "flyhook"
        if (collision.gameObject.name == "flyhook")
        {
            isInsideCollider = false;  // �뿪��ײ��
            Debug.Log("�뿪��ײ��: flyhook");
        }
    }

   

    // ��ѡ������ͨ��һ���������ص�ǰ�Ƿ�����ײ����
    private void Update()
    {
       
        // �����������ײ���ڣ����Ұ����� S ��
        if (isInsideCollider && Input.GetKeyDown(KeyCode.S) && targetObject != null)
        {
            MoveTowards();  // �����ƶ�����
        }
    }
    private void MoveTowards()
    {
        // ���㷽������
        Vector3 direction = (targetObject.transform.position - transform.position).normalized;

        // �ƶ�����
        transform.position += direction * moveSpeed * Time.deltaTime;

        // �����־���鿴�����Ƿ������ƶ�
        Debug.Log("�����ƶ���Ŀ�귽��: " + targetObject.name);
    }



    //public Transform shark;         // Reference to the shark object
    //public Transform fishingHook;   // Reference to the fishing hook object
    //public float moveDistance = 5f; // Distance the shark moves forward when pressing "S"
    //public float Max;
    //public float Min;
    //private Vector3 recordedPosition;
    //private Vector3 recordedRotation;
    //bool Working;
    //private void Start()
    //{
    //    Working = true;
    //}
    //void Update()
    //{
    //    // ��ⰴ��"S"��
    //    if (Input.GetKeyDown(KeyCode.S))
    //    {
    //        if (fishingHook.position.y < -4f )
    //        {
    //            Debug.LogWarning("�㹳������");
    //        }

    //        // ��ȡ�㹳��Yλ�ã�ȷ��С��-4
    //        if ( shark.eulerAngles.y < Max && shark.transform.eulerAngles.y > Min)
    //        {



    //                Working = true;
    //                BanFollowLine();
    //                MoveTowardsObject(moveDistance);
    //                Debug.LogWarning("�ƶ���");




    //        }
    //    }
    //}



    //public void BanFollowLine()
    //{
    //    SplineAnimate splineScript = GetComponent<SplineAnimate>();
    //    splineScript.enabled = false;
    //    recordedPosition = transform.position;
    //    recordedRotation = transform.eulerAngles;

    //}
    //public void MoveTowardsObject( float distance)
    //{
    //    // ����Ŀ��λ�ã�targetObject�ķ��� + Ŀ����룩
    //    Vector3 directionToTarget = (fishingHook.position - shark.position).normalized;
    //    Vector3 targetPosition = shark.position + directionToTarget * distance;

    //    // ʹ�� DOTween �������ƶ���Ŀ��λ��
    //    shark.DOMove(targetPosition, 1f).OnComplete(() => { MoveToTargetAndRotate(recordedPosition,recordedRotation); });  // 1f ���ƶ���ʱ�䣬���Ը�����Ҫ����
    //}
    //public void EnableFollowLine()
    //{
    //    SplineAnimate splineScript = GetComponent<SplineAnimate>();
    //    splineScript.enabled = true;

    //}
    //public void MoveToTargetAndRotate(Vector3 targetPosition, Vector3 targetRotation)
    //{
    //    // �ƶ����嵽Ŀ��λ�ã��������ƶ�ʱ�䣨���� 2 �룩
    //    transform.DOMove(targetPosition, 1f).OnComplete(() =>
    //    {
    //        // ������ɺ󣬵��� EnableFollowLine ����
    //        EnableFollowLine();
    //        Working = false;
    //    });

    //    // ��ת���嵽Ŀ��Ƕȣ�ŷ���ǣ�
    //    transform.DORotate(targetRotation, 2f);
    //}
}
