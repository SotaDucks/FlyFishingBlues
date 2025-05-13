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
            // 尝试查找鱼对象
            GameObject trout = GameObject.Find("TroutWithJawfbx");

            // 如果没找到鱼，或者找到了但 FishBiteHook 为空/未咬钩，都算作“鱼还没咬”
            if (trout == null)
            {
                Fsm.Event(sendEvent);
                return;
            }

            FishBiteHook biteHook = trout.GetComponent<FishBiteHook>();
            if (biteHook == null || !biteHook.isFishBite)
            {
                Fsm.Event(sendEvent);
            }
        }
    }
}
#endif
