using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followerEnemy : INpc
{
    public override void Health()
    {
        Debug.Log("follower Enemy movement");
    }

    public override void movement()
    {
        Debug.Log("follower enemy movement");
    }

    public override void Die()
    {
        Debug.Log("follower enemy Die");
    }

    public override void interact()
    {
        Debug.Log("follower enemy interact");
    }

    public override void hitPlayer()
    {
        Debug.Log("follower enemy hitplayer");
    }

   
}
