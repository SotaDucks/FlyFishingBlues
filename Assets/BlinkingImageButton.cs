using UnityEngine;
using UnityEngine.UI;

public class BlinkingImageButton : MonoBehaviour
{
    public float blinkDuration = 1f;  // ��˸����ʱ��
    public float minAlpha = 0f;       // ��С͸����
    public float maxAlpha = 1f;       // ���͸����

    private Image imageComponent;     // ͼƬ���
    private float timer = 0f;

    void Start()
    {
        // ��ȡ Image ���
        imageComponent = GetComponent<Image>();
        if (imageComponent == null)
        {
            Debug.LogError("δ���ҵ�Image������뽫�˽ű����ӵ�����Image�����UI�����ϡ�");
        }
    }

    void Update()
    {
        if (imageComponent != null)
        {
            // ʹ��UnscaledDeltaTime�Բ���Time.timeScale��Ӱ��
            timer += Time.unscaledDeltaTime;

            // ����͸���ȵı仯��ѭ������0-1-0��
            float alpha = Mathf.Lerp(minAlpha, maxAlpha, Mathf.PingPong(timer / blinkDuration, 1f));

            // ��ȡ��ǰ��ɫ���޸�Alphaֵ���ٸ�ֵ��Image���
            Color currentColor = imageComponent.color;
            currentColor.a = alpha;
            imageComponent.color = currentColor;
        }
    }
}

