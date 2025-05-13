// PullFish.cs
using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

[RequireComponent(typeof(FishDragLine))]
public class PullFish : MonoBehaviour
{
    [Tooltip("拖拽线脚本 (从同 GameObject 上或场景中名为 FlyLine 的对象自动获取)")]
    [SerializeField] private FishDragLine fishDragLine;

    [Header("Input System 动作")]
    [Tooltip("用于拉线的 Input Action 引用 (按下开始收线，松开停止收线)")]
    [SerializeField] private InputActionReference pullAction;

    private void Awake()
    {
        // 自动查找 FishDragLine
        if (fishDragLine == null)
        {
            // 先尝试同物体上
            fishDragLine = GetComponent<FishDragLine>();
            if (fishDragLine == null)
            {
                // 再尝试场景中的 FlyLine 对象
                var go = GameObject.Find("FlyLine");
                if (go != null)
                    fishDragLine = go.GetComponent<FishDragLine>();
            }
        }

        if (fishDragLine == null)
            Debug.LogError("PullFish: 未找到 FishDragLine 组件，请确保已有组件或场景中有名为 'FlyLine' 的对象。");
    }

    private void OnEnable()
    {
#if ENABLE_INPUT_SYSTEM
        if (pullAction != null)
        {
            pullAction.action.Enable();
            pullAction.action.performed += OnPullPerformed;
            pullAction.action.canceled  += OnPullCanceled;
        }
#endif
    }

    private void OnDisable()
    {
#if ENABLE_INPUT_SYSTEM
        if (pullAction != null)
        {
            pullAction.action.performed -= OnPullPerformed;
            pullAction.action.canceled  -= OnPullCanceled;
            pullAction.action.Disable();
        }
#endif
    }

#if ENABLE_INPUT_SYSTEM
    private void OnPullPerformed(InputAction.CallbackContext ctx)
    {
        fishDragLine?.StartPulling();
        Debug.Log("PullFish: 收线开始");
    }

    private void OnPullCanceled(InputAction.CallbackContext ctx)
    {
        fishDragLine?.StopPulling();
        Debug.Log("PullFish: 收线停止");
    }
#endif
}