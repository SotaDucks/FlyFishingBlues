using System.Collections;
using System.Collections.Generic;
using Opsive.UltimateInventorySystem.UI.Panels;
using UnityEngine;

public class I_MechantTriggerUI : MonoBehaviour
{
    public GameObject promptBtn;        // Btn_OpenShop
    public DisplayPanel shopPanel;      // Shop DisplayPanel

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) promptBtn.SetActive(true);
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) promptBtn.SetActive(false);
    }

    // 让按钮 OnClick 调用这个
    public void OpenShop()
    {
        shopPanel.SmartOpen();
    }
}
