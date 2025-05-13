#if ENABLE_INPUT_SYSTEM
using UnityEngine;
using UnityEngine.InputSystem;
using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("PlayerInput")]
    [Tooltip("Sends an Event when an InputAction in a PlayerInput component is Performed, but only if no fish is biting.")]
    public class ConditionalResetLineInputEvent : PlayerInputActionBase
    {
        [Tooltip("The event to send when the input is performed and fish has not bitten.")]
        public FsmEvent sendEvent;

        public override void Reset()
        {
            base.Reset();
            sendEvent = null;
        }

        protected override void OnPerformed(InputAction.CallbackContext ctx)
        {
            // 查找场景中的鱼对象（名称需与场景中一致）
            GameObject trout = GameObject.Find("TroutWithJawfbx");
            if (trout == null)
                return;

            // 获取 FishBiteHook 组件并检查是否尚未咬钩
            FishBiteHook biteHook = trout.GetComponent<FishBiteHook>();
            if (biteHook != null && !biteHook.isFishBite)
            {
                Fsm.Event(sendEvent);
            }
        }
    }
}
#endif