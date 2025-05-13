// TireFish.cs
using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

public class TireFish : MonoBehaviour
{
    [Header("Stamina 消耗设置")]
    [Tooltip("每次消耗的体力值")]
    public int staminaCost = 15;

    [Header("输入设置 (新 Input System)")]
    [Tooltip("引用一个用于消耗体力的 Input Action")]
    [SerializeField] private InputActionReference consumeAction;

    private FishStaminaBar staminaBar;

    private void Awake()
    {
        // 自动寻找场景中名为 "FishStaminaBar" 的 GameObject 并获取组件
        GameObject barGO = GameObject.Find("FishStaminaBar");
        if (barGO != null)
        {
            staminaBar = barGO.GetComponent<FishStaminaBar>();
            if (staminaBar == null)
                Debug.LogError("在 GameObject 'FishStaminaBar' 上未找到 FishStaminaBar 组件。");
        }
        else
        {
            Debug.LogError("场景中未找到名为 'FishStaminaBar' 的 GameObject。");
        }
    }

    private void OnEnable()
    {
        if (consumeAction != null)
            consumeAction.action.Enable();
    }

    private void OnDisable()
    {
        if (consumeAction != null)
            consumeAction.action.Disable();
    }

    private void Update()
    {
        // 当绑定的 Input Action 被触发时消耗体力
        if (consumeAction != null && consumeAction.action.triggered)
        {
            if (staminaBar != null)
                staminaBar.UseStamina(staminaCost);
        }
    }
}
