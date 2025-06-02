using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BigHitbox : MonoBehaviour
{
    public PlayerSmall playerSmall;
    public PlayerMovement playerBig;
    public bool isWin = false;
    public Lives live;
    public PauseMenu pauseMenu;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Spike"))
        {
            playerBig.LoseLives();
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
public void UnlockNewLv()
    {
        if(SceneManager.GetActiveScene().buildIndex < PlayerPrefs.GetInt("UnlockedLV"))
        {
            Debug.Log("a");
        }
        else
        PlayerPrefs.SetInt("UnlockedLV", PlayerPrefs.GetInt("UnlockedLV")+1);
        PlayerPrefs.Save();
    }
void OnTriggerStay2D(Collider2D collision)
{
    if (collision.CompareTag("Spike"))
    {
        playerBig.LoseLives();
    }
        if (collision.CompareTag("Goal"))
        {
            isWin = true;
        }

    }
    }
