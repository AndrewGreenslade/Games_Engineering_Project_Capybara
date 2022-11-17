using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollScript : MonoBehaviour
{
    const float OPACITY_INCREASE = 0.01f;
    float opacity = 0.0f;
    bool isScrollDespawning = false;
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<SpriteRenderer>().material.color = new Color(1.0f, 1.0f, 1.0f, opacity);

        StartCoroutine(startSelfDestruct());
    }

    // Update is called once per frame
    void Update()
    {
        if (isScrollDespawning == false)
        {
            increaseVisibility();
        }
        else
        {
            decreaseVisibility();
        }
        this.GetComponent<SpriteRenderer>().material.color = new Color(1.0f, 1.0f, 1.0f, opacity);

        if (opacity == 0 && isScrollDespawning == true)
        {
            Destroy(gameObject);
        }
    }

    void increaseVisibility()
    {
        opacity += OPACITY_INCREASE;

        if (opacity > 1)
        {
            opacity = 1;
        }
    }

    void decreaseVisibility()
    {
        opacity -= OPACITY_INCREASE;

        if (opacity < 0)
        {
            opacity = 0;
        }
    }

    IEnumerator startSelfDestruct()
    {
        yield return new WaitForSeconds(5);
        isScrollDespawning = true;
    }
}
