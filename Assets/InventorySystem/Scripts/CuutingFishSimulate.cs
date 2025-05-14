using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CuutingFishSimulate : MonoBehaviour
{
    [Tooltip("场景中要被 LB “按住/松开” 的按钮对象")]
    public GameObject leftBtnObj;
    [Tooltip("场景中要被 RB “按住/松开” 的按钮对象")]
    public GameObject rightBtnObj;

    void Update()
    {
        // LB 按住
        if (Input.GetKeyDown(KeyCode.JoystickButton4))
            ExecuteEvents.Execute(leftBtnObj,
                new PointerEventData(EventSystem.current),
                ExecuteEvents.pointerDownHandler);

        // LB 松开
        if (Input.GetKeyUp(KeyCode.JoystickButton4))
            ExecuteEvents.Execute(leftBtnObj,
                new PointerEventData(EventSystem.current),
                ExecuteEvents.pointerUpHandler);

        // RB 按住
        if (Input.GetKeyDown(KeyCode.JoystickButton5))
            ExecuteEvents.Execute(rightBtnObj,
                new PointerEventData(EventSystem.current),
                ExecuteEvents.pointerDownHandler);

        // RB 松开
        if (Input.GetKeyUp(KeyCode.JoystickButton5))
            ExecuteEvents.Execute(rightBtnObj,
                new PointerEventData(EventSystem.current),
                ExecuteEvents.pointerUpHandler);

        // A 键（JoystickButton0）→ 确认（一次性点击 LeftBtnObj 举例）
        if (Input.GetKeyDown(KeyCode.JoystickButton0))
            ExecuteEvents.Execute(leftBtnObj,
                new PointerEventData(EventSystem.current),
                ExecuteEvents.pointerClickHandler);
    }
}
