using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class INpc : MonoBehaviour
{

    public abstract void Health();    

    public abstract void movement();

    public abstract void Die();
   
    public abstract void interact();

    public abstract void hitPlayer();
    
    
}


