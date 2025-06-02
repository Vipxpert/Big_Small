using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using TMPro.Examples;
using System.IO;

public class LevelPicker : MonoBehaviour
{
    public Button[] buttons;
    public Image[] images; 
    public GameObject lvButton;
    AudioManager audioManager;
    public void Awake()
    {
        ButtonToArray();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        int unlockedLv = PlayerPrefs.GetInt("UnlockedLV",1);
        
        for (int i = 1; i < buttons.Length; i++)
        {
            buttons[i].interactable = false;
        }
        for (int i = 1; i <= unlockedLv; i++)
        {
            buttons[i].interactable = true;
            var newColor = new Color(1.0f, 1.0f, 1.0f, 1f);
            images[i].color = newColor;
        }

    }
    public void OpenLevel(int levelId)
    {
        string levelName = "Level" + levelId;
        SceneManager.LoadScene(levelName);
    }

    public void SFX(){
        audioManager.PlaySFX(audioManager.gameEnter[Random.Range(0, audioManager.gameEnter.Length)]);
    }
    void ButtonToArray()
    {
        int childCount = lvButton.transform.childCount;
        buttons = new Button[childCount];
        images = new Image[childCount];
        for(int i = 0; i < childCount; i++)
        {
            buttons[i] = lvButton.transform.GetChild(i).gameObject.GetComponent<Button>();
            if(i>0){
                images[i] = buttons[i].transform.GetChild(0).gameObject.GetComponent<Image>();
                var newColor = new Color(1.0f, 1.0f, 1.0f, 0.5f);
                images[i].color = newColor;
            }
        }
        //b
    }
}
