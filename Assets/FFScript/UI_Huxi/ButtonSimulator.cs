using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSimulator : MonoBehaviour
{
    public Button buttonToSimulate; // �� Inspector �з��䰴ť

    void Start()
    {
        Invoke("BtnSimulator", 0.2f);
        
    }
    private void BtnSimulator()
    {

        buttonToSimulate.onClick.Invoke();
    }
}
