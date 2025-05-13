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
    public void ContinueConversation()
    {
        
            ConversationManager.Instance.PressSelectedOption();
    }
}
