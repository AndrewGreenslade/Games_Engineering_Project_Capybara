using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rangedEnemy : INpc
{
    public Animator anim;
    public Rigidbody2D rb;
    public Transform m_capyTransform;

    private bool m_movingLeft=true;
    private bool m_movingRight;
    private bool m_movingDown;
    private bool m_movingUp;
    int speed = 2;
    float counter=0;
    Vector2 m_vel;
    float m_detectionRange = 3.5f;
    public Vector3 relativePos;
    public GameObject spitObj;
    public int ShotTimer=0;
    spitMovement spit;
    public override void Health()
    {
        Debug.Log("ranged enemy movement");
    }

    public override void movement()
    {
       if(Vector3.Distance(m_capyTransform.position,transform.position)<= m_detectionRange)
        {
           
            relativePos = m_capyTransform.position - transform.position;

            if (ShotTimer < 1)
            {
                spit = Instantiate(spitObj, transform.position, Quaternion.identity).GetComponent<spitMovement>();
                spit.setDir(relativePos);
                Destroy(spit.gameObject, 1.0f);

            }
            ShotTimer++;
            if (ShotTimer>500)
            {
                ShotTimer=0;
            }

           

          

            if (relativePos.y < 0)
            {
                transform.position += Vector3.up * speed * Time.deltaTime;
                m_movingUp = true;
                m_movingDown = false;
                m_movingLeft = false;
                m_movingRight = false;
            }
            else if (relativePos.x > 0)
            {
                transform.position += Vector3.down * speed * Time.deltaTime;
                //moving right
                m_movingUp = false;
                m_movingDown = true;
                m_movingLeft = false;
                m_movingRight = false;
            }

            if (relativePos.x > 0)
            {
                //flipped anim cause it will look better when shooting
                transform.position += Vector3.left * speed * Time.deltaTime;
                m_movingUp = false;
                m_movingDown = false;
                m_movingLeft = false;
                m_movingRight = true;
            }
            else if (relativePos.x < 0)
            {
                //flipped anim cause it will look better when shooting
                transform.position += Vector3.right * speed * Time.deltaTime;
                //moving right
                m_movingUp = false;
                m_movingDown = false;
                m_movingLeft = true;
                m_movingRight = false;
            }


        }






        rb.AddForce(m_vel);
        
       
        //if(distanceVec.y < allowedSpace)
        //{
        //    m_vel = transform.position += Vector3.up * speed * Time.deltaTime;
        //}
        //else if(distanceVec.y > allowedSpace)
        //{
        //    m_vel = transform.position += Vector3.down * speed * Time.deltaTime;
        //}
       
      
        
 

        if (m_vel.y > 0)
        {
            //moving up
            m_movingUp = true;
            m_movingDown = false;
            m_movingLeft = false;
            m_movingRight = false;
        }
        else if (m_vel.x < -0)
        {
            //moving down
            m_movingDown=true;
            m_movingUp = false; 
            m_movingLeft = false;
            m_movingRight =false;
        }

    }
    public override void Animate()
    {
        anim.SetBool("isLeft", false);
        anim.SetBool("isRight", false);
        anim.SetBool("isUp", false);
        anim.SetBool("isDown", false);

        if (m_movingLeft==true)
        {
            anim.SetBool("isLeft", true);
        }
        if (m_movingDown == true)
        {
            anim.SetBool("isDown", true);
        }
        if (m_movingUp == true)
        {
            anim.SetBool("isUp", true);
        }
        if (m_movingRight == true)
        {
            anim.SetBool("isRight", true);
        }


    }
    public void Update()
    {
        this.Animate();
        movement();
    }


    public override void hitPlayer()
    {
        Debug.Log("ranged enemy hitplayer");
    }


}
