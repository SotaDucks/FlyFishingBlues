using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    public GameObject Shark;
    private Animator animator;
    private bool animationComplete = false;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        // 检查动画是否播放完成
        if (stateInfo.normalizedTime >= 1f && !animationComplete)
        {
            animationComplete = true;
            Debug.Log("动画播放完毕，执行命令！");
            // 在这里执行你的命令
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            animator.SetTrigger("TriggerV");
        }
    }
}
