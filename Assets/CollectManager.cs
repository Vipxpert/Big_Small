using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectManager : MonoBehaviour
{
    public AudioManager sfx;
    private Dictionary<string, int> collectedItems = new Dictionary<string, int>();
        
// Function to add collected items and update the count
private void Awake()
    {
        sfx = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
public void AddCollectedItem(string itemType)
    {
        if (collectedItems.ContainsKey(itemType))
        {
            collectedItems[itemType]++;
        }
        else
        {
            collectedItems[itemType] = 1;
        }
            sfx.PlaySFX(sfx.collect);
    }
}
