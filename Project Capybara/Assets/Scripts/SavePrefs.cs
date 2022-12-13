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

    public GameObject player;
    private Player p;
    private InventoryManager inventory;


    private void Start()
    {
        p = player.GetComponent<Player>();
        inventory = FindObjectOfType<InventoryManager>();
        
       
    }
    public void setSaveValues()
    {

        healthToSave = p.playerHealth * 2;
        healthInt = (int)healthToSave;

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
            Debug.Log("Loaded Health Float" + healthFloat);
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

            Debug.Log(PlayerPrefs.GetInt("HealthSaveInt"));
            Debug.Log(PlayerPrefs.GetString("AxeSaveString"));
            Debug.Log(PlayerPrefs.GetString("SwordSaveString"));
            Debug.Log(PlayerPrefs.GetString("BowSaveString"));
        }
        else
            Debug.Log("There is no save data!");
    }

    void resetSaves()
    {
        PlayerPrefs.DeleteAll();
    }
}
