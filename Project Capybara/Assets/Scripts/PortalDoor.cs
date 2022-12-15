using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalDoor : MonoBehaviour
{
    public bool hasKey = false;
  
    public void openDoor()
    {
        GetComponent<Animator>().SetBool("DoorOpen", true);
        hasKey = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (hasKey)
            {
                if (Input.GetKey(KeyCode.F))
                {
                    SceneManager.LoadScene("HubWorld");
                }
            }
        }
    }
}
