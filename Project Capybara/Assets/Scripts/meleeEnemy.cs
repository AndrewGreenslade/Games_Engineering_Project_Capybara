using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meleeEnemy : INpc
{
    public Animator anim;
    public Transform m_capyTransform;
    public GameObject attackObject;
    private bool AttackPlayer = false;
    private bool m_movingRight;
    private bool isMovingLeft;
    private bool m_movingIdle;
    private bool flip = false;
    private bool playerHasAttacked = false;
    int speed = 2;
    float timerForAttackAlive;
    float m_detectionRange = 3.5f;
    public Vector3 relativePos;

   
   


    public SpriteRenderer myRenderer;

    private void Start()
    {
        myRenderer = GetComponent<SpriteRenderer>();
    }


    public override void Health()
    {
        Debug.Log("cat health");
    }

    public override void movement()

    {


        if (Vector3.Distance(m_capyTransform.position, transform.position) <= m_detectionRange)
        {

            timerForAttackAlive -= Time.deltaTime;
            AttackPlayer = true;


            {
                if (Vector3.Distance(m_capyTransform.position, transform.position) <= m_detectionRange)
                {

                    relativePos = m_capyTransform.position - transform.position;

                    if (relativePos.y < 0)
                    {


                        transform.position += Vector3.down * speed * Time.deltaTime;

                    }
                    else if (relativePos.y > 0)
                    {

                        transform.position += Vector3.up * speed * Time.deltaTime;


                        transform.position -= Vector3.up * speed * Time.deltaTime;
                    }
                    if (relativePos.y > 0)
                    {
                        transform.position -= Vector3.down * speed * Time.deltaTime;

                    }

                    if (relativePos.x > 0)
                    {

                        //cat is right the player
                        //flip the sprite here
                        transform.position += Vector3.right * speed * Time.deltaTime;
                        m_movingRight = true;

                        //flip the sprite here
                        transform.position -= Vector3.left * speed * Time.deltaTime;
                        myRenderer.flipX = false;

                        m_movingRight = false;

                        m_movingIdle = false;
                        isMovingLeft = false;
                    }
                    else if (relativePos.x < 0)
                    {
                        //cat is right the player
                        //flipped anim cause it will look better when shooting

                        transform.position += Vector3.left * speed * Time.deltaTime;
                        if (flip == false)
                        {
                            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
                            flip = true;
                        }

                        isMovingLeft = true;
                        m_movingRight = false;

                        transform.position -= Vector3.right * speed * Time.deltaTime;
                        myRenderer.flipX = true;

                        m_movingRight = true;

                        m_movingIdle = false;
                    }
                }
                else
                {

                    timerForAttackAlive = 0.5f;


                    m_movingRight = false;
                    m_movingIdle = true;
                    isMovingLeft = false;
                }
                if (flip == true && m_movingIdle == true)
                {
                    transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
                    flip = false;
                }
                if (AttackPlayer == true)
                {
                    hitPlayer();
                    AttackPlayer = false;
                }
            }
        }
    }

    public override void Animate()
    {

        anim.SetBool("isIdle", false);
        anim.SetBool("isMovingRight", false);
       
        if (isMovingLeft==true)
        {
            //isMovingLeft = true;
         
            anim.SetBool("isMovingRight", false);
            anim.SetBool("isIdle", false);

        }

        anim.SetBool("isMovingRight", false);
        anim.SetBool("isIdle", false);  
      

        if (m_movingRight == true)
        {
            anim.SetBool("isMovingRight", true);
            anim.SetBool("isIdle", false);
            isMovingLeft = false;
            flip = false;
        }
        if (m_movingIdle == true)
        {
            anim.SetBool("isIdle", true);
            anim.SetBool("isMovingRight", false);
            isMovingLeft = false;
            flip = false;
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
       
        if (playerHasAttacked==false)
        {
            GameObject cloneAttack = Instantiate(attackObject, gameObject.transform.position, Quaternion.identity);
            Destroy(cloneAttack, 0.5f);
            playerHasAttacked = true;
            timerForAttackAlive = 1.5f;

        }
        
         

        if (timerForAttackAlive <= 0.03f)
        {
            playerHasAttacked = false;
        }
        else
        {
            playerHasAttacked= true;
        }

    }
}
