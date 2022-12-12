using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowScript : MonoBehaviour
{
    public InventoryManager inventoryManager;


    // Start is called before the first frame update
    void Start()
    {
        inventoryManager = FindObjectOfType<InventoryManager>();

  
    }

    // Update is called once per frame
    void Update()
    {
        if (inventoryManager.hasBow)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (Input.GetKey(KeyCode.E))
            {
                inventoryManager.hasBow = true;
                Destroy(this.gameObject);
            }

        }
    }
}
