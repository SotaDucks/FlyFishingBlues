using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneAccrodingToScene : MonoBehaviour
{
    [Header("场景名称设置")]
    [Tooltip("如果上上个场景名等于此值，则加载 Scene If Match")]
    public string expectedPrePreviousScene;  // A

    [Tooltip("当上上个场景名等于 expectedPrePreviousScene 时加载此场景")]
    public string sceneIfMatch;              // B

    [Tooltip("当上上个场景名不等于 expectedPrePreviousScene 时加载此场景")]
    public string sceneIfNotMatch;           // C

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
