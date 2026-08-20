using UnityEngine;
using System.Collections;

public class FollowPlayer: MonoBehaviour
{
    public Vector3 offset;
    public Transform playerTransform;
    void Start()
    {
        offset = new Vector3(0, 2, -10);
        transform.position = playerTransform.position + offset;
    }

    void Update()
    {
        transform.position = playerTransform.position + offset;
        transform.LookAt(playerTransform);
    }
}