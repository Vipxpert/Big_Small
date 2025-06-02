using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerManager : MonoBehaviour
{
    public static TriggerManager Instance; // Singleton instance

    public List<TriggerObj> triggerObjs = new List<TriggerObj>();

    private void Awake()
    {
        // Ensure there is only one instance of GameManager
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Don't destroy on scene load
        }
        else
        {
            Destroy(gameObject); // If an instance already exists, destroy this one
        }
    }
}
