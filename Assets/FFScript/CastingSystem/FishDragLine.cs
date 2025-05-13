using UnityEngine;
using Obi;
using HutongGames.PlayMaker;

public class FishDragLine : MonoBehaviour
{
    [Header("速度设置")]
    public float dragSpeed    = 1f;    // 拉动时绳子延长速度
    public float retrieveSpeed= 1f;    // 收线时绳子缩短速度
    public float struggleSpeed= 1f;    // 挣扎时绳子延长速度
    public float pullSpeed    = 1f;    // 被拉时绳子缩短速度

    [Header("长度限制")]
    [UnityEngine.Tooltip("绳子的最短 RestLength，当收线时不会比此值更短")]
    public float minRopeLength = 0.5f;

    private ObiRope       rope;              
    private ObiRopeCursor ropeCursor;  

    public bool isDragging   = false;
    public bool isRetrieving = false;
    public bool isStruggling = false;
    public bool isPulling    = false;

    private PlayMakerFSM playerFsm;

    void Start()
    {
        // 获取 ObiRope 与 ObiRopeCursor
        rope       = GetComponent<ObiRope>();
        ropeCursor = GetComponent<ObiRopeCursor>();
        if (rope == null || ropeCursor == null)
            Debug.LogError("请确认 FlyLine 上挂有 ObiRope 和 ObiRopeCursor 组件。");

        // 获取 PlayMaker FSM
        var characterObject = GameObject.Find("PlayerArmature");
        if (characterObject != null)
        {
            playerFsm = characterObject.GetComponent<PlayMakerFSM>();
            if (playerFsm == null)
                Debug.LogError("未找到 PlayerArmature 上的 PlayMakerFSM 组件。");
        }
        else
        {
            Debug.LogError("Scene 中找不到名为 'PlayerArmature' 的 GameObject。");
        }
    }

    void Update()
    {
        if (rope == null || ropeCursor == null) return;

        // 根据状态控制绳索长度变化
        if (isDragging)    ExtendRope(dragSpeed);
        if (isRetrieving)  ExtendRope(-retrieveSpeed);
        if (isStruggling)  ExtendRope(struggleSpeed);
        if (isPulling)     ExtendRope(-pullSpeed);

        // PlayMaker 切回钩挂状态时自动停止所有动作
        if (playerFsm != null && playerFsm.Fsm.ActiveStateName == "FishOnSetTheHook")
            StopDragging();
    }

    // 开始/停止各动作
    public void StartDragging()    { isDragging   = true;  }
    public void StopDragging()     { isDragging   = false; }
    public void StartRetrieving()  { isRetrieving = true;  }
    public void StopRetrieving()   { isRetrieving = false; }
    public void StartStruggling()  { isStruggling = true;  }
    public void StopStruggling()   { isStruggling = false; }
    public void StartPulling()     { isPulling    = true;  }
    public void StopPulling()      { isPulling    = false; }

    // 停止所有状态标记
    public void StopAllActions()
    {
        isDragging    = false;
        isRetrieving  = false;
        isStruggling  = false;
        isPulling     = false;
    }

    // 调用 ObiRopeCursor 修改绳子长度，同时不允许缩短到 minRopeLength 以下
    private void ExtendRope(float speed)
    {
        float delta = speed * Time.deltaTime;

        // 如果是收线（delta < 0），且会低于最小长度，则限制缩短量
        if (delta < 0f)
        {
            float currentLength = rope.restLength;
            float targetLength  = currentLength + delta;
            if (targetLength < minRopeLength)
                delta = minRopeLength - currentLength;
        }

        ropeCursor.ChangeLength(delta);
        Debug.Log($"Rope length changed by {delta:F4}, new restLength ~ {rope.restLength:F4}");
    }
}
