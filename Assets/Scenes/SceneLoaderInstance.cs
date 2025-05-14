using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

/// <summary>
/// 通过一个空白“TransferScene”做中转，实现场景间的干净切换
/// </summary>
public class TransferSceneLoader : MonoBehaviour
{
    public static TransferSceneLoader Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// 从任意当前场景通过 TransferScene 中转再去加载目标场景
    /// </summary>
    public void LoadWithTransfer(string targetSceneName)
    {
        StartCoroutine(TransferAndLoad(targetSceneName));
    }

    private IEnumerator TransferAndLoad(string targetSceneName)
    {
        // 1) 先加载空白中转场景，清空当前场景
        yield return SceneManager.LoadSceneAsync("TransferScene", LoadSceneMode.Single);

        // 2) 等一帧确保所有旧对象都卸载完成
        yield return null;

        // 3) 再加载真正的目标场景
        yield return SceneManager.LoadSceneAsync(targetSceneName, LoadSceneMode.Single);
    }
}
