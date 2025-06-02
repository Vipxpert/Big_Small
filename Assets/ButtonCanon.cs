using UnityEngine;
using System.Collections.Generic;

public class ButtonCannon : MonoBehaviour
{
    public List<Cannon> cannons;
    public bool isEnable = false;
    public bool isDisable = false;
    public bool isToggle = true;

    // This method is called when a collision starts
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!collision.CompareTag("Ignore"))
        foreach (Cannon cannon in cannons)
        {
            if (isToggle)
            {
                cannon.ToggleShooting();
            }
            if (isEnable)
            {
                cannon.EnableShooting();
            }
            if (isDisable)
            {
                cannon.DisableShooting();
            }
        }
    }
}