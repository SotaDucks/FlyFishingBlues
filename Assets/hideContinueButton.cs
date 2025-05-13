using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class hideContinueButton : MonoBehaviour
{
   
    void Update()
    {

        // 找到即时生成的 ConversationButton 并隐藏
        var btn = GameObject.Find("ConversationButton(Clone)");
        if (btn != null)
        {
            btn.SetActive(false);
       
        }
    
    }
}
