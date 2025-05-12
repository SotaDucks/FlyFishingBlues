using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BinfdPickupButton : MonoBehaviour
{
    [SerializeField] private Button pickupButton;
    private GlobalPickupKey _pickupKey;

    private void Start()
    {
        // 可以在 Start 里做一次绑定，或留空由外部调用 TTT()
        Bind();
    }

    /// <summary>
    /// 查找全局的 GlobalPickupKey 并绑定按钮点击事件
    /// </summary>
    public void Bind()
    {
        // 找到场景中唯一的 GlobalPickupKey 实例
        _pickupKey = FindObjectOfType<GlobalPickupKey>();
        if (_pickupKey == null)
        {
            Debug.LogError("BindPickupButton: 场景中找不到 GlobalPickupKey 组件！");
            return;
        }
        if (pickupButton == null)
        {
            Debug.LogError("BindPickupButton: Inspector 里没有指定 pickupButton！");
            return;
        }
        // 清除旧的监听，确保重复调用也不会累加
        pickupButton.onClick.RemoveAllListeners();
        // 绑定点击后执行一次 AddItemByIndex(0)
        pickupButton.onClick.AddListener(() => _pickupKey.AddItemByIndex(0));
    }

    /// <summary>
    /// 外部可以调用这个方法来手动触发一次绑定与添加操作
    /// </summary>
    public void TTT()
    {
        // 如果还没绑定，就先 Bind
        if (_pickupKey == null || pickupButton == null)
        {
            Bind();
        }
        // 触发一次点击逻辑（直接调用，不需用户点按钮）
        _pickupKey.AddItemByIndex(0);
    }
}
