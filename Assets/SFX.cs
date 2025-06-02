using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFX : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource collect;
    public AudioSource openDoor;
    public AudioSource health;
    public AudioSource theme;
    public AudioSource menu;
    public AudioSource loseLive;
    public AudioSource die;
    public AudioSource win;
    public AudioSource[] gameEnter;
    public AudioSource withGameEnter;
    
    public void StopTheme() {
        theme.Stop();
    }
    public void PlayGameEnter() {
        
        gameEnter[Random.Range(0, gameEnter.Length)].Play();
        withGameEnter.PlayDelayed(1f);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
