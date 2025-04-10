using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using DialogueEditor;
using UnityEngine;

public class ManGoWithShark : MonoBehaviour
{
    public Transform objectA;  // ���ƶ������� A
    public Transform objectB;  // Ŀ������ B
    public GameObject Child;

    [Tooltip("ǰ5�׵��˶�ʱ�䣬���鱣�ֽ϶�")]
    public float fastMoveDuration = 0.1f;
    [Tooltip("ʣ��ÿ5�������ĵ�ʱ�䣬��ֵԽ�����Խ����")]
    public float slowMoveDurationPer5Meters = 1.0f;
    public NPCConversation Conversation;
    public void LoadConversation()
    {
        ConversationManager.Instance.StartConversation(Conversation);

    }
    private Animator animator;
    private bool animationComplete = false;
    private void Start()
    {
        animator = Child.GetComponent<Animator>();
        
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            animator.SetTrigger("TriggerV");
            Invoke("GoShark", 0.5f);
        }
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        // ��鶯���Ƿ񲥷����
        if (stateInfo.normalizedTime >= 1f && !animationComplete)
        {
            animationComplete = true;
            Debug.Log("����������ϣ�ִ������123123��");
            // ������ִ���������
           
        }
       
    }
    public void GoShark()
    {
        Debug.Log("����������ϣ�ִ�����");
        // ���¿ո��ʱ�������˶� tween


        Invoke("LoadConversation", 1f);
            // ȡ�� objectA ���е� Tween����ֹ��ͻ
            objectA.DOKill();

            float totalDistance = Vector3.Distance(objectA.position, objectB.position);
            if (totalDistance <= 2f)
            {
                // ���Ŀ�����С�ڵ���5�ף�ֱ�Ӳ��ÿ����ƶ�
                objectA.DOMove(objectB.position, fastMoveDuration).SetEase(Ease.OutQuad);
            }
            else
            {
                // ����������5�ף���Ϊ�����׶Σ�
                // ��һ�׶Σ�ǰ5�׿����ƶ�
                Vector3 direction = (objectB.position - objectA.position).normalized;
                Vector3 intermediatePos = objectA.position + direction * 2f;

                // ʣ������˶�ʱ�������٣�����ʱ����ÿ5������ slowMoveDurationPer5Meters �룩
                float remainingDistance = totalDistance - 2f;
                float slowDuration = (remainingDistance / 2f) * slowMoveDurationPer5Meters;

                // ���� DOTween ����
                Sequence seq = DOTween.Sequence();
                seq.Append(objectA.DOMove(intermediatePos, fastMoveDuration)
                                  .SetEase(Ease.Linear));
                seq.Append(objectA.DOMove(objectB.position, slowDuration)
                                  .SetEase(Ease.OutQuad));
            
        }
    }
}
