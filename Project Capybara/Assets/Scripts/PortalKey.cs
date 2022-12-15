using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalKey : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            FindObjectOfType<PortalDoor>().openDoor();
            FindObjectOfType<InventoryManager>().keysStored += 1;
            Destroy(gameObject);
        }
    }
}
