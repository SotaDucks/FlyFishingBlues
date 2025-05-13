// (c) Copyright HutongGames, LLC 2021. All rights reserved.

#if ENABLE_INPUT_SYSTEM
using UnityEngine;
using UnityEngine.InputSystem;
using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("PlayerInput")]
    [Tooltip("Sends an Event when an InputAction in a PlayerInput component is Performed, but only if no fish is biting.")]
    public class CastBackNoFishBiteInputEvent : PlayerInputActionBase
    {
        [Tooltip("The event to send on Input Performed")]
        public FsmEvent sendEvent;

        public override void Reset()
        {
            base.Reset();
            sendEvent = null;
        }

        protected override void OnPerformed(InputAction.CallbackContext ctx)
        {
            // Only fire when the fish has not bitten the hook
            GameObject trout = GameObject.Find("TroutWithJawfbx");
            if (trout != null)
            {
                FishBiteHook biteHook = trout.GetComponent<FishBiteHook>();
                if (biteHook != null && !biteHook.isFishBite)
                {
                    Fsm.Event(sendEvent);
                }
            }
        }
    }
}
#endif
