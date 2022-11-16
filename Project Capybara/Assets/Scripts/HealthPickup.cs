using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : Pickup
{
    private GameObject Health;
    private string PowerUpName;
    private string powerupDescription;
    // Start is called before the first frame update
    void Start()
    {
        Health = GetComponent<GameObject>();
        PowerUpName = "Health Regeneration";
    }

    // Update is called once per frame
    void Update()
    {
    }

    public override void outputPowerupName()
    {
        Debug.Log(" You picked up " + PowerUpName);
    }

    public override void outputPowerupDescription()
    {
        Debug.Log(powerupDescription);
    }
}
