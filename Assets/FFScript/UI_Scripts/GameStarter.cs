using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;
using UnityEngine.UI;

public class GameStarter : MonoBehaviour
{
    [Header("Scene Loading")]
    [Tooltip("要加载的下一个场景名称")]
    public string nextSceneName;

    [Header("Cinemachine Settings")]
    [Tooltip("要调整的虚拟摄像机")]
    public CinemachineVirtualCamera virtualCamera;
    [Tooltip("Framing Transposer 中 Screen Y 的目标值")]
    public float targetScreenY = 0.8f;
    [Tooltip("调整到目标值的速度（单位：ScreenY/秒）")]
    public float adjustSpeed = 0.5f;

    [Header("Logo Fade")]
    [Tooltip("要渐显的 Logo Image")]
    public Image logoImage;
    [Tooltip("开始调整摄像机后多久（秒）开始 Logo 渐显")]
    public float logoFadeDelay = 1f;
    [Tooltip("Logo 渐显速度（Alpha/秒）")]
    public float logoFadeSpeed = 1f;

    [Header("Go To Tutorial")]
    [Tooltip("到位后激活的 Tutorial Image 们")]
    public Image[] goToTutorialImages;
    [Tooltip("Camera 到位后等待多久（秒）激活 GoToTutorialImages")]
    public float goToTutorialDelay = 1f;

    [Header("UI")]
    [Tooltip("在第一次按键后失活的 Canvas")]
    public Canvas uiCanvas;

    // 防止多次触发
    private bool hasStarted = false;

    void Start()
    {
        // 初始设置 Logo
        if (logoImage != null)
        {
            logoImage.gameObject.SetActive(true);
            var c = logoImage.color;
            c.a = 0f;
            logoImage.color = c;
        }
        // 初始隐藏 Tutorial Images
        if (goToTutorialImages != null)
        {
            foreach (var img in goToTutorialImages)
                if (img != null)
                    img.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        bool ps5XPressed  = Input.GetKeyDown(KeyCode.JoystickButton1);
        bool enterPressed = Input.GetKeyDown(KeyCode.Return);
        bool keyPressed   = ps5XPressed || enterPressed;

        // 第一次按键：隐藏初始 UI 并启动序列
        if (!hasStarted && !string.IsNullOrEmpty(nextSceneName) && keyPressed)
        {
            hasStarted = true;
            if (uiCanvas != null)
                uiCanvas.gameObject.SetActive(false);
            StartCoroutine(StartSequence());
        }
        // Tutorial Images 激活后再按键：加载场景
        else if (hasStarted && goToTutorialImages != null && goToTutorialImages.Length > 0 && goToTutorialImages[0].gameObject.activeSelf && keyPressed)
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }

    private IEnumerator StartSequence()
    {
        // 并行：延迟后渐显 Logo
        StartCoroutine(DelayedLogoFade());
        // 等待摄像机调整完成
        yield return StartCoroutine(AdjustCameraCoroutine());

        // 摄像机到位后延迟激活 Tutorial Images
        yield return new WaitForSeconds(goToTutorialDelay);
        if (goToTutorialImages != null)
        {
            foreach (var img in goToTutorialImages)
                if (img != null)
                    img.gameObject.SetActive(true);
        }
    }

    private IEnumerator AdjustCameraCoroutine()
    {
        var framing = virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
        while (!Mathf.Approximately(framing.m_ScreenY, targetScreenY))
        {
            framing.m_ScreenY = Mathf.MoveTowards(
                framing.m_ScreenY,
                targetScreenY,
                adjustSpeed * Time.deltaTime
            );
            yield return null;
        }
    }

    private IEnumerator DelayedLogoFade()
    {
        yield return new WaitForSeconds(logoFadeDelay);
        if (logoImage == null)
            yield break;

        var c = logoImage.color;
        while (c.a < 1f)
        {
            c.a = Mathf.Min(1f, c.a + logoFadeSpeed * Time.deltaTime);
            logoImage.color = c;
            yield return null;
        }
    }
}
