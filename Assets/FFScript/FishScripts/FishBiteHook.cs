using UnityEngine;
using Obi;

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
    private Animator characterAnimator;      // 角色的 Animator
    private bool isMovingToHook = true;      // 当前阶段：游向钩子
    private float waitTimer = 0f;            // 等待计时器
    private FishDragLine fishDragLine;       // 鱼线拖拽脚本

    void Start()
    {
        // 查找钓饵
        GameObject flyhookObject = GameObject.Find("flyhook");
        if (flyhookObject != null)
            flyhook = flyhookObject.transform;
        else
            Debug.LogError("未找到名为 'flyhook' 的 GameObject");

        // 获取鱼的 Animator
        fishAnimator = GetComponent<Animator>();
        if (fishAnimator == null)
            Debug.LogError("未找到鱼的 Animator 组件");

        // 获取玩家角色 Animator
        GameObject characterObject = GameObject.Find("PlayerArmature");
        if (characterObject != null)
            characterAnimator = characterObject.GetComponent<Animator>();
        else
            Debug.LogError("未找到名为 'PlayerArmature' 的角色 GameObject");

        // 获取鱼线拖拽组件
        var lineObj = GameObject.Find("FlyLine");
        if (lineObj != null)
            fishDragLine = lineObj.GetComponent<FishDragLine>();
        else
            Debug.LogError("未找到名为 'FlyLine' 的 GameObject");
    }

    void Update()
    {
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
            Vector3 direction = (flyhook.position - transform.position).normalized;
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

            // 通知玩家 Animator 已咬钩
            if (characterAnimator != null)
                characterAnimator.SetBool("FishHasBite", true);
        }
    }

    private void EscapeToExit()
    {
        isFishBite = true;
        if (waitTimer > 0)
        {
            waitTimer -= Time.deltaTime;
            return;
        }

        // 检测玩家上钩动作
        if (characterAnimator != null && characterAnimator.GetCurrentAnimatorStateInfo(0).IsName("SetTheHook"))
        {
            if (fishDragLine != null)
                fishDragLine.StopDragging();

            var rb = GetComponent<Rigidbody>();
            if (rb != null)
                rb.isKinematic = false;

            this.enabled = false;
            GetComponent<FishLanding>().enabled = true;
            return;
        }

        // 未上钩则继续逃离
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
        var flyLine = GameObject.Find("FlyLine");
        if (flyLine == null) { Debug.LogError("未找到 FlyLine"); return; }

        var attachments = flyLine.GetComponents<ObiParticleAttachment>();
        if (attachments.Length >= 3)
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
