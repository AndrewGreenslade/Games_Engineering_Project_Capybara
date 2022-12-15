using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class VolumeScript : MonoBehaviour
{
    public Slider slider;
    public float sliderValue;
    public GameObject audioManager;
    GameObject obj ;
    // Start is called before the first frame update
    void Start()
    {
        obj = GameObject.FindGameObjectWithTag("AudioManager");
    }

    // Update is called once per frame
    void Update()
    {
        sliderValue = slider.value;
    }

    void changeMusic()
    {
        obj.GetComponent<AudioManager>().changeVolume(slider.value);

    }
}
