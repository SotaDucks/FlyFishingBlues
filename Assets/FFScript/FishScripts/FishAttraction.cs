using UnityEngine;
using UnityEngine.Splines;
using UnityEngine.Events;

public class FishAttraction : MonoBehaviour
{
    [Header("References")]
    // 可以在 Inspector 留空，脚本会在运行时查找
    public Transform flyhook;
    public Transform exit1;
    public Transform exit2;

    [Header("Movement Speeds")]
    public float moveSpeed = 2f;
    public float returnMoveSpeed = 3f;

    [Header("Distances and Timing")]
    public float stopDistance = 0.5f;
    public float maxFollowDistance = 5f;    // 最大跟随距离阈值
    public float attractionDuration = 5f;

    [Header("Environment")]
    public float waterSurfaceHeight = 1f;   // 水面高度

    [Header("Bite Chance")]
    [Range(0f, 1f)]
    public float BiteChance = 0.5f;         // 咬钩概率

    [Header("Events")]
    public UnityEvent onEscape;             // 鱼逃跑时触发的事件

    private SplineAnimate splineAnimate;
    public bool isAttracted = false;
    private bool isReturning = false;
    private float attractionTimer = 0f;
    private Transform currentTarget;

    void Start()
    {
        // 初次查找钓饵，如果尚未实例化，后续会在 Update 中重试
        TryFindFlyhook();
        splineAnimate = GetComponent<SplineAnimate>();
    }

    void Update()
    {
        if (flyhook == null)
            TryFindFlyhook();

        if (isAttracted && flyhook != null)
        {
            float distanceToFlyhook = Vector3.Distance(transform.position, flyhook.position);

            // 超出最大跟随距离，触发逃跑
            if (distanceToFlyhook > maxFollowDistance)
            {
                ExitAttraction();
                return;
            }

            attractionTimer += Time.deltaTime;
            if (attractionTimer >= attractionDuration)
                CheckBite();
            else
                FollowFlyhook();
        }

        if (isReturning)
            MoveTowardsTarget();
    }

    private void TryFindFlyhook()
    {
        var go = GameObject.Find("flyhook");
        if (go != null)
            flyhook = go.transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (flyhook != null && other.transform == flyhook)
        {
            splineAnimate.enabled = false;
            isAttracted = true;
            attractionTimer = 0f;
        }
    }

    private void FollowFlyhook()
    {
        float dist = Vector3.Distance(transform.position, flyhook.position);
        if (dist > stopDistance)
        {
            Vector3 dir = (flyhook.position - transform.position).normalized;
            transform.rotation = Quaternion.Slerp(transform.rotation,
                                                 Quaternion.LookRotation(dir),
                                                 Time.deltaTime * moveSpeed);
            Vector3 target = Vector3.MoveTowards(transform.position,
                                                 flyhook.position,
                                                 moveSpeed * Time.deltaTime);
            target.y = Mathf.Min(target.y, waterSurfaceHeight);
            transform.position = target;
        }
        else
        {
            Vector3 dir = (flyhook.position - transform.position).normalized;
            transform.rotation = Quaternion.Slerp(transform.rotation,
                                                 Quaternion.LookRotation(dir),
                                                 Time.deltaTime * moveSpeed);
        }
    }

    private void CheckBite()
    {
        if (Random.value < BiteChance)
        {
            var biteHook = GetComponent<FishBiteHook>();
            if (biteHook != null)
                biteHook.enabled = true;
            this.enabled = false;
        }
        else
        {
            ExitAttraction();
        }
    }

    private void ExitAttraction()
    {
        isAttracted = false;
        isReturning = true;
        currentTarget = exit1;

        // 触发逃跑事件
        if (onEscape != null)
            onEscape.Invoke();
    }

    private void MoveTowardsTarget()
    {
        if (currentTarget == null) return;

        float dist = Vector3.Distance(transform.position, currentTarget.position);
        if (dist > stopDistance)
        {
            Vector3 dir = (currentTarget.position - transform.position).normalized;
            transform.rotation = Quaternion.Slerp(transform.rotation,
                                                 Quaternion.LookRotation(dir),
                                                 Time.deltaTime * returnMoveSpeed);
            Vector3 target = Vector3.MoveTowards(transform.position,
                                                 currentTarget.position,
                                                 returnMoveSpeed * Time.deltaTime);
            target.y = Mathf.Min(target.y, waterSurfaceHeight);
            transform.position = target;
        }
        else
        {
            if (currentTarget == exit1)
                currentTarget = exit2;
            else
                Destroy(transform.parent.gameObject);
        }
    }
}