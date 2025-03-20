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
    public Rigidbody fishRigidbody; // ��Ҫֹͣ�ƶ�������
    public Vector3 fixedRotation; // Ҫ���ֵĹ̶���ת�Ƕȣ���ŷ���Ǳ�ʾ��
    public GameObject Hand;
    public GameObject Hook;
   public GameObject Fish;
    public GameObject FishStruggling; // ��Ҫ���ýű�������
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
        }// ��ʼЭ��
        
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
        // �õ�һ������ɼ�
        if (Hook != null)
        {
            Hook.SetActive(true);
        }

        // �õڶ������岻�ɼ�
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
            // ֹͣ�ƶ�
            fishRigidbody.velocity = Vector3.zero;
            fishRigidbody.angularVelocity = Vector3.zero;
             // ֹͣ����������
          //  Fish.transform.position= FshiPosition;
            // ���̶ֹ�����ת�Ƕ�
            Fish.transform.rotation = Quaternion.Euler(fixedRotation);
        }
    }
    public void EnableUnhookScript()
    {
        // ��ȡ��ǰGameObject��Unhook���
        Unhook unhookScript = Hook.GetComponent<Unhook>();

        // ���Unhook����Ƿ����
        if (unhookScript != null)
        {
            // ����Unhook�ű�
            unhookScript.enabled = true;
            Debug.Log("Unhook�ű�������");
        }
        else
        {
            Debug.LogError("δ�ҵ�Unhook�ű�");
        }
    }
    private IEnumerator AddForceEveryThreeSeconds()
    {
        while (true) // ����ѭ��
        {
            // ÿ���������һ�����ϵ���
            rb.AddForce(Vector3.up * 20f, ForceMode.Impulse);
            yield return new WaitForSeconds(0.2f); // �ȴ�3��
        }
    }
    public void EnableFish()
    { 
    Fish.SetActive(true);
      
    }
        
    
}
