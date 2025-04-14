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
    public Transform objectA;  // ���ƶ������� A
    public Transform objectB;  // Ŀ������ B
    public GameObject Child;
    public GameObject Fish;
    private bool OneTime;
    public RawImage rawImage;
    public float fadeDuration = 1.0f;

    [Tooltip("ǰ5�׵��˶�ʱ�䣬���鱣�ֽ϶�")]
    public float fastMoveDuration = 0.1f;
    [Tooltip("ʣ��ÿ5�������ĵ�ʱ�䣬��ֵԽ�����Խ����")]
    public float slowMoveDurationPer5Meters = 1.0f;
    public NPCConversation Conversation;
    public bool GetIsFishBite(GameObject targetObject)
    {
        // ��ȡĿ�������ϵ� FishBiteHook �ű�
        FishBiteHook fishBiteHook = targetObject.GetComponent<FishBiteHook>();

        if (fishBiteHook != null)
        {
            // ���� isFishBite ��ֵ
            return fishBiteHook.isFishBite;
        }
        else
        {
            // ����Ҳ��� FishBiteHook �ű������� false
            Debug.LogError("FishBiteHook script not found on the target object.");
            return false;
        }
    }
    IEnumerator FadeIn()
    {
        Color startColor = rawImage.color;
        startColor.a = 0; // ������ʼ͸����Ϊ 0
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
    {       // ��ʼ������
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

        // ��鶯���Ƿ񲥷����
        if (stateInfo.normalizedTime >= 1f && !animationComplete)
        {
            animationComplete = true;
            Debug.Log("����������ϣ�ִ������LoadEndScene��");
          
            // ������ִ���������
           
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
        // ȡ�� objectA ���е� Tween����ֹ��ͻ
        objectA.DOKill();

            float totalDistance = Vector3.Distance(objectA.position, objectB.position);
            if (totalDistance <= 2f)
            {
                // ���Ŀ�����С�ڵ���5�ף�ֱ�Ӳ��ÿ����ƶ�
                objectA.DOMove(objectB.position, fastMoveDuration).SetEase(Ease.OutQuad);
            }
            else
            {
                // ����������5�ף���Ϊ�����׶Σ�
                // ��һ�׶Σ�ǰ5�׿����ƶ�
                Vector3 direction = (objectB.position - objectA.position).normalized;
                Vector3 intermediatePos = objectA.position + direction * 2f;

                // ʣ������˶�ʱ�������٣�����ʱ����ÿ5������ slowMoveDurationPer5Meters �룩
                float remainingDistance = totalDistance - 2f;
                float slowDuration = (remainingDistance / 2f) * slowMoveDurationPer5Meters;

                // ���� DOTween ����
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
