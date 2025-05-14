using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;
public class NewDialogueSystem : MonoBehaviour
{


    [System.Serializable]
    public class DialogueNode
    {
        public string text;           // 对话文本
        public Sprite background;     // 背景图片
        public Sprite characterImage; // 角色图片

        public enum ProgressType
        {
            KeyPress,       // 特定按键触发
            Collision,      // 碰撞触发
            AutoProgress    // 自动前进
        }

        public ProgressType progressType;
        public string actionName;     // 按键名称(当progressType为KeyPress)
        public string collisionTag;   // 碰撞标签(当progressType为Collision)
        public float autoDelay;       // 自动前进延迟(当progressType为AutoProgress)
    }

    [Header("UI元素")]
    public GameObject dialoguePanel;
    public Image backgroundImage;
    public Image characterImage;
    public TextMeshProUGUI dialogueText;

    [Header("对话内容")]
    public DialogueNode[] dialogueNodes;

    [Header("输入")]
    public PlayerInput playerInput;

    private int currentNodeIndex = -1;
    private bool waitingForCollision = false;
    private InputAction boundAction;

    void Start()
    {
        StartDialogue();
    }

    // 开始对话
    public void StartDialogue()
    {
        currentNodeIndex = -1;
        NextNode();
    }

    // 进入下一节点
    public void NextNode()
    {
        // 清除之前的输入绑定
        if (boundAction != null)
        {
            boundAction.performed -= OnInputPerformed;
            boundAction = null;
        }

        // 停止之前的自动进行协程
        StopAllCoroutines();

        currentNodeIndex++;

        // 检查对话是否结束
        if (currentNodeIndex >= dialogueNodes.Length)
        {
            EndDialogue();
            return;
        }

        // 获取当前节点
        DialogueNode currentNode = dialogueNodes[currentNodeIndex];

        // 更新UI
        dialoguePanel.SetActive(true);
        backgroundImage.sprite = currentNode.background;
        characterImage.sprite = currentNode.characterImage;
        dialogueText.text = currentNode.text;

        // 根据进度类型设置如何前进到下一个节点
        switch (currentNode.progressType)
        {
            case DialogueNode.ProgressType.KeyPress:
                // 绑定输入动作
                var actionMap = playerInput.currentActionMap;
                boundAction = actionMap.FindAction(currentNode.actionName);
                if (boundAction != null)
                {
                    boundAction.performed += OnInputPerformed;
                    Debug.Log($"等待按键: {currentNode.actionName}");
                }
                else
                {
                    Debug.LogError($"找不到输入动作: {currentNode.actionName}");
                }
                break;

            case DialogueNode.ProgressType.Collision:
                // 等待碰撞触发
                waitingForCollision = true;
                Debug.Log($"等待碰撞: {currentNode.collisionTag}");
                break;

            case DialogueNode.ProgressType.AutoProgress:
                // 自动延迟进入下一节点
                StartCoroutine(AutoProgressAfterDelay(currentNode.autoDelay));
                break;
        }
    }

    // 输入动作触发
    private void OnInputPerformed(InputAction.CallbackContext context)
    {
        Debug.Log($"按键已触发: {context.action.name}");
        NextNode();
    }

    // 自动前进协程
    private IEnumerator AutoProgressAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        NextNode();
    }

    // 碰撞触发
    public void OnTriggerDetected(string tag)
    {
        if (waitingForCollision && currentNodeIndex < dialogueNodes.Length)
        {
            if (dialogueNodes[currentNodeIndex].collisionTag == tag)
            {
                Debug.Log($"检测到碰撞: {tag}");
                waitingForCollision = false;
                NextNode();
            }
        }
    }

    // 结束对话
    private void EndDialogue()
    {
        dialoguePanel.SetActive(false);
        Debug.Log("对话结束");
    }

}
