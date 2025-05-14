using DialogueEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startConversation : MonoBehaviour
{
    public NPCConversation conversation;
    // Start is called before the first frame update
    void Start()
    {
        ConversationManager.Instance.StartConversation(conversation);

    }


}
