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

  


    public override void hitPlayer()
    {
        Debug.Log("follower enemy hitplayer");
    }
    public override void Animate()
    {
        Debug.Log("follower enemy hitplayer");
    }


}
