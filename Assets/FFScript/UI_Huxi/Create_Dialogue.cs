using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class Create_Dialogue : MonoBehaviour
{
    public NPCConversation Conversation;


    public void CreateDialogue()
    {
        ConversationManager.Instance.StartConversation(Conversation);
    }


}
