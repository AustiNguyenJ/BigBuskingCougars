using UnityEngine;
//Attach this to the drumstick objects
public class DrumStickXR : MonoBehaviour
{
    public float velocity;
    public float acceleration;

    Vector3 lastPosition;
    float lastVelocity;

    void Start()
    {
        lastPosition = transform.position;
    }

    void Update()
    {
        velocity = (transform.position - lastPosition).magnitude / Time.deltaTime;

        acceleration = (velocity - lastVelocity) / Time.deltaTime;

        lastVelocity = velocity;
        lastPosition = transform.position;
    }
}