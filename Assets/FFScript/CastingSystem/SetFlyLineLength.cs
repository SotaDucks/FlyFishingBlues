using UnityEngine;
using Obi;

/// <summary>
/// Minimal controller – applies an initial length delta to the rope at start,
/// allowing positive or negative values, clamped so restLength never drops below zero.
/// Attach this script to the GameObject that has both ObiRope and ObiRopeCursor components.
/// </summary>
[RequireComponent(typeof(ObiRopeCursor))]
[RequireComponent(typeof(ObiRope))]
public class SetFlyLineLength : MonoBehaviour
{
    [Header("Rope Length Settings")]
    [Tooltip("Delta length to apply when the scene starts. Can be positive (extend) or negative (shorten).")]
    public float initialLength = 5f;

    private ObiRopeCursor cursor;
    private ObiRope rope;

    private void Awake()
    {
        cursor = GetComponent<ObiRopeCursor>();
        rope   = GetComponent<ObiRope>();
    }

    private void Start()
    {
        // Compute the minimum allowed delta so that restLength + delta >= 0
        float minDelta = -rope.restLength;
        // Clamp the initialLength to [minDelta, +infinity)
        float clampedDelta = Mathf.Clamp(initialLength, minDelta, float.MaxValue);

        // Apply the clamped delta to the rope
        cursor.ChangeLength(clampedDelta);

        Debug.Log($"[SetFlyLineLength] Applied initial delta {clampedDelta:F2}, new restLength: {rope.restLength:F2}");
    }
}