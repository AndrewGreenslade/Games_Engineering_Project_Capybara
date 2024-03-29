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
  
    private bool m_movingIdle;
    int speed = 2;
    float timerForAttackAlive;
    float m_detectionRange = 3.5f;
    public Vector3 relativePos;
    private bool playerHasAttacked = false;
    public SpriteRenderer myRenderer;
    public float hp;


	private float targetTime;
	private float howLongForDamage = 2.0f; //seconds 

	private void Start()
    {
        myRenderer = GetComponent<SpriteRenderer>();
		targetTime = howLongForDamage;
        m_capyTransform = FindObjectOfType<Player>().transform;
    }

    public override void Health()
    {
        if(hp<=0)
        {
            Destroy(gameObject);
        }
    }

    public override void movement()
    {

        timerForAttackAlive -= Time.deltaTime;
        AttackPlayer = true;
        if (Vector3.Distance(m_capyTransform.position, transform.position) <= m_detectionRange)
        {
            relativePos = m_capyTransform.position - transform.position;

            if(relativePos.y < 0)
            {
                transform.position -= Vector3.up * speed * Time.deltaTime;
            }
            if (relativePos.y > 0)
            {
                transform.position -= Vector3.down * speed * Time.deltaTime;
            }

            if (relativePos.x > 0)
            {
               //flip the sprite here
                transform.position -= Vector3.left * speed * Time.deltaTime;
                myRenderer.flipX = false;

                m_movingRight = false;
                m_movingIdle = false;
            }
            if (relativePos.x < 0)
            {
                //flipped anim cause it will look better when shooting
                transform.position -= Vector3.right * speed * Time.deltaTime;
                myRenderer.flipX = true;

                m_movingRight = true;
                m_movingIdle = false;
            }
        }
        else
        {
            m_movingRight = false;
            m_movingIdle = true;
        }
        if (AttackPlayer == true)
        {
            hitPlayer();
            AttackPlayer = false;
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
        Health();
        this.Animate();
		targetTime -= Time.deltaTime;

	}

	public override void hitPlayer()
    {
        if (playerHasAttacked == false)
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
            playerHasAttacked = true;
        }
    }

  
    public void degregadeHP(float t_degrade)
    {
        hp -= t_degrade;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
      

        if (collision.gameObject.CompareTag("attack"))
        {
            degregadeHP(FindObjectOfType<DamageValue>().ClawsDamage);
        }

        if (collision.gameObject.CompareTag("realSwordOnCapy"))
        {

            if (targetTime <= 0.0f)
            {

                degregadeHP(FindObjectOfType<DamageValue>().SwordDamage);

                targetTime = howLongForDamage;
            }
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("realAxeOnCapy"))
        {

            if (targetTime <= 0.0f)
            {

                degregadeHP(FindObjectOfType<DamageValue>().AxeDamage);

                targetTime = howLongForDamage;
            }
            Destroy(collision.gameObject);
            Destroy(gameObject);

        }




    }




}
