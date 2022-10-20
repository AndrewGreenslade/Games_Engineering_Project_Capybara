using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPickup : Pickup
{
    private GameObject Speed;
    private string PowerName;
    private string powerupDescription;
    // Start is called before the first frame update
    void Start()
    {
        Speed = GetComponent<GameObject>();
        PowerName = "Speed Boost";
        powerupDescription = "Gives capybara a temporary speed boost";
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
        if (collision.tag == "Player")
        {
            Debug.Log("player picked up speed");
            Destroy(this.gameObject);
            collision.GetComponent<Player>().speedChangeForSpeedPowerup();
        }
    }
}
