using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FollowPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    
    public GameObject PlayerBig;
    public float FollowSpeed = 2f;
    public float yOffset = 1f;
    public Transform target_1;
    public Transform target_2;
    bool isLookingForward = false;
    public float speed = 20f;
    public float xLimit = 5f;
    public float yLimit = 5f;
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            isLookingForward=!isLookingForward;
        }
        if (isLookingForward)
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            Vector3 newPosition = gameObject.transform.position + new Vector3(horizontal, vertical, 0) * speed * Time.deltaTime;
            if (PlayerBig.activeSelf)
            {
                newPosition.x = Mathf.Clamp(newPosition.x, target_1.position.x - xLimit, target_1.position.x + xLimit);
                newPosition.y = Mathf.Clamp(newPosition.y, target_1.position.y - yLimit, target_1.position.y + yLimit);
            }
            else
            {
                newPosition.x = Mathf.Clamp(newPosition.x, target_2.position.x - xLimit, target_2.position.x + xLimit);
                newPosition.y = Mathf.Clamp(newPosition.y, target_2.position.y - yLimit, target_2.position.y + yLimit);
            }
            transform.position = newPosition;
        }
        else
        {
            if (PlayerBig.activeSelf)
            {
                Vector3 newPos = new Vector3(target_1.position.x, target_1.position.y + yOffset, -10f);
                transform.position = Vector3.Slerp(transform.position, newPos, FollowSpeed * Time.deltaTime);
            }
            else
            {
                Vector3 newPos = new Vector3(target_2.position.x, target_2.position.y + yOffset, -10f);
                transform.position = Vector3.Slerp(transform.position, newPos, FollowSpeed * Time.deltaTime);
            }
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
