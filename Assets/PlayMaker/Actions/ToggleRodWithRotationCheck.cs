using UnityEngine;
using HutongGames.PlayMaker;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("Custom")]
    [Tooltip("When the specified InputAction is triggered AND the GameObject's local rotation on a chosen axis is below a threshold, send the specified event or store the result in a bool.")]
    public class ToggleRodWithRotationCheck : FsmStateAction
    {
        [Tooltip("Optional: Event to send when conditions are met.")]
        [UIHint(UIHint.FsmEvent)]
        public FsmEvent sendEvent;

        [RequiredField]
        [Tooltip("The GameObject owning this FSM or another target.")]
        public FsmOwnerDefault gameObject;

    #if ENABLE_INPUT_SYSTEM
        [Tooltip("Input Action to listen for (e.g. CastBack).")]
        public InputActionReference inputAction;
    #endif

        [Tooltip("Optional Bool variable to store whether the action was triggered.")]
        [UIHint(UIHint.Variable)]
        public FsmBool storeResult;

        public enum RotationAxis { X, Y, Z }

        [Tooltip("Which local Euler axis to check (X, Y, or Z).")]
        public RotationAxis axis = RotationAxis.Y;

        [Tooltip("Threshold angle in degrees. Fires when localEulerAngles.axis < threshold.")]
        public FsmFloat threshold;

        private GameObject _go;
    #if ENABLE_INPUT_SYSTEM
        private InputAction _action;
    #endif

        public override void Reset()
        {
            sendEvent = null;
            gameObject = new FsmOwnerDefault { OwnerOption = OwnerDefaultOption.UseOwner };
        #if ENABLE_INPUT_SYSTEM
            inputAction = null;
        #endif
            storeResult = null;
            axis = RotationAxis.Y;
            threshold = 45f;
        }

        public override void OnEnter()
        {
            _go = Fsm.GetOwnerDefaultTarget(gameObject);
        #if ENABLE_INPUT_SYSTEM
            if (inputAction != null && inputAction.action != null)
            {
                _action = inputAction.action;
                _action.Enable();
            }
        #endif
            if (!storeResult.IsNone)
                storeResult.Value = false;
        }

        public override void OnExit()
        {
        #if ENABLE_INPUT_SYSTEM
            if (_action != null)
                _action.Disable();
        #endif
        }

        public override void OnUpdate()
        {
            if (_go == null)
                return;

            // 1. 检查 local rotation 是否低于阈值
            Vector3 la = _go.transform.localEulerAngles;
            float val = axis == RotationAxis.X ? la.x
                      : axis == RotationAxis.Y ? la.y
                      : la.z;
            if (val > 180f) val -= 360f;
            if (val >= threshold.Value)
                return;

            // 2. 检测输入触发
            bool triggered = false;
        #if ENABLE_INPUT_SYSTEM
            if (_action != null)
            {
                if (_action.triggered)
                    triggered = true;
            }
            else
        #endif
            {
                if (Input.GetKeyDown(KeyCode.Tab))
                    triggered = true;
            }

            if (!triggered)
                return;

            // 3. 存储结果
            if (!storeResult.IsNone)
                storeResult.Value = true;

                        // 4. 发送事件（如果设置了）
            if (sendEvent != null && !string.IsNullOrEmpty(sendEvent.Name))
            {
                Fsm.Event(sendEvent);
            }
        }
    }
}