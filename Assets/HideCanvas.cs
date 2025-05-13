using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideCanvas : MonoBehaviour
{
   
    [Header("Animator 与参数设置")]
    [Tooltip("挂载了 HasFlyRod 参数的 Animator")]
    public Animator animator;

    [Tooltip("Animator 中的 Bool 参数名称（区分大小写）")]
    public string parameterName = "HasFlyRod";

    private int paramHash;

    [Header("要切换的目标物体")]
    public GameObject targetObject;

    void Awake()
    {
        // 如果 inspector 里没填，则尝试自己找
        if (animator == null)
        {
            animator = GetComponent<Animator>();
            if (animator == null)
                Debug.LogError("[FlyRodToggle] 没有找到 Animator，请检查引用。");
        }

        // 缓存哈希以提高性能
        paramHash = Animator.StringToHash(parameterName);

        if (targetObject == null)
            Debug.LogError("[FlyRodToggle] 没有设置 targetObject，请拖入要切换的物体。");
    }

    void Update()
    {
        if (animator == null || targetObject == null) return;

        // 读取参数
        bool hasFlyRod = animator.GetBool(paramHash);

        // 切换显示
        targetObject.SetActive(!hasFlyRod);

        // 每秒最多打印一次，方便调试（可注释掉）
        if (Time.frameCount % 60 == 0)
            Debug.Log($"[FlyRodToggle] {parameterName} = {hasFlyRod}");
    }
}
