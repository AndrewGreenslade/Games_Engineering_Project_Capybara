using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioCheckerScript : MonoBehaviour
{
    private bool isMusicPlaying;
    public GameObject audioManager;
    // Start is called before the first frame update
    void Start()
    {
        //////foreach (GameObject obj in Object.FindObjectsOfType(typeof(GameObject)))
        //////{
        //////    Debug.Log(obj.name);

        //////    if (obj.name == "AudioManager")
        //////    {
        //////        isMusicPlaying = true;
        //////    }
        //////}

        //////if (isMusicPlaying == false)
        //////{
        //////    Instantiate(audioManager);
        //////}

        //if (GameObject.Find("AudioManager"). == false)
        //{
            Instantiate(audioManager);
        //}
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
