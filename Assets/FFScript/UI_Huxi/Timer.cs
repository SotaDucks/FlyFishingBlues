using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColliderTimes : MonoBehaviour
{
    public Text timerText;      // ������ʾʱ���UI�ı�
    private float _currentTime;
    private bool _isTiming;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            _isTiming = true;
            _currentTime = 0f;  // ÿ�ΰ�F���ü�ʱ
        }

        if (_isTiming)
        {
            _currentTime += Time.deltaTime;
            UpdateDisplay();
        }
    }

    void UpdateDisplay()
    {
        int minutes = (int)(_currentTime / 60);
        int seconds = (int)(_currentTime % 60);
        timerText.text = $"{minutes:00}:{seconds:00}";
    }
}
