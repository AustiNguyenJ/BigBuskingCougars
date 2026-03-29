using UnityEngine;
using UnityEngine.XR;

// Attach this to the drumstick objects
public class DrumStickXR : MonoBehaviour
{
    public enum StickHand
    {
        Left,
        Right
    }

    public StickHand hand;

    public float velocity;
    public float acceleration;

    Vector3 lastPosition;
    float lastVelocity;

    InputDevice device;

    void Start()
    {
        lastPosition = transform.position;
        RefreshDevice();
    }

    void Update()
    {
        velocity = (transform.position - lastPosition).magnitude / Time.deltaTime;
        acceleration = (velocity - lastVelocity) / Time.deltaTime;

        lastVelocity = velocity;
        lastPosition = transform.position;

        if (!device.isValid)
            RefreshDevice();
    }

    void RefreshDevice()
    {
        XRNode node = hand == StickHand.Left ? XRNode.LeftHand : XRNode.RightHand;
        device = InputDevices.GetDeviceAtXRNode(node);
    }

    public void SendHaptics(float amplitude, float duration)
    {
        if (device.isValid)
        {
            amplitude = Mathf.Clamp01(amplitude);
            duration = Mathf.Max(0f, duration);
            device.SendHapticImpulse(0u, amplitude, duration);
        }
    }
}