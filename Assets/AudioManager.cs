using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Source")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource sfxSource;
    [Header("Audio Clip")]
    public AudioClip[] background;
    public AudioClip collect;
    public AudioClip openDoor;
    public AudioClip health;
    public AudioClip menu;
    public AudioClip loseLive;
    public AudioClip die;
    public AudioClip win;
    public AudioClip[] gameEnter;
    private List<AudioClip> playlist;
    private AudioClip lastClip;
    public static AudioManager instance;
    private string currentScene;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }
    private void Start()
    {
        musicSource.loop = true;
        musicSource.clip = menu;
        musicSource.Play();
        SceneManager.sceneLoaded += OnSceneLoaded;

    }
    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }
    public void StopTheme()
    {
        musicSource.Stop();
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        sfxSource.Stop();
        AudioClip nextClip;
        if (currentScene != scene.name)
        {
            // Choose a random song that's not the same as the current one
            do
            {
                nextClip = background[Random.Range(0, background.Length)];
            } while (nextClip == lastClip);

            musicSource.clip = nextClip;
            if (scene.name == "Main")
            {
                musicSource.clip = menu;
            }
            musicSource.Play();
            lastClip = nextClip;
        }
        else if (!musicSource.isPlaying)
        {
            musicSource.Play();
        }
        currentScene = scene.name;
        //b
    }
}