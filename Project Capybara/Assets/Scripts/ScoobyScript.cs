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
    public int m_hp;


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

    public void degregadeHP(int t_damage)
    {
        m_hp -= t_damage;
    }




	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("attack"))
		{
            SceneManager.LoadScene("GameWon");
            // Destroy(collision.gameObject);
            // do collision here 
            Debug.Log("Player hits scoob");

            /// WHEN HEALTH IS ADDED PUT THIS SHIT IN THE IF STATEMENT WHEN HEALT IS 0 SO IT DELETES ENEMY, 
            /// 
            degregadeHP(1);
            //// enemyCat health = enemy lama healt - sword daamge;
            /// makke a simple timer from here https://answers.unity.com/questions/351420/simple-timer-1.html
            /// and just make it so that enemy will lose health when it collides wvery 2 seconds
            /// 

            //Remove this after fix
          

            //fIX THE DAMNCODE AND REMOVE THIS AND MAKE IT PROPERLY
            Destroy(collision.gameObject);
			Destroy(gameObject);
        }



		if (collision.gameObject.CompareTag("realSwordOnCapy"))
		{
			// Destroy(collision.gameObject);
			// do collision here 
			Debug.Log("Player with sword hits Cat");

			if (targetTime <= 0.0f)
			{
                // DO DAMAGE HERE

                /// WHEN HEALTH IS ADDED PUT THIS SHIT IN THE IF STATEMENT WHEN HEALT IS 0 SO IT DELETES ENEMY, 
                /// 
                degregadeHP(2);
				//// enemyCat health = enemy lama healt - sword daamge;
				/// and just make it so that enemy will lose health when it collides wvery 2 seconds

				// reset timer 
				targetTime = howLongForDamage;
			}

            //fIX THE DAMNCODE AND REMOVE THIS AND MAKE IT PROPERLY

            //Remove this after fix
            SceneManager.LoadScene("GameWon");

            // if(health is zero=)
            // kill enemy 
            // put theses in a statement 
            Destroy(collision.gameObject);
			Destroy(gameObject);
        }

        //sdfsdfsdfsd
		if (collision.gameObject.CompareTag("realAxeOnCapy"))
		{
			// Destroy(collision.gameObject);
			// do collision here 
			Debug.Log("Player with axe hits Cat");

			if (targetTime <= 0.0f)
			{
                // DO DAMAGE HERE

                /// WHEN HEALTH IS ADDED PUT THIS SHIT IN THE IF STATEMENT WHEN HEALT IS 0 SO IT DELETES ENEMY, 
                /// 
                degregadeHP(4);
				//// enemyCat health = enemy lama healt - sword daamge;
				/// and just make it so that enemy will lose health when it collides wvery 2 seconds

				// reset timer 
				targetTime = howLongForDamage;
			}

            // if(health is zero=)
            // kill enemy 
            // put theses in a statement 

            //Remove this after fix
            SceneManager.LoadScene("GameWon");

            Destroy(collision.gameObject);
			Destroy(gameObject);


        }

        if (collision.gameObject.CompareTag("Player"))
        {
                degregadeHP(4);
     
        }



    }





}
