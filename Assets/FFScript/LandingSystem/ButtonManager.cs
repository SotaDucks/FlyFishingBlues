using UnityEngine;
using UnityEngine.UI; // ���ڲ��� Button ���
using DialogueEditor; // ���ʹ�� Dialogue Editor����Ҫ�������ռ�

public class ButtonManager : MonoBehaviour
{
    // �� Inspector ����������������ť GameObject
    public GameObject button1;
    public GameObject button2;


    void Start()
    {
        // ȷ����ť����Ϸ��ʼʱ�����ص�
        button1.SetActive(false);
        button2.SetActive(false);

        // ���ĶԻ������¼��������� Dialogue Editor��
        ConversationManager.OnConversationEnded += ShowButtons;
    }

    // ��ʾ��ť�ķ���
    void ShowButtons()
    {
            button1.SetActive(true);
            button2.SetActive(true);
    }

    // ȡ�������¼��������ڴ�й©
    void OnDestroy()
    {
        ConversationManager.OnConversationEnded -= ShowButtons;
    }
}