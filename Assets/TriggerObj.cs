using System.Collections.Generic;
using UnityEngine;

public class TriggerObj : MonoBehaviour
{
    public int ID;
    public List<int> doorID = new List<int>();
    public List<int> keyID = new List<int>();
    public List<int> keyHoldingID = new List<int>();
    public bool isMustStay = false;
    public bool isCollect = false;
    public bool isToggle = false;
    public bool isAnythingCollide = true;
    private AudioManager sfx;
    private float unlockCooldown = 0.1f;
    private float unlockTimer = 0f;
    public bool isUnlocked = false;


    private void Awake()
    {
        sfx = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }



    private void Update()
    {
        if (keyID.Count > 0)
            OpenDoor();
        if (unlockTimer > 0)
        {
            unlockTimer -= Time.deltaTime;
        }

        if (keyID.Count > 0 && unlockTimer <= 0)
        {
            OpenDoor();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((!isAnythingCollide && collision.CompareTag("Player")) || (isAnythingCollide && (!collision.CompareTag("Ignore"))))
        {
            // Check if the object acts as a key
            if (doorID.Count > 0 && unlockTimer <= 0) // Check if the cooldown timer is not active
            {
                UnlockDoors();
            }

            // Check if the object acts as a door
            if (keyID.Count > 0 && unlockTimer <= 0)
            {
                OpenDoor();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // If isMustStay is true, remove its ID from the door
        if (isMustStay && doorID.Count > 0)
        {
            foreach (int doorIDToRemove in doorID)
            {
                TriggerObj door = FindTriggerObjByID(doorIDToRemove);
                if (door != null)
                {
                    door.keyHoldingID.Remove(ID);
                }
            }
        }
    }

    // Unlock doors if the object acts as a key
    private void UnlockDoors()
    {
        foreach (int doorIDToUnlock in doorID)
        {
            // Find the door with the corresponding doorID
            TriggerObj door = FindTriggerObjByID(doorIDToUnlock);

            if (door != null && door.unlockTimer <= 0)
            {
                // If isToggle is true, check if doorId exists, if it does, remove it, if not, add it
                if (isToggle)
                {
                    if (door.keyHoldingID.Contains(ID))
                    {
                        door.keyHoldingID.Remove(ID);
                    }
                    else
                    {
                        door.keyHoldingID.Add(ID);
                    }
                }
                else
                {
                    // Assign the key's ID to the door's keyHoldingID
                    door.keyHoldingID.Add(ID);
                }

                door.unlockTimer = door.unlockCooldown; // Start the cooldown timer for the door
            }
        }

        if (isCollect)
        {
            gameObject.SetActive(false);
        }
    }

    // Open the door if the object acts as a door and all required keys are present
    private void OpenDoor()
{
    if (CheckKeys())
    {
        if (!isUnlocked)
        {
            {
                sfx.PlaySFX(sfx.openDoor);

                gameObject.GetComponent<Renderer>().enabled = false;
                Collider2D[] colliders = gameObject.GetComponents<Collider2D>();
                foreach (Collider2D collider in colliders)
                {
                    collider.enabled = false;
                }
                isUnlocked = true;
            }
            isUnlocked = true;
        }
    }
    else
    {
        if (isUnlocked)
        {
            {
                gameObject.GetComponent<Renderer>().enabled = true;
                Collider2D[] colliders = gameObject.GetComponents<Collider2D>();
                foreach (Collider2D collider in colliders)
                {
                    collider.enabled = true;
                }
                isUnlocked = false;
            }
            isUnlocked = false;
        }
    }
}

    // Check if all required keys are present in keyHoldingID
    private bool CheckKeys()
    {
        foreach (int requiredKeyID in keyID)
        {
            if (!keyHoldingID.Contains(requiredKeyID))
            {
                return false;
            }
        }
        return true;
    }

    // Find TriggerObj by ID
    private TriggerObj FindTriggerObjByID(int id)
    {
        TriggerObj[] allTriggerObjs = FindObjectsOfType<TriggerObj>();
        foreach (TriggerObj triggerObj in allTriggerObjs)
        {
            if (triggerObj.ID == id)
            {
                return triggerObj;
            }
        }
        return null;
    }
}

