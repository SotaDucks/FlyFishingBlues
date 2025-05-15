using System.Collections;
using System.Collections.Generic;
using DialogueEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SimulateMBdown : MonoBehaviour
{
    public GameObject optionsPanel; // 在 Inspector 中赋值，例如对话选项的父对象
    void Update()
    {
        /* if (ConversationManager.Instance != null && ConversationManager.Instance.IsConversationActive)
         {
             if (Gamepad.current.dpad.left.wasPressedThisFrame) // 左保险杠，选择上一个选项
             {
                 ConversationManager.Instance.SelectPreviousOption(); ConversationManager.Instance.PressSelectedOption();
             }
             else if (Gamepad.current.dpad.right.wasPressedThisFrame) // 右保险杠，选择下一个选项
             {
                 ConversationManager.Instance.SelectNextOption(); ConversationManager.Instance.PressSelectedOption();
             }

         }*/

        Button[] options = optionsPanel.GetComponentsInChildren<Button>();
        if (options.Length >= 2)
        {
            if (Gamepad.current.dpad.up.wasPressedThisFrame)
            {
                options[0].onClick.Invoke(); // 左 D-pad 选择第一个选项
            }
            else if (Gamepad.current.dpad.down.wasPressedThisFrame)
            {
                options[1].onClick.Invoke(); // 右 D-pad 选择第二个选项
            }
        }



    }
}
