using HutongGames.PlayMaker;
using UnityEngine;
using Obi;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("Obi")]
    [HutongGames.PlayMaker.Tooltip("Extend or shorten an Obi rope by a fixed amount once when the state is entered, after an optional delay. Enforces min and max length.")]
    public class CastingLineGrow : FsmStateAction
    {
        [RequiredField]
        [HutongGames.PlayMaker.Tooltip("GameObject that owns ObiRope & ObiRopeCursor components (usually your fly line).")]
        public FsmOwnerDefault ropeObject;

        [HutongGames.PlayMaker.Tooltip("How many metres to add (positive) or remove (negative) each time this state is entered.")]
        public FsmFloat growAmount = 1f;

        [HutongGames.PlayMaker.Tooltip("Extension/retraction speed in metres per second.")]
        public FsmFloat growSpeed = 4f;

        [HutongGames.PlayMaker.Tooltip("Delay in seconds before beginning the rope length change. Can be fractional.")]
        public FsmFloat delay = 0f;

        [HutongGames.PlayMaker.Tooltip("Minimum total rope length allowed in metres.")]
        public FsmFloat minLength = 0f;

        [HutongGames.PlayMaker.Tooltip("Optional: maximum total rope length allowed. Negative values will be clamped to zero.")]
        public FsmFloat maxLength = float.PositiveInfinity;

        private ObiRopeCursor cursor;
        private ObiRope rope;
        private float targetLength;
        private float delayTimer;
        private bool waiting;

        public override void Reset()
        {
            ropeObject = null;
            growAmount = 1f;
            growSpeed  = 4f;
            delay      = 0f;
            minLength  = 0f;
            maxLength  = float.PositiveInfinity;
        }

        public override void OnEnter()
        {
            GameObject go = Fsm.GetOwnerDefaultTarget(ropeObject);
            if (go == null)
            {
                Debug.LogError("[CastingLineGrow] Rope GameObject reference is null.");
                Finish();
                return;
            }

            cursor = go.GetComponent<ObiRopeCursor>();
            rope   = go.GetComponent<ObiRope>();
            if (cursor == null || rope == null)
            {
                Debug.LogError("[CastingLineGrow] ObiRopeCursor or ObiRope component missing on target object.");
                Finish();
                return;
            }

            // Compute desired total length: allow negative grow amount to shorten
            float desired = rope.restLength + growAmount.Value;
            // Clamp to [minLength, maxLength]
            desired = Mathf.Clamp(desired, minLength.Value, maxLength.Value);
            targetLength = desired;

            // Initialize delay
            delayTimer = Mathf.Max(0f, delay.Value);
            waiting = delayTimer > 0f;
        }

        public override void OnUpdate()
        {
            // Handle delay
            if (waiting)
            {
                delayTimer -= Time.deltaTime;
                if (delayTimer > 0f)
                    return;
                waiting = false;
            }

            // Perform length change towards target
            float current = rope.restLength;
            if (!Mathf.Approximately(current, targetLength))
            {
                float diff  = targetLength - current;
                float delta = Mathf.Sign(diff) * growSpeed.Value * Time.deltaTime;
                // Do not overshoot
                if (Mathf.Abs(delta) > Mathf.Abs(diff))
                    delta = diff;

                cursor.ChangeLength(delta);
            }
            else
            {
                Finish();
            }
        }
    }
}
