using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using DG.Tweening;
using UnityEngine;

public class TutorialUI : MonoBehaviour
{

    private Tweener scaleTweener; // ���ڴ洢DOTween��������
    private bool isActive = true; // ���ư�ť�Ƿ��Ծ
    public GameObject Mouse;
    public GameObject Button1;
    

    void Update()
    {
        HandleButtonAnimationAndInput();
    }
    public void PresssF()
    { 
    
    }
    private void HandleButtonAnimationAndInput()
    {
        // �����ť�Ѿ����ɼ�������ִ���߼�
        if (!isActive) return;

        // ���������δ��ʼ�������������Ŷ���
        if (scaleTweener == null || !scaleTweener.IsActive())
        {
            scaleTweener = Button1.transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 0.5f)
                .SetLoops(-1, LoopType.Yoyo) // ����ѭ�����Ŵ����С
                .SetEase(Ease.InOutQuad);    // ƽ���Ļ�������
        }

        // ���F���Ƿ񱻰���
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // ֹͣ����
            scaleTweener.Kill();
            // ���ذ�ť
            gameObject.SetActive(false);
            // ����״̬
            isActive = false;
            Mouse.SetActive(true);
        }
    }

}
