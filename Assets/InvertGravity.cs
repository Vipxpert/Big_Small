using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvertGravity : MonoBehaviour
{
    private Gravity gravity;
    private void Start()
    {
        gravity = GameObject.Find("Gravity").GetComponent<Gravity>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "BigHitbox"|| collision.gameObject.name == "SmallHitbox")
        {
            Debug.Log(collision.tag);
            gravity.isGravityInverted = !gravity.isGravityInverted;
            Rigidbody2D[] allRigidbodies2D = FindObjectsOfType<Rigidbody2D>();

            foreach (Rigidbody2D rb in allRigidbodies2D)
            {
                rb.gravityScale *= -1;
            }
        }
    }
}
