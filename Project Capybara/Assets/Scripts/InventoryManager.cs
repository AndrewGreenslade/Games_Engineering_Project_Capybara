using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    private GameObject inventoryCanvas;
    public static InventoryManager instance;
    private bool hasAxe = false;
    private bool hasSword = false;
    private bool hasBow = false;

    // Start is called before the first frame update
    void Start()
    {


        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        inventoryCanvas = GameObject.FindGameObjectWithTag("InventoryCanvas");
        inventoryCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            inventoryCanvas.SetActive(true);
        }
        else if (Input.GetKeyUp(KeyCode.Tab))
        {
            inventoryCanvas.SetActive(false);
        }
    }
}
