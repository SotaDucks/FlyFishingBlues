using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class FishCamera : MonoBehaviour
{
    public object targetObjcet; // ��Ҫ������Image���
    public float duration = 1f; // ��������ʱ��
    public Vector2 targetPosition; // Ŀ��λ��
    public float targetScale = 0.5f; // Ŀ�����ű���
    private Image targetImage;
    private Color originalColor; // ��ʼ��ɫ
    private Vector2 originalPosition; // ��ʼλ��
    private Vector3 originalScale; // ��ʼ����
    private void Start()
    {
        targetImage =GetComponent<Image>();
        originalColor = targetImage.color;
        originalScale = targetImage.rectTransform.localScale;
    }

    public void CombinedFadeInAnimation()
    {
        targetImage.enabled = true;
        // ȷ��targetImage��Ч
        if (targetImage == null) return;

        // ��ʼ״̬��͸��������Ϊ0
        targetImage.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);
        targetImage.rectTransform.localScale = Vector3.zero;

        // ��������
        Sequence mySequence = DOTween.Sequence();

        // ͬʱִ�е��������
        mySequence.Join(targetImage.DOFade(1f, duration).SetEase(Ease.OutQuad));
        mySequence.Join(targetImage.rectTransform.DOScale(originalScale, duration).SetEase(Ease.OutBack));

        // ������ɻص�
        mySequence.OnComplete(() =>
        {
            Debug.Log("��϶�����ɣ�");
        });

        // ���Ŷ���
        mySequence.Play();
    }
    public void PlayAnimation()
    {
        
        // ���õ���ʼ״̬

       

        // ����һ��Sequence��ͬʱִ�����ź��ƶ�
        Sequence mySequence = DOTween.Sequence();

        // ������Ŷ���
        mySequence.Join(targetImage.rectTransform.DOScale(targetScale, duration)
            .SetEase(Ease.InOutQuad)); // ʹ�û�������

        // ����ƶ�����
        mySequence.Join(targetImage.rectTransform.DOAnchorPos(targetPosition, duration)
            .SetEase(Ease.InOutQuad));

        // ����ѭ������ѡ��
        DOVirtual.DelayedCall(duration, () =>
        {
            // ����һ��Sequence��ͬʱִ�����ź��ƶ�
            Sequence mySequence = DOTween.Sequence();

            // ������Ŷ���
            mySequence.Join(targetImage.rectTransform.DOScale(targetScale, duration)
                .SetEase(Ease.InOutQuad));

            // ����ƶ�����
            mySequence.Join(targetImage.rectTransform.DOAnchorPos(targetPosition, duration)
                .SetEase(Ease.InOutQuad));

            // ����ѭ������ѡ��
            // mySequence.SetLoops(-1, LoopType.Yoyo);

            // ���Ŷ���
            mySequence.Play();

           
        });

    }


}
