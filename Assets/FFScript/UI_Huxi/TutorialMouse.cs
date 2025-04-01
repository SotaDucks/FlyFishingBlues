using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class TutorialMouse : MonoBehaviour
{
    public float targetScale = 1.5f;  // Ŀ�����ű��������� 1.5 ��ʾ�Ŵ� 1.5 ��
    public float duration = 1f;       // ��������ʱ�䣨�룩
    public Image image;

    void Start()
    {
        // ��ȡ Image ���
       

        // ʹ�� DOTween ������ RectTransform �� localScale
        image.rectTransform.DOScale(targetScale, duration);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        { 
        this.gameObject.SetActive(false);
        
        }
    }
}
