using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meleeEnemy : INpc
{
    public Animator anim;
    public Transform m_capyTransform;

 
    private bool m_movingRight;
  
    private bool m_movingIdle;
    int speed = 2;
  
    float m_detectionRange = 3.5f;
    public Vector3 relativePos;
  
   
   
    public override void Health()
    {
        Debug.Log("cat health");
    }

    public override void movement()
    {

        
        if (Vector3.Distance(m_capyTransform.position, transform.position) <= m_detectionRange)
        {

            relativePos = m_capyTransform.position - transform.position;
            if(relativePos.y < 0)
            {
                transform.position -= Vector3.up * speed * Time.deltaTime;
              
                m_movingRight = true;
                m_movingIdle = false;
            }
             if (relativePos.y > 0)
            {
                transform.position -= Vector3.down * speed * Time.deltaTime;
                //moving right
                m_movingRight = true;
                m_movingIdle = false;
            }

            if (relativePos.x > 0)
            {
               //flip the sprite here
                transform.position -= Vector3.left * speed * Time.deltaTime;
              
                m_movingRight = true;
                m_movingIdle = false;
            }
            if (relativePos.x < 0)
            {
                //flipped anim cause it will look better when shooting
                transform.position -= Vector3.right * speed * Time.deltaTime;
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);

                 
                m_movingRight = true;
                m_movingIdle = false;
            }


        }
        else
        {
       
            m_movingRight = false;
            m_movingIdle = true;
        }

    }
    public override void Animate()
    {
       
        anim.SetBool("isMovingRight", false);
    
      
        anim.SetBool("isIdle", false);


       
      
        if (m_movingRight == true)
        {
            anim.SetBool("isMovingRight", true);
        }
        if (m_movingIdle == true)
        {
            anim.SetBool("isIdle", true);
        }
    }
    public void Update()
    {
        movement();

        this.Animate();
    }

    public override void Die()
    {
        Debug.Log("cat enemy Die");
    }

    public override void interact()
    {
        Debug.Log("cat enemy interact");
    }

    public override void hitPlayer()
    {
        Debug.Log("cat enemy hitplayer");
    }

}
