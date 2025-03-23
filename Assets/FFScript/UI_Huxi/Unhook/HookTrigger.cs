using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookTrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        // ������������Ƿ��� Hook
        Unhook hook = other.GetComponent<Unhook>();
        if (hook != null)
        {
            // ���� Hook �ϵķ���
            hook.Fishfreehook = true;
          
        }
    }
}
