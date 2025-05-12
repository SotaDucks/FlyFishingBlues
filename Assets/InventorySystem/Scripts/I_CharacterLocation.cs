using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class I_CharacterLocation : MonoBehaviour
{
    [System.Serializable]
    public struct SceneSpawn
    {
        public string sceneName;    // 场景的名字（与 Build Settings 中一致）
        public Vector3 position;    // 玩家在该场景中的生成坐标
        public Quaternion rotation; // （可选）玩家面朝方向
    }

    [Tooltip("为每个场景设置对应的生成点")]
    public SceneSpawn[] sceneSpawns;

    // 防止在同一场景里重复移动
    private string _lastScene = "";

    private void Awake()
    {
        // 确保这个物体不被销毁
        DontDestroyOnLoad(gameObject);
        // 监听所有场景加载完成事件
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 如果和上一次是同一个场景，就不再次移动
        if (scene.name == _lastScene) return;
        _lastScene = scene.name;

        // 在配置表里找对应的场景名
        foreach (var spawn in sceneSpawns)
        {
            if (spawn.sceneName == scene.name)
            {
                // 瞬移玩家到指定位置和旋转
                transform.position = spawn.position;
                transform.rotation = spawn.rotation;
                return;
            }
        }
        // 如果没找到对应条目，则保持当前位置不变
    }
}
