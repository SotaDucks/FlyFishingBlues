using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public string TheIdentifierTag;
    public string triggerTag;
    private NewDialogueSystem dialogueSystem;
    private void Start()
    {
        // 找到场景中的对话系统
        dialogueSystem = FindObjectOfType<NewDialogueSystem>();
        if (dialogueSystem == null)
        {
            Debug.LogError("场景中找不到DialogueSystem");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(TheIdentifierTag) && dialogueSystem != null)
        {
            dialogueSystem.OnTriggerDetected(triggerTag);
        }

    }
}