using DynamicMeshCutter;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class KnifeController : MonoBehaviour
{
    [Header("震动参数")]
    [Tooltip("低频马达速度，范围 0–1")]
    public float lowFrequency = 0.5f;
    [Tooltip("高频马达速度，范围 0–1")]
    public float highFrequency = 0.5f;
    [Tooltip("震动持续时长（秒）")]
    public float duration = 0.2f;
    [Header("移动设置")]
    public float moveSpeed = 5f;          // W、A、S、D键的移动速度
    public float threshold = 0.1f;
    [Header("位移设置")]
    public float moveDownDistance = 0.8f; // 按下空格时沿世界Y轴下移的距离
    public float moveDownDuration = 0.4f; // 下移的持续时间

    private Vector3 originalPosition;     // 刀的原始位置

    private bool isSpacePressed = false;  // 是否按下空格键
    private bool isAnimating = false;     // 是否正在执行动画

    private Coroutine currentCoroutine = null;  // 当前运行的协程
    private PlaneBehaviour planeBehaviour;
    private float prevTrigger = 0f;
    void Start()
    {
        // 存储刀的原始位置
        originalPosition = transform.position;

        planeBehaviour = GetComponentInChildren<PlaneBehaviour>(); // 获取子对象中的PlaneBehaviour组件
    }
    private IEnumerator DoVibration(Gamepad pad)
    {
        // 开始震动
        pad.SetMotorSpeeds(lowFrequency, highFrequency);

        // 持续一段时间
        yield return new WaitForSeconds(duration);

        // 停止震动
        pad.SetMotorSpeeds(0f, 0f);
    }
    void Update()
    {
        HandleMovement();

        if (Gamepad.current.rightTrigger.ReadValue() > threshold && prevTrigger <= threshold)
        {
            StartCoroutine(DoVibration(Gamepad.current));
            isSpacePressed = true;
            StartCutAction();
        }
    }

    // 处理W、A、S、D键的移动
    void HandleMovement()
    {// 如果空格按下或动画正在执行，则禁用移动
        if (isSpacePressed || isAnimating)
            return;

        var gamepad = Gamepad.current;
        if (gamepad == null)
            return;  // 没插手柄则不动

        // 读左摇杆，x 对应水平，y 对应前后
        Vector2 stick = gamepad.leftStick.ReadValue();
        float moveX = stick.x;
        float moveZ = stick.y;

        // 如果需要和原版一样强制单位速度一致
        Vector3 raw = new Vector3(moveX, 0f, moveZ);
        Vector3 dir = raw.sqrMagnitude > 1f ? raw.normalized : raw;

        // 执行移动
        Vector3 move = dir * moveSpeed * Time.deltaTime;
        transform.Translate(move, Space.World);
    }

    // 开始空格按下后的动作，包括切割行为
    void StartCutAction()
    {
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }
        currentCoroutine = StartCoroutine(SpacePressedRoutine());
    }

    // 协程：按下空格键时的动作
    IEnumerator SpacePressedRoutine()
    {
        isAnimating = true;

        // 下移（仅Y轴）
        Vector3 targetDownPosition = originalPosition + new Vector3(0, -moveDownDistance, 0);
        yield return StartCoroutine(MoveToY(targetDownPosition.y, moveDownDuration));

        // 切割操作在刀下移完成后调用
        planeBehaviour.Cut();

        // 等待一小段时间（可选）
        yield return new WaitForSeconds(0.1f);

        // 自动复位位置（仅Y轴）
        yield return StartCoroutine(MoveToY(originalPosition.y, moveDownDuration * 2));

        isAnimating = false;
        isSpacePressed = false; // 重置空格键状态（可选，根据需要）
    }

    // 协程：平滑移动到目标Y位置（仅Y轴）
    IEnumerator MoveToY(float targetY, float duration)
    {
        float startY = transform.position.y;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float newY = Mathf.Lerp(startY, targetY, elapsed / duration);
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.position = new Vector3(transform.position.x, targetY, transform.position.z);
    }
    public void deleteTHeknife()
    {

        KnifeController knifeController = GetComponentInChildren<KnifeController>();
        // 如果 MeshRenderer 在子对象上
        MeshRenderer meshRenderer = GetComponentInChildren<MeshRenderer>();
        meshRenderer.enabled = false;
        knifeController.enabled = false;
    }
}
