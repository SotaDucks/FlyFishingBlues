using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaSuggestionBlinker : MonoBehaviour
{
    [Tooltip("Reference to the sprite blinking controller")]
    public SpriteBlinkController spriteController;

    [Tooltip("Reference to the stamina system")]
    public FishStaminaBar staminaSystem;

    [Tooltip("Should the suggestion blink when stamina is positive")]
    public bool blinkWhenActive = true;

    [Tooltip("Should the sprite be visible when stamina is zero")]
    public bool visibleWhenInactive = false;

    private void Start()
    {
        // Find SpriteBlinkController if not assigned
        if (spriteController == null)
        {
            spriteController = GetComponent<SpriteBlinkController>();
            
            if (spriteController == null)
            {
                spriteController = gameObject.AddComponent<SpriteBlinkController>();
            }
        }

        // Find StaminaSystem if not assigned
        if (staminaSystem == null)
        {
            staminaSystem = FindObjectOfType<FishStaminaBar>();
            
            if (staminaSystem == null)
            {
                Debug.LogWarning("StaminaSuggestionBlinker: No StaminaSystem found in the scene. Please assign it in the inspector.");
            }
        }

        // No need to get a SpriteRenderer here - spriteController already has one
        if (spriteController != null && spriteController.targetRenderer == null)
        {
            Debug.LogError("StaminaSuggestionBlinker: No targetRenderer set on the spriteController. Please ensure it's properly assigned.");
        }

        // Initial update
        UpdateVisibility();
    }

    private void Update()
    {
        UpdateVisibility();
    }

    private void UpdateVisibility()
    {
        if (spriteController == null || spriteController.targetRenderer == null || staminaSystem == null)
            return;

        // Check if stamina is positive
        bool hasStamina = staminaSystem.currentStamina > 0f;

        // Control visibility based on stamina state
        if (hasStamina)
        {
            // When stamina is positive, show and blink
            spriteController.targetRenderer.enabled = true;
            spriteController.SetBlinking(blinkWhenActive);
        }
        else
        {
            // When stamina is zero, control visibility based on settings
            spriteController.targetRenderer.enabled = visibleWhenInactive;
            spriteController.SetBlinking(false);
        }
    }
}

