using UnityEngine;

/// <summary>
/// Controls sprite blinking effect for visual indicators.
/// </summary>
public class SpriteBlinkController : MonoBehaviour
{
    [Tooltip("Reference to the SpriteRenderer component")]
    public SpriteRenderer targetRenderer;

    [Tooltip("Blinking speed (seconds)")]
    public float blinkSpeed = 0.5f;

    [Tooltip("Minimum alpha value (0-1)")]
    [Range(0, 1)]
    public float minAlpha = 0.2f;

    [Tooltip("Maximum alpha value (0-1)")]
    [Range(0, 1)]
    public float maxAlpha = 1.0f;

    [Tooltip("Should the sprite blink")]
    public bool isBlinking = true;

    private float timer = 0f;
    private bool increasing = true;
    private Color originalColor;
    private float originalAlpha;

    private void Start()
    {
        // Try to get the SpriteRenderer if not assigned
        if (targetRenderer == null)
        {
            targetRenderer = GetComponent<SpriteRenderer>();
            
            if (targetRenderer == null)
            {
                Debug.LogError("SpriteBlinkController: No SpriteRenderer component found. Please assign it in the inspector.");
                enabled = false;
                return;
            }
        }

        // Store the original color and alpha
        originalColor = targetRenderer.color;
        originalAlpha = originalColor.a;
    }

    private void Update()
    {
        if (!isBlinking || targetRenderer == null)
            return;

        // Increment timer
        timer += Time.deltaTime;

        // Calculate alpha based on ping-pong pattern
        float t = Mathf.PingPong(timer / blinkSpeed, 1.0f);
        float alpha = Mathf.Lerp(minAlpha, maxAlpha, t);

        // Only change the alpha component, preserve the original RGB
        targetRenderer.color = new Color(
            originalColor.r, 
            originalColor.g, 
            originalColor.b, 
            alpha
        );
    }

    /// <summary>
    /// Enables or disables the blinking effect.
    /// </summary>
    /// <param name="state">True to enable blinking, false to disable</param>
    public void SetBlinking(bool state)
    {
        isBlinking = state;
        
        // Reset to full opacity when not blinking
        if (!isBlinking && targetRenderer != null)
        {
            targetRenderer.color = new Color(
                originalColor.r, 
                originalColor.g, 
                originalColor.b, 
                maxAlpha
            );
        }
    }

    /// <summary>
    /// Resets the sprite to its original color
    /// </summary>
    public void ResetSprite()
    {
        if (targetRenderer != null)
        {
            targetRenderer.color = originalColor;
        }
    }
} 