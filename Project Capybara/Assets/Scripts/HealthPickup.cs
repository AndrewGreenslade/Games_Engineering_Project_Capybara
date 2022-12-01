using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : Pickup
{
    private string PowerName;
    private string powerupDescription;
    public float powerupDuration;
    public GameObject Scroll;

    void Start()
    {
        base.Start();
        PowerName = "Speed Boost";
        powerupDescription = "Gives capybara a temporary speed boost";
    }

    // Update is called once per frame
    void Update()
    {
    }

    public override void outputPowerupName()
    {
        base.SetPowerUpNameText(PowerName);
    }

    public override void outputPowerupDescription()
    {
        base.SetPowerUpText(powerupDescription);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            enableText();
            outputPowerupName();
            outputPowerupDescription();
            collision.GetComponent<Player>().healthIncreaseForHealthPowerup();
            this.transform.position = new Vector3(5000.0f, 5000.0f, 100.0f);
            var newScroll = Instantiate(Scroll);
            StartCoroutine(SelfDestruct(collision));
        }
    }

    IEnumerator SelfDestruct(Collider2D collision)
    {
        yield return new WaitForSeconds(powerupDuration);
        //collision.GetComponent<Player>().speedChangeResetForSpeedPowerup();
        disableText();
        Destroy(gameObject);

    }
}
