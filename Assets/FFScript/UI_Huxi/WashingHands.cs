using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class WashingHands : MonoBehaviour
{
    public GameObject Fish;  // ��Ҫ����� Fish ����
    public GameObject Hands; // �ֵ� GameObject
    public bool WashHands = false;
    public GameObject tutorial;

    // ����ϴ�����̵ķ���
    public void WashingHandsMethod()
    {
        Fish.gameObject.SetActive(false); // ������ Fish ����
        WashHands = true;                 // ���� WashHands Ϊ true
        CameratoBucket(-20,20);                    // ִ���ֵĶ���
    }
    private void CameratoBucket(float fromAngle, float toAngle)
    {
        Camera mainCamera = Camera.main;
        mainCamera.transform.DORotate(
               new Vector3(mainCamera.transform.eulerAngles.x, toAngle, mainCamera.transform.eulerAngles.z),
               1f  // ��������ʱ�� 1 ��
           ).SetEase(Ease.InOutQuad)  // ƽ���Ļ�������
            .OnComplete(() => HandsMoving()); // ��ת���ʱ���� Fish
    }
    // �ֵ��ƶ�����
    private void HandsMoving()
    {
        float currentY = Hands.transform.position.y; // ��ȡ���嵱ǰ��Y����
        Hands.transform.DOMoveY(currentY -0.5f, 1f) // �ӵ�ǰλ�������ƶ�0.5����λ������1��
            .SetLoops(2, LoopType.Yoyo)              // ����ѭ��2�Σ�ʹ��Yoyo���ͣ������˶���
            .SetEase(Ease.InOutQuad)            // ƽ���Ļ�������
            .OnComplete(() => CameraRotation(20, -20)); // ������ɺ���ת���
    }

    // �����ת����
    private void CameraRotation(float fromAngle, float toAngle)
    {
        Camera mainCamera = Camera.main;

        if (mainCamera != null)
        {
            // ���������ʼ�Ƕȣ�ֻ�޸� Y �ᣩ
            mainCamera.transform.eulerAngles = new Vector3(
                mainCamera.transform.eulerAngles.x,
                fromAngle,
                mainCamera.transform.eulerAngles.z
            );

            // ִ�������ת������Ŀ��Ƕ�
            mainCamera.transform.DORotate(
                new Vector3(mainCamera.transform.eulerAngles.x, toAngle, mainCamera.transform.eulerAngles.z),
                1f  // ��������ʱ�� 1 ��
            ).SetEase(Ease.InOutQuad)  // ƽ���Ļ�������
             .OnComplete(() => ActivateFish()); // ��ת���ʱ���� Fish
        }
        else
        {
            Debug.LogError("�����δ�ҵ���");
        }
    }

    // ���� Fish ����ķ���
    private void ActivateFish()
    {
        Fish.gameObject.SetActive(true); // �� Fish ����Ϊ�ɼ�
        tutorial.gameObject.SetActive(true);
}
}