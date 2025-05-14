using UnityEngine;
using DialogueEditor;

public class SignTrigger : MonoBehaviour
{
    [Header("Collision 设置")]
    [Tooltip("标记 Flyhook 的 tag")]
    public string flyhookTag = "Flyhook";

    [Header("玩偶 拖放")]
    [Tooltip("被 Flyhook 抓着的玩偶")]
    public GameObject toyObject;
    [Tooltip("放下玩偶时的位置（可留空，则保持原地）")]
    public Transform dropSpot;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(flyhookTag)) return;

        // 1. 推进对话
        ConversationManager.Instance.SetBool("SignT", true);
        ConversationManager.Instance.PressSelectedOption();

        // 2. 解绑玩偶
        if (toyObject != null)
        {
            // 把玩偶从钩子上脱离（parent 设为 null）
            toyObject.transform.SetParent(null, true);

            // 可选：把玩偶移动到指定点
            if (dropSpot != null)
                toyObject.transform.position = dropSpot.position;

            // 如果玩偶有 Rigidbody，可以解除 kinematic 让它受物理控制
            var rb = toyObject.GetComponent<Rigidbody>();
            if (rb != null)
                rb.isKinematic = false;
        }
        else
        {
            Debug.LogWarning("[SignTrigger] toyObject 未设置，无法解绑放下玩偶");
        }

        // 只触发一次
        enabled = false;
    }
}
