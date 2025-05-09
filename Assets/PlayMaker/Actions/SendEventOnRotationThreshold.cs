using UnityEngine;
using HutongGames.PlayMaker;

namespace HutongGames.PlayMaker.Actions
{
    public enum RotationAxis { X, Y, Z }

    [ActionCategory("Custom")]
    [Tooltip("Sends the specified event when the GameObject's world rotation on a chosen axis exceeds a threshold.")]
    public class SendEventOnRotationThreshold : FsmStateAction
    {
        [RequiredField]
        [Tooltip("Event to send when the rotation exceeds the threshold.")]
        public FsmEvent sendEvent;

        [RequiredField]
        [Tooltip("The GameObject that owns this FSM, or another target object.")]
        public FsmOwnerDefault gameObject;

        [Tooltip("Which world Euler axis to check (X, Y, or Z).")]
        public RotationAxis axis = RotationAxis.Y;

        [Tooltip("Threshold angle in degrees. The action fires when transform.eulerAngles.axis > threshold.")]
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

            // Read world Euler angles
            Vector3 euler = _go.transform.eulerAngles;
            float angle = axis == RotationAxis.X ? euler.x
                       : axis == RotationAxis.Y ? euler.y
                       : euler.z;

            // Convert 0-360 to -180 to 180 range
            if (angle > 180f) angle -= 360f;

            // Check threshold
            if (angle > threshold.Value)
            {
                Fsm.Event(sendEvent);
            }
        }
    }
}
