using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum States
{
    Idle = 0,
    RunningLeft = 1,
    RunningRight = 2,
    RunningUp = 3,
    RunningDown = 4,
    Dead = 5,
    Hurt = 6
}







public class Player : MonoBehaviour
{
    // Normal Movements Variables
    public float walkSpeed;
    private float curSpeed;
    private float maxSpeed;
    private float charSpeed = 5.0f;
    private float agility = 10.0f;
    public float sprintSpeed;
    public Animator anim;
    public States state;
    public TextMeshProUGUI levelText;


    void Start()
    {

        walkSpeed = (float)(charSpeed + (agility / 5));
        sprintSpeed = walkSpeed + (walkSpeed / 2);
        anim = GetComponent<Animator>();
        state = States.Idle;
    }

    void FixedUpdate()
    {
        curSpeed = walkSpeed;

        // Move senteces
        GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Lerp(0, Input.GetAxis("Horizontal") * curSpeed, 0.8f),
                                             Mathf.Lerp(0, Input.GetAxis("Vertical") * curSpeed, 0.8f));


        if (Input.GetKey(KeyCode.LeftShift))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Lerp(0, Input.GetAxis("Horizontal") * sprintSpeed, 0.8f),
                                                         Mathf.Lerp(0, Input.GetAxis("Vertical") * sprintSpeed, 0.8f));

        }

    }



    private void Update()
    {
        checkStatesForAnimator();




    }






    void checkStatesForAnimator()
    {

        //////
        ///Idle animations Conrolls
        //////
        if (state == States.Idle)
        {
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                state = States.RunningLeft;
            }

            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                state = States.RunningRight;

            }

            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                state = States.RunningUp;

            }

            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                state = States.RunningDown;

            }
        }




        //////
        ///Moving UP/DOWN/RIGHT/LEFT 
        //////
        if (state == States.RunningRight)
        {
            if (Input.GetKey(KeyCode.D) == false && Input.GetKey(KeyCode.RightArrow) == false)
            {
                state = States.Idle;
            }
        }
        if (state == States.RunningLeft)
        {
            if (Input.GetKey(KeyCode.A) == false && Input.GetKey(KeyCode.LeftArrow) == false)
            {
                state = States.Idle;
            }
        }

        if (state == States.RunningUp)
        {
            if (Input.GetKey(KeyCode.W) == false && Input.GetKey(KeyCode.UpArrow) == false)
            {
                state = States.Idle;
            }
        }

        if (state == States.RunningDown)
        {
            if (Input.GetKey(KeyCode.S) == false && Input.GetKey(KeyCode.DownArrow) == false)
            {
                state = States.Idle;
            }
        }

        //////
        ///LEave it hERE DONT TOUCH THIS OR HANDS WILL BE THROWN
        //////
        anim.SetInteger("State", (int)state);
    }

    public void speedChangeForSpeedPowerup()
    {
        walkSpeed = (float) (walkSpeed * 1.5);
        sprintSpeed = (float) (sprintSpeed * 1.5);
    }

    public void speedChangeResetForSpeedPowerup()
    {
        walkSpeed = (float)(walkSpeed / 1.5);
        sprintSpeed = (float)(sprintSpeed / 1.5);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Level1") || collision.gameObject.CompareTag("Level2")
            || collision.gameObject.CompareTag("Level3")|| collision.gameObject.CompareTag("Level4")
            || collision.gameObject.CompareTag("LevelBoss"))
        {
            levelText.gameObject.SetActive(false);
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Level1"))
        {
            levelText.gameObject.SetActive(true);
            levelText.text = "Press SPACE to Enter:\r\nLevel 1";
        }
        else if(collision.gameObject.CompareTag("Level2"))
        {
            levelText.gameObject.SetActive(true);
            levelText.text = "Press SPACE to Enter:\r\nLevel 2";
        }
        else if (collision.gameObject.CompareTag("Level3"))
        {
            levelText.gameObject.SetActive(true);
            levelText.text = "Press SPACE to Enter:\r\nLevel 3";
        }
        else if (collision.gameObject.CompareTag("Level4"))
        {
            levelText.gameObject.SetActive(true);
            levelText.text = "Press SPACE to Enter:\r\nLevel 4";
        }
        else if (collision.gameObject.CompareTag("LevelBoss"))
        {
            levelText.gameObject.SetActive(true);
            levelText.text = "Press SPACE to Enter:\r\nCentral Chamber";
        }
    }
}