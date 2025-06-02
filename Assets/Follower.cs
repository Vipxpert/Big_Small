using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
    public Transform follow;
    public float xOffset = 2f;
    public float yOffset = 2f;
    void Update()
    {
        if (follow != null)
        {
            // Calculate the target position with the offset
            Vector3 targetPosition = new Vector3(follow.position.x + xOffset, follow.position.y + yOffset, transform.position.z);

            // Move towards the target position smoothly
            transform.position = Vector3.Lerp(transform.position, targetPosition, 1f);
        }
    }
}
