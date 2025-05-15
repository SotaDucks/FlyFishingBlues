using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class CuutingFishSimulate : MonoBehaviour
{
    string prePrevName;
    private void Awake()
    {
       prePrevName = A_Global.GetPrePreviousSceneName();
    }
    private void Update()
    {
    
        
        var pad = Gamepad.current;

        if (pad.dpad.up.wasPressedThisFrame)
        {
            
            if (prePrevName == "Stream1") 
            {
              
                SceneManager.LoadScene("Stream2");
            }
            else if(prePrevName== "SeaScene")
            {
                SceneManager.LoadScene("SeaScene");
            }
            SceneManager.LoadScene("Stream2");

        }

        if (pad.dpad.down.wasPressedThisFrame)
        {
            SceneManager.LoadScene("SceneTown");
        }




    }


}
