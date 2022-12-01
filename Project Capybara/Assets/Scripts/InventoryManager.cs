using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class InventoryManager : MonoBehaviour
{
    private GameObject inventoryCanvas;
    private TextMeshProUGUI axeText;
    private TextMeshProUGUI swordText;
    private TextMeshProUGUI bowText;
    public static InventoryManager instance;
    private bool hasAxe = false;
    private bool hasSword = false;
    private bool hasBow = false;

    // Start is called before the first frame update
    void Start()
    {
        inventoryCanvas = GameObject.FindGameObjectWithTag("InventoryCanvas");
        axeText = GameObject.FindGameObjectWithTag("AxeText").GetComponent<TextMeshProUGUI>();
        swordText = GameObject.FindGameObjectWithTag("SwordText").GetComponent<TextMeshProUGUI>();
        bowText = GameObject.FindGameObjectWithTag("BowText").GetComponent<TextMeshProUGUI>();

        axeText.enabled = false;
        swordText.enabled = false;
        bowText.enabled = false;

        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }


        inventoryCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
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

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Axe"))
        {
            if (Input.GetKey(KeyCode.E))
            {
                hasAxe = true;
            }

        }
        else if (collision.gameObject.CompareTag("Sword"))
        {
            if (Input.GetKey(KeyCode.E))
            {
                hasSword = true;
            }

        }
        else if (collision.gameObject.CompareTag("Bow"))
        {
            if (Input.GetKey(KeyCode.E))
            {
                hasBow = true;
            }

        }
    }
}

