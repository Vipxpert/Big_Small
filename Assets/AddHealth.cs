using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddHealth : MonoBehaviour
{
    private Lives live;
    private void Start()
    {
        live = GameObject.Find("Player").GetComponent<Lives>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            live.AddHealth();
            Destroy(gameObject);
        }
    }
}
