using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonLogger : MonoBehaviour
{
    private static ButtonLogger _instance;
    private List<string> logs = new List<string>();

    void Awake()
    {
        // 单例模式，保证跨场景存在
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        _instance = this;
        DontDestroyOnLoad(this.gameObject);

        // 监听场景加载，确保新场景中的按钮也能注册
        SceneManager.sceneLoaded += OnSceneLoaded;

        // 注册当前场景的所有 UI 按钮
        RegisterAllButtons();
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        RegisterAllButtons();
    }

    void RegisterAllButtons()
    {
        // 查找场景中所有 Button 并添加点击回调
        Button[] buttons = FindObjectsOfType<Button>();
        foreach (Button btn in buttons)
        {
            string btnName = btn.name;
            btn.onClick.AddListener(() => LogPress(btnName));
        }
    }

    void Update()
    {
        // 监听手柄按键 (JoystickButton0 到 JoystickButton19)
        for (int i = 0; i <= 19; i++)
        {
            KeyCode code = (KeyCode)System.Enum.Parse(typeof(KeyCode), "JoystickButton" + i);
            if (Input.GetKeyDown(code))
            {
                LogPress("JoystickButton" + i);
            }
        }
    }

    public void LogPress(string btnName)
    {
        string time = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        logs.Add(time + " - " + btnName);
        Debug.Log("Logged button: " + btnName);
    }

    void OnApplicationQuit()
    {
        // 应用退出时自动保存日志
        SaveLog();
    }

    public void SaveLog()
    {
        // 保存到持久化数据路径
        string fileName = "ButtonLog_" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".txt";
        string path = Application.persistentDataPath + "/" + fileName;
        File.WriteAllLines(path, logs);
        Debug.Log("Button log saved to: " + path);
    }
}
