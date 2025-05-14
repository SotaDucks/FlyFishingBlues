using UnityEngine;
using Obi;
using HutongGames.PlayMaker;
using UnityEngine.Events;    // ← 新增

public class FishBiteHook : MonoBehaviour
{
    [Header("Events")]
    public UnityEvent BiteFailToEscape;    // ← 新增：当咬钩失败、超出范围逃跑时触发

    [Header("Target Settings")]
    public Transform flyhook;              // 钓饵 Transform 引用
    public Transform exit1;                // 逃离出口 Transform 引用

    [Header("Movement Settings")]
    public float moveSpeed = 3f;           
    public float maxChaseDistance = 5f;    // 攻击前最大追逐距离阈值
    public float stopDistance = 0.5f;      
    public float waitTime = 1f;            
    public float escapeSpeed = 5f;         
    public bool isFishBite = false;        

    private Animator fishAnimator;         
    private Animator characterAnimator;    
    private PlayMakerFSM playerFsm;        
    private bool isMovingToHook = true;    
    private float waitTimer = 0f;          
    private FishDragLine fishDragLine;     
    private ObiParticleAttachment[] attachments;

    void Start()
    {
        // 确保事件实例化，防止为空
        if (BiteFailToEscape == null)
            BiteFailToEscape = new UnityEvent();

        // 查找钩饵
        var flyhookObj = GameObject.Find("flyhook");
        if (flyhookObj != null)
            flyhook = flyhookObj.transform;
        else
            Debug.LogError("未找到名为 'flyhook' 的 GameObject");

        // 获取鱼的 Animator
        fishAnimator = GetComponent<Animator>();
        if (fishAnimator == null)
            Debug.LogError("未找到鱼的 Animator 组件");

        // 获取玩家角色 Animator 与 FSM
        var charObj = GameObject.Find("PlayerArmature");
        if (charObj != null)
        {
            characterAnimator = charObj.GetComponent<Animator>();
            playerFsm = charObj.GetComponent<PlayMakerFSM>();
            if (playerFsm == null)
                Debug.LogError("未找到 PlayerArmature 上的 PlayMakerFSM 组件");
        }
        else
        {
            Debug.LogError("未找到名为 'PlayerArmature' 的角色 GameObject");
        }

        // 获取鱼线拖拽组件与粒子附着列表
        var lineObj = GameObject.Find("FlyLine");
        if (lineObj != null)
        {
            fishDragLine = lineObj.GetComponent<FishDragLine>();
            attachments = lineObj.GetComponents<ObiParticleAttachment>();
        }
        else
        {
            Debug.LogError("未找到名为 'FlyLine' 的 GameObject");
        }
    }

    void Update()
    {
        // 每帧检测第3个粒子附着组件是否已绑定鱼体
        bool bound = attachments != null
                  && attachments.Length >= 3
                  && attachments[2].target != null;
        isFishBite = bound;

        // 将绑定状态同步给玩家 Animator
        if (characterAnimator != null)
            characterAnimator.SetBool("FishHasBite", bound);

        // 分阶段执行
        if (isMovingToHook)
            MoveToHook();
        else
            EscapeToExit();
    }

    private void MoveToHook()
    {
        float dist = Vector3.Distance(transform.position, flyhook.position);

        // ← 新增：当超过最大追逐距离，触发咬钩失败逃跑事件
        if (dist > maxChaseDistance)
        {
            Debug.Log("鱼因超出追逐范围而放弃攻击并逃跑");
            BiteFailToEscape.Invoke();
            isMovingToHook = false;
            return;
        }

        if (dist > stopDistance)
        {
            // 向钩子移动
            transform.position = Vector3.MoveTowards(transform.position, flyhook.position, moveSpeed * Time.deltaTime);
        }
        else
        {
            // 到达钩子，附着并进入等待
            AttachFishToFlyline();
            isMovingToHook = false;
            waitTimer = waitTime;

            if (fishAnimator != null)
                fishAnimator.SetTrigger("TroutBite");
        }
    }

    private void EscapeToExit()
    {
        if (waitTimer > 0f)
        {
            waitTimer -= Time.deltaTime;
            return;
        }

        // 如果 PlayMaker FSM 处于上钩状态，则切换到 FishLanding
        if (playerFsm != null && playerFsm.Fsm.ActiveStateName == "FishOnSetTheHook")
        {
            if (fishDragLine != null)
                fishDragLine.StopDragging();

            var rb = GetComponent<Rigidbody>();
            if (rb != null)
                rb.isKinematic = false;

            this.enabled = false;
            var landing = GetComponent<FishLanding>();
            if (landing != null)
                landing.enabled = true;
            return;
        }

        // 继续逃离：同步角色动画并开始拉线（仅当已咬钩）
        if (characterAnimator != null)
            characterAnimator.SetBool("FishOn", true);

        if (fishDragLine != null && isFishBite)
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
