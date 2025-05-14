using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuutingFishSimulate : MonoBehaviour
{
    public Material selectedMaterial; // 选中时的材质
    private Material normalMaterial;  // 原始材质
    private Renderer rend;            // 渲染器组件

    void Start()
    {
        rend = GetComponent<Renderer>();
        if (rend != null)
        {
            normalMaterial = rend.material; // 存储原始材质
        }
    }

    public void Select()
    {
        if (rend != null)
        {
            rend.material = selectedMaterial; // 应用选中材质
        }
        // 可以添加其他选中逻辑，例如播放音效
    }

    public void Deselect()
    {
        if (rend != null)
        {
            rend.material = normalMaterial; // 恢复原始材质
        }
        // 可以添加其他取消选中逻辑
    }
}
