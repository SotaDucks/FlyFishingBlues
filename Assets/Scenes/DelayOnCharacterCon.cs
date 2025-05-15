using UnityEngine;
using System.Collections;

/// <summary>
/// Temporarily disables a CharacterController at scene start, then
/// re‑enables it after a user‑defined delay (rounded to 0.1 s).
/// Attach this script to the same GameObject as the CharacterController,
/// or to an empty GameObject and assign the reference.
/// </summary>
[DisallowMultipleComponent]
public class DelayOnCharacterCon : MonoBehaviour
{
    [Header("Target Character Controller")]
    [Tooltip("The CharacterController to activate after the delay.\n" +
             "If left empty, the script will try to find one on the same GameObject.")]
    public CharacterController targetController;

    [Header("Delay Settings")]
    [Tooltip("Delay (seconds) before the CharacterController is enabled.\n" +
             "Rounded to one decimal place in the Inspector.")]
    [Range(0f, 10f)] public float delay = 0.5f;

    private void Awake()
    {
        // Auto‑assign if not linked manually
        if (targetController == null)
            targetController = GetComponent<CharacterController>();

        // Make sure the controller starts disabled
        if (targetController != null)
            targetController.enabled = false;
    }

    private void Start()
    {
        // Begin countdown
        if (targetController != null)
            StartCoroutine(EnableAfterDelay());
        else
            Debug.LogWarning($"{nameof(DelayOnCharacterCon)}: No CharacterController found.");
    }

    /// <summary>Coroutine that waits, then enables the controller.</summary>
    private IEnumerator EnableAfterDelay()
    {
        yield return new WaitForSeconds(delay);
        targetController.enabled = true;
    }

#if UNITY_EDITOR
    // Keep delay precision to one decimal in the Inspector
    private void OnValidate()
    {
        delay = Mathf.Round(delay * 10f) * 0.1f;
    }
#endif
}
