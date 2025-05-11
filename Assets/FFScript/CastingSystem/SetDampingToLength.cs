using UnityEngine;
using Obi;

[RequireComponent(typeof(ObiSolver))]
public class SetDampingToLength : MonoBehaviour
{
    [System.Serializable]
    public class DampingSetting
    {
        [Tooltip("Maximum rope length (restLength) for this damping setting.")]
        public float lengthThreshold;
        [Tooltip("Damping value to apply when rope length is within this threshold.")]
        public float damping;
    }

    [Header("Length-Based Damping Settings")]
    [Tooltip("Applied when restLength <= secondSetting.lengthThreshold")]
    public DampingSetting firstSetting;
    [Tooltip("Applied when restLength <= thirdSetting.lengthThreshold")]
    public DampingSetting secondSetting;
    [Tooltip("Applied when restLength > thirdSetting.lengthThreshold")]
    public DampingSetting thirdSetting;

    private ObiSolver solver;
    private ObiRope rope;
    private float lastAppliedDamping = float.NaN;

    void Start()
    {
        solver = GetComponent<ObiSolver>();
        if (solver == null)
        {
            Debug.LogError("[SetDampingToLength] ObiSolver not found.");
            enabled = false;
            return;
        }
    }

    void Update()
    {
        // 如果还没找到 rope，就尝试绑定
        if (rope == null)
        {
            rope = GetFirstRope();
            if (rope == null)
            {
                Debug.Log("[SetDampingToLength] Waiting for rope to be registered...");
                return;
            }
            else
            {
                Debug.Log("[SetDampingToLength] Rope bound successfully.");
            }
        }

        float currentLength = rope.restLength;
        float currentDamping = solver.parameters.damping;

        // 每帧打印以便调试
        Debug.Log($"[SetDampingToLength] Current restLength={currentLength:F2}, current damping={currentDamping:F3}");

        float targetDamping;
        if (currentLength < secondSetting.lengthThreshold)
            targetDamping = firstSetting.damping;
        else if (currentLength < thirdSetting.lengthThreshold)
            targetDamping = secondSetting.damping;
        else
            targetDamping = thirdSetting.damping;

        if (!Mathf.Approximately(targetDamping, lastAppliedDamping))
        {
            solver.parameters.damping = targetDamping;
            solver.PushSolverParameters();
            lastAppliedDamping = targetDamping;
            Debug.Log($"[SetDampingToLength] Applied new damping={targetDamping:F3} for restLength={currentLength:F2}");
        }
    }

    private ObiRope GetFirstRope()
    {
        if (solver == null) return null;
        foreach (var actor in solver.actors)
            if (actor is ObiRope r) return r;
        return null;
    }
}
