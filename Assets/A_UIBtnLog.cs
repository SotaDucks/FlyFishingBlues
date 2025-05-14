using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class A_UIBtnLog : MonoBehaviour
{
    void Start()
    {
        // 查找场景中所有激活的 Button 组件
        Button[] buttons = Object.FindObjectsOfType<Button>();

        foreach (var btn in buttons)
        {
            // 缓存按钮名称
            string btnName = btn.gameObject.name;

            // 为每个按钮的 onClick 事件添加监听
            btn.onClick.AddListener(() =>
            {
                // 调用全局数据记录器
                A_GlobalDatalogger.Instance.LogSelection("ButtonClick", btnName);
            });
        }

        // 如果项目中按钮较多，也可以包含非激活状态的按钮：
        // Button[] allButtons = Object.FindObjectsOfType<Button>(true);
        // :contentReference[oaicite:5]{index=5}
    }
}
