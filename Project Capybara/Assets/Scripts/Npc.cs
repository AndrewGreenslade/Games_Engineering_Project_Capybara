using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class INpc : MonoBehaviour
{

    public abstract void Health();    

    public abstract void movement();

  

    public abstract void Animate();
  

    public abstract void hitPlayer();
    
    
}


