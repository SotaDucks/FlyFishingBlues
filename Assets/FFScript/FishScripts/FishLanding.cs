using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement; // 使用 SceneManager 进行场景切换

public class FishLanding : MonoBehaviour
{
    [Header("Stamina Bar 设置")]
    public float activationDelay = 2f;        // 过几秒后激活耐力条
    public GameObject fishStaminaCanvas;      // 耐力条 Canvas

    [Header("逃跑设置")]
    public Transform escapePoint;             // 逃跑目标点
    public float moveSpeed = 5f;              // 鱼的移动速度

    [Header("场景切换")]
    [Tooltip("设置要加载的下一个场景名称，可在 Inspector 中修改")]
    public string nextSceneName = "Unhook Man";  // 新增：下一个场景名称

    private Rigidbody fishRigidbody;
    private FishStaminaBar staminaBar;
    private Canvas canvasComponent;
    private FishDragLine fishDragLine;
    private Animator characterAnimator;

    private Collider waterSurfaceTriggerCollider;
    private Collider fishLandPointCollider;
    private bool isInWater = false;

    private void Start()
    {
        fishRigidbody = GetComponent<Rigidbody>();
        staminaBar = FishStaminaBar.instance;
        if (staminaBar == null)
        {
            Debug.LogError("FishStaminaBar instance is not found.");
            return;
        }

        // FishDragLine 查找
        GameObject flyLineGO = GameObject.Find("FlyLine");
        if (flyLineGO != null)
            fishDragLine = flyLineGO.GetComponent<FishDragLine>();
        if (fishDragLine == null)
            Debug.LogError("FishDragLine component not found on 'FlyLine'.");

        // WaterSurfaceTrigger 查找
        GameObject waterSurfaceTrigger = GameObject.Find("WaterSurfaceTrigger");
        if (waterSurfaceTrigger != null)
            waterSurfaceTriggerCollider = waterSurfaceTrigger.GetComponent<Collider>();
        if (waterSurfaceTriggerCollider == null)
            Debug.LogError("WaterSurfaceTrigger Collider 未找到或未设置 Is Trigger.");

        // FishLandPoint 查找
        GameObject fishLandPoint = GameObject.Find("FishLandPoint");
        if (fishLandPoint != null)
            fishLandPointCollider = fishLandPoint.GetComponent<Collider>();
        if (fishLandPointCollider == null)
            Debug.LogError("FishLandPoint Collider 未找到或未设置 Is Trigger.");

        // FishStaminaCanvas Canvas 组件
        if (fishStaminaCanvas != null)
        {
            canvasComponent = fishStaminaCanvas.GetComponent<Canvas>();
            if (canvasComponent == null)
                Debug.LogError("Canvas component not found on FishStaminaCanvas.");
            else
                canvasComponent.enabled = false;
        }
        else
        {
            Debug.LogError("FishStaminaCanvas 未在 Inspector 中赋值.");
        }

        // Character Animator 查找
        GameObject character = GameObject.Find("autoriggedmainch");
        if (character != null)
            characterAnimator = character.GetComponent<Animator>();
        if (characterAnimator == null)
            Debug.LogError("Animator component not found on 'autoriggedmainch'.");

        // 开始延迟激活耐力条
        StartCoroutine(ActivateStaminaBar());
    }

    private IEnumerator ActivateStaminaBar()
    {
        yield return new WaitForSeconds(activationDelay);
        if (canvasComponent != null)
            canvasComponent.enabled = true;
        StartCoroutine(CheckStamina());
    }

    private IEnumerator CheckStamina()
    {
        while (true)
        {
            if (staminaBar == null || fishDragLine == null)
                yield break;

            if (staminaBar.currentStamina > 0 && isInWater)
            {
                fishRigidbody.isKinematic = true;
                fishDragLine.StartStruggling();

                // 朝 escapePoint 方向移动
                Vector3 dir = (escapePoint.position - transform.position).normalized;
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * 5f);
                transform.position += dir * moveSpeed * Time.deltaTime;

                if (characterAnimator != null)
                    characterAnimator.SetBool("IsDraging", true);
            }
            else
            {
                fishRigidbody.isKinematic = false;
                fishDragLine.StopStruggling();
                if (characterAnimator != null)
                    characterAnimator.SetBool("IsDraging", false);
            }

            yield return null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == waterSurfaceTriggerCollider)
            isInWater = true;

        if (other == fishLandPointCollider)
            LoadNextScene();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other == waterSurfaceTriggerCollider)
            isInWater = false;
    }

    private void LoadNextScene()
    {
        Debug.Log("Done");
        if (!string.IsNullOrEmpty(nextSceneName))
            TransferSceneLoader.Instance.LoadWithTransfer(nextSceneName);
        else
            Debug.LogError("Next scene name is empty. 请在 Inspector 中设置 nextSceneName.");
    }
}
