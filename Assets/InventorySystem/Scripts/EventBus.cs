using System;

public static class GameEvents
{
    /// <summary>
    /// 请求拾取物品事件，int 参数是要添加的列表索引
    /// </summary>
    public static event Action<int> OnItemPickupRequested;

    /// <summary>
    /// 外部调用该方法来触发一次物品拾取请求
    /// </summary>
    public static void RequestItemPickup(int index)
    {
        OnItemPickupRequested?.Invoke(index);
    }
}
