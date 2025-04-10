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

        // ��鶯���Ƿ񲥷����
        if (stateInfo.normalizedTime >= 1f && !animationComplete)
        {
            animationComplete = true;
            Debug.Log("����������ϣ�ִ�����");
            // ������ִ���������
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            animator.SetTrigger("TriggerV");
        }
    }
}
