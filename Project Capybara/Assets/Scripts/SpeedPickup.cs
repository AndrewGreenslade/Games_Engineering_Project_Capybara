using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPickup : Pickup
{
    private GameObject Speed;
    private string PowerName;
    private string powerupDescription;
    private bool hasCollisionHappened;
    private float powerupDuration;
    // Start is called before the first frame update
    void Start()
    {
        Speed = GetComponent<GameObject>();
        PowerName = "Speed Boost";
        powerupDescription = "Gives capybara a temporary speed boost";
        hasCollisionHappened = false;
        powerupDuration = 5;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public override void outputPowerupName()
    {
        Debug.Log(" You picked up " + PowerName);
    }

    public override void outputPowerupDescription()
    {
        Debug.Log(powerupDescription);
    }

    public override void powerupImplementation()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (hasCollisionHappened == false)
        {
            if (collision.tag == "Player")
            {
                outputPowerupName();
                outputPowerupDescription();
                collision.GetComponent<Player>().speedChangeForSpeedPowerup();
                this.transform.position = new Vector3(5000.0f, 5000.0f, 100.0f);
                StartCoroutine(SelfDestruct(collision));
            }
        }
    }

    IEnumerator SelfDestruct(Collider2D collision)
    {
        yield return new WaitForSeconds(powerupDuration);
        collision.GetComponent<Player>().speedChangeResetForSpeedPowerup();
        Destroy(gameObject);
    }
}
