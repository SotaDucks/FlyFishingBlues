using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class A_Global : MonoBehaviour
{
    public static A_Global Instance { get; private set; }

    // 当前、上一个、上上个场景名
    public static string CurrentSceneName { get; private set; }
    public static string PreviousSceneName { get; private set; }
    public static string PrePreviousSceneName { get; private set; }

    void Awake()
    {
        // 如果已经有实例，销毁自己
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // 设置单例并保活
        Instance = this;
        DontDestroyOnLoad(gameObject);

        // 初始化当前场景名
        CurrentSceneName = SceneManager.GetActiveScene().name;
        PreviousSceneName = PrePreviousSceneName = null;

        // 订阅场景加载事件
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy()
    {
        // 取消订阅，防止内存泄漏
        if (Instance == this)
            SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // 每次场景加载后更新三段历史
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PrePreviousSceneName = PreviousSceneName;
        PreviousSceneName = CurrentSceneName;
        CurrentSceneName = scene.name;
    }

    /// <summary>
    /// 获取上上个场景的名字（可能为 null，表示还没加载过两次）
    /// </summary>
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
    public static string GetPrePreviousSceneName()
    {
        return PrePreviousSceneName;
    }
}
