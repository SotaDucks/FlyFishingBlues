using UnityEngine;
using UnityEngine.SceneManagement;

public class Reload : MonoBehaviour
{
    [Tooltip("如果此对象被销毁，则重新加载场景")]
    public GameObject target;

    // 为了避免多次重复触发，这里加个标志
    private bool isReloading = false;

    void Update()
    {
        // 当 target 被销毁且尚未开始重载
        if (!isReloading && target == null)
        {
            isReloading = true;
            // 重新加载当前场景
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
