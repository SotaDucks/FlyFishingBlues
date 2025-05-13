using System.Collections.Generic;
using UnityEngine;
using Opsive.UltimateInventorySystem.Core;      // ItemDefinition

using Opsive.UltimateInventorySystem.Core.InventoryCollections; // Inventory

public class GlobalPickupKey : MonoBehaviour
{
    public static GlobalPickupKey Instance { get; private set; }

    [Tooltip("玩家身上的 Opsive Inventory 组件")]
    [SerializeField] private Inventory m_PlayerInventory;

    [Tooltip("按事件索引要添加到背包的物品列表")]
    [SerializeField] private List<ItemEntry> m_ItemsToAdd = new List<ItemEntry>();

    [System.Serializable]
    public struct ItemEntry
    {
        public ItemDefinition itemDefinition;
        public int amount;
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        if (m_PlayerInventory == null)
        {
            m_PlayerInventory = FindObjectOfType<Inventory>();
        }
        if (m_PlayerInventory == null)
        {
            Debug.LogError("GlobalPickupKey: 找不到 Inventory 组件！");
        }
    }

    /// <summary>
    /// 向背包添加 m_ItemsToAdd 中指定索引的物品
    /// </summary>
    public void AddItemByIndex(int index)
    {
        if (m_PlayerInventory == null) return;
        if (index < 0 || index >= m_ItemsToAdd.Count)
        {
            Debug.LogWarning($"GlobalPickupKey: 索引 {index} 越界");
            return;
        }
        var entry = m_ItemsToAdd[index];
        if (entry.itemDefinition == null)
        {
            Debug.LogWarning($"GlobalPickupKey: 索引 {index} 的 ItemDefinition 为空");
            return;
        }
        m_PlayerInventory.AddItem(entry.itemDefinition, entry.amount);
        Debug.Log($"已添加到背包：{entry.itemDefinition.name} x{entry.amount}");
    }
}
