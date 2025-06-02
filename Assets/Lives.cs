using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lives : MonoBehaviour
{
    public AudioManager sfx;
    bool diePlayed = false;
    bool loseLivePlayed = false;
    public GameObject bigCube;
    public GameObject smallCube;
    public GameObject heartPrefab;
    public Transform heartsContainer;

    public int lives = 3;
    public bool isInvincible = false;
    /*public bool isLoseLives = false;*/
    public bool isDead = false;


    public float invincibilityDuration = 2f; // Adjust the duration as needed
    public float invincibilityTimer = 0f;
    public SpriteRenderer playerBigRenderer;
    public SpriteRenderer playerSmallRenderer;
    public float flickerSpeed = 1f; // Initial flicker speed
    public float flickerTimer = 0f;
    public float disappearDuration = 3f;
    public float disappearTimer = 0f;
    public PauseMenu pauseMenu;
    private void Awake()
    {
        sfx = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    private void Update()
    {
        if (!isInvincible)
        {
            loseLivePlayed = false;
        }

        //die.Play();
        /*if(disappearTimer > 0f)
        {
            if (!sfx.loseLive.isPlaying && !loseLivePlayed)
            {
                sfx.loseLive.PlayDelayed(0.2f);
                loseLivePlayed=true;
            }
        }*/
                
        if (isDead)
        {
            lives = -1;
            sfx.StopTheme();
            if (!diePlayed)
            {
                sfx.PlaySFX(sfx.die);
                diePlayed = true;
            }
            pauseMenu.Pause();
        }
    }
    public void AddHealth()
    {
        sfx.PlaySFX(sfx.health);
        lives++;
        UpdateHearts();
    }
    public void RestoreHealth()
    {
        sfx.PlaySFX(sfx.health);
        if (lives < 3)
        {
            lives = 3;
        }
        UpdateHearts();
    }

    public void UpdateHearts()
    {
        // Remove existing hearts
        foreach (Transform child in heartsContainer)
        {
            Destroy(child.gameObject);
        }

        // Duplicate hearts based on the current lives
        for (int i = 0; i < lives; i++)
        {
            GameObject heart = Instantiate(heartPrefab, heartsContainer);
            // Adjust the position based on your requirements
            heart.transform.localPosition = new Vector3(i * 1.5f, 0f, 1f);
        }
    }

    public void FlickerRenderer()
    {
        if (!loseLivePlayed)
        {
            sfx.PlaySFX(sfx.loseLive);
            loseLivePlayed = true;
        }

        // Toggle the visibility of the sprite renderer for the flickering effect
        flickerTimer += Time.deltaTime;
        if (flickerTimer >= flickerSpeed)
        {
            playerBigRenderer.enabled = !playerBigRenderer.enabled;
            playerSmallRenderer.enabled = !playerSmallRenderer.enabled;
            flickerTimer = 0f;

            // Gradually increase flicker speed as invincibility period comes to an end
            if (isInvincible)
            {
                flickerSpeed = Mathf.Lerp(0.05f, 0.001f, 1 - (invincibilityTimer / invincibilityDuration));
            }
        }
    }

}
