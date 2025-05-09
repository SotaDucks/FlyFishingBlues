using UnityEngine;
using HutongGames.PlayMaker;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("Custom")]
    [Tooltip("Sends the specified event when the Tab key is pressed.")]
    public class SendEventOnTabPress : FsmStateAction
    {
        [RequiredField]
        [Tooltip("Event to send when Tab is pressed.")]
        public FsmEvent sendEvent;

        public override void Reset()
        {
            sendEvent = null;
        }

        public override void OnUpdate()
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                Fsm.Event(sendEvent);
            }
        }
    }
}
