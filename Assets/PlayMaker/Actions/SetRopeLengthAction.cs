using UnityEngine;
using Obi;
using HutongGames.PlayMaker;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("Obi")]
    [Tooltip("Reset the Obi rope to a specified length (absolute) when this state is entered.")]
    public class SetRopeLengthAction : FsmStateAction
    {
        [RequiredField]
        [Tooltip("The GameObject containing ObiRope and ObiRopeCursor components.")]
        public FsmOwnerDefault ropeObject;

        [RequiredField]
        [Tooltip("The desired absolute rope rest length in meters.")]
        public FsmFloat ropeLength;

        private ObiRopeCursor cursor;
        private ObiRope rope;

        public override void Reset()
        {
            ropeObject = null;
            ropeLength = 5f;
        }

        public override void OnEnter()
        {
            // Fetch target GameObject
            GameObject go = Fsm.GetOwnerDefaultTarget(ropeObject);
            if (go == null)
            {
                Debug.LogError("[SetRopeLengthAction] No GameObject specified for ropeObject.");
                Finish();
                return;
            }

            // Get Obi components
            cursor = go.GetComponent<ObiRopeCursor>();
            rope   = go.GetComponent<ObiRope>();
            if (cursor == null || rope == null)
            {
                Debug.LogError("[SetRopeLengthAction] ObiRopeCursor or ObiRope missing on target.");
                Finish();
                return;
            }

            // Compute delta to reach absolute length
            float current = rope.restLength;
            float desired = Mathf.Max(0f, ropeLength.Value);
            float delta = desired - current;

            // Apply length change
            cursor.ChangeLength(delta);

            Finish();
        }
    }
}
