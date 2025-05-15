using UnityEngine;

/// <summary>
/// Controls the PointerLand object based on fish bite status.
/// </summary>
public class PointerLandController : MonoBehaviour
{
    [Tooltip("Reference to the FishBiteHook script")]
    public FishBiteHook fishBiteHook;

    [Tooltip("GameObject to enable/disable based on fish bite status")]
    public GameObject pointerLandObject;

    private void Start()
    {
        // Find FishBiteHook if not assigned
        if (fishBiteHook == null)
        {
            // Try to find in the scene
            fishBiteHook = FindObjectOfType<FishBiteHook>();
            
            if (fishBiteHook == null)
            {
                Debug.LogError("PointerLandController: No FishBiteHook component found. Please assign it in the inspector.");
            }
        }

        // Validate pointer land object is assigned
        if (pointerLandObject == null)
        {
            Debug.LogError("PointerLandController: No pointerLandObject assigned. Please assign it in the inspector.");
        }

        // Initially update the object state
        UpdateObjectState();
    }

    private void Update()
    {
        // Update the object state based on the fish bite status
        UpdateObjectState();
    }

    private void UpdateObjectState()
    {
        if (fishBiteHook != null && pointerLandObject != null)
        {
            // If isFishBite is true, enable the object; otherwise, disable it
            pointerLandObject.SetActive(fishBiteHook.isFishBite);
        }
    }
} 