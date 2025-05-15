using System.Collections;
using UnityEngine;
using Cinemachine;

[RequireComponent(typeof(Collider))]
public class CheckFishCameraController : MonoBehaviour
{
    [Header("Trigger Settings")]
    [Tooltip("Ensure this collider is set to 'Is Trigger' and is on the same GameObject.")]
    public Collider triggerCollider;

    [Header("Camera References")]
    [Tooltip("Your Cinemachine Virtual Camera to disable/enable")]
    public CinemachineVirtualCamera vCam;
    [Tooltip("Main Camera Transform (usually Camera.main.transform)")]
    public Transform mainCam;

    [Header("Move‑To Target (Drag your marker here)")]
    [Tooltip("Drag a GameObject here; camera will move to its world position")]
    public Transform targetMarker;

    [Header("Move & Hold Settings")]
    [Tooltip("Time to smoothly move to target position (seconds)")]
    public float smoothTime = 1f;
    [Tooltip("How long to hold at the target before returning (seconds)")]
    public float holdDuration = 3f;

    [Header("Disable Player Controls")]
    [Tooltip("All MonoBehaviours that handle player input")]
    public MonoBehaviour[] controlsToDisable;

    // Ensure we only trigger this sequence once
    private bool hasTriggered = false;

    void Reset()
    {
        triggerCollider = GetComponent<Collider>();
        triggerCollider.isTrigger = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (!hasTriggered && other.CompareTag("Player"))
        {
            hasTriggered = true;
            StartCoroutine(CameraSequence());
        }
    }

    private IEnumerator CameraSequence()
    {
        // 1. Disable all listed control scripts
        foreach (var mb in controlsToDisable)
            if (mb != null) mb.enabled = false;

        // 2. Disable the Cinemachine virtual camera
        if (vCam != null) vCam.enabled = false;

        // 3. Record the camera's original position
        Vector3 originalPos = mainCam.position;

        // 4. Smoothly move to the targetMarker position
        Vector3 startPos = originalPos;
        Vector3 endPos = targetMarker.position;
        float t = 0f;
        while (t < smoothTime)
        {
            t += Time.deltaTime;
            float lerp = Mathf.SmoothStep(0f, 1f, t / smoothTime);
            mainCam.position = Vector3.Lerp(startPos, endPos, lerp);
            yield return null;
        }
        mainCam.position = endPos;

        // 5. Hold at the target position
        yield return new WaitForSeconds(holdDuration);

        // 6. Smoothly move back to the original position
        t = 0f;
        while (t < smoothTime)
        {
            t += Time.deltaTime;
            float lerp = Mathf.SmoothStep(0f, 1f, t / smoothTime);
            mainCam.position = Vector3.Lerp(endPos, originalPos, lerp);
            yield return null;
        }
        mainCam.position = originalPos;

        // 7. Re-enable Cinemachine and player controls
        if (vCam != null) vCam.enabled = true;
        foreach (var mb in controlsToDisable)
            if (mb != null) mb.enabled = true;
    }
}
