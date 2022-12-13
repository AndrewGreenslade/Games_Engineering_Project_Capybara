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
    public GameObject equippedPanel;
    public List<GameObject> hotbarItems;
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
        equippedPanel = GameObject.FindGameObjectWithTag("EquippedPanel");
        axeText = GameObject.FindGameObjectWithTag("AxeText").GetComponent<TextMeshProUGUI>();
        swordText = GameObject.FindGameObjectWithTag("SwordText").GetComponent<TextMeshProUGUI>();
        bowText = GameObject.FindGameObjectWithTag("BowText").GetComponent<TextMeshProUGUI>();
        damageText = GameObject.FindGameObjectWithTag("DamageText").GetComponent<TextMeshProUGUI>();

        foreach (var weapon in hotbarItems)
        {
            weapon.SetActive(false);
        }

        hotbarItems[0].SetActive(true);

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
                equippedPanel.transform.localPosition = hotbarItems[0].transform.localPosition;
                damageText.text = "Damage Output:\r\n0.7";
                break;
            case Weapons.Sword:
                equippedPanel.transform.localPosition = hotbarItems[1].transform.localPosition;
                damageText.text = "Damage Output:\r\n1.2";
                break;
            case Weapons.Axe:
                equippedPanel.transform.localPosition = hotbarItems[2].transform.localPosition;
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
                hotbarItems[2].SetActive(true);
            }
            if (hasSword)
            {
                swordText.enabled = true;
                hotbarItems[1].SetActive(true);
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

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            equippedWeapon = Weapons.Claws;
            axeText.color = Color.red;
            swordText.color = Color.red;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && hasSword == true)
        {
            equippedWeapon = Weapons.Sword;
            swordText.color = Color.green;
            axeText.color = Color.red;

        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && hasAxe == true)
        {
            equippedWeapon = Weapons.Axe;
            axeText.color = Color.green;
            swordText.color = Color.red;
        }


    }
}

