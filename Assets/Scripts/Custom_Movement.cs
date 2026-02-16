using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class Custom_Movements : MonoBehaviour
{
    [Header("Input Actions")]
    public InputActionReference moveAction; // drag XRI LeftHand Locomotion / Move here

    [Header("XR Origin")]
    public Transform xrOrigin; // XR Origin / Camera Offset

    [Header("Movement Settings")]
    public float moveSpeed = 1.5f;

    private void OnEnable()
    {
        moveAction.action.Enable();
    }

    private void OnDisable()
    {
        moveAction.action.Disable();
    }

    void Update()
    {
        if (xrOrigin != null)
        {
            Vector2 input = moveAction.action.ReadValue<Vector2>();

            Vector3 forward = new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z).normalized;
            Vector3 right = new Vector3(Camera.main.transform.right.x, 0, Camera.main.transform.right.z).normalized;

            Vector3 move = forward * input.y + right * input.x;
            xrOrigin.position += move * moveSpeed * Time.deltaTime;
        }
    }
}
