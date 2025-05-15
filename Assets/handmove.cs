using DG.Tweening;
using HutongGames.PlayMaker.Actions;
using UnityEngine;
using UnityEngine.InputSystem;

public class handmove : MonoBehaviour
{
    [SerializeField] private float downOffset = 0.6f;
    // 单程用时
    [SerializeField] private float duration = 0.1f;
    // 是否正在播放 Tween，避免重复触发
    private bool isTweening = false;
    public float moveSpeed = 5f;
    private float height;
    public float originalHeight = 1.5f;
    public float lowdownhand = 0.6f;
    public float distance = 0.5f; // Z轴的距离（根据相机的设置调整）
    private Animator animator;
    private bool isGrabbing = false; // 标记是否按下了左键（抓取状态）

    public bool allowHandMovementWhileGrabbing = true; // 控制抓取时是否允许手部移动

    private HandGrabber handGrabber;

    void Start()
    {
        // 获取 Animator 组件
        animator = GetComponent<Animator>();

        // 获取 HandGrabber 脚本
        handGrabber = GetComponent<HandGrabber>();
    }

    void Update()
    {
       

        // 检测鼠标左键按下
        if (Input.GetMouseButtonDown(0)|| Gamepad.current.rightTrigger.ReadValue() ==1&& !isTweening)
        {
         PlayTriggerTween();
            // 播放抓的动画
            animator.Play("GrabHold");

            // 标记为抓取状态
            isGrabbing = true;

            // 将手的Y轴减少0.6
         // MoveHandDown();

            // 尝试抓取物体
            if (handGrabber != null)
            {
                handGrabber.TryGrabObject();
            }
        }

        // 检测鼠标左键松开
        if (Input.GetMouseButtonUp(0) || Gamepad.current.rightTrigger.ReadValue() == 0)
        {
            // 播放放手的动画
            animator.Play("GrabRelease");

            // 恢复到默认的Y轴高度
          //  ResetHandY();

            // 重置抓取状态
            isGrabbing = false;

            // 释放物体
            if (handGrabber != null)
            {
                handGrabber.ReleaseObject();
            }
        }
        HandleMovementR();
        // 让 GameObject 随鼠标移动
       
    }
    private void PlayTriggerTween()
    {
        isTweening = true;

        // 记录原始 Y
        float originalY = transform.position.y;
        float downY = originalY - downOffset;

        // 向下移动然后 Yoyo 回弹
        transform
            .DOMoveY(downY, duration)
            .SetLoops(2, LoopType.Yoyo)
            .SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                // 确保回到精确原位
                var pos = transform.position;
                pos.y = originalY;
                transform.position = pos;

                isTweening = false;
            });
    }
    void HandleMovementR()
    {// 如果空格按下或动画正在执行，则禁用移动
        if (isGrabbing && !allowHandMovementWhileGrabbing  )
        {
            return;
        }
        var gamepad = Gamepad.current;
        if (gamepad == null)
            return;
        if ( !isTweening && Gamepad.current.rightTrigger.ReadValue() == 0)
        {
            Vector2 input = gamepad.rightStick.ReadValue();
            // （可选）加一个小死区过滤抖动
            if (input.magnitude < 0.1f)
                return;
         
            Vector3 delta = new Vector3(input.x, 0, input.y)
                            * 10
                            * Time.deltaTime;

            transform.Translate(delta, Space.World);

        }
      
    }
    // 让 GameObject 作为光标移动
    void MoveWithMouse()
    {
        // 如果不允许抓取时移动且正在抓取，则不更新手部位置
        if (isGrabbing && !allowHandMovementWhileGrabbing)
        {
            return;
        }

        // 获取鼠标位置并将其转换为世界坐标
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = distance; // 设置Z轴距离（根据相机的设置调整）

        // 将鼠标的屏幕坐标转换为世界坐标
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        // 设置手部的高度
        if (!isGrabbing)
        {
            worldPosition.y = originalHeight;  // 在非抓取状态时，锁定Y轴为原高度
        }
        else
        {
            worldPosition.y = Mathf.Lerp(worldPosition.y, height, Time.deltaTime * moveSpeed);
        }

        // 更新手部的位置
        transform.position = worldPosition;
    }

    // 将手的Y轴减少0.6
    void MoveHandDown()
    {
        // 减少Y轴高度
        Debug.LogError(height);
        height = originalHeight - lowdownhand;
        Debug.LogError(height);
    }

    // 恢复手的默认Y轴高度
    void ResetHandY()
    {
        height = originalHeight; // 恢复到初始的Y轴高度
    }
}
