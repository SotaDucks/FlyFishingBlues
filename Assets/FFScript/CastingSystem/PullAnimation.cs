using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

[RequireComponent(typeof(Animator))]
public class PullAnimation : MonoBehaviour
{
    [Header("Animator 参数名")]
    [Tooltip("Animator 中的 Bool 参数：鱼是否咬钩")]
    [SerializeField] private string fishHasBiteParam = "FishHasBite";
    [Tooltip("Animator 中的 Bool 参数：是否已挂钩")]
    [SerializeField] private string setHookParam     = "SetHook";
    [Tooltip("Animator 中要设置的 Pull Bool")]
    [SerializeField] private string pullBoolParam   = "Pull";

    [Header("输入设置 (新 Input System)")]
    [Tooltip("引用一个按键或触发器的 Input Action，用于拉线检测")]
    [SerializeField] private InputActionReference pullAction;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        if (pullAction != null)
            pullAction.action.Enable();
    }

    private void OnDisable()
    {
        if (pullAction != null)
            pullAction.action.Disable();
    }

    private void Update()
    {
        // 1. Animator 中的两个 bool 必须都为 true
        bool hasBite  = animator.GetBool(fishHasBiteParam);
        bool isHooked = animator.GetBool(setHookParam);

        // 2. 检测玩家是否正在按住该输入动作
        bool isHolding = pullAction != null && pullAction.action.IsPressed();

        // 计算新的 Pull 状态
        bool shouldPull = hasBite && isHooked && isHolding;

        // 如果状态发生变化，立即同步到 Animator
        animator.SetBool(pullBoolParam, shouldPull);
    }
}
