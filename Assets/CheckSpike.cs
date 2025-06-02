using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckSpike : MonoBehaviour
{
    public bool isSpike = false;
    public bool isWin = false;
    public Lives live;
    public PauseMenu pauseMenu;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Spike") )
        {
            isSpike = true;
        }
        if (collision.CompareTag("FatalSpike"))
        {
            live.isDead = true;
        }
        if (collision.CompareTag("Goal"))
        {
            UnlockNewLv();
            isWin = true;
            pauseMenu.Pause();
        }
    }

    void UnlockNewLv()
    {
        if(SceneManager.GetActiveScene().buildIndex < PlayerPrefs.GetInt("UnlockedLV"))
        {
        }
        else
        PlayerPrefs.SetInt("UnlockedLV", PlayerPrefs.GetInt("UnlockedLV")+1);
        PlayerPrefs.Save();
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Spike") )
        {
            isSpike = true;
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Spike") )
        {
            isSpike = false;
        }
    }
}
