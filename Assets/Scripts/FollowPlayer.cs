using UnityEngine;
using System.Collections;

public class FollowPlayer: MonoBehaviour
{
    public Vector3 offset;
    public Transform playerTransform;

    public float smoothTime = 0.25f;
    public Vector3 currentVelocity = Vector3.zero;
    void Start()
    {
        offset = new Vector3(0, 2, -10);
        if (playerTransform != null) transform.position = playerTransform.position + offset;
    }

    void LateUpdate()
    {
        if (playerTransform == null) return;

        Vector3 targetPosition = playerTransform.position + offset;

        transform.position = Vector3.SmoothDamp(
            transform.position,
            targetPosition,
            ref currentVelocity,
            smoothTime
        );
    }
}