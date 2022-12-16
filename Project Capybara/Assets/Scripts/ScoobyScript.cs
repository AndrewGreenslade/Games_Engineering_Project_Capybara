using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoobyScript : INpc
{
    SpriteRenderer myRenderer;
    public Animator anim;
    float speed=3;
    float timer;
    bool movingLeft = false;
    bool movingJump= false;
    bool movingIdle = false;
    public Transform m_capyTransform;
    float m_detectionRange = 4.0f;
    public GameObject shockWave;
    float ShotTimer;
    public float m_hp;


	private float targetTime;
	private float howLongForDamage = 2.0f; //seconds 


	private void Start()
    {
        myRenderer = GetComponent<SpriteRenderer>();
        timer = Time.deltaTime;
        movingIdle = true;
        ShotTimer = Random.Range(0,25);
		targetTime = howLongForDamage;

	}

	public override void Health()
    {
        if(m_hp<=0)
        {
            Destroy(gameObject);
        }
    }

    public override void movement()
    {

        if (Vector3.Distance(m_capyTransform.position, transform.position) <= m_detectionRange)
        {
            movingLeft = false;
            movingJump = true;
            movingIdle = false;
            ShockWave();
        }
           
       

        if (Vector3.Distance(m_capyTransform.position, transform.position) >= m_detectionRange)
        {
            timer += Time.deltaTime;
            Debug.Log(timer);
            if (timer < 5)
            {
                movingLeft = false;
                movingJump = false;
                movingIdle = true;
            }
            else if (timer < 10 && timer > 5)
            {
                //scooby moves left
                movingLeft = true;
                movingJump = false;
                movingIdle = false;
                myRenderer.flipX = true;
                transform.position += Vector3.left * speed * Time.deltaTime;
            }
            else if (timer > 10 && timer < 15)
            {
                //scooby moves right
                movingLeft = true;
                movingJump = false;
                movingIdle = false;
                myRenderer.flipX = false;
                transform.position += Vector3.right * speed * Time.deltaTime;
            }
            else if (timer > 15 && timer < 20)
            {
                //scooby moves right
                movingLeft = true;
                movingJump = false;
                movingIdle = false;
                myRenderer.flipX = true;
                transform.position += Vector3.up * speed * Time.deltaTime;
            }
            else if (timer > 20 && timer < 25)
            {
                //scooby moves right
                movingLeft = true;
                movingJump = false;
                movingIdle = false;
                myRenderer.flipX = false;
                transform.position += Vector3.down * speed * Time.deltaTime;
            }
            else if (timer > 25)
            {
                timer = 0;
            }
        }
    }

    void ShockWave()
    {
        ShotTimer += Time.deltaTime;
        if(gameObject!=null)
        {
            if (ShotTimer > 1)
            {
                GameObject temp = Instantiate(shockWave, transform.position, Quaternion.identity);
                Destroy(temp, 3.0f);
                ShotTimer = 0;
            }
        }
      
    }

    public override void Animate()
    {
        anim.SetBool("Left", false);
        anim.SetBool("jump", false);
        anim.SetBool("Idle", false);

        if (movingLeft == true)
        {
            anim.SetBool("Left", true);
        }
     
        if ( movingJump== true)
        {
            anim.SetBool("jump", true);
        }
        if (movingIdle == true)
        {
            anim.SetBool("Idle", true);
        }
        if(movingJump==true)
        {
            anim.SetBool("jump", true);
        }

    }

    public void Update()
    {
        movement();
        Animate();
        Health();
		targetTime -= Time.deltaTime;
        if(m_hp <= 0)
        {
            SceneManager.LoadScene("GameWon");
        }

	}

	public override void hitPlayer()
    {
        
    }

    public void degregadeHP(float t_damage)
    {
        m_hp -= t_damage;
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
       
        }

        //sdfsdfsdfsd
		if (collision.gameObject.CompareTag("realAxeOnCapy"))
		{
			
			if (targetTime <= 0.0f)
			{
              
                degregadeHP(FindObjectOfType<DamageValue>().AxeDamage);
				
				targetTime = howLongForDamage;
			}


        }


    }





}
