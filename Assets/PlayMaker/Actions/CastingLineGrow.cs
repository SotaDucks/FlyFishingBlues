using HutongGames.PlayMaker;
using UnityEngine;
using Obi;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("Obi")]
    [HutongGames.PlayMaker.Tooltip("Extend an Obi rope by a fixed amount once when the state is entered, after an optional delay.")]
    public class CastingLineGrow : FsmStateAction
    {
        [RequiredField]
        [HutongGames.PlayMaker.Tooltip("GameObject that owns ObiRope & ObiRopeCursor components (usually your fly line).")]
        public FsmOwnerDefault ropeObject;

        [HutongGames.PlayMaker.Tooltip("How many metres to add to the rope each time this state is entered.")]
        public FsmFloat growAmount = 1f;

        [HutongGames.PlayMaker.Tooltip("Extension speed in metres per second.")]
        public FsmFloat growSpeed = 4f;

        [HutongGames.PlayMaker.Tooltip("Delay in seconds before beginning the rope extension. Can be fractional.")]
        public FsmFloat delay = 0f;

        // Internal fields
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

            // Compute goal length
            targetLength = rope.restLength + Mathf.Max(0f, growAmount.Value);

            // Initialize delay timer
            delayTimer = Mathf.Max(0f, delay.Value);
            waiting = delayTimer > 0f;
        }

        public override void OnUpdate()
        {
            // Handle waiting period
            if (waiting)
            {
                delayTimer -= Time.deltaTime;
                if (delayTimer > 0f)
                    return;
                waiting = false;
            }

            // Perform extension
            if (rope.restLength < targetLength)
            {
                float delta = Mathf.Min(growSpeed.Value * Time.deltaTime, targetLength - rope.restLength);
                cursor.ChangeLength(delta);
            }
            else
            {
                Finish(); // done
            }
        }
    }
}