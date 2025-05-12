using DialogueEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class TriggerConversation : MonoBehaviour
{
    public NPCConversation conversation;   // Inspector 拖入
    private bool started;

    private void OnTriggerEnter(Collider other)
    {
        if (started || !other.CompareTag("Player")) return;

        ConversationManager.Instance.StartConversation(conversation); // 启动对话 :contentReference[oaicite:0]{index=0}:contentReference[oaicite:1]{index=1}
        started = true;
    }

    // 供 Trigger2 调用
    public void ContinueConversationAndSetFlag(bool value)
    {
        if (!ConversationManager.Instance.IsConversationActive) return;

        // ① 更新参数
        ConversationManager.Instance.SetBool("FishedOn", value);      // 设置对话参数 :contentReference[oaicite:2]{index=2}:contentReference[oaicite:3]{index=3}

        // ② 模拟点“Continue”
        ConversationManager.Instance.PressSelectedOption();           // 等同用户按继续 :contentReference[oaicite:4]{index=4}:contentReference[oaicite:5]{index=5}
    }


}
