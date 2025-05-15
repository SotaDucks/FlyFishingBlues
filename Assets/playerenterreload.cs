using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerEnterReload : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // 检查进入触发区域的是否是玩家
        if (other.CompareTag("Player"))
        {
            // 获取当前场景名称
            string currentSceneName = SceneManager.GetActiveScene().name;
            
            // 立即重新加载当前场景
            SceneManager.LoadScene(currentSceneName);
        }
    }
}
