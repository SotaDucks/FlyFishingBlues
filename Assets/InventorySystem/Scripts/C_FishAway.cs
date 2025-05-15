using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_FishAway : MonoBehaviour
{
    public GameObject objectToEnable;

    // 当有物体进入trigger时调用
    private void OnTriggerEnter(Collider other)
    {
        // 判断tag是否为"fish"
        if (other.CompareTag("fish"))
        {
            if (objectToEnable != null)
            {
                objectToEnable.SetActive(true);
            }
        }
    }
}
