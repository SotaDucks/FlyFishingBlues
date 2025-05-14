using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideObjAccrodingToPPScene : MonoBehaviour
{
    [Header("预期的上上个场景名")]
    [Tooltip("如果上上个场景名等于此值，将执行隐藏/显示操作")]
    public string expectedPrePreviousSceneName;

    [Header("目标物体")]
    [Tooltip("要隐藏或显示的物体；若留空，则作用于当前 GameObject")]
    public GameObject targetObject;

    [Header("隐藏逻辑")]
    [Tooltip("若为 true，则在匹配时隐藏物体；否则在不匹配时隐藏")]
    public bool hideOnMatch = true;

    void Start()
    {
        // 如果没手动指定，就用挂脚本的这个物体
        if (targetObject == null)
        {
            targetObject = gameObject;
        }

        // 从全局单例中取出上上个场景名
        string prePrev = A_Global.GetPrePreviousSceneName();
        Debug.Log($"[HideObjAccordingToPPScene] 上上个场景名：{prePrev}");

        bool isMatch = !string.IsNullOrEmpty(prePrev)
                       && prePrev == expectedPrePreviousSceneName;

        // 根据匹配结果和 hideOnMatch 决定激活状态
        if (isMatch == hideOnMatch)
        {
            targetObject.SetActive(false);
            Debug.Log($"隐藏物体 \"{targetObject.name}\"（匹配 = {isMatch}）");
        }
        else
        {
            targetObject.SetActive(true);
            Debug.Log($"显示物体 \"{targetObject.name}\"（匹配 = {isMatch}）");
        }
    }
}
