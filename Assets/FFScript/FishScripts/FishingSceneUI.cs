using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingSceneUI : MonoBehaviour
{
    public GameObject LeftRight;
    public GameObject SpaceBar;
    public FishBiteHook fishBiteHook;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       if (fishBiteHook.isFishBite)
        {
            LeftRight.SetActive(false);
            SpaceBar.SetActive(false);
        }
        else
        {
            if (Input.GetKey(KeyCode.Space))
            {
                LeftRight.SetActive(true);
                SpaceBar.SetActive(false);
            }
            else {
                SpaceBar.SetActive(true);
                LeftRight.SetActive(false);
            }
        }
    }
}
