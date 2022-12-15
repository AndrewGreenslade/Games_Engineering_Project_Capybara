using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioCheckerScript : MonoBehaviour
{
    public GameObject audioManager;
    // Start is called before the first frame update
    void Start()
    {
            Instantiate(audioManager);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
