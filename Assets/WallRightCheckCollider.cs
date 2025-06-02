using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRightCheckCollider : MonoBehaviour
{
    public bool isOnWallRight;
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            //Debug.Log("Right");
            isOnWallRight = true;
        }

    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            //Debug.Log("Not right");
            isOnWallRight = false;
        }
    }
}
