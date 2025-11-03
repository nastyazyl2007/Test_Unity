using UnityEngine;

public class CameraTracker : MonoBehaviour
{
    public Transform trackedObject;
    public float updateSpeed = 10;
    public Vector2 trackingOffset;
    private Vector3 offset;

    void Start()
    {
        offset = (Vector3)trackingOffset;
        offset.z = transform.position.z - trackedObject.position.z;

    }

    void LateUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, trackedObject.position + offset, updateSpeed * Time.deltaTime);
    }
}
