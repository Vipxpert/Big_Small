using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityUp : MonoBehaviour
{
    private Gravity gravity;
    private void Start()
    {
        gravity = GameObject.Find("Gravity").GetComponent<Gravity>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gravity.isGravityInverted = true;
            Rigidbody2D[] allRigidbodies2D = FindObjectsOfType<Rigidbody2D>();

            foreach (Rigidbody2D rb in allRigidbodies2D)
            {
                if (!rb.gameObject.name.Contains("Box inverted gravity"))
                {
                    rb.gravityScale = -Mathf.Abs(rb.gravityScale);
                }
                else
                {
                    rb.gravityScale = Mathf.Abs(rb.gravityScale);
                }
            }
        }
    }
}
