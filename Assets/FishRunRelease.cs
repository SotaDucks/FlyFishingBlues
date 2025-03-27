using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class FishRunRelease : MonoBehaviour
{
    public Button releaseButton;                 // �����ͷŵİ�ť
    public float delayBeforeRelease = 2f;        // ��ť���º�ȴ�������
    public Transform escapePoint;                // �����
    public float moveSpeed = 5f;                 // �ƶ��ٶ�

    public MonoBehaviour componentToActivate;    // ��Ҫ��������
    public string previousSceneName;             // Ҫ���ص���һ����������
    public float arrivalThreshold = 0.5f;        // ���ﳷ������ֵ

    private Rigidbody rb;
    private bool isRunning = false;

    void Start()
    {
       
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("δ�������Ϸ�������ҵ�Rigidbody�����");
            return;
        }

        if (releaseButton != null)
        {
            releaseButton.onClick.AddListener(OnButtonClick);
        }
        else
        {
            Debug.LogError("δ�����ͷŰ�ť��");
        }

        // ȷ������ڿ�ʼʱ������
        if (componentToActivate != null)
        {
            componentToActivate.enabled = false;
        }
    }

    void OnButtonClick()
    {
        if (!isRunning)
        {
            StartCoroutine(ReleaseAfterDelay());
        }
    }

    IEnumerator ReleaseAfterDelay()
    {
        yield return new WaitForSeconds(delayBeforeRelease);

        // ����ָ�����
        if (componentToActivate != null)
        {
            componentToActivate.enabled = true;
        }

        // ����RigidbodyΪKinematic
        rb.isKinematic = true;

        // ��ת���������
        Vector3 direction = (escapePoint.position - transform.position).normalized;
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 1f);
        }

        isRunning = true;
    }

    void Update()
    {
        if (isRunning && escapePoint != null)
        {
            Vector3 direction = (escapePoint.position - transform.position).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;

            // ����Ƿ񵽴ﳷ���
            float distance = Vector3.Distance(transform.position, escapePoint.position);
            if (distance <= arrivalThreshold)
            {
                LoadPreviousScene();
            }
        }
    }

    void LoadPreviousScene()
    {
        if (!string.IsNullOrEmpty(previousSceneName))
        {
            SceneManager.LoadScene(previousSceneName);
        }
        else
        {
            Debug.LogError("δ���� previousSceneName��");
        }
    }
}
