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
        // ...（省略原有查找组件逻辑，保持不变）

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
            // ...（省略原有耐力检测和移动逻辑）
            yield return null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == waterSurfaceTriggerCollider)
            isInWater = true;

        if (other == fishLandPointCollider)
            LoadNextScene();   // 触发场景切换
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
        {
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            Debug.LogError("Next scene name is empty. Please set nextSceneName in the Inspector.");
        }
    }
}
