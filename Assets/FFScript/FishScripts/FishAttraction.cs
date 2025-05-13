using UnityEngine;
using UnityEngine.Splines;

public class FishAttraction : MonoBehaviour
{
    [Header("References")]
    // You can leave this unset in the Inspector; the script will pick it up at runtime
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

    private SplineAnimate splineAnimate;
    public bool isAttracted = false;
    private bool isReturning = false;
    private float attractionTimer = 0f;
    private Transform currentTarget;

    void Start()
    {
        // Attempt an initial find; if flyhook isn't in the scene yet, we'll retry in Update()
        TryFindFlyhook();
        splineAnimate = GetComponent<SplineAnimate>();
    }

    void Update()
    {
        // If we haven't got a reference to the flyhook yet, try again each frame
        if (flyhook == null)
            TryFindFlyhook();

        if (isAttracted && flyhook != null)
        {
            float distanceToFlyhook = Vector3.Distance(transform.position, flyhook.position);

            // If we've strayed too far, abandon and exit
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
        // Only trigger attraction if we've found the flyhook
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
            // Rotate smoothly toward the hook
            Vector3 dir = (flyhook.position - transform.position).normalized;
            transform.rotation = Quaternion.Slerp(transform.rotation,
                                                 Quaternion.LookRotation(dir),
                                                 Time.deltaTime * moveSpeed);
            // Move toward the hook, clamped to waterSurfaceHeight
            Vector3 target = Vector3.MoveTowards(transform.position,
                                                 flyhook.position,
                                                 moveSpeed * Time.deltaTime);
            target.y = Mathf.Min(target.y, waterSurfaceHeight);
            transform.position = target;
        }
        else
        {
            // Only rotate, no translation
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
