using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePrefs : MonoBehaviour
{
    float healthToSave;
    int healthInt;
    float healthFloat;
    string doesPlayerHaveAxe;
    string doesPlayerHaveSword;
    string doesPlayerHaveBow;
    int keys;

    public GameObject player;
    private Player p;
    private InventoryManager inventory;


    private void Start()
    {
        p = player.GetComponent<Player>();
        inventory = FindObjectOfType<InventoryManager>();
        LoadGame();
       
    }
    public void setSaveValues()
    {

        healthToSave = p.playerHealth * 2;
        healthInt = (int)healthToSave;

        keys = inventory.keysStored;

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

    public void SaveGame()
    {
        PlayerPrefs.SetInt("HealthSaveInt", healthInt);
        PlayerPrefs.SetInt("KeySaveInt", keys);
        PlayerPrefs.SetString("AxeSaveString", doesPlayerHaveAxe);
        PlayerPrefs.SetString("SwordSaveString", doesPlayerHaveSword);
        PlayerPrefs.SetString("BowSaveString", doesPlayerHaveBow);
        PlayerPrefs.Save();
        Debug.Log("GAME SAVED!");
    }


    public void LoadGame()
    {
        if (PlayerPrefs.HasKey("AxeSaveString"))
        {
            healthInt = PlayerPrefs.GetInt("HealthSaveInt");
            healthFloat = (float)healthInt;
            healthFloat /= 2.0f;
            p.playerHealth = healthFloat;
            inventory.keysStored = PlayerPrefs.GetInt("KeySaveInt");
            if (PlayerPrefs.GetString("AxeSaveString") == "FALSE")
            {

                inventory.hasAxe = false;
            }
            else
            {
                inventory.hotbarItems[2].SetActive(true);
                inventory.hasAxe = true;
            }

            if (PlayerPrefs.GetString("SwordSaveString") == "FALSE")
            {
                inventory.hasSword = false;
            }
            else
            {
                inventory.hotbarItems[1].SetActive(true);
                inventory.hasSword = true;
            }

            if (PlayerPrefs.GetString("BowSaveString") == "FALSE")
            {
                inventory.hasBow = false;
            }
            else
            {
                inventory.hasBow = true;
            }
        }
        else
            Debug.Log("There is no save data!");
    }

    public void resetSaves()
    {
        PlayerPrefs.DeleteAll();
    }
}
