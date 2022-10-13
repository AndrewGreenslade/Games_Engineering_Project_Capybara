using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rangedEnemy : INpc
{
    public override void Health()
    {
        Debug.Log("ranged enemy movement");
    }

    public override void movement()
    {
        Debug.Log("ranged enemy movement");
    }

    public override void Die()
    {
        Debug.Log("v enemy Die");
    }

    public override void interact()
    {
        Debug.Log("ranged enemy interact");
    }

    public override void hitPlayer()
    {
        Debug.Log("ranged enemy hitplayer");
    }


}
