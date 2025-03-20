using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ColliderTimes : MonoBehaviour
{
    [Header("�����")]
    [SerializeField] private Slider timerSlider;
    [SerializeField] private TextMeshProUGUI timeText;
    
    public GameObject Blood;

    [Header("��������")]
    [Range(1, 60)] public float totalTime = 30f;
    public Color startColor = Color.green;
    public Color endColor = Color.red;
    public Text Ltime;

    private float currentTime;
    private bool isCounting;
    private bool isBlooding=false;

    void Start()
    {
        InitializeTimer();
    }

    void Update()
    {
        if (isCounting)
        {
            currentTime -= Time.deltaTime;
            Ltime.text=currentTime.ToString();
            UpdateVisuals();
            
            if (currentTime <= 27   && !isBlooding) {
                Debug.Log("132123");      isBlooding = true;  Blood.SetActive(true); }
            if (currentTime <= 0)
            {
                currentTime = 0;
                isCounting = false;
                Debug.Log("����ʱ����");
            }
        }
    }

    private void InitializeTimer()
    {
        currentTime = totalTime;
        timerSlider.maxValue = totalTime;
        timerSlider.value = currentTime;
        isCounting = true;
    }

    private void UpdateVisuals()
    {
        // ���»�����
        timerSlider.value = currentTime;

        // �����ı���ʾ
        timeText.text = currentTime.ToString("0.0") + "s";

        // ��ɫ����
        float colorRatio = currentTime / totalTime;
        
    }

    // �ⲿ���÷���
    public void ResetTimer()
    {
        InitializeTimer();
    }
}
