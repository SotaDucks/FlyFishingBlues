using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class I_PersistentObj : MonoBehaviour
{
    void Awake()
    {
        // 这行代码会让该 GameObject 在加载新场景时不被销毁
        DontDestroyOnLoad(gameObject);
    }
}

