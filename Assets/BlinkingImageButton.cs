using UnityEngine;
using UnityEngine.UI;

public class BlinkingImageButton : MonoBehaviour
{
    public float blinkDuration = 1f;  // 闪烁周期时长
    public float minAlpha = 0f;       // 最小透明度
    public float maxAlpha = 1f;       // 最大透明度

    private Image imageComponent;     // 图片组件
    private float timer = 0f;

    void Start()
    {
        // 获取 Image 组件
        imageComponent = GetComponent<Image>();
        if (imageComponent == null)
        {
            Debug.LogError("未能找到Image组件，请将此脚本附加到带有Image组件的UI对象上。");
        }
    }

    void Update()
    {
        if (imageComponent != null)
        {
            // 使用UnscaledDeltaTime以不受Time.timeScale的影响
            timer += Time.unscaledDeltaTime;

            // 计算透明度的变化（循环渐变0-1-0）
            float alpha = Mathf.Lerp(minAlpha, maxAlpha, Mathf.PingPong(timer / blinkDuration, 1f));

            // 获取当前颜色并修改Alpha值，再赋值给Image组件
            Color currentColor = imageComponent.color;
            currentColor.a = alpha;
            imageComponent.color = currentColor;
        }
    }
}

