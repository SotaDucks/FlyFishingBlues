using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class TutorialMouse : MonoBehaviour
{
    public float targetScale = 1.5f;  // 目标缩放比例，例如 1.5 表示放大 1.5 倍
    public float duration = 1f;       // 动画持续时间（秒）
    public Image image;

    void Start()
    {
        // 获取 Image 组件
       

        // 使用 DOTween 动画化 RectTransform 的 localScale
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
