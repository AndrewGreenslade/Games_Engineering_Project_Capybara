using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    SavePrefs prefs;
    //private float volume;
    //public Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        prefs = FindObjectOfType<SavePrefs>();
    }

    // Update is called once per frame
    void Update()
    {
        //volume = slider.value;
    }

    public void Exit()
    {
        Application.Quit();
    }
    public void Settings()
    {
        SceneManager.LoadScene("Settings");
    }
    public void Play()
    {
        GameObject obj = GameObject.FindGameObjectWithTag("AudioManager");
        obj.GetComponent<AudioManager>().changeToLevelMusic();
        prefs.resetSaves();
        SceneManager.LoadScene("HubWorld");
    }
    public void Load()
    {
        GameObject obj = GameObject.FindGameObjectWithTag("AudioManager");
        obj.GetComponent<AudioManager>().changeToLevelMusic();
        SceneManager.LoadScene("HubWorld");
    }
    public void Back()
    {
        SceneManager.LoadScene("Main Menu");
    }

}
