using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spitMovement : MonoBehaviour
{
    private int speed = 2;
    Vector3 direction;

    public void setDir(Vector3 NewDir)
    {
        direction = NewDir;
    }

    private void FixedUpdate()
    {
        transform.position += direction * speed * Time.deltaTime;
    }
}
