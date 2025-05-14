using UnityEngine;
using Opsive.UltimateInventorySystem.Core;
using Opsive.UltimateInventorySystem.Core.InventoryCollections;

public class AddItemByIndex : MonoBehaviour
{
    [Tooltip("要添加物品的背包组件")]
    public Inventory inventory;

    [Tooltip("可选的物品定义列表，按索引对应添加")]
    public ItemDefinition[] itemDefinitions;

    [Tooltip("在启用时自动添加的默认索引，设为负数可禁用默认行为")]
    public int defaultIndex = -1;

    private void OnEnable()
    {
        // 若设置了默认索引，则在启用时自动添加对应物品
        if (defaultIndex >= 0)
        {
            AddElement(defaultIndex);
        }
    }

    /// <summary>
    /// 根据传入的索引，从 itemDefinitions 中取出对应物品并添加到背包。
    /// </summary>
    /// <param name="index">数组索引，0 表示第一个元素，以此类推</param>
    public void AddElement(int index)
    {
        // 检查索引范围
        if (itemDefinitions == null || index < 0 || index >= itemDefinitions.Length)
        {
            Debug.LogWarning($"[AddItemByIndex] 无效的索引：{index}");
            return;
        }

        // 添加一个该索引对应的物品
        inventory.AddItem(itemDefinitions[index], 1);
    }
}
