using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class DrumGrabController : MonoBehaviour
{
    public XRNode leftHand = XRNode.LeftHand;
    public XRNode rightHand = XRNode.RightHand;

    public float followSpeed = 40f;
    public float rotateSpeed = 40f;

    private InputDevice leftDevice;
    private InputDevice rightDevice;

    private bool isGrabbed = false;
    private Transform grabbingHand;

    private Rigidbody rb;
    
    private static DrumGrabController leftHandHeld;
    private static DrumGrabController rightHandHeld;
    
    private Vector3 grabOffsetPosition;
    private Quaternion grabOffsetRotation;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        leftDevice = InputDevices.GetDeviceAtXRNode(leftHand);
        rightDevice = InputDevices.GetDeviceAtXRNode(rightHand);
    }

    void Update()
    {
        bool leftGrip, rightGrip;

        leftDevice.TryGetFeatureValue(CommonUsages.gripButton, out leftGrip);
        rightDevice.TryGetFeatureValue(CommonUsages.gripButton, out rightGrip);

        if (!isGrabbed)
        {
            if (leftGrip)
                TryGrab(leftHand);

            if (rightGrip)
                TryGrab(rightHand);
        }
        else
        {
            bool stillHolding = false;

            if (grabbingHand == GetHandTransform(leftHand))
                leftDevice.TryGetFeatureValue(CommonUsages.gripButton, out stillHolding);

            if (grabbingHand == GetHandTransform(rightHand))
                rightDevice.TryGetFeatureValue(CommonUsages.gripButton, out stillHolding);

            if (!stillHolding)
                Release();
        }
    }

    void FixedUpdate()
    {
        if (isGrabbed && grabbingHand != null)
        {
            Vector3 targetPos = grabbingHand.position + grabOffsetPosition;
            Quaternion targetRot = grabbingHand.rotation * grabOffsetRotation;

            rb.linearVelocity = (targetPos - transform.position) * followSpeed;

            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRot,
                rotateSpeed * Time.fixedDeltaTime
            );
        }
    }

    void TryGrab(XRNode handNode)
    {
        Transform handTransform = GetHandTransform(handNode);
        if (handTransform == null) return;

        float distance = Vector3.Distance(transform.position, handTransform.position);
        if (distance > 0.3f) return;

        // Check if hand is already holding something
        if (handNode == XRNode.LeftHand && leftHandHeld != null) return;
        if (handNode == XRNode.RightHand && rightHandHeld != null) return;

        // Assign this drum to the hand
        if (handNode == XRNode.LeftHand) leftHandHeld = this;
        if (handNode == XRNode.RightHand) rightHandHeld = this;

        isGrabbed = true;
        grabbingHand = handTransform;

        // Store offset
        grabOffsetPosition = transform.position - grabbingHand.position;
        grabOffsetRotation = Quaternion.Inverse(grabbingHand.rotation) * transform.rotation;

        rb.useGravity = false;
        rb.linearDamping = 10f;
    }

    void Release()
    {
        // Clear which hand was holding this
        if (leftHandHeld == this) leftHandHeld = null;
        if (rightHandHeld == this) rightHandHeld = null;

        isGrabbed = false;
        grabbingHand = null;

        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    Transform GetHandTransform(XRNode node)
    {
        GameObject obj = GameObject.Find(node == XRNode.LeftHand ? "XR Controller Left" : "XR Controller Right");
        return obj != null ? obj.transform : null;
    }
}