using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class S_LoadByName : MonoBehaviour
{
    private void Start()
    {
        string prePrevName = A_Global.GetPrePreviousSceneName();
    }
    public void KeepOnFishing()
    {
        
        SceneManager.LoadScene("Stream2");

    } public void LoadTownScene()
    {
        SceneManager.LoadScene("SceneTown");

    }
}
