using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePrefs : MonoBehaviour
{
    float healthToSave;
    string doesPlayerHaveAxe;
    string doesPlayerHaveSword;
    string doesPlayerHaveBow;

    public GameObject player;

    void setSaveValues()
    {
        Player p = player.GetComponent<Player>();
        InventoryManager inventory = player.GetComponent<InventoryManager>();

        healthToSave = p.playerHealth;

        if (inventory.hasAxe == false)
        {
            doesPlayerHaveAxe = "FALSE";
        }
        else
        {
            doesPlayerHaveAxe = "TRUE";
        }

        if (inventory.hasSword == false)
        {
            doesPlayerHaveSword = "FALSE";
        }
        else
        {
            doesPlayerHaveSword = "TRUE";
        }

        if (inventory.hasBow == false)
        {
            doesPlayerHaveBow = "FALSE";
        }
        else
        {
            doesPlayerHaveBow = "TRUE";
        }
    }

    void SaveGame()
    {
        PlayerPrefs.SetFloat("HealthSaveFloat", healthToSave);
        PlayerPrefs.SetString("AxeSaveString", doesPlayerHaveAxe);
        PlayerPrefs.SetString("SwordSaveString", doesPlayerHaveSword);
        PlayerPrefs.SetString("BowSaveString", doesPlayerHaveBow);
        PlayerPrefs.Save();
        Debug.Log("Game data saved!");
    }
}
