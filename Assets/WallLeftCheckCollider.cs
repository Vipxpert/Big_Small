using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallLeftCheckCollider : MonoBehaviour
{
    public bool isOnWallLeft;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            //Debug.Log("Left");
            isOnWallLeft = true;
        }

    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            //Debug.Log("Not left");
            isOnWallLeft = false;
        }
    }
}
