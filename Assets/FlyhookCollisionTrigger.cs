using UnityEngine;
using DialogueEditor;

public class FlyhookCollisionTrigger : MonoBehaviour
{
    [Tooltip("标记 Flyhook 的 tag")]
    public string flyhookTag = "Flyhook";

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(flyhookTag)) return;

        // 玩家将玩偶钩住
        ConversationManager.Instance.SetBool("targetOnT", true);
        ConversationManager.Instance.PressSelectedOption();

        // 可选：只触发一次
        enabled = false;
    }
}
