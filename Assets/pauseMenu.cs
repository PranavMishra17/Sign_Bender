using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pauseMenu : MonoBehaviour
{
    public GameObject pausemenuPanel;

    public GameObject pauseButton;
    public AudioSource bgaudio;
    public AudioClip pauseMenuMusic;
    public AudioClip gameMusic;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PauseGame()
    {
        Time.timeScale = 0;
        pauseButton.SetActive(false);
        pausemenuPanel.SetActive(true);
        bgaudio.clip = pauseMenuMusic;
        bgaudio.Play();
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        pauseButton.SetActive(true);
        pausemenuPanel.SetActive(false);
        bgaudio.clip = gameMusic;
        bgaudio.Play();
    }
}
