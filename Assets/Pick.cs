using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pick : MonoBehaviour
{
    private AudioManager sfx;
    public string itemType = "Default";
    private CollectManager collector;

    private void Awake()
    {
        sfx = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    private void Start()
    {
        collector = GameObject.Find("Collectibles").GetComponent<CollectManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (collector != null)
            {
                collector.AddCollectedItem(itemType);
                sfx.PlaySFX(sfx.collect);
                Destroy(gameObject);
            }
        }
    }
}
