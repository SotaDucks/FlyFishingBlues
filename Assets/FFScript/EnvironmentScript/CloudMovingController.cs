using UnityEngine;

public class CloudMovingController : MonoBehaviour
{
    [Header("Point Settings")]
    [Tooltip("移动目标点（移动结束后瞬移到 Point2，再回到此点）")]
    public Transform point1;
    [Tooltip("瞬移位置（到达 Point1 后瞬移到此处）")]
    public Transform point2;

    [Header("Movement Settings")]
    [Tooltip("移动速度，单位：单位/秒")]
    public float speed = 2f;
    [Tooltip("判定到达 Point1 的距离阈值")]
    public float arrivalThreshold = 0.1f;

    // 当前要前往的位置
    private Vector3 targetPos;

    void Start()
    {
        if (point1 == null || point2 == null)
        {
            Debug.LogError("CloudMovingController: 请在 Inspector 中为 point1 和 point2 指定 Transform！");
            enabled = false;
            return;
        }
        // 初始目标设为 Point1
        targetPos = point1.position;
    }

    void Update()
    {
        // 以匀速向目标移动
        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);

        // 判断是否到达 Point1（只在 targetPos == point1 时才触发瞬移）
        if (targetPos == point1.position &&
            Vector3.Distance(transform.position, point1.position) <= arrivalThreshold)
        {
            // 瞬移到 Point2
            transform.position = point2.position;
            // 下一步又开始向 Point1 移动
            targetPos = point1.position;
        }
    }
}
