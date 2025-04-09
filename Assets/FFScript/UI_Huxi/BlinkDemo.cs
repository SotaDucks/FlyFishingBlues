using UnityEngine;
using UnityEngine.UI;
using Beautify.Universal;
using DG.Tweening;
public class Demo : MonoBehaviour {




    [Header("模糊参数")]
    public float blurStrength = 4f; // 最大模糊强度
    public float blurDuration = 0.5f; // 每半周期时间（模糊一次往返时间）

    [Header("眨眼参数")]
    public float blinkDuration = 0.2f; // 单次眨眼时长
    public int blinkLoops = 3; // 眨眼来回次数

    void Start()
    {
        StartDizzyEffect();
    }

    public void StartDizzyEffect()
    {
        // 确保一开始模糊效果被激活
        BeautifySettings.settings.blurIntensity.Override(0f);

        // ----------------------
        // ----------------------
        DOTween.To(() => BeautifySettings.settings.blurIntensity.value,
                   x => BeautifySettings.settings.blurIntensity.Override(x),
                   blurStrength,
                   blurDuration)
               .SetLoops(6, LoopType.Yoyo) // 3 个来回 = 6 次循环
               .SetEase(Ease.InOutSine)
               .OnComplete(() =>
               {
                   // 模糊效果完成，重置为清晰
                   BeautifySettings.settings.blurIntensity.Override(0f);
                   BeautifySettings.settings.blurIntensity.overrideState = false;
                   Debug.Log("模糊完成，画面清晰！");
               });

        // ----------------------
        // ----------------------

        // Blink 间隔时间，稍微延迟一点，避免和模糊完全对齐，效果更自然
        float blinkInterval = blurDuration;

        // 使用 DOTween 定时循环执行 Blink
        DOVirtual.DelayedCall(0f, BlinkOnce) // 立即开始第一次 Blink
            .OnComplete(() =>
            {
                // 循环调用 Blink
                DOTween.Sequence()
                    .AppendInterval(blinkInterval)
                    .AppendCallback(BlinkOnce)
                    .SetLoops(blinkLoops - 1); // 已经调用过一次，所以减 1
            });
    }

    private void BlinkOnce()
    {
        BeautifySettings.Blink(blinkDuration);
        Debug.Log("眨眼一次");
    }
}
