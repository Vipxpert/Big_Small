using UnityEngine;

public class Portal : MonoBehaviour
{
    public string portalID; // Assign the same ID to both portals
    bool teleported = false;
    Collider2D portalCollider;
    public float maxVelocity = 30f;

    void Start()
    {
        portalCollider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !teleported)
        {
            TeleportObject(other.gameObject, maxVelocity);
            teleported = true;
            Invoke("ResetTeleported", 0.1f); // Reset the flag after 1 second
        }
    }

    private void TeleportObject(GameObject objToTeleport, float maxVelocity)
    {
        Portal[] allPortals = FindObjectsOfType<Portal>();

        foreach (Portal portal in allPortals)
        {
            if (portal.portalID == portalID && portal != this)
            {
                Rigidbody2D objRigidbody = objToTeleport.GetComponent<Rigidbody2D>();

                // Check if the object has a Rigidbody2D
                if (objRigidbody != null)
                {
                    // Teleport the object
                    objToTeleport.transform.position = portal.transform.position;

                    // Cap the velocity if it exceeds the maximum allowed velocity
                    if (objRigidbody.velocity.magnitude > maxVelocity)
                    {
                        objRigidbody.velocity = objRigidbody.velocity.normalized * maxVelocity;
                    }

                    portal.teleported = true;
                    portal.Invoke("ResetTeleported", 0.1f); // Reset the flag after 0.1 seconds
                    break;
                }
                else
                {
                    Debug.LogWarning("Object to teleport does not have a Rigidbody2D component.");
                }
            }
        }
    }


    private void ResetTeleported()
    {
        teleported = false;
    }

}
