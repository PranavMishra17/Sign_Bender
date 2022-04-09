using UnityEngine.Audio;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip TitleMusic;
    public AudioClip NAmbience;
    public AudioClip BossMusic;
    public AudioClip HAmbience;

    public Sound[] sounds;
    public static AudioManager instance;
    
    AudioSource audio;

    // Start is called before the first frame update
    void Awake()
    {
       /* if (instance == null) instance = this;
        else {Destroy(gameObject); return; 
        }*/

       // DontDestroyOnLoad(gameObject);
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.name = s.name;
            s.source.loop = s.loop;
        }
    }

    private void Start()
    {
        audio = GetComponent<AudioSource>();
        string currentscene = SceneManager.GetActiveScene().ToString();
        if (currentscene == "Tutorial")
        {
            audio.clip = TitleMusic;
            audio.Play();
        }
        else if (currentscene == "LVL1")
        {
            audio.clip = NAmbience;
            audio.Play();
        }
        else if (currentscene == "Menu")
        {
            audio.clip = HAmbience;
            audio.Play();
        }
    }
    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s==null)
        {
            Debug.Log("Sound not found");
            return;
        }
        s.source.Play();
        s.source.Pause();
    }
    public void Pause(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            return;
        }
        s.source.Pause();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
