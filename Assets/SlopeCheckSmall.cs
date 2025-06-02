using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlopeCheckSmall : MonoBehaviour
{
    public bool isAllowJump = false;
    public bool isOnSlopeRight = false;
    public bool isOnAcuteSlopeRight = false;
    public bool isOnObtuseSlopeRight = false;
    public bool isOnSlopeLeft = false;
    public bool isOnAcuteSlopeLeft = false;
    public bool isOnObtuseSlopeLeft = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("SlopeRight"))
        {
            isAllowJump = true;
            isOnSlopeRight = true;
        }
        if (collision.CompareTag("SlopeLeft"))
        {
            isAllowJump = true;
            isOnSlopeLeft = true;
        }
        if (collision.CompareTag("AcuteSlopeRight"))
        {
            isAllowJump = true;
            isOnAcuteSlopeRight = true;
        }
        if (collision.CompareTag("AcuteSlopeLeft"))
        {
            isAllowJump = true;
            isOnAcuteSlopeRight = true;
        }
        if (collision.CompareTag("ObtuseSlopeRight"))
        {
            isAllowJump = true;
            isOnAcuteSlopeRight = true;
        }
        if (collision.CompareTag("ObtuseSlopeLeft"))
        {
            isAllowJump = true;
            isOnAcuteSlopeRight = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("SlopeRight"))
        {
            isAllowJump = true;
            isOnSlopeRight = true;
            //Debug.Log("R");
        }
        if (collision.CompareTag("SlopeLeft"))
        {
            isAllowJump = true;
            isOnSlopeLeft = true;
            //Debug.Log("L");
        }
        if (collision.CompareTag("AcuteSlopeRight"))
        {
            isAllowJump = true;
            isOnAcuteSlopeRight = true;
        }
        if (collision.CompareTag("AcuteSlopeLeft"))
        {
            isAllowJump = true;
            isOnAcuteSlopeRight = true;
        }
        if (collision.CompareTag("ObtuseSlopeRight"))
        {
            isAllowJump = true;
            isOnAcuteSlopeRight = true;
        }
        if (collision.CompareTag("ObtuseSlopeLeft"))
        {
            isAllowJump = true;
            isOnAcuteSlopeRight = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isAllowJump = false;
    }
}
