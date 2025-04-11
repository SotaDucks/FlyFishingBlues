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

        // ���� Animator �����ж�����
        for (int i = 0; i < animator.layerCount; i++)
        {
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(i);
            // �����ǰ��Ķ�����û������������ϻ����ڹ����У�����Ϊ��δ����
            if (stateInfo.normalizedTime < 1.0f || animator.IsInTransition(i))
            {
                allAnimationsFinished = false;
                break;
            }
        }

        // �����ж�����������ɺ󣬴���ת������
        if (allAnimationsFinished)
        {
            // ������ Trigger �� Bool ���������� Trigger ���� "AllFinished"
            animator.SetTrigger("AllFinished");
        }
    }
}
