using UnityEngine;
using Obi;
using HutongGames.PlayMaker;

public class FishDragLine : MonoBehaviour
{
    public float dragSpeed = 1f;       // 拉动时绳子延长速度
    public float retrieveSpeed = 1f;   // 收线时绳子缩短速度
    public float struggleSpeed = 1f;   // 挣扎时绳子延长速度
    public float pullSpeed = 1f;       // 被拉时绳子缩短速度

    private ObiRope rope;              // ObiRope 组件，用于控制绳索
    private ObiRopeCursor ropeCursor;  // ObiRopeCursor 组件，用于移动绳索游标
    public bool isDragging = false;
    public bool isRetrieving = false;
    public bool isStruggling = false;
    public bool isPulling = false;

    private FishDragLine fishDragLine;
    private PlayMakerFSM playerFsm;    // 引用玩家角色上的 PlayMaker FSM

    void Start()
    {
        // 获取 ObiRope 与 ObiRopeCursor
        rope = GetComponent<ObiRope>();
        ropeCursor = GetComponent<ObiRopeCursor>();
        if (rope == null || ropeCursor == null)
            Debug.LogError("请确认 FlyLine 上挂有 ObiRope 和 ObiRopeCursor 组件。");

        // 获取玩家角色上的 FSM
        GameObject characterObject = GameObject.Find("PlayerArmature");
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
        // 根据状态控制绳索长度变化
        if (isDragging && rope != null && ropeCursor != null)
            ExtendRope(dragSpeed);
        if (isRetrieving && rope != null && ropeCursor != null)
            ExtendRope(-retrieveSpeed);
        if (isStruggling && rope != null && ropeCursor != null)
            ExtendRope(struggleSpeed);
        if (isPulling && rope != null && ropeCursor != null)
            ExtendRope(-pullSpeed);

        // 自动清除所有拉伸/收缩动作 —— 当 PlayMaker 状态为 FishOnSetTheHook 时触发
        if (playerFsm != null && playerFsm.Fsm.ActiveStateName == "FishOnSetTheHook")
        {
            StopAllActions();
        }
    }

    // 开始/停止各动作
    public void StartDragging()    { isDragging = true; }
    public void StopDragging()     { isDragging = false; }

    public void StartRetrieving()  { isRetrieving = true; }
    public void StopRetrieving()   { isRetrieving = false; }

    public void StartStruggling()  { isStruggling = true; }
    public void StopStruggling()   { isStruggling = false; }

    public void StartPulling()     { isPulling = true; }
    public void StopPulling()      { isPulling = false; }

    // 停止所有状态标记
    public void StopAllActions()
    {
        isDragging = false;
        isRetrieving = false;
        isStruggling = false;
        isPulling = false;
    }

    // 调用 ObiRopeCursor 修改绳子长度
    private void ExtendRope(float speed)
    {
        ropeCursor.ChangeLength(speed * Time.deltaTime);
        Debug.Log("Rope extended by: " + (speed * Time.deltaTime));
    }
}
