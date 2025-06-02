using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void Update()
    {
        // Check if the bullet is off-screen
        /*if (!IsVisible())
        {
            // Destroy the bullet when it goes off-screen
            Destroy(gameObject);
        }*/
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Collider2D collider = collision.gameObject.GetComponent<Collider2D>();
        if (collider != null && collider.enabled && !collider.CompareTag("Ignore") && !collision.gameObject.name.Equals("Bullet(Clone)"))
        {
            Destroy(gameObject);
        }
    }


    // Check if the bullet is visible on the screen
    /*private bool IsVisible()
    {
        Vector3 viewportPosition = Camera.main.WorldToViewportPoint(transform.position);
        return (viewportPosition.x > 0 && viewportPosition.x < 1 && viewportPosition.y > 0 && viewportPosition.y < 1);
    }*/
}