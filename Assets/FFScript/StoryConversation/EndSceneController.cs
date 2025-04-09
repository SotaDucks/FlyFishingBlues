using UnityEngine;
using UnityEngine.UI;
using DialogueEditor;
using System.Collections;

public class EndSceneController : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private float moveDistance = 8f;
    [SerializeField] private float moveSpeed = 5f;
    
    [Header("Character Animation")]
    [SerializeField] private Animator oldFishermanAnimator;   // 老渔夫的Animator
    
    [Header("UI")]
    [SerializeField] private Image fadePanel;  // 黑色遮罩UI
    
    private bool isMoving = true;
    private Vector3 targetPosition;
    private Vector3 targetPosition_end;
    private bool hasStartedConversation = false;
    private bool isTalk = true; 
    private bool isEnd = false;

    public NPCConversation Conversation;

    private void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
        targetPosition = mainCamera.transform.position + Vector3.down * moveDistance;
        
        // 初始化遮罩UI
        if (fadePanel != null)
        {
            Color color = fadePanel.color;
            color.a = 0;
            fadePanel.color = color;
        }
    }

    private void Update()
    {
        if (isMoving)
        {
            mainCamera.transform.position = Vector3.MoveTowards(
                mainCamera.transform.position,
                targetPosition,
                moveSpeed * Time.deltaTime
            );

            if (Vector3.Distance(mainCamera.transform.position, targetPosition) < 0.01f)
            {
                isMoving = false;
                StartConversation();
            }
        }
    }

    private void StartConversation()
    {
        if (!hasStartedConversation && Conversation != null)
        {
            ConversationManager.Instance.StartConversation(Conversation);
            hasStartedConversation = true;
        }
    }

    // 以下是可以在Dialogue Editor中调用的动画方法
    public void SwitchConversation(bool isTalk)
    {
        oldFishermanAnimator.SetTrigger("Talk");
    }

    public void EndConversation(bool isEnd)
    {
        if (isEnd)
        {
            StartCoroutine(FadeToBlack());
        }
    }

    private IEnumerator FadeToBlack()
    {
        if (fadePanel == null) yield break;

        float fadeTime = 2f; // 渐变时间
        float elapsedTime = 0f;
        Color color = fadePanel.color;

        while (elapsedTime < fadeTime)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Lerp(0f, 1f, elapsedTime / fadeTime);
            fadePanel.color = color;
            yield return null;
        }

        // 确保最终完全不透明
        color.a = 1f;
        fadePanel.color = color;
    }
} 