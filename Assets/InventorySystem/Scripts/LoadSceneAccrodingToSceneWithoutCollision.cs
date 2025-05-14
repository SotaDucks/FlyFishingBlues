using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneAccrodingToSceneWithoutCollision : MonoBehaviour
{
    [Header("场景名称设置")]
    [Tooltip("如果上上个场景名等于此值，则加载 Scene If Match")]
    public string expectedPrePreviousScene;  // A

    [Tooltip("当上上个场景名等于 expectedPrePreviousScene 时加载此场景")]
    public string sceneIfMatch;              // B

    [Tooltip("当上上个场景名不等于 expectedPrePreviousScene 时加载此场景")]
    public string sceneIfNotMatch;           // C

    /// <summary>
    /// 调用此方法即可根据上上个场景名加载对应场景
    /// </summary>
    public void LoadSceneByHistory()
    {
        // 从全局单例读取上上个场景名
        string prePrevName = A_Global.GetPrePreviousSceneName();
        Debug.Log($"[LoadSceneByHistory] 上上个场景: {prePrevName}");

        // 判断并加载
        if (!string.IsNullOrEmpty(prePrevName) && prePrevName == expectedPrePreviousScene)
        {
            Debug.Log($"匹配到“{expectedPrePreviousScene}”，加载场景 “{sceneIfMatch}”");
            SceneManager.LoadScene(sceneIfMatch);
        }
        else
        {
            Debug.Log($"未匹配到“{expectedPrePreviousScene}”，加载场景 “{sceneIfNotMatch}”");
            SceneManager.LoadScene(sceneIfNotMatch);
        }
    }
}
