using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public enum Weapons
{
    Claws,
    Sword,
    Axe,
    Bow
}

public class InventoryManager : MonoBehaviour
{

    public Weapons equippedWeapon;

    private GameObject inventoryCanvas;
    private TextMeshProUGUI damageText;
    private TextMeshProUGUI axeText;
    private TextMeshProUGUI swordText;
    private TextMeshProUGUI bowText;
    public static InventoryManager instance;
    public bool hasAxe = false;
    public bool hasSword = false;
    public bool hasBow = false;

    // Start is called before the first frame update
    void Start()
    {
        inventoryCanvas = GameObject.FindGameObjectWithTag("InventoryCanvas");
        axeText = GameObject.FindGameObjectWithTag("AxeText").GetComponent<TextMeshProUGUI>();
        swordText = GameObject.FindGameObjectWithTag("SwordText").GetComponent<TextMeshProUGUI>();
        bowText = GameObject.FindGameObjectWithTag("BowText").GetComponent<TextMeshProUGUI>();
        damageText = GameObject.FindGameObjectWithTag("DamageText").GetComponent<TextMeshProUGUI>();

        axeText.enabled = false;
        swordText.enabled = false;
        bowText.enabled = false;
        damageText.enabled = true;

        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        equippedWeapon = Weapons.Claws; //default attack

        inventoryCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        switch(equippedWeapon)
        {
            case Weapons.Claws:
                damageText.text = "Damage Output:\r\n0.7";
                break;
            case Weapons.Sword:
                damageText.text = "Damage Output:\r\n1.2";
                break;
            case Weapons.Axe:
                damageText.text = "Damage Output:\r\n1.8";
                break;
            case Weapons.Bow:
                damageText.text = "Damage Output:\r\n0.7";
                break;
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            inventoryCanvas.SetActive(true);
            if(hasAxe)
            {
                axeText.enabled = true;
            }
            if (hasSword)
            {
                swordText.enabled = true;
            }
            if (hasBow)
            {
                bowText.enabled = true;
            }
        }
        else if (Input.GetKeyUp(KeyCode.Tab))
        {
            inventoryCanvas.SetActive(false);
            
        }
    }
}

