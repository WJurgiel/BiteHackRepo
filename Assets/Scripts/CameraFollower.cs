using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [SerializeField] private Vector3 offset = new Vector3(0f, 0f, -10f);
    [SerializeField] private float smoothTime = 0.25f;
    private Vector3 _velocity = Vector3.zero;
    [SerializeField] private Transform playerTarget;

    void FixedUpdate()
    {
        Vector3 targetPos = playerTarget.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref _velocity, smoothTime);
    }
}
