using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace StarterAssets
{
    /// <summary>
    /// 侧视版输入脚本——
    /// 把左右 1D 输入写入 move.x，保留 move.y=0，这样现有的控制器无需改动。
    /// </summary>
    public class StarterAssetsInputsSideView : MonoBehaviour
    {
        [Header("Character Input Values")]
        [Tooltip("左右移动输入：-1=左, +1=右")]
        public Vector2 move;
        [Tooltip("视角输入")]
        public Vector2 look;
        [Tooltip("跳跃")]
        public bool jump;
        [Tooltip("冲刺")]
        public bool sprint;

        [Header("Movement Settings")]
        public bool analogMovement;

        [Header("Mouse Cursor Settings")]
        public bool cursorLocked = true;
        public bool cursorInputForLook = true;

    #if ENABLE_INPUT_SYSTEM
        // 1D 左右移动
        public void OnMoveSideView(InputValue value)
        {
            float v = value.Get<float>();
            move = new Vector2(v, 0f);
        }

        // 鼠标/右杆看
        public void OnLook(InputValue value)
        {
            if (cursorInputForLook)
                look = value.Get<Vector2>();
        }

        public void OnJump(InputValue value)
            => jump = value.isPressed;

        public void OnSprint(InputValue value)
            => sprint = value.isPressed;
    #endif

        private void OnApplicationFocus(bool hasFocus)
        {
            Cursor.lockState = cursorLocked 
                ? CursorLockMode.Locked 
                : CursorLockMode.None;
        }
    }
}
