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
    private Player p;
    private InventoryManager inventory;


    private void Start()
    {      
        p = player.GetComponent<Player>();
        inventory = player.GetComponent<InventoryManager>();
        //PlayerPrefs.DeleteAll();
    }
    public void setSaveValues()
    {

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

    public void SaveGame()
    {
        PlayerPrefs.SetFloat("HealthSaveFloat", healthToSave);
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

            //p.playerHealth = PlayerPrefs.GetInt("HealthSaveFloat");
            if (PlayerPrefs.GetString("AxeSaveString") == "FALSE")
            {
                inventory.hasAxe = false;
            }
            else
            {
                inventory.hasAxe = true;
            }

            if (PlayerPrefs.GetString("SwordSaveString") == "FALSE")
            {
                inventory.hasSword = false;
            }
            else
            {
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

            Debug.Log(PlayerPrefs.GetFloat("HealthSaveFloat"));
            Debug.Log(PlayerPrefs.GetString("AxeSaveString"));
            Debug.Log(PlayerPrefs.GetString("SwordSaveString"));
            Debug.Log(PlayerPrefs.GetString("BowSaveString"));
    }
        else
            Debug.LogError("There is no save data!");
    }
}
