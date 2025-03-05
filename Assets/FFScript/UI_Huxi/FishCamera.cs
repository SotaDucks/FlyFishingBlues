using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class FishCamera : MonoBehaviour
{
    public object targetObjcet; // 需要动画的Image组件
    public float duration = 1f; // 动画持续时间
    public Vector2 targetPosition; // 目标位置
    public float targetScale = 0.5f; // 目标缩放比例
    private Image targetImage;
    private Color originalColor; // 初始颜色
    private Vector2 originalPosition; // 初始位置
    private Vector3 originalScale; // 初始缩放
    private void Start()
    {
        targetImage =GetComponent<Image>();
        originalColor = targetImage.color;
        originalScale = targetImage.rectTransform.localScale;
    }

    public void CombinedFadeInAnimation()
    {
        targetImage.enabled = true;
        // 确保targetImage有效
        if (targetImage == null) return;

        // 初始状态：透明且缩放为0
        targetImage.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);
        targetImage.rectTransform.localScale = Vector3.zero;

        // 创建序列
        Sequence mySequence = DOTween.Sequence();

        // 同时执行淡入和缩放
        mySequence.Join(targetImage.DOFade(1f, duration).SetEase(Ease.OutQuad));
        mySequence.Join(targetImage.rectTransform.DOScale(originalScale, duration).SetEase(Ease.OutBack));

        // 动画完成回调
        mySequence.OnComplete(() =>
        {
            Debug.Log("组合动画完成！");
        });

        // 播放动画
        mySequence.Play();
    }
    public void PlayAnimation()
    {
        
        // 重置到初始状态

       

        // 创建一个Sequence来同时执行缩放和移动
        Sequence mySequence = DOTween.Sequence();

        // 添加缩放动画
        mySequence.Join(targetImage.rectTransform.DOScale(targetScale, duration)
            .SetEase(Ease.InOutQuad)); // 使用缓动曲线

        // 添加移动动画
        mySequence.Join(targetImage.rectTransform.DOAnchorPos(targetPosition, duration)
            .SetEase(Ease.InOutQuad));

        // 设置循环（可选）
        DOVirtual.DelayedCall(duration, () =>
        {
            // 创建一个Sequence来同时执行缩放和移动
            Sequence mySequence = DOTween.Sequence();

            // 添加缩放动画
            mySequence.Join(targetImage.rectTransform.DOScale(targetScale, duration)
                .SetEase(Ease.InOutQuad));

            // 添加移动动画
            mySequence.Join(targetImage.rectTransform.DOAnchorPos(targetPosition, duration)
                .SetEase(Ease.InOutQuad));

            // 设置循环（可选）
            // mySequence.SetLoops(-1, LoopType.Yoyo);

            // 播放动画
            mySequence.Play();

           
        });

    }


}
