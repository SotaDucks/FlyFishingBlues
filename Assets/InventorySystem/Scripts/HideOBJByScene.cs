using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HideOBJByScene : MonoBehaviour
{
    [Header("要判断的上上一个场景名")]
    [Tooltip("如果上一个场景名与此字段一致，则隐藏 targetObject")]
    public string previousSceneName;

    [Header("要隐藏的目标物体")]
    [Tooltip("当 previousSceneName 匹配时，此对象会被隐藏")]
    public GameObject targetObject;

    private void Start()
    {
        // 1. 读取上一个场景名（第一次运行会返回空字符串）
        string lastScene = A_GlobalDatalogger.GetPrePreviousSceneName();

        // 2. 如果匹配，就隐藏目标物体
        if (!string.IsNullOrEmpty(lastScene)
            && lastScene == previousSceneName
            && targetObject != null)
        {
            targetObject.SetActive(false);
            Debug.Log($"[PreviousSceneHide] 因为上上个场景是 “{lastScene}”，隐藏了 {targetObject.name}");
        }

        // 3. 存储当前场景名，供下一次场景加载判断用
        string currentScene = SceneManager.GetActiveScene().name;
        PlayerPrefs.SetString("LastScene", currentScene);
        PlayerPrefs.Save();
    }
}
