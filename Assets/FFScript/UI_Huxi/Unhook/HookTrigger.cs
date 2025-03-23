using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookTrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        // 检查进入的物体是否是 Hook
        Unhook hook = other.GetComponent<Unhook>();
        if (hook != null)
        {
            // 调用 Hook 上的方法
            hook.Fishfreehook = true;
          
        }
    }
}
