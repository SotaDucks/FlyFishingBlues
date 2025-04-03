using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using DG.Tweening;
using UnityEngine;

public class TutorialUI : MonoBehaviour
{

    private Tweener scaleTweener; // 用于存储DOTween动画对象
    private bool isActive = true; // 控制按钮是否活跃
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
        // 如果按钮已经不可见，则不再执行逻辑
        if (!isActive) return;

        // 如果动画尚未初始化，则设置缩放动画
        if (scaleTweener == null || !scaleTweener.IsActive())
        {
            scaleTweener = Button1.transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 0.5f)
                .SetLoops(-1, LoopType.Yoyo) // 无限循环，放大后缩小
                .SetEase(Ease.InOutQuad);    // 平滑的缓动曲线
        }

        // 检测F键是否被按下
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // 停止动画
            scaleTweener.Kill();
            // 隐藏按钮
            gameObject.SetActive(false);
            // 更新状态
            isActive = false;
            Mouse.SetActive(true);
        }
    }

}
