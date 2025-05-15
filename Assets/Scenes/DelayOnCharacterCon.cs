using UnityEngine;
using System.Collections;

#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;   // 仅在新输入系统启用时编译
#endif

/// <summary>
/// Temporarily disables CharacterController, Animator, and all PlayerInput components
/// at scene start, then re-enables them after a user-defined delay (rounded to 0.1 s).
/// Attach this script to the same GameObject as the target components,
/// or to an empty GameObject and assign the references.
/// </summary>
[DisallowMultipleComponent]
public class DelayOnCharacterCon : MonoBehaviour
{
    #region Inspector Fields
    [Header("Target Components")]

    [Tooltip("CharacterController to activate after the delay.")]
    public CharacterController targetController;

    [Tooltip("Animator to activate after the delay.")]
    public Animator targetAnimator;

#if ENABLE_INPUT_SYSTEM
    [Tooltip("PlayerInput components to activate after the delay.\n" +
             "If empty, the script will look for all PlayerInput components on this GameObject and its children.")]
    public PlayerInput[] targetPlayerInputs;
#endif

    [Header("Delay Settings")]
    [Tooltip("Delay (seconds) before components are enabled. (0–10 s, kept to 1 dp)")]
    [Range(0f, 10f)] public float delay = 0.5f;
    #endregion

    // ─────────────────────────────────────────────────────────────────────────────

    private void Awake()
    {
        // Auto-assign if not linked manually
        if (targetController == null)
            targetController = GetComponent<CharacterController>();

        if (targetAnimator == null)
            targetAnimator = GetComponent<Animator>();

#if ENABLE_INPUT_SYSTEM
        if (targetPlayerInputs == null || targetPlayerInputs.Length == 0)
            targetPlayerInputs = GetComponentsInChildren<PlayerInput>(true); // 包含禁用的组件
#endif

        // Disable components immediately
        if (targetController != null)
            targetController.enabled = false;

        if (targetAnimator != null)
            targetAnimator.enabled = false;

#if ENABLE_INPUT_SYSTEM
        foreach (var pi in targetPlayerInputs)
            if (pi != null) pi.enabled = false;
#endif
    }

    private void Start()
    {
        // Begin countdown if至少有一个组件
        bool hasTargets =
            targetController != null ||
            targetAnimator != null
#if ENABLE_INPUT_SYSTEM
            || (targetPlayerInputs != null && targetPlayerInputs.Length > 0)
#endif
            ;

        if (hasTargets)
            StartCoroutine(EnableAfterDelay());
        else
            Debug.LogWarning($"{nameof(DelayOnCharacterCon)}: No target components found.");
    }

    /// <summary>Coroutine that waits, then enables every valid component.</summary>
    private IEnumerator EnableAfterDelay()
    {
        yield return new WaitForSeconds(delay);

        if (targetController != null)
            targetController.enabled = true;

        if (targetAnimator != null)
            targetAnimator.enabled = true;

#if ENABLE_INPUT_SYSTEM
        foreach (var pi in targetPlayerInputs)
            if (pi != null) pi.enabled = true;
#endif
    }

#if UNITY_EDITOR
    // Keep delay precision to one decimal place in the Inspector
    private void OnValidate()
    {
        delay = Mathf.Round(delay * 10f) * 0.1f;
    }
#endif
}
