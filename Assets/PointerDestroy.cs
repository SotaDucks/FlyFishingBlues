using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerDestroy : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // 检查进入触发区域的是否是玩家
        if (other.CompareTag("Player"))
        {
            // 销毁自身
            Destroy(gameObject);
        }
    }
} 