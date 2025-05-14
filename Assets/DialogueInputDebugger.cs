using UnityEngine;
using UnityEngine.InputSystem;
using DialogueEditor;

public class DialogueBoolDebugger : MonoBehaviour
{
    [Header("PlayerInput 引用")]
    public PlayerInput playerInput;

    private InputAction switchToRod;
    private InputAction castBack;
    private InputAction castForward;
    private InputAction endFishing;
    private InputAction switchToNoRod;

    void Awake()
    {
        var map = playerInput.currentActionMap;
        switchToRod = map.FindAction("SwitchToRod");
        castBack = map.FindAction("CastBack");
        castForward = map.FindAction("CastForward");
        endFishing = map.FindAction("EndFishing");
        switchToNoRod = map.FindAction("SwitchToNoRod");

        Debug.Assert(switchToRod != null, "[Debugger] 找不到 SwitchToRod");
        Debug.Assert(castBack != null, "[Debugger] 找不到 CastBack");
        Debug.Assert(castForward != null, "[Debugger] 找不到 CastForward");
        Debug.Assert(endFishing != null, "[Debugger] 找不到 EndFishing");
        Debug.Assert(switchToNoRod != null, "[Debugger] 找不到 SwitchToNoRod");

        switchToRod.performed += _ => DebugAction("takeout");
        castBack.performed += _ => DebugAction("castback");
        castForward.performed += _ => DebugAction("castforward");
        endFishing.performed += _ => DebugAction("ReelIn");
        switchToNoRod.performed += _ => DebugAction("Yeah");
    }

    void OnEnable()
    {
        switchToRod.Enable();
        castBack.Enable();
        castForward.Enable();
        endFishing.Enable();
        switchToNoRod.Enable();
    }

    void OnDisable()
    {
        switchToRod.Disable();
        castBack.Disable();
        castForward.Disable();
        endFishing.Disable();
        switchToNoRod.Disable();
    }

    private void DebugAction(string paramName)
    {
        Debug.Log($"[Debugger] 按键触发 → SetBool({paramName}, true)");
        // 仅设置和读取，不推进对话
        bool val = ConversationManager.Instance.GetBool(paramName);
        Debug.Log($"[Debugger] GetBool({paramName}) = {val}");

        // 如果以后需要恢复推进，把下面一行取消注释即可：
        // ConversationManager.Instance.PressSelectedOption();
    }

}
