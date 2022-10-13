using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rangedEnemy : INpc
{
    public Animator anim;
    public Rigidbody2D rb;

    private bool m_movingLeft=true;
    private bool m_movingRight;
    private bool m_movingDown;
    private bool m_movingUp;
    int speed = 2;
    float counter=0;
    Vector2 m_vel;

    public override void Health()
    {
        Debug.Log("ranged enemy movement");
    }

    public override void movement()
    {

       //Time.deltaTime.
       //     if ())
       //     {
       //             m_vel= transform.position += Vector3.up * speed * Time.deltaTime;
       //     }

       //     if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
       //     {
       //         transform.position += Vector3.right * speed * Time.deltaTime;
       //     }

       //     if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
       //     {
       //         transform.position += Vector3.left * speed * Time.deltaTime;

       //     }
        
        if (m_vel.x>0)
        {
            //moving right
            m_movingUp = false;
            m_movingDown = false;
            m_movingLeft = false;
            m_movingRight = true;
        }
        else if(m_vel.x<-0)
        {
            //moving left
            m_movingUp = false;
            m_movingDown = false;
            m_movingLeft = true;
            m_movingRight = false;
        }

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
    }

    public override void Die()
    {
        Debug.Log("v enemy Die");
    }

    public override void interact()
    {
        Debug.Log("ranged enemy interact");
    }

    public override void hitPlayer()
    {
        Debug.Log("ranged enemy hitplayer");
    }


}
