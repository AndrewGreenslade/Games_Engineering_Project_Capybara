using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerPickup : Pickup
{
    private GameObject Power;
    private string PowerName;
    private string powerupDescription;
    // Start is called before the first frame update
    void Start()
    {
        Power = GetComponent<GameObject>();
        PowerName = "Power Buff";
        powerupDescription = "Gives capybara an attack boost";
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
}
