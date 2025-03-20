using System.Collections;
using System.Collections.Generic;
using DialogueEditor;
using UnityEngine;
using VHACD.Unity;

public class GameController : MonoBehaviour
{
    public GameObject CameraOBJ;
    //public Vector3 FshiPosition;
    public NPCConversation Conversation;
    public Rigidbody fishRigidbody; // 需要停止移动的物体
    public Vector3 fixedRotation; // 要保持的固定旋转角度（用欧拉角表示）
    public GameObject Hand;
    public GameObject Hook;
   public GameObject Fish;
    public GameObject FishStruggling; // 需要禁用脚本的物体
    private bool fishmoving = true;
    public bool EnableStruggleWhileUnhook;
    private Rigidbody rb;
    private Rigidbody Hookrb;
    private ComplexCollider Hookcollider;
    private void Start()
    {
        Hookcollider = Hook.GetComponent<ComplexCollider>();
       ConversationManager.Instance.StartConversation(Conversation);
        rb = Fish.GetComponent<Rigidbody>();
        Hookrb=Hook.GetComponent<Rigidbody>();
        if (EnableStruggleWhileUnhook)
        {
            StartCoroutine(AddForceEveryThreeSeconds());
        }// 开始协程
        
    }
    private void Update()
    {
        if (fishmoving) 
        {
            if (Input.GetKey(KeyCode.F))
            {
                fishmoving = false;
                Debug.Log("Btn F");
                CameraOBJ.SetActive(true);
                StopObjectAndSetRotation();
                SetObjectVisibility();
                DisableFishStrugglingScript();
                EnableUnhookScript();
               Invoke(nameof(EnableKinematic), 1f);
            }
        }
    }
  private  void EnableKinematic()
    {
        Hookrb.isKinematic = false;
        Hookcollider.enabled = true;
    }
    public void SetObjectVisibility()
    {
        // 让第一个物体可见
        if (Hook != null)
        {
            Hook.SetActive(true);
        }

        // 让第二个物体不可见
        if (Hand != null)
        {
            Hand.SetActive(true);
        }
    }
    void DisableFishStrugglingScript()
    {
        FishStruggling.GetComponent<NewStrug>().enabled = false;
       FishStruggling.GetComponent<FishTransform>().enabled = true;
    }
    private void StopObjectAndSetRotation()
    {
        if (fishRigidbody != null)
        {
            // 停止移动
            fishRigidbody.velocity = Vector3.zero;
            fishRigidbody.angularVelocity = Vector3.zero;
             // 停止所有物理交互
          //  Fish.transform.position= FshiPosition;
            // 保持固定的旋转角度
            Fish.transform.rotation = Quaternion.Euler(fixedRotation);
        }
    }
    public void EnableUnhookScript()
    {
        // 获取当前GameObject的Unhook组件
        Unhook unhookScript = Hook.GetComponent<Unhook>();

        // 检查Unhook组件是否存在
        if (unhookScript != null)
        {
            // 启用Unhook脚本
            unhookScript.enabled = true;
            Debug.Log("Unhook脚本已启用");
        }
        else
        {
            Debug.LogError("未找到Unhook脚本");
        }
    }
    private IEnumerator AddForceEveryThreeSeconds()
    {
        while (true) // 无限循环
        {
            // 每隔三秒添加一个向上的力
            rb.AddForce(Vector3.up * 20f, ForceMode.Impulse);
            yield return new WaitForSeconds(0.2f); // 等待3秒
        }
    }
    public void EnableFish()
    { 
    Fish.SetActive(true);
      
    }
        
    
}
