using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhenBtnPressed2 : MonoBehaviour
{
    [Header("A 键对应的状态")]
    public GameObject A1; // 正常状态显示的对象，带有闪烁脚本
    public GameObject A2; // 按下状态显示的对象

    [Header("W 键对应的状态")]
    public GameObject W1; // 正常状态显示的对象，带有闪烁脚本
    public GameObject W2; // 按下状态显示的对象

    [Header("鱼的状态")]
    [Tooltip("如果鱼累了设置为 true，否则为 false。")]
    public static bool isFishTired = false;

    // 获取 A1 和 W1 上的闪烁脚本组件（假设类名为 BlinkingScript）
    private BlinkingImageButton blinkingA;
    private BlinkingImageButton blinkingW;

    void Start()
    {
        // 初始时显示正常状态，隐藏按下状态
        if (A1 != null) A1.SetActive(true);
        if (A2 != null) A2.SetActive(false);
        if (W1 != null) W1.SetActive(true);
        if (W2 != null) W2.SetActive(false);

        // 尝试获取闪烁脚本组件
        if (A1 != null)
            blinkingA = A1.GetComponent<BlinkingImageButton>();
        if (W1 != null)
            blinkingW = W1.GetComponent<BlinkingImageButton>();
    }

    void Update()
    {
        // 检测 A 键
        if (Input.GetKey(KeyCode.A))
        {
            // 按下 A 键：显示 A2，隐藏 A1
            if (A1 != null) A1.SetActive(false);
            if (A2 != null) A2.SetActive(true);
        }
        else
        {
            // A 键未按下：显示 A1，隐藏 A2
            if (A1 != null) A1.SetActive(true);
            if (A2 != null) A2.SetActive(false);

            // 交换条件：当鱼累了，A 开启闪烁；否则关闭闪烁（显示静态）
            if (blinkingA != null)
            {
                blinkingA.enabled = isFishTired;
            }
        }

        // 检测 W 键
        if (Input.GetKey(KeyCode.W))
        {
            // 按下 W 键：显示 W2，隐藏 W1
            if (W1 != null) W1.SetActive(false);
            if (W2 != null) W2.SetActive(true);
        }
        else
        {
            // W 键未按下：显示 W1，隐藏 W2
            if (W1 != null) W1.SetActive(true);
            if (W2 != null) W2.SetActive(false);

            // 交换条件：当鱼不累，W 开启闪烁；否则关闭闪烁（显示静态）
            if (blinkingW != null)
            {
                blinkingW.enabled = !isFishTired;
            }
        }
    }
}
