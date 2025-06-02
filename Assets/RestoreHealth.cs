using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestoreHealth : MonoBehaviour
{
    Lives live;
    private void Start()
    {
        live = GameObject.Find("Player").GetComponent<Lives>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            live.RestoreHealth();
            Destroy(gameObject);
        }
    }
}
