using UnityEngine;

public class LureFishingController : MonoBehaviour
{
    private Animator animator;
    private bool isFishing = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("找不到Animator组件！");
        }
        else
        {
            Debug.Log("Animator组件已找到");
        }
    }

    void Update()
    {
        // 处理空格键按下的状态切换
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("空格键被按下");
            if (!isFishing)
            {
                // 切换到钓鱼状态
                isFishing = true;
                Debug.Log("尝试播放Fishing动画");
                animator.Play("Fishing");
            }
            else
            {
                // 切换回准备状态
                isFishing = false;
                Debug.Log("尝试播放LureCastReady动画");
                animator.Play("LureCastReady");
            }
        }
    }
} 