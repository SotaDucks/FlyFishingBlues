using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class Unhook : MonoBehaviour
{
    public bool Fishfreehook;
    public quaternion hookPosition;
    public float maxRotation = 40f; // 最大旋转角度
    public float maxSpeed = 100f; // 最大旋转速度
    private float screenCenterX;
    public float moveSpeed = 5f; // 移动速度
    private bool Fisdown;
   

    private Vector2 moveInput;    // 存摇杆读数


    void Start()
    {
    
        //  transform.rotation = hookPosition;
        screenCenterX = Screen.width / 2; // 获取屏幕中心 X 坐标
       
    }
   
    private void ReadGamepadInput()
    {
        var gamepad = Gamepad.current;
        if (gamepad == null)
        {
            moveInput = Vector2.zero;
            return;
        }

        // 直接读左摇杆，x 是左右，y 是上下
        moveInput = gamepad.leftStick.ReadValue();

        // 如果想确保在所有方向速度一致，可以归一化一下：
        if (moveInput.sqrMagnitude > 1f)
            moveInput.Normalize();
    }

    /// <summary>
    /// 根据 moveInput 驱动钩子移动
    /// </summary>
    private void MoveHook()
    {
        // 在二维平面内移动（Z 轴不动）
        Vector3 delta = new Vector3(moveInput.x, moveInput.y, 0f)
                        * moveSpeed * Time.deltaTime;
        transform.Translate(delta, Space.World);
    }

    void Update()
    {
        HookPosition();
        ReadGamepadInput();
        MoveHook();
    }
  
    private void HookPosition()
    {

        if(Fishfreehook)
        {
            GetComponentInParent<FishFree>().enabled = true;

        }


    }
}





