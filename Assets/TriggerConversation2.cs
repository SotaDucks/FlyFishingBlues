using DialogueEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerConversation2 : MonoBehaviour
{
    [Header("Trigger 1 上的对话控制器")]
    public TriggerConversation dialogueController;

    [Header("要设置的参数名（可在 Inspector 调整）")]
    [SerializeField] private string secondPointParamName = "SecondPoint";

    

    private void OnTriggerEnter(Collider other)
    {
        if ( !other.CompareTag("Player")) return;

        // ① 标记已到达第二检查点
        ConversationManager.Instance.SetBool(secondPointParamName, true);

        // ② 如果对话仍开启，则模拟点击 Continue
        if (ConversationManager.Instance.IsConversationActive)
        {
            dialogueController.ContinueConversation(); // 内部调用 PressSelectedOption()
        }


    }
}
