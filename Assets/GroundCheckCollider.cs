using Unity.VisualScripting;
using UnityEngine;


public class GroundCheckCollider : MonoBehaviour
{

    public bool isOnGround;
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.layer == 3 || collision.CompareTag("Object") || collision.CompareTag("JumpPad") || collision.CompareTag("JumpThrough")) && !collision.CompareTag("Water"))
        {
            isOnGround = true;
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if ((collision.gameObject.layer == 3 || collision.CompareTag("Object") || collision.CompareTag("JumpPad") || collision.CompareTag("JumpThrough")) && !collision.CompareTag("Water"))
        {
            isOnGround = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if ((collision.gameObject.layer == 3 || collision.CompareTag("Object") || collision.CompareTag("JumpPad") || collision.CompareTag("JumpThrough")) && !collision.CompareTag("Water"))
        {
            isOnGround = false;
        }
    }
}
