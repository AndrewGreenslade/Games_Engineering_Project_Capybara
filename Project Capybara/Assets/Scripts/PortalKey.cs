using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalKey : MonoBehaviour
{
    bool isPickedUp = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && isPickedUp == false)
        {
            isPickedUp = true;
            FindObjectOfType<PortalDoor>().openDoor();
            FindObjectOfType<InventoryManager>().keysStored++;
            Destroy(gameObject);
        }
    }
}
