using DialogueEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scare : MonoBehaviour
{
    public TriggerConversation dlgCtrl;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        dlgCtrl.ContinueConversation();   // 直接推进
        gameObject.SetActive(false);      // 触发一次即可
    }
}
