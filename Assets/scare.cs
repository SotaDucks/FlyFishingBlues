using DialogueEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scare : MonoBehaviour
{
    [Header("可选：调用一次后是否禁用触发器")]
    [SerializeField] private bool disableAfterTrigger = true;

    private bool hasTriggered = false;

    private void Reset()
    {
        // 确保 Collider 是触发器
        var col = GetComponent<Collider>();
        col.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (hasTriggered) return;
        if (!other.CompareTag("Player")) return;

        // ① 将 FishedOn 设为 true
        ConversationManager.Instance.SetBool("FishedOn", true);

      

        hasTriggered = true;

        if (disableAfterTrigger)
            gameObject.SetActive(false);
    }
}
