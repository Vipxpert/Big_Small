using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action : MonoBehaviour
{

    public void OpenDoor()
    {
        Debug.Log("Door opened!");
        gameObject.SetActive(false);
    }

    public void Collide()
    {
        Debug.Log("Key inserted!");
    }
}
