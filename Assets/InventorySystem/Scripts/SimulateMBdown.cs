using System.Collections;
using System.Collections.Generic;
using DialogueEditor;
using UnityEngine;

public class SimulateMBdown : MonoBehaviour
{
    void Update()
    {
        // 仅在对话界面激活时响应
        if (ConversationManager.Instance != null
            && ConversationManager.Instance.IsConversationActive)
        {
            if (Input.GetKeyDown(KeyCode.JoystickButton0))
            {
                // 循环切回到第一项（若已在第一项则无影响）
                ConversationManager.Instance.SelectPreviousOption();  // :contentReference[oaicite:0]{index=0}
                // 确认当前高亮项
                ConversationManager.Instance.PressSelectedOption();    // :contentReference[oaicite:1]{index=1}
            }
            // Btn2 → 选中第二个选项并确认
            else if (Input.GetKeyDown(KeyCode.JoystickButton1))
            {
                // 移动到下一项（即第二项）
                ConversationManager.Instance.SelectNextOption();      // :contentReference[oaicite:2]{index=2}
                // 确认当前高亮项
                ConversationManager.Instance.PressSelectedOption();
            }

        }
    }
}
