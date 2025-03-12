using UnityEngine;
using System.Collections;

public class BitePauser : MonoBehaviour
{
    // ����FishBiteHook�ű�������Inspector�н��и�ֵ
    public FishBiteHook fishBiteHook;

    // �ӳٴ����������ʱ�䣨����Ϊ��λ��
    public float delayDuration = 1f;

    // ������Ҫ����/ʧ���UI���������Inspector�н��и�ֵ
    public GameObject uiComponent1;
    public GameObject uiComponent2;
    public GameObject uiComponent3; // ������UI���

    // ��ֹ��δ�������
    private bool isFrozen = false;

    void Start()
    {
        // ����Ϸ��ʼʱȷ������UI�������δ����״̬
        if (uiComponent1 != null)
            uiComponent1.SetActive(false);
        if (uiComponent2 != null)
            uiComponent2.SetActive(false);
        if (uiComponent3 != null)
            uiComponent3.SetActive(false); // ȷ��������UI�����ʼΪδ����
    }

    void Update()
    {
        // ���FishBiteHook�ű��Ƿ���ڣ�����isFishBiteΪtrue�ҵ�ǰδ������
        if (fishBiteHook != null && fishBiteHook.isFishBite && !isFrozen)
        {
            StartCoroutine(HandleFreeze());
        }
    }

    IEnumerator HandleFreeze()
    {
        isFrozen = true;

        // �����ڶ���ǰ0.1�뼤��UI�ĵȴ�ʱ��
        float uiActivationDelay = delayDuration - 0.3f;

        // ȷ��uiActivationDelay��Ϊ����
        if (uiActivationDelay > 0)
        {
            yield return new WaitForSecondsRealtime(uiActivationDelay);
        }
        else
        {
            // ���delayDurationС��0.1�룬��������UI
            uiActivationDelay = 0f;
        }

        // ��������UI�����������ʾ
        if (uiComponent1 != null)
            uiComponent1.SetActive(true);
        if (uiComponent2 != null)
            uiComponent2.SetActive(true);

        // �ȴ�ʣ���0.1���ٶ�����Ϸ
        float remainingDelay = delayDuration - uiActivationDelay;
        if (remainingDelay > 0)
        {
            yield return new WaitForSecondsRealtime(remainingDelay);
        }

        // ������Ϸ
        Time.timeScale = 0f;

        // �ȴ���Ұ���W���Իָ���Ϸ
        while (true)
        {
            // �������Ƿ���W��
            if (Input.GetKeyDown(KeyCode.W))
            {
                // �ָ���Ϸʱ��
                Time.timeScale = 1f;

                // ʧ������UI���
                if (uiComponent1 != null)
                    uiComponent1.SetActive(false);
                if (uiComponent2 != null)
                    uiComponent2.SetActive(false);

                // ���������UI���
                if (uiComponent3 != null)
                    uiComponent3.SetActive(true);

                // ���ø��������ֹ�ٴζ���
                this.enabled = false;

                // ����Э��
                yield break;
            }

            // �ȴ���һ֡
            yield return null;
        }
    }
}
