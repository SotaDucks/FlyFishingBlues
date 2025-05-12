using UnityEngine;

public class FishermanContinueTrigger : MonoBehaviour
{
    public TriggerConversation dlgCtrl;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        dlgCtrl.ContinueConversationAndSetFlag(true); // true＝已经上鱼
        gameObject.SetActive(false);                  // 触发一次即可
    }
}
