using UnityEngine;

public class TestCastBack : MonoBehaviour
{
    [Header("Animator 组件（填两个，如果只需一个就留空另一个）")]
    public Animator animatorA;
    public Animator animatorB;

    [Header("要触发的 Trigger 名称")]
    public string triggerName1 = "TestCastBack";
    public string triggerName2 = "TestCastForward";

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            // 向两个 Animator 同时发送 trigger（判空防止未赋值报错）
            if (animatorA) animatorA.SetTrigger(triggerName1);
            if (animatorB) animatorB.SetTrigger(triggerName1);
        }
    
    if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            // 向两个 Animator 同时发送 trigger（判空防止未赋值报错）
            if (animatorA) animatorA.SetTrigger(triggerName2);
            if (animatorB) animatorB.SetTrigger(triggerName2);
        }
    
    
    
    }
}
