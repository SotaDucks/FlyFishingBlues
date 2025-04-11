using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        bool allAnimationsFinished = true;

        // 遍历 Animator 的所有动画层
        for (int i = 0; i < animator.layerCount; i++)
        {
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(i);
            // 如果当前层的动画还没有完整播放完毕或正在过渡中，则认为还未结束
            if (stateInfo.normalizedTime < 1.0f || animator.IsInTransition(i))
            {
                allAnimationsFinished = false;
                break;
            }
        }

        // 当所有动画都播放完成后，触发转换参数
        if (allAnimationsFinished)
        {
            // 可以用 Trigger 或 Bool 参数，例如 Trigger 参数 "AllFinished"
            animator.SetTrigger("AllFinished");
        }
    }
}
