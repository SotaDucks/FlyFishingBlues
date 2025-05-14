using UnityEngine;
using DialogueEditor;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class hideContinueButton : MonoBehaviour
{
    private void OnEnable()
    {
        ConversationManager.OnConversationStarted += OnConversationStarted;
    }

    private void OnDisable()
    {
        ConversationManager.OnConversationStarted -= OnConversationStarted;
    }

    private void OnConversationStarted()
    {
        // 等一帧让对话 UI 都生成完
        StartCoroutine(HideGraphicsNextFrame());
    }

    private IEnumerator HideGraphicsNextFrame()
    {
        yield return null;

        // 找到场景中所有 Button，针对名称包含 ConversationButton 的那几个
        var allButtons = FindObjectsOfType<Button>(true);
        foreach (var btn in allButtons)
        {
            if (!btn.gameObject.name.Contains("ConversationButton"))
                continue;


            // 2. 隐藏它自己的 Image（背景）
            var bg = btn.GetComponent<Image>();
            if (bg)
            {
                bg.enabled = false;
                bg.raycastTarget = false;
            }

            // 3. 隐藏它子物体里的所有 Image（可能还有装饰）和 Text / TMP_Text
            foreach (var img in btn.GetComponentsInChildren<Image>(true))
                img.enabled = false;
            foreach (var txt in btn.GetComponentsInChildren<Text>(true))
                txt.enabled = false;
            foreach (var tmp in btn.GetComponentsInChildren<TMP_Text>(true))
                tmp.enabled = false;
        }
    }
}
