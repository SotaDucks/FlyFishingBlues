using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FishStaminaBar : MonoBehaviour
{
    public Slider fishStaminaBar;

    private int maxStamina = 100;
    public float currentStamina;

    // ���������ظ��ٶ�
    public float normalRegenSpeed = 2f; // ÿ��ظ�������ֵ

    // ����Ϊ0ʱ���ӳٺ��ض��ظ��ٶ�
    public float zeroStaminaDelay = 2f; // �ӳ�ʱ��
    public float staminaChargingSpeed = 5f; // �ض���ÿ��ظ��ٶ�

    private bool isRegeneratingAfterZero = false; // ����Ƿ����ض��ظ�״̬
    private float zeroStaminaTimer = 0f; // �����ľ���ļ�ʱ��

    public static FishStaminaBar instance;

    // �����ı���
    public int rechargeTimes = 2; // �����������Ա����»ָ��Ĵ���
    private int currentRechargeTimes = 0; // ��ǰ�Ѿ��ָ��Ĵ���

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        currentStamina = maxStamina;
        fishStaminaBar.maxValue = maxStamina;
        fishStaminaBar.value = currentStamina;
    }

    void Update()
    {
        // ���ָ�����δ�ﵽ����ʱ�����������ظ�
        if (currentRechargeTimes < rechargeTimes)
        {
            if (currentStamina < maxStamina)
            {
                if (currentStamina > 0 && !isRegeneratingAfterZero)
                {
                    // �����ظ�
                    RegenerateStamina(normalRegenSpeed);
                }
                else if (currentStamina <= 0)
                {
                    // �����ľ�����ӳټ�ʱ
                    zeroStaminaTimer += Time.deltaTime;
                    if (zeroStaminaTimer >= zeroStaminaDelay)
                    {
                        isRegeneratingAfterZero = true;
                        RegenerateStamina(staminaChargingSpeed);

                        if (currentStamina >= maxStamina)
                        {
                            isRegeneratingAfterZero = false;
                            zeroStaminaTimer = 0f;
                            currentRechargeTimes++;
                        }
                    }
                }
                else if (currentStamina > 0 && isRegeneratingAfterZero)
                {
                    // ���ض��ظ�״̬�¼����ظ�
                    RegenerateStamina(staminaChargingSpeed);

                    if (currentStamina >= maxStamina)
                    {
                        isRegeneratingAfterZero = false;
                        zeroStaminaTimer = 0f;
                        currentRechargeTimes++;
                    }
                }
            }
        }
        else
        {
            // �ָ������Ѵ����ޣ�ֹͣһ�������ظ�
            isRegeneratingAfterZero = false;
            zeroStaminaTimer = 0f;
        }
    }

    private void RegenerateStamina(float regenSpeed)
    {
        currentStamina += regenSpeed * Time.deltaTime;
        currentStamina = Mathf.Min(currentStamina, maxStamina);
        fishStaminaBar.value = currentStamina;
    }

    public void UseStamina(int amount)
    {
        if (isRegeneratingAfterZero)
        {
            // ���ض��ظ��ڼ䣬�����޷�������
            return;
        }

        if (currentStamina - amount >= 0)
        {
            currentStamina -= amount;
            fishStaminaBar.value = currentStamina;
        }
        else
        {
            currentStamina = 0;
            fishStaminaBar.value = currentStamina;

            // ����Ƿ��Ѵﵽ�����ܴ���
            if (currentRechargeTimes >= rechargeTimes)
            {
                // ������������ UI ���
                DisableStaminaBarUI();
            }
        }
    }

    /// <summary>
    /// ������������ UI �����ʹ������Ϸ�в�����ʾ��
    /// </summary>
    private void DisableStaminaBarUI()
    {
        if (fishStaminaBar != null)
        {
            // ���� Slider ����� GameObject
            fishStaminaBar.gameObject.SetActive(false);

            // ��ѡ��������־�Ե���
            Debug.Log("������ UI �ѱ����ã���Ϊ�ﵽ�����ܴ������������������㡣");
        }
        else
        {
            Debug.LogWarning("fishStaminaBar ��δ����ֵ��");
        }
    }
}
