using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public abstract class Pickup : MonoBehaviour
{
    public TextMeshProUGUI powerupText;
    public TextMeshProUGUI powerupNameText;
    public Canvas canvas;
    public Pickup()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        canvas = GetComponent<Canvas>();
        //powerupText = canvas.GetComponentInChildren<TextMeshProUGUI>()
        //powerupNameText = TextMeshProUGUI.FindGameObjectWithTag("PowerupNameText");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public abstract void outputPowerupName();

    public abstract void outputPowerupDescription();


}
