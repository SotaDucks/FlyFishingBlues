using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using DialogueEditor;
using UnityEngine;

public class ManGoWithShark : MonoBehaviour
{
    public Transform objectA;  // 待移动的物体 A
    public Transform objectB;  // 目标物体 B
    public GameObject Child;

    [Tooltip("前5米的运动时间，建议保持较短")]
    public float fastMoveDuration = 0.1f;
    [Tooltip("剩余每5米所消耗的时间，数值越大减速越明显")]
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

        // 检查动画是否播放完成
        if (stateInfo.normalizedTime >= 1f && !animationComplete)
        {
            animationComplete = true;
            Debug.Log("动画播放完毕，执行命令123123！");
            // 在这里执行你的命令
           
        }
       
    }
    public void GoShark()
    {
        Debug.Log("动画播放完毕，执行命令！");
        // 按下空格键时，启动运动 tween


        Invoke("LoadConversation", 1f);
            // 取消 objectA 现有的 Tween，防止冲突
            objectA.DOKill();

            float totalDistance = Vector3.Distance(objectA.position, objectB.position);
            if (totalDistance <= 2f)
            {
                // 如果目标距离小于等于5米，直接采用快速移动
                objectA.DOMove(objectB.position, fastMoveDuration).SetEase(Ease.OutQuad);
            }
            else
            {
                // 如果距离大于5米，分为两个阶段：
                // 第一阶段：前5米快速移动
                Vector3 direction = (objectB.position - objectA.position).normalized;
                Vector3 intermediatePos = objectA.position + direction * 2f;

                // 剩余距离运动时采用慢速，计算时长（每5米消耗 slowMoveDurationPer5Meters 秒）
                float remainingDistance = totalDistance - 2f;
                float slowDuration = (remainingDistance / 2f) * slowMoveDurationPer5Meters;

                // 创建 DOTween 序列
                Sequence seq = DOTween.Sequence();
                seq.Append(objectA.DOMove(intermediatePos, fastMoveDuration)
                                  .SetEase(Ease.Linear));
                seq.Append(objectA.DOMove(objectB.position, slowDuration)
                                  .SetEase(Ease.OutQuad));
            
        }
    }
}
