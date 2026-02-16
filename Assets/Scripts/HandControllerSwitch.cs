using UnityEngine;
using UnityEngine.XR.Hands;
using UnityEngine.XR.Interaction.Toolkit;

public class XRHandsControllerSwitcher : MonoBehaviour
{
    [Header("Left Hand References")]
    public GameObject leftHandPrefab;       // XR Hands prefab for left hand
    public GameObject leftControllerPrefab; // XR Controller model prefab for left hand

    [Header("Right Hand References")]
    public GameObject rightHandPrefab;      // XR Hands prefab for right hand
    public GameObject rightControllerPrefab;// XR Controller model prefab for right hand

    private XRHandSubsystem handSubsystem;

    void Start()
    {
        // Automatically find the active XR Hand Subsystem
        var subsystems = new System.Collections.Generic.List<XRHandSubsystem>();
        SubsystemManager.GetSubsystems(subsystems);

        if (subsystems.Count > 0)
            handSubsystem = subsystems[0];
        else
            Debug.LogWarning("No XRHandSubsystem found. Make sure Hand Tracking is enabled in OpenXR settings.");
    }

    void Update()
    {
        if (handSubsystem == null)
            return;

        // Left Hand
        bool leftTracked = handSubsystem.leftHand.isTracked;
        if (leftHandPrefab) leftHandPrefab.SetActive(leftTracked);
        if (leftControllerPrefab) leftControllerPrefab.SetActive(!leftTracked);

        // Right Hand
        bool rightTracked = handSubsystem.rightHand.isTracked;
        if (rightHandPrefab) rightHandPrefab.SetActive(rightTracked);
        if (rightControllerPrefab) rightControllerPrefab.SetActive(!rightTracked);
    }
}
