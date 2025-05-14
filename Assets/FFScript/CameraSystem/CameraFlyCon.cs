using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class CameraFlyCon : MonoBehaviour
{
    [Header("Target Settings")]
    public Transform target;              // 要观察的对象，如鱼
    public float smoothSpeed = 0.125f;    // 平滑跟随速度
    public Vector3 offset;                // 相机偏移量

    [Header("Viewport Rect Animation Settings")]
    public float initialViewportX = 0.99f;
    public float initialViewportY = 0.99f;
    public float finalViewportX = 0.6f;
    public float finalViewportY = 0.6f;
    public float viewportAnimationDuration = 1.0f;

    [Header("Camera Close Delay")]
    [Tooltip("鱼逃跑后，延迟多久关闭摄像机（秒）")]
    public float escapeCameraDelay = 2f;

    private Camera targetCamera;
    private FishAttraction fishAttraction;
    private FishBiteHook fishBiteHook;              // ← 新增：引用 FishBiteHook
    private bool isCameraActivated = false;

    void Start()
    {
        // 获取 Camera 组件并初始禁用
        targetCamera = GetComponent<Camera>();
        if (targetCamera == null)
        {
            Debug.LogError("未找到 Camera 组件。");
            return;
        }
        targetCamera.enabled = false;

        // 初始化 Viewport Rect
        targetCamera.rect = new Rect(initialViewportX, initialViewportY, targetCamera.rect.width, targetCamera.rect.height);

        if (target == null)
        {
            Debug.LogError("未设置目标 Transform。");
            return;
        }

        // 获取 FishAttraction 并订阅逃跑事件
        fishAttraction = target.GetComponent<FishAttraction>();
        if (fishAttraction == null)
        {
            Debug.LogError("未在目标上找到 FishAttraction 组件。");
            return;
        }
        fishAttraction.onEscape.AddListener(OnFishEscape);

        // ← 新增：获取 FishBiteHook 并订阅 BiteFailToEscape 事件
        fishBiteHook = target.GetComponent<FishBiteHook>();
        if (fishBiteHook == null)
        {
            Debug.LogError("未在目标上找到 FishBiteHook 组件。");
        }
        else
        {
            fishBiteHook.BiteFailToEscape.AddListener(OnFishEscape);
        }
    }

    void LateUpdate()
    {
        if (target == null || fishAttraction == null) return;

        // 鱼被吸引时激活摄像机
        if (fishAttraction.isAttracted && !isCameraActivated)
        {
            ActivateCamera();
        }

        // 平滑跟随和朝向目标
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
        transform.LookAt(target);
    }

    private void ActivateCamera()
    {
        targetCamera.enabled = true;
        isCameraActivated = true;
        Debug.Log("摄像机已激活。");
        StartCoroutine(AnimateViewportRect());
    }

    private void OnFishEscape()
    {
        // 鱼逃跑或咬钩失败后延迟关闭摄像机
        fishAttraction.isAttracted = false;
    StartCoroutine(DeactivateCameraAfterDelay());
    }

    private IEnumerator DeactivateCameraAfterDelay()
    {
        yield return new WaitForSeconds(escapeCameraDelay);
        targetCamera.enabled = false;
        isCameraActivated = false;
        Debug.Log("摄像机已关闭 (延迟后)。");
    }

    private IEnumerator AnimateViewportRect()
    {
        float elapsedTime = 0f;
        Rect startRect = targetCamera.rect;
        Rect endRect = new Rect(finalViewportX, finalViewportY, startRect.width, startRect.height);

        while (elapsedTime < viewportAnimationDuration)
        {
            float t = elapsedTime / viewportAnimationDuration;
            float currentX = Mathf.Lerp(initialViewportX, finalViewportX, t);
            float currentY = Mathf.Lerp(initialViewportY, finalViewportY, t);
            targetCamera.rect = new Rect(currentX, currentY, startRect.width, startRect.height);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        targetCamera.rect = new Rect(finalViewportX, finalViewportY, targetCamera.rect.width, targetCamera.rect.height);
    }
}
