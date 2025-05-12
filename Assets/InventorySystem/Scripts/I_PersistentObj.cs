using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class I_PersistentObj : MonoBehaviour
{
    [Tooltip("唯一标识，同一类常驻对象请设置成相同的 Tag 或这个标识")]
    public string persistentID = "GlobalPersistent";

    private static readonly System.Collections.Generic.HashSet<string> _initializedIDs
        = new System.Collections.Generic.HashSet<string>();

    void Awake()
    {
        // 如果已有同ID的实例记录，说明这是个重复加载进来的对象
        if (_initializedIDs.Contains(persistentID))
        {
            Destroy(gameObject);
            return;
        }
        // 首次遇到这个ID，注册并设为不销毁
        _initializedIDs.Add(persistentID);
        DontDestroyOnLoad(gameObject);
    }
}

