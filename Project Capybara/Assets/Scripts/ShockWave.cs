using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockWave : MonoBehaviour
{

    int directionChoice;
    int speed = 3;

    // Start is called before the first frame update
    void Start()
    {
        directionChoice = Random.Range(1, 4);
    }

    private void FixedUpdate()
    {
        switch (directionChoice)
        {
            case 1:
                transform.position += Vector3.left * speed * Time.deltaTime;
                break;
            case 2:
                transform.position += Vector3.right * speed * Time.deltaTime;
                break;
            case 3:
                transform.position += Vector3.up * speed * Time.deltaTime;
                break;
            case 4:
                transform.position += Vector3.down * speed * Time.deltaTime;
                break;
            default:
                break;
        }
    }
}
