using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheckBig : MonoBehaviour
{
    public bool isOnGround;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.CompareTag("Ground") || collision.CompareTag("Object") || collision.CompareTag("JumpPad") || collision.CompareTag("JumpThrough") ) && !collision.CompareTag("Water"))
        {
            isOnGround = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if ((collision.CompareTag("Ground") || collision.CompareTag("Object") || collision.CompareTag("JumpPad") || collision.CompareTag("JumpThrough")) && !collision.CompareTag("Water"))
        {
            isOnGround = true;

        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if ((collision.CompareTag("Ground") || collision.CompareTag("Object") || collision.CompareTag("JumpPad") || collision.CompareTag("JumpThrough")) && !collision.CompareTag("Water"))
        {
            isOnGround = false;
        }
    }
}
