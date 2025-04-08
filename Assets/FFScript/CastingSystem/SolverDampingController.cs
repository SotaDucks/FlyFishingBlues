using UnityEngine;
using Obi;

public class SolverDampingController : MonoBehaviour
{
    // ObiSolver ���
    private ObiSolver solver;

    // Damping ���ýṹ
    [System.Serializable]
    public class DampingSetting
    {
        [Tooltip("���ӵĳ��ȣ����ڿո������ʱ��Ч��")]
        public float length; // ���ӳ���

        [Tooltip("��Ӧ�� damping ֵ")]
        public float damping; // ��Ӧ�� damping ֵ

        [Tooltip("��Ӧ�� Gravity Y ��ֵ")]
        public float gravityY; // ��Ӧ�� Gravity Y ��ֵ
    }

    // ��� damping ���ã��������ӳ��ȣ�
    [Header("Length-Based Damping Settings")]
    public DampingSetting firstDamping;
    public DampingSetting secondDamping;
    public DampingSetting thirdDamping;
    public DampingSetting fourthDamping;
    public DampingSetting fifthDamping;

    // ���������ȼ���ߵ� IfCastingDampingGravity ���ã����������ӳ��ȣ�
    [Header("Highest Priority Setting (IfCastingDamping&Gravity)")]
    [Tooltip("���ո��δ������ʱӦ�õ� damping ֵ")]
    public float castingDamping; // �� damping ֵ

    [Tooltip("���ո��δ������ʱӦ�õ� Gravity Y ֵ")]
    public float castingGravityY; // �� Gravity Y ֵ

    void Start()
    {
        // ��ȡ ObiSolver ���
        solver = GetComponent<ObiSolver>();

        if (solver == null)
        {
            Debug.LogError("ObiSolver component not found on this GameObject.");
        }
    }

    void Update()
    {
        // ���û���ҵ� ObiSolver���򲻼���ִ��
        if (solver == null) return;

        // // ���ո���Ƿ񱻰���
        // bool isSpacePressed = Input.GetKey(KeyCode.Space);

        // if (!isSpacePressed)
        // {
        //     //     // �ո��δ�����£�Ӧ��������ȼ��� damping �� gravity ����
        //     ApplyCastingDampingAndGravity();
        // }
        // else
        // {
        //     // �ո�������£�����ԭ�еĸ������ӳ��ȿ��� damping �� gravity ���߼�
        //     // ��ȡ�����ĵ�һ�� ObiRope �ĳ���
        ObiRope rope = GetFirstRope();
        if (rope == null) return;

        float currentLength = rope.restLength;

            // ������Ϣ�������ǰ���ӵĳ���
        Debug.Log($"Current Rope Length: {currentLength}");

            // �������ӳ������� damping �� gravity
        UpdateDampingAndGravityBasedOnLength(currentLength);
        
    }

    // ��ȡ��һ�� ObiRope ʵ��
    private ObiRope GetFirstRope()
    {
        foreach (var actor in solver.actors)
        {
            if (actor is ObiRope rope)
            {
                return rope;
            }
        }
        return null;
    }

    // �������ӳ��ȸ��� damping �� gravity
    void UpdateDampingAndGravityBasedOnLength(float currentLength)
    {
        // ƥ�䵱ǰ���ӵĳ��Ȳ����� damping �� gravity
        if (currentLength >= firstDamping.length && currentLength < secondDamping.length)
        {
            ApplyDampingAndGravity(firstDamping, "firstDamping");
        }
        else if (currentLength >= secondDamping.length && currentLength < thirdDamping.length)
        {
            ApplyDampingAndGravity(secondDamping, "secondDamping");
        }
        else if (currentLength >= thirdDamping.length && currentLength < fourthDamping.length)
        {
            ApplyDampingAndGravity(thirdDamping, "thirdDamping");
        }
        else if (currentLength >= fourthDamping.length && currentLength < fifthDamping.length)
        {
            ApplyDampingAndGravity(fourthDamping, "fourthDamping");
        }
        else if (currentLength >= fifthDamping.length)
        {
            ApplyDampingAndGravity(fifthDamping, "fifthDamping");
        }
    }

    // Ӧ��������ȼ��� damping �� gravity ���ã����������ӳ��ȣ�
    void ApplyCastingDampingAndGravity()
    {
        solver.parameters.damping = castingDamping;
        solver.gravity = new Vector3(solver.gravity.x, castingGravityY, solver.gravity.z);
        solver.PushSolverParameters(); // ǿ�Ƹ��²���
        Debug.Log($"[IfCastingDamping&Gravity] Damping updated to {castingDamping}, Gravity Y updated to {castingGravityY}");
    }

    // ͨ�õķ�����Ӧ�� damping �� gravity�������������Ϣ
    void ApplyDampingAndGravity(DampingSetting setting, string settingName)
    {
        solver.parameters.damping = setting.damping;
        solver.gravity = new Vector3(solver.gravity.x, setting.gravityY, solver.gravity.z);
        solver.PushSolverParameters(); // ǿ�Ƹ��²���
        Debug.Log($"[{settingName}] Damping updated to {setting.damping}, Gravity Y updated to {setting.gravityY} for rope length {setting.length}");
    }
}
