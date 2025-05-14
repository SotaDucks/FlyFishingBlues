using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

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
    [Tooltip("到达目标后等待多少秒再切换场景")]
    public float delaySeconds = 1f;

    [Header("UI")]
    [Tooltip("在玩家按键后要失活的 Canvas")]
    public Canvas uiCanvas;

    // 防止多次触发
    private bool hasStarted = false;

    void Update()
    {
        // PS5 手柄 X 键在旧输入管理器中对应 JoystickButton15
        bool ps5XPressed  = Input.GetKeyDown(KeyCode.JoystickButton15);
        bool enterPressed = Input.GetKeyDown(KeyCode.Return);

        if (!hasStarted && !string.IsNullOrEmpty(nextSceneName) && (ps5XPressed || enterPressed))
        {
            hasStarted = true;

            // 失活指定的 UI Canvas
            if (uiCanvas != null)
                uiCanvas.gameObject.SetActive(false);

            StartCoroutine(AdjustCameraAndLoad());
        }
    }

    private IEnumerator AdjustCameraAndLoad()
    {
        var framing = virtualCamera
            .GetCinemachineComponent<CinemachineFramingTransposer>();

        // 顺滑地将 ScreenY 从当前值移动到 targetScreenY
        while (!Mathf.Approximately(framing.m_ScreenY, targetScreenY))
        {
            framing.m_ScreenY = Mathf.MoveTowards(
                framing.m_ScreenY,
                targetScreenY,
                adjustSpeed * Time.deltaTime
            );
            yield return null;
        }

        // 到达之后再等一会儿
        yield return new WaitForSeconds(delaySeconds);

        // 切换到指定场景
        SceneManager.LoadScene(nextSceneName);
    }
}
