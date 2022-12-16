using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageValue : MonoBehaviour
{
    public float SwordDamage;
    public float AxeDamage;
    public float BowDamage;
    public float ClawsDamage;
    // Start is called before the first frame update
    void Start()
    {
        SwordDamage = 1.2f;
        AxeDamage = 1.8f;
        BowDamage = 0.7f;
        ClawsDamage = 0.7f;
    }

   
}
