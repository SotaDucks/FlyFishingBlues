using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodEffect : MonoBehaviour
{
    [Header("目标物体控制")]
    [Tooltip("拖入需要控制的GameObject")]
    public GameObject targetObject;  // 需在Inspector中指定物体

    void Start()
    {
        // 初始化时关闭物体[3](@ref)
        if (targetObject != null)
            targetObject.SetActive(false);
    }

    void Update()
    {
        // 检测空格键按下事件[1,2](@ref)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // 切换物体激活状态[3](@ref)
            if (targetObject.activeSelf == false)
            {
                targetObject.SetActive(true);
                Debug.Log("物体已激活"); // 调试信息输出[3](@ref)
            }
        }
    }
}
