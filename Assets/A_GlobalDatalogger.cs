using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

public class A_GlobalDatalogger : MonoBehaviour
{
    public static A_GlobalDatalogger Instance { get; private set; }
    public static string currentSceneName { get; private set; }
    public static string previousSceneName { get; private set; }
    public static string prePreviousSceneName { get; private set; }
    private string folderPath;
    private StringBuilder logBuffer = new StringBuilder();

    void Awake()
    {
        // 如果已有实例，自己销毁并退出
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        // 第一次进来，设置单例、保活
        Instance = this;
        DontDestroyOnLoad(gameObject);

        // 初始化场景历史
        currentSceneName = SceneManager.GetActiveScene().name;
        previousSceneName = prePreviousSceneName = null;

        // 订阅一次场景加载事件
        SceneManager.sceneLoaded += OnSceneLoaded;

        // 日志系统初始化
        CreateLogFolder();
        AddLog("[Logger] Session Start");
    }

    void OnDestroy()
    {
        // 取消注册，防止内存泄漏
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnApplicationQuit()
    {
        Flush();
    }

    /// <summary>
    /// 场景加载时的回调
    /// </summary>
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        LogSelection("SceneLoaded", scene.name);
        // 历史记录往前推
        prePreviousSceneName = previousSceneName;
        previousSceneName = currentSceneName;
        currentSceneName = scene.name;
    }

    private void CreateLogFolder()
    {
        string root = Path.Combine(Application.persistentDataPath, "Logs");
        Directory.CreateDirectory(root);
        string timeStamp = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
        folderPath = Path.Combine(root, timeStamp);
        Directory.CreateDirectory(folderPath); Debug.Log("Persistent Data Path: " + Application.persistentDataPath);

    }

    public void AddLog(string entry)
    {
        logBuffer.AppendLine($"[{DateTime.Now:HH:mm:ss.fff}] {entry}");
    }

    /// <summary>
    /// 通用记录接口：记录任意事件类型与内容
    /// </summary>
    public void LogSelection(string eventType, string detail)
    {
        AddLog($"{eventType}: {detail}");
    }

    public void Flush()
    {
        if (logBuffer.Length == 0) return;
        string fileName = $"log_{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.txt";
        string filePath = Path.Combine(folderPath, fileName);
        File.AppendAllText(filePath, logBuffer.ToString(), Encoding.UTF8);
        Debug.Log($"[Logger] Saved log to {filePath}");
        logBuffer.Clear();
    }
   
    public static string GetPrePreviousSceneName()
    {
        return prePreviousSceneName;
    }
}
