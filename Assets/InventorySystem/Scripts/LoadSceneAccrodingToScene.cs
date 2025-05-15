using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneAccrodingToScene : MonoBehaviour
{
   
    void Reset()
    {
        // 确保 Collider 是 trigger
        var col = GetComponent<Collider>();
        col.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        // 只对 Player 标签的物体响应
        if (!other.CompareTag("Player"))
            return;

        // 读取全局存储的上上个场景名
        string prePrevName = A_Global.GetPrePreviousSceneName();
        Debug.Log($"[SceneLoadTrigger] 上上个场景: {prePrevName}");

        // 根据名称来决定加载哪个场景
        if ( prePrevName == "Stream1")
        {
            SceneManager.LoadScene("Stream1");
        }
        else if(prePrevName =="Stream2")
        {
            SceneManager.LoadScene("Stream2");
        }
        else if (prePrevName == "SeaScene")
        {
            SceneManager.LoadScene("SeaScene");
        }
        SceneManager.LoadScene("SceneTown");
    }
}
