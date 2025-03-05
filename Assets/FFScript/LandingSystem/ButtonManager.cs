using UnityEngine;
using UnityEngine.UI; // 用于操作 Button 组件
using DialogueEditor; // 如果使用 Dialogue Editor，需要此命名空间

public class ButtonManager : MonoBehaviour
{
    // 在 Inspector 中拖入您的两个按钮 GameObject
    public GameObject button1;
    public GameObject button2;


    void Start()
    {
        // 确保按钮在游戏开始时是隐藏的
        button1.SetActive(false);
        button2.SetActive(false);

        // 订阅对话结束事件（适用于 Dialogue Editor）
        ConversationManager.OnConversationEnded += ShowButtons;
    }

    // 显示按钮的方法
    void ShowButtons()
    {
            button1.SetActive(true);
            button2.SetActive(true);
    }

    // 取消订阅事件，避免内存泄漏
    void OnDestroy()
    {
        ConversationManager.OnConversationEnded -= ShowButtons;
    }
}