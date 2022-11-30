using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public abstract class Pickup : MonoBehaviour
{
    public TextMeshProUGUI powerupText;
    public TextMeshProUGUI powerupNameText;

    public void Start()
    {
        powerupText = GameObject.FindGameObjectWithTag("PowerupText").GetComponent<TextMeshProUGUI>();

        powerupText.faceColor = new Color32(255, 255, 255, 0);

        powerupNameText = GameObject.FindGameObjectWithTag("PowerupNameText").GetComponent<TextMeshProUGUI>();

        powerupNameText.faceColor = new Color32(255, 255, 255, 0);
    }

    public void SetPowerUpNameText(string t_text)
    {
        powerupNameText.text = t_text;
    }

    public void SetPowerUpText(string t_text)
    {
        powerupText.text = t_text;
    }

    public void enableText()
    {
        powerupNameText.faceColor = new Color32(255,255,255,225);

        powerupText.faceColor = new Color32(255, 255, 255, 225);
    }

    public void disableText()
    {
        powerupNameText.faceColor = new Color32(255, 255, 255, 0);

        powerupText.faceColor = new Color32(255, 255, 255, 0);
    }

    public abstract void outputPowerupName();

    public abstract void outputPowerupDescription();
}
