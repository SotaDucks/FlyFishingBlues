using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using DialogueEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class ManGoWithShark : MonoBehaviour
{
    public Transform objectA;  // 待移动的物体 A
    public Transform objectB;  // 目标物体 B
    public GameObject Child;
    public GameObject Fish;
    private bool OneTime;
    public RawImage rawImage;
    public float fadeDuration = 1.0f;

    [Tooltip("前5米的运动时间，建议保持较短")]
    public float fastMoveDuration = 0.1f;
    [Tooltip("剩余每5米所消耗的时间，数值越大减速越明显")]
    public float slowMoveDurationPer5Meters = 1.0f;
    public NPCConversation Conversation;
    public bool GetIsFishBite(GameObject targetObject)
    {
        // 获取目标物体上的 FishBiteHook 脚本
        FishBiteHook fishBiteHook = targetObject.GetComponent<FishBiteHook>();

        if (fishBiteHook != null)
        {
            // 返回 isFishBite 的值
            return fishBiteHook.isFishBite;
        }
        else
        {
            // 如果找不到 FishBiteHook 脚本，返回 false
            Debug.LogError("FishBiteHook script not found on the target object.");
            return false;
        }
    }
    IEnumerator FadeIn()
    {
        Color startColor = rawImage.color;
        startColor.a = 0; // 设置起始透明度为 0
        rawImage.color = startColor;

        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsedTime / fadeDuration);
            Color currentColor = rawImage.color;
            currentColor.a = alpha;
            rawImage.color = currentColor;
            yield return null;
        }
    }
    public void PlayerVideo()
    {

        StartCoroutine(FadeIn());
        PlayVideo();

    }
    private Animator animator;
    private bool animationComplete = false;
    private void Start()
    {       // 初始化设置
        videoPlayer.playOnAwake = false;
        videoPlayer.isLooping = true;
        videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;
        videoPlayer.EnableAudioTrack(0, true);
        videoPlayer.SetTargetAudioSource(0, GetComponent<AudioSource>());
        animator = Child.GetComponent<Animator>();
        OneTime = true; 
        


    }
    void Update()
    {
        Debug.Log(GetIsFishBite(Fish)+"123");
        if (GetIsFishBite(Fish))
        {
            if (OneTime)
            {
                OneTime = false;
                animator.SetTrigger("TriggerV");
                Invoke("GoShark", 0.5f);

            }
           
        }
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        // 检查动画是否播放完成
        if (stateInfo.normalizedTime >= 1f && !animationComplete)
        {
            animationComplete = true;
            Debug.Log("动画播放完毕，执行命令LoadEndScene！");
          
            // 在这里执行你的命令
           
        }
       
    }

    private void LoadScene()
    {
        SceneManager.LoadScene("EndScene");
    
    
    }
    public void GoShark()
    {
      


        Invoke("PlayerVideo", 2f);
        Invoke("LoadScene",15f);
        // 取消 objectA 现有的 Tween，防止冲突
        objectA.DOKill();

            float totalDistance = Vector3.Distance(objectA.position, objectB.position);
            if (totalDistance <= 2f)
            {
                // 如果目标距离小于等于5米，直接采用快速移动
                objectA.DOMove(objectB.position, fastMoveDuration).SetEase(Ease.OutQuad);
            }
            else
            {
                // 如果距离大于5米，分为两个阶段：
                // 第一阶段：前5米快速移动
                Vector3 direction = (objectB.position - objectA.position).normalized;
                Vector3 intermediatePos = objectA.position + direction * 2f;

                // 剩余距离运动时采用慢速，计算时长（每5米消耗 slowMoveDurationPer5Meters 秒）
                float remainingDistance = totalDistance - 2f;
                float slowDuration = (remainingDistance / 2f) * slowMoveDurationPer5Meters;

                // 创建 DOTween 序列
                Sequence seq = DOTween.Sequence();
                seq.Append(objectA.DOMove(intermediatePos, fastMoveDuration)
                                  .SetEase(Ease.Linear));
                seq.Append(objectA.DOMove(objectB.position, slowDuration)
                                  .SetEase(Ease.OutQuad));
            
        }
    }


    public VideoPlayer videoPlayer;


    public void PlayVideo()
    {
        if (!videoPlayer.isPlaying)
        {
            videoPlayer.Play();
        }
    }

    public void PauseVideo()
    {
        if (videoPlayer.isPlaying)
        {
            videoPlayer.Pause();
        }
    }

    public void StopVideo()
    {
        if (videoPlayer.isPlaying)
        {
            videoPlayer.Stop();
        }
    }




}
