using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FishStaminaBar : MonoBehaviour
{
    [Header("UI Components")]
    public Slider fishStaminaBar;

    [Header("Stamina Settings")]
    public int maxStamina = 100;
    public float currentStamina;

    [Tooltip("Regeneration speed when stamina > 0")]
    public float normalRegenSpeed = 2f;
    [Tooltip("Delay before regenerating after reaching zero")]
    public float zeroStaminaDelay = 2f;
    [Tooltip("Regeneration speed when recovering from zero")]
    public float staminaChargingSpeed = 5f;

    [Header("Recharge Limits")]
    public int rechargeTimes = 2;
    private int currentRechargeTimes = 0;

    private bool isRegeneratingAfterZero = false;
    private float zeroStaminaTimer = 0f;

    public static FishStaminaBar instance;

    void Awake()
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
        // Merge TireFish functionality: Use stamina on W key
        if (Input.GetKeyDown(KeyCode.W))
        {
            UseStamina(15);
        }

        // Regeneration and recharge logic
        if (currentRechargeTimes < rechargeTimes)
        {
            if (currentStamina < maxStamina)
            {
                if (currentStamina > 0 && !isRegeneratingAfterZero)
                {
                    RegenerateStamina(normalRegenSpeed);
                }
                else if (currentStamina <= 0)
                {
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
            // Exhausted recharge times, stop regeneration
            isRegeneratingAfterZero = false;
            zeroStaminaTimer = 0f;
        }
    }

    /// <summary>
    /// Regenerates stamina by given speed.
    /// </summary>
    private void RegenerateStamina(float regenSpeed)
    {
        currentStamina += regenSpeed * Time.deltaTime;
        currentStamina = Mathf.Min(currentStamina, maxStamina);
        fishStaminaBar.value = currentStamina;
    }

    /// <summary>
    /// Attempt to use a specified amount of stamina.
    /// </summary>
    public void UseStamina(int amount)
    {
        if (isRegeneratingAfterZero)
        {
            // Cannot use stamina while recovering from zero
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

            // If out of recharge attempts, disable UI
            if (currentRechargeTimes >= rechargeTimes)
            {
                DisableStaminaBarUI();
            }
        }
    }

    /// <summary>
    /// Disables the stamina bar UI when no more recharge attempts left.
    /// </summary>
    private void DisableStaminaBarUI()
    {
        if (fishStaminaBar != null)
        {
            fishStaminaBar.gameObject.SetActive(false);
            Debug.Log("Stamina UI disabled: no recharges left.");
        }
        else
        {
            Debug.LogWarning("fishStaminaBar reference is null.");
        }
    }
}
