using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    private Gravity gravity;
    float bounceForceMultiplier = 1.005f; // Adjust this value to control the bounce force
    private void Start()
    {
        gravity = GameObject.Find("Gravity").GetComponent<Gravity>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();
        if (playerRb!=null)
        {
            

                //Debug.Log(collision.relativeVelocity.y + " "+ bounceForceMultiplier + " "+ Mathf.Abs(collision.relativeVelocity.y) * bounceForceMultiplier);
                // Apply the bounce effect by modifying the y-component of the velocity
                
            if (gravity.isGravityInverted)
            {
                playerRb.velocity = new Vector2(playerRb.velocity.x, -Mathf.Abs(collision.relativeVelocity.y) * bounceForceMultiplier);
            }
            else if (!gravity.isGravityInverted)
            {
                playerRb.velocity = new Vector2(playerRb.velocity.x, Mathf.Abs(collision.relativeVelocity.y) * bounceForceMultiplier);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        /*if (collision.gameObject.tag == "Player")
        {
            Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();

            if (playerRb.velocity.y < 0) // Only bounce if the player is falling
            {
                Debug.Log("Bounce");
                // Apply the bounce effect by modifying the y-component of the velocity
                playerRb.velocity = new Vector2(playerRb.velocity.x, playerRb.velocity.y * bounceForceMultiplier);
            }
        }*/
    }
}
