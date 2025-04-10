using UnityEngine;
using UnityEngine.UI;
using DialogueEditor;
using UnityEngine.SceneManagement;

public class StorySceneController : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private float moveDistance = 8f;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Button playButton;
    
    [Header("Character Animation")]
    [SerializeField] private Animator youngFishermanAnimator; // 年轻渔夫的Animator
    [SerializeField] private Animator oldFishermanAnimator;   // 老渔夫的Animator
    
    private bool isMoving = false;
    private Vector3 targetPosition;
    private bool hasStartedConversation = false;
    private bool isYoung = true; // 默认是年轻渔夫

    public NPCConversation Conversation;

    private void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
        targetPosition = mainCamera.transform.position + Vector3.down * moveDistance;
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

    public void OnPlayButtonPressed()
    {
        isMoving = true;
        if (playButton != null)
        {
            playButton.gameObject.SetActive(false);
        }
    }

    // 以下是可以在Dialogue Editor中调用的动画方法
    public void SwitchCharacter(bool isYoungFisherman)
    {
        isYoung = isYoungFisherman;
        // 根据当前说话的角色来触发相应的动画
        if (isYoungFisherman)
        {
            if (youngFishermanAnimator != null)
            {
                youngFishermanAnimator.SetTrigger("YoungTalk");
            }
        }
        else
        {
            if (oldFishermanAnimator != null)
            {
                oldFishermanAnimator.SetTrigger("OldTalk");
            }
        }
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene("FFInstructionScene");
    }
} 