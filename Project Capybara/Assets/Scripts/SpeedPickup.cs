using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPickup : Pickup
{
    private string PowerName;
    private string powerupDescription;
    public float powerupDuration;
    public GameObject Scroll;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        PowerName = "Speed Boost";
        powerupDescription = "Gives capybara a temporary speed boost";
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
            collision.GetComponent<Player>().speedChangeForSpeedPowerup();
            this.transform.position = new Vector3(5000.0f, 5000.0f, 100.0f);
            var newScroll = Instantiate(Scroll);
            StartCoroutine(SelfDestruct(collision));
        }
    }

    IEnumerator SelfDestruct(Collider2D collision)
    {
        yield return new WaitForSeconds(powerupDuration);
        collision.GetComponent<Player>().speedChangeResetForSpeedPowerup();
        Destroy(gameObject);
    }
}
