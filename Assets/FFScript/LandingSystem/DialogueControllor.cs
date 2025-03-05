using UnityEngine;
using DialogueEditor;

public class DialogueTrigger : MonoBehaviour
{
    public NPCConversation conversation;
    private bool hasTalked = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !hasTalked && !ConversationManager.Instance.IsConversationActive)
        {
            ConversationManager.Instance.StartConversation(conversation);
        }
    }

    private void OnEnable()
    {
        ConversationManager.OnConversationEnded += HandleConversationEnded;
    }

    private void OnDisable()
    {
        ConversationManager.OnConversationEnded -= HandleConversationEnded;
    }

    private void HandleConversationEnded()
    {
        hasTalked = true; // 更新标志位
    }
}