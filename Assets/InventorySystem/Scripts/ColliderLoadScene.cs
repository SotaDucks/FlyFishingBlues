using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ColliderLoadScene : MonoBehaviour
{
    [Tooltip("要加载的场景名称（需在 Build Settings 中添加）")]
    public string sceneName;
    void OnTriggerEnter(Collider other)
    {


        if (other.CompareTag("Player")) { SceneManager.LoadScene(sceneName);
            Debug.Log("123123");
        }
 
        
    }

   
}
