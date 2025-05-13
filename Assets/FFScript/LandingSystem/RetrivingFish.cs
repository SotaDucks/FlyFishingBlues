// RetrivingFish.cs
using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(FishDragLine))]
public class RetrivingFish : MonoBehaviour
{
    [Header("Animator 参数名")]
    [Tooltip("Animator 中的 Bool 参数：鱼是否咬钩")]
    [SerializeField] private string fishHasBiteParam = "FishHasBite";
    [Tooltip("Animator 中的 Bool 参数：是否已挂钩")]
    [SerializeField] private string setHookParam     = "SetHook";

    [Header("输入设置 (新 Input System)")]
    [Tooltip("引用一个用于收线的 Input Action")]
    [SerializeField] private InputActionReference retrieveAction;

    private Animator animator;
    private FishDragLine fishDragLine;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        fishDragLine = GetComponent<FishDragLine>();
        if (fishDragLine == null)
            Debug.LogError("RetrivingFish: 未在该 GameObject 上找到 FishDragLine 组件。");
    }

    private void OnEnable()
    {
        if (retrieveAction != null)
            retrieveAction.action.Enable();
    }

    private void OnDisable()
    {
        if (retrieveAction != null)
            retrieveAction.action.Disable();
    }

    private void Update()
    {
        // 条件1：Animator 中两个 bool 都为 true
        bool hasBite  = animator.GetBool(fishHasBiteParam);
        bool isHooked = animator.GetBool(setHookParam);

        // 条件2：触发收线输入
        bool triggered = retrieveAction != null && retrieveAction.action.triggered;

        if (hasBite && isHooked && triggered)
        {
            fishDragLine.StartPulling();
        }
    }
}
