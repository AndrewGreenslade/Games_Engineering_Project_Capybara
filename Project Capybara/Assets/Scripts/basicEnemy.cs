using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basicEnemy : INpc
{

    public override void Health()
    {
        Debug.Log("basic enemy movement");
    }

    public override void  movement()
    {
        Debug.Log("basic enemy movement");
    }

    public override void Die()
    {
        Debug.Log("basic enemy Die");
    }

    public override void interact()
    {
        Debug.Log("basic enemy interact");
    }

    public override void hitPlayer()
    {
        Debug.Log("basic enemy hitplayer");
    }
    public override void Animate()
    {
        throw new System.NotImplementedException();
    }

}
