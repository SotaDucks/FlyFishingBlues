using System.Collections;
using System.Collections.Generic;
using DialogueEditor;
using UnityEngine;

public class SimulateMBdown : MonoBehaviour
{
    void Update()
    {
        if (ConversationManager.Instance != null && ConversationManager.Instance.IsConversationActive)
        {
            if (Input.GetKeyDown(KeyCode.JoystickButton4)) // 左保险杠，选择上一个选项
            {
                ConversationManager.Instance.SelectPreviousOption(); ConversationManager.Instance.PressSelectedOption();
            }
            else if (Input.GetKeyDown(KeyCode.JoystickButton5)) // 右保险杠，选择下一个选项
            {
                ConversationManager.Instance.SelectNextOption(); ConversationManager.Instance.PressSelectedOption();
            }
            
        }
    }
}
