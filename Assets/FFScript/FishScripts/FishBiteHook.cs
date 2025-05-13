using UnityEngine;
using Obi;
using HutongGames.PlayMaker;

public class FishBiteHook : MonoBehaviour
{
    public Transform flyhook;                // 钓饵 Transform 引用
    public Transform exit1;                  // 逃离出口 Transform 引用
    public float moveSpeed = 3f;             // 游向钩子的速度
    public float maxChaseDistance = 5f;      // 攻击前最大追逐距离阈值
    public float stopDistance = 0.5f;        // 停止移动的最小距离
    public float waitTime = 1f;              // 等待上钩动作的时间
    public float escapeSpeed = 5f;           // 逃离时的速度
    public bool isFishBite = false;          // 是否已咬钩

    private Animator fishAnimator;           // 鱼的 Animator
    private Animator characterAnimator;      // 玩家角色的 Animator
    private PlayMakerFSM playerFsm;          // 玩家角色上的 PlayMaker FSM
    private bool isMovingToHook = true;      // 当前阶段：游向钩子
    private float waitTimer = 0f;            // 等待计时器
    private FishDragLine fishDragLine;       // 鱼线拖拽脚本
    private ObiParticleAttachment[] attachments; // 鱼线上粒子附着组件数组

    void Start()
    {
        // 查找钩饵
        GameObject flyhookObject = GameObject.Find("flyhook");
        if (flyhookObject != null)
            flyhook = flyhookObject.transform;
        else
            Debug.LogError("未找到名为 'flyhook' 的 GameObject");

        // 获取鱼的 Animator
        fishAnimator = GetComponent<Animator>();
        if (fishAnimator == null)
            Debug.LogError("未找到鱼的 Animator 组件");

        // 获取玩家角色 Animator 及 FSM
        GameObject characterObject = GameObject.Find("PlayerArmature");
        if (characterObject != null)
        {
            characterAnimator = characterObject.GetComponent<Animator>();
            playerFsm = characterObject.GetComponent<PlayMakerFSM>();
            if (playerFsm == null)
                Debug.LogError("未找到 PlayerArmature 上的 PlayMakerFSM 组件");
        }
        else
        {
            Debug.LogError("未找到名为 'PlayerArmature' 的角色 GameObject");
        }

        // 获取鱼线拖拽组件与粒子附着列表
        GameObject lineObj = GameObject.Find("FlyLine");
        if (lineObj != null)
        {
            fishDragLine = lineObj.GetComponent<FishDragLine>();
            attachments = lineObj.GetComponents<ObiParticleAttachment>();
        }
        else
            Debug.LogError("未找到名为 'FlyLine' 的 GameObject");
    }

    void Update()
    {
        // 每帧检测第3个粒子附着组件是否已绑定鱼体
        bool bound = attachments != null && attachments.Length >= 3 && attachments[2].target != null;
        isFishBite = bound;

        // 将绑定状态传给玩家 Animator
        if (characterAnimator != null)
            characterAnimator.SetBool("FishHasBite", bound);

        // 分阶段执行逻辑
        if (isMovingToHook)
            MoveToHook();
        else
            EscapeToExit();
    }

    private void MoveToHook()
    {
        // 如果钩子距离过远，直接放弃攻击，进入逃离阶段
        float distanceToHook = Vector3.Distance(transform.position, flyhook.position);
        if (distanceToHook > maxChaseDistance)
        {
            isMovingToHook = false;
            return;
        }

        // 游向钩子逻辑
        if (distanceToHook > stopDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, flyhook.position, moveSpeed * Time.deltaTime);
        }
        else
        {
            // 咬钩并附着到鱼线
            AttachFishToFlyline();
            isMovingToHook = false;
            waitTimer = waitTime;

            // 播放鱼的咬钩动画
            if (fishAnimator != null)
                fishAnimator.SetTrigger("TroutBite");
        }
    }

    private void EscapeToExit()
    {
        if (waitTimer > 0)
        {
            waitTimer -= Time.deltaTime;
            return;
        }

        // 检测 PlayMaker FSM 状态
        if (playerFsm != null && playerFsm.Fsm.ActiveStateName == "FishOnSetTheHook")
        {
            if (fishDragLine != null)
                fishDragLine.StopDragging();

            Rigidbody rb = GetComponent<Rigidbody>();
            if (rb != null)
                rb.isKinematic = false;

            this.enabled = false;
            FishLanding landing = GetComponent<FishLanding>();
            if (landing != null)
                landing.enabled = true;
            return;
        }

        // 未达到上钩状态则继续逃离
        if (characterAnimator != null)
            characterAnimator.SetBool("FishOn", true);
        if (fishDragLine != null)
            fishDragLine.StartDragging();

        Vector3 dir = (exit1.position - transform.position).normalized;
        transform.rotation = Quaternion.LookRotation(dir);
        transform.position = Vector3.MoveTowards(transform.position, exit1.position, escapeSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, exit1.position) < stopDistance)
        {
            Destroy(transform.parent.gameObject);
            Debug.Log("鱼已逃离并销毁");
            if (fishDragLine != null)
                fishDragLine.StopDragging();
        }
    }

    private void AttachFishToFlyline()
    {
        if (attachments != null && attachments.Length >= 3)
        {
            attachments[2].target = this.transform;
            Debug.Log("已将鱼附着到鱼线上");
        }
        else
        {
            Debug.LogError("FlyLine 上 ObiParticleAttachment 数量不足");
        }
    }
}