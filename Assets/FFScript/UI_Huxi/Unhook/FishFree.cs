using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class FishFree : MonoBehaviour
{
    public GameObject StuggleBone;
    public Slider timeSlider;
    public GameObject Hook;
    public GameObject Hand;
    public Camera MainCamera;
 
    // Start is called before the first frame update
    void Start()
    {
    }
    private void OnEnable()
    {
        StuggleBone.GetComponent<NewStrug>().enabled = false;
     StuggleBone.GetComponent<FishPath>().enabled=true;
        StuggleBone.GetComponent<FishTransform>().enabled = false;
        timeSlider.enabled = false;
        Hook.gameObject.SetActive(false);
        Hand.gameObject.SetActive(false);
        this.GetComponent<Rigidbody>().isKinematic = false;
        this.GetComponent<Rigidbody>().drag = 10;
        MainCamera.transform.DORotate(
              new Vector3(0f, -40f, 0f), // 相对旋转 -40 度（逆时针）
              3f,
              RotateMode.Fast
          ).SetEase(Ease.OutQuad).SetRelative();

        DOTween.To(
           () => MainCamera.fieldOfView,      // 获取当前值
           x => MainCamera.fieldOfView = x,   // 更新值
           60,                    // 目标值
           3)
           .SetEase(Ease.OutQuad);
    }
    // Update is called once per frame
    private void Struggle()
    {
       
      
       
      
    }
}
