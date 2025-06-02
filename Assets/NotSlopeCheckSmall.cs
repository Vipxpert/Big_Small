using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotSlopeCheckSmall : MonoBehaviour
{
    public bool isNotSlope;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.layer == 3 || collision.CompareTag("Object") || collision.CompareTag("JumpPad")) && !collision.CompareTag("Water"))
        {
            isNotSlope = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if ((collision.gameObject.layer == 3 || collision.CompareTag("Object") || collision.CompareTag("JumpPad")) && !collision.CompareTag("Water"))
        {
            isNotSlope = true;

        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if ((collision.gameObject.layer == 3 || collision.CompareTag("Object") || collision.CompareTag("JumpPad")) && !collision.CompareTag("Water"))
        {
            isNotSlope = false;
        }
    }
}
