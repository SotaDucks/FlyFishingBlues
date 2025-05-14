using UnityEngine;
using UnityEngine.InputSystem;
using DialogueEditor;

public class DialogueInputHandler : MonoBehaviour
{
    [Header("PlayerInput 引用")]
    public PlayerInput playerInput; // 你的 InputActionAsset 关联的组件

    private InputAction switchToRod;
    private InputAction castBack;
    private InputAction castForward;
    private InputAction endFishing;
    private InputAction switchToNoRod;

    private void Awake()
    {
        // 从 PlayerInput 找到对应的 Action
        var map = playerInput.currentActionMap;
        switchToRod = map.FindAction("SwitchToRod");
        castBack = map.FindAction("CastBack");
        castForward = map.FindAction("CastForward");
        endFishing = map.FindAction("EndFishing");
        switchToNoRod = map.FindAction("SwitchToNoRod");

        // 绑定回调
        switchToRod.performed += _ => OnAction("takeout");
        castBack.performed += _ => OnAction("castback");
        castForward.performed += _ => OnAction("castforward");
        endFishing.performed += _ => OnAction("ReelIn");
        switchToNoRod.performed += _ => OnAction("Yeah");
    }

    private void OnEnable()
    {
        switchToRod.Enable();
        castBack.Enable();
        castForward.Enable();
        endFishing.Enable();
        switchToNoRod.Enable();
    }

    private void OnDisable()
    {
        switchToRod.Disable();
        castBack.Disable();
        castForward.Disable();
        endFishing.Disable();
        switchToNoRod.Disable();
    }

    private void OnAction(string paramName)
    {
        // 设置 Dialogue 参数
        ConversationManager.Instance.SetBool(paramName, true);
        // 模拟 “Continue” 推进对话
        ConversationManager.Instance.PressSelectedOption();
    }
}
