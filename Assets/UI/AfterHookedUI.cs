using UnityEngine;

public class AfterHookedUI : MonoBehaviour
{
    [Header("Reference to your FishBiteHook script")]
    [Tooltip("Drag the GameObject (or component) that has the FishBiteHook script here")]
    public FishBiteHook fishBiteHook;

    [Header("UI to Activate")]
    [Tooltip("Drag the UI GameObject you want to show after the fish is hooked")]
    public GameObject uiToActivate;

    // Prevents repeated activation
    private bool _hasActivated = false;

    void Update()
    {
        // 如果还没激活，并且已经咬钩，则激活 UI
        if (!_hasActivated && fishBiteHook != null && fishBiteHook.isFishBite)
        {
            if (uiToActivate != null)
                uiToActivate.SetActive(true);

            _hasActivated = true;
        }
    }
}
