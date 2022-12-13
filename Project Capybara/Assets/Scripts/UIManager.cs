using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    SavePrefs prefs;
    // Start is called before the first frame update
    void Start()
    {
        prefs = FindObjectOfType<SavePrefs>();
    }

    // Update is called once per frame
    void Update()
    {
        
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
        SceneManager.LoadScene("HubWorld");
    }
    public void Load()
    {
        SceneManager.LoadScene("HubWorld");
        prefs.LoadGame();
    }
    public void Back()
    {
        SceneManager.LoadScene("Main Menu");
    }

}
