using UnityEngine;

public class StickVisualOffset : MonoBehaviour
{
    [Header("References")]
    public Transform visualMesh; // drag Drum Stick child here

    [Header("Settings")]
    public float returnSpeed = 20f;

    Transform currentDrumSurface;
    Vector3 visualRestLocalPos;

    void Start()
    {
        if (visualMesh == null)
            visualMesh = GetComponentInChildren<MeshRenderer>().transform;

        visualRestLocalPos = visualMesh.localPosition;
    }

    public void OnEnterDrumSurface(Transform drumSurface)
    {
        currentDrumSurface = drumSurface;
    }

    public void OnExitDrumSurface()
    {
        currentDrumSurface = null;
    }

    void LateUpdate()
    {
        if (currentDrumSurface != null)
        {
            // Get drum surface Y in world space
            float surfaceY = currentDrumSurface.position.y;
            float stickTipY = visualMesh.position.y;

            // If stick tip is below surface, push visual up to surface
            if (stickTipY < surfaceY)
            {
                Vector3 pos = visualMesh.position;
                pos.y = surfaceY;
                visualMesh.position = pos;
            }
        }
        else
        {
            // Smoothly return to rest when not hitting anything
            visualMesh.localPosition = Vector3.Lerp(
                visualMesh.localPosition,
                visualRestLocalPos,
                Time.deltaTime * returnSpeed
            );
        }
    }
}
