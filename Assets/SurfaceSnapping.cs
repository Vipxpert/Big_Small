using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurfaceSnapping : MonoBehaviour
{
    public LayerMask surfaceLayer; // Set this in the Unity Editor to the layer of surfaces you want to snap to

    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -transform.up, 1f, surfaceLayer);

        if (hit.collider != null)
        {
            Vector2 surfaceNormal = hit.normal;
            float angle = Mathf.Atan2(surfaceNormal.y, surfaceNormal.x) * Mathf.Rad2Deg;

            // Check if the surface angle is close to 45, -45, 60, or 30 degrees
            if (Mathf.Approximately(angle, 45f))
            {
                // Snap the rotation to the surface angle
                transform.rotation = Quaternion.Euler(0f, 0f, 45f);
            }
            else if (Mathf.Approximately(angle, -45f))
            {
                // Snap the rotation to the surface angle
                transform.rotation = Quaternion.Euler(0f, 0f, -45f);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
    }
}
