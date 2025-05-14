using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DialogueEditor;
using UnityEngine.InputSystem;

public class DialogueEditorLoadScene : MonoBehaviour
{
    [Tooltip("当 LoadKill 为 true 且手柄右键按下时加载此场景")]
    public string sceneToLoad = "CuttingFish";

    void Update()
    {
       
        if (Gamepad.current?.buttonEast.wasPressedThisFrame == true)
        {
            // 加载场景
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
