using UnityEngine;
using UnityEngine.UI;

public class PushFishOnClick : MonoBehaviour
{
    public Button pushButton; // ��Ҫ���õİ�ť
    public GameObject fish;   // ��Ҫ�ƶ���Fish����
    public float pushForce = 10f; // �����Ĵ�С
    public Camera mainCamera; // ��Ҫ����FOV�������
    public float targetFOV = 70f; // Ŀ��FOVֵ
    public float fovSmoothSpeed = 2f; // FOV������ƽ���ٶ�

    private Rigidbody fishRigidbody;

    void Start()
    {
        
        if (fish != null)
        {
            fishRigidbody = fish.GetComponent<Rigidbody>();
        }

        if (pushButton != null)
        {
            pushButton.onClick.AddListener(OnButtonClick);
        }
    }

    void OnButtonClick()
    {
        ApplyPushForce();
        StartCoroutine(SmoothAdjustFOV());
    }

    void ApplyPushForce()
    {
        if (fishRigidbody != null)
        {
            Vector3 pushDirection = fish.transform.forward;
            fishRigidbody.AddForce(pushDirection * pushForce, ForceMode.Impulse);
        }
    }

    System.Collections.IEnumerator SmoothAdjustFOV()
    {
        while (Mathf.Abs(mainCamera.fieldOfView - targetFOV) > 0.1f)
        {
            mainCamera.fieldOfView = Mathf.Lerp(mainCamera.fieldOfView, targetFOV, fovSmoothSpeed * Time.deltaTime);
            yield return null;
        }

        mainCamera.fieldOfView = targetFOV;
    }
}
