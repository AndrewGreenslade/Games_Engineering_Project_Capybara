using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public float volume;
    public Slider slider;
    public AudioSource backgroundMusic;
    public AudioSource levelMusic;
    public AudioSource bossMusic;
    public AudioSource attackSound;
    public AudioSource hurtSound;
    // Start is called before the first frame update
    void Start()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("AudioManager");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
        backgroundMusic.Play();
        volume = 1;
        backgroundMusic.volume = volume;
        levelMusic.volume = volume;
        bossMusic.volume = volume;
        attackSound.volume = volume;
        hurtSound.volume = volume;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void changeToLevelMusic()
    {
        backgroundMusic.Stop();
        levelMusic.Play();
    }

    public void changeToBossMusic()
    {
        levelMusic.Stop();
        bossMusic.Play();
    }

    public void changeVolume(float t_sliderValue)
    {
        volume = t_sliderValue;
        backgroundMusic.volume = volume;
        levelMusic.volume = volume;
        bossMusic.volume = volume;
        attackSound.volume = volume;    
        hurtSound.volume = volume;
    }

    public void playAttack()
    {
        attackSound.Play();
    }

    public void playHurt()
    {
        hurtSound.Play();
    }
}
