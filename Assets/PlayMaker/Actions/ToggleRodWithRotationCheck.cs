using UnityEngine;
using HutongGames.PlayMaker;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("Custom")]
    [Tooltip("When Tab is pressed AND the GameObject's local rotation on a chosen axis is below a threshold, send the specified event.")]
    public class ToggleRodWithRotationCheck : FsmStateAction
    {
        [RequiredField]
        [Tooltip("Event to send when both conditions are met.")]
        public FsmEvent sendEvent;

        [RequiredField]
        [Tooltip("The GameObject owning this FSM or another target.")]
        public FsmOwnerDefault gameObject;

        public enum RotationAxis { X, Y, Z }

        [Tooltip("Which local Euler axis to check (X, Y, or Z).")]
        public RotationAxis axis = RotationAxis.Y;

        [Tooltip("Threshold angle in degrees. The action fires when localEulerAngles.axis < threshold.")]
        public FsmFloat threshold;

        private GameObject _go;

        public override void Reset()
        {
            sendEvent = null;
            gameObject = new FsmOwnerDefault { OwnerOption = OwnerDefaultOption.UseOwner };
            axis = RotationAxis.Y;
            threshold = 45f;
        }

        public override void OnEnter()
        {
            _go = Fsm.GetOwnerDefaultTarget(gameObject);
        }

        public override void OnUpdate()
        {
            if (_go == null) return;

            // 1. 检测 Tab 按下
            if (!Input.GetKeyDown(KeyCode.Tab))
                return;

            // 2. 读取 localEulerAngles 并选取指定轴
            Vector3 la = _go.transform.localEulerAngles;
            float val = axis == RotationAxis.X ? la.x
                      : axis == RotationAxis.Y ? la.y
                      : la.z;

            // 处理 360→-180 到 180 范围
            if (val > 180f) val -= 360f;

            // 3. 检查阈值（小于判断）
            if (val < threshold.Value)
            {
                Fsm.Event(sendEvent);
            }
        }
    }
}