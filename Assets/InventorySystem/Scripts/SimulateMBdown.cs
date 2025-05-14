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
                ConversationManager.Instance.SelectPreviousOption();
            }
            else if (Input.GetKeyDown(KeyCode.JoystickButton5)) // 右保险杠，选择下一个选项
            {
                ConversationManager.Instance.SelectNextOption();
            }
            if (Input.GetKeyDown(KeyCode.JoystickButton0)) // A键，确认选择
            {
                ConversationManager.Instance.PressSelectedOption();
            }
        }
    }
}
