using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    private float volume;
    public Slider slider;
    public AudioSource backgroundMusic;
    public AudioSource levelMusic;
    public AudioSource bossMusic;  
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
    }

    // Update is called once per frame
    void Update()
    {
        //volume = slider.value;
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
}
