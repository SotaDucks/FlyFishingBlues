using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
   public void CutFish() { 
        SceneManager.LoadScene("CuttingFish");
    }
    public void DelayLoad()
    {
        Invoke("FishFree", 1.5f);
    }
    public void FishFree()
    {

        SceneManager.LoadScene("LandScene");
    }

}
