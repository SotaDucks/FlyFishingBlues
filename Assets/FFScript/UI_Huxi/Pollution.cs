using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pollution : MonoBehaviour
{
    public bool isPolluted;
    public GameObject pollution;
    // Start is called before the first frame update
    void Start()
    {
        if (isPolluted)
        {
            pollution.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
