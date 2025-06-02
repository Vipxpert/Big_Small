using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigCheckUpperMiddleLeft : MonoBehaviour
{
    public bool isTooBig = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isTooBig = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
            isTooBig = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
            isTooBig = false;
    }
}
