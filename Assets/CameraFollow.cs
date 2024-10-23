using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float FollowSped = 2f;
    public Transform target;

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 newPos = new(target.position.x, target.position.y, -10f);
        transform.position = Vector3.Slerp(transform.position, newPos, FollowSped * Time.deltaTime);
    }
}