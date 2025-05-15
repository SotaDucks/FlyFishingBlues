using UnityEngine;

/// <summary>
/// Controls the blinking effect of control suggestion icons.
/// </summary>
public class ControlSuggestionBlinker : MonoBehaviour
{
    [Tooltip("Reference to the sprite blinking controller")]
    public SpriteBlinkController spriteController;

    [Tooltip("Reference to the FishBiteHook script to determine activation")]
    public FishBiteHook fishBiteHook;

    [Tooltip("Should the suggestion blink when active")]
    public bool blinkWhenActive = true;

    [Tooltip("Should the sprite be visible when not active")]
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

        // Ensure there's a sprite renderer
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        if (renderer == null)
        {
            Debug.LogError("ControlSuggestionBlinker: No SpriteRenderer component found on this GameObject.");
        }
        else
        {
            spriteController.targetRenderer = renderer;
        }

        // Find FishBiteHook if not assigned
        if (fishBiteHook == null)
        {
            fishBiteHook = FindObjectOfType<FishBiteHook>();
            
            if (fishBiteHook == null)
            {
                Debug.LogWarning("ControlSuggestionBlinker: No FishBiteHook component found. The control suggestion won't be triggered automatically.");
            }
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
        if (spriteController == null || spriteController.targetRenderer == null)
            return;

        bool isActive = (fishBiteHook != null) ? fishBiteHook.isFishBite : false;

        // Control visibility based on active state
        if (isActive)
        {
            // When active, enable renderer and control blinking
            spriteController.targetRenderer.enabled = true;
            spriteController.SetBlinking(blinkWhenActive);
        }
        else
        {
            // When inactive, control visibility based on settings
            spriteController.targetRenderer.enabled = visibleWhenInactive;
            spriteController.SetBlinking(false);
        }
    }

    /// <summary>
    /// Manually set the activation state
    /// </summary>
    /// <param name="active">Whether the suggestion should be active</param>
    public void SetActive(bool active)
    {
        if (spriteController == null || spriteController.targetRenderer == null)
            return;

        if (active)
        {
            spriteController.targetRenderer.enabled = true;
            spriteController.SetBlinking(blinkWhenActive);
        }
        else
        {
            spriteController.targetRenderer.enabled = visibleWhenInactive;
            spriteController.SetBlinking(false);
        }
    }
} 