﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public enum States
{
    Idle = 0,
    RunningLeft = 1,
    RunningRight = 2,
    RunningUp = 3,
    RunningDown = 4,
    Attack = 5,
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
    public float timerForAttackAlive = 0.5f;
    private int attackDirection = 0;
    public float playerHealth = 2.5f;
    public float sprintSpeed;
    private bool isHealthAdded = false;
    public Animator anim;
    public States state;

    private GameObject cloneAttack;
    private GameObject healthClone;
    public Vector3 originalLocalScale;
    public GameObject attackObject;
    public GameObject swordPrefab;
    public GameObject axePrefab;

    public GameObject heartObject;
    public bool playerHasAttacked = false;
    //public bool levelTwoUnlock = false;
    //public bool levelThreeUnlock = false;
    //public bool levelFourUnlock = false;
    //public bool bossUnlock = false;
    public GameObject saveObject;
    public GameObject audio;

    public InventoryManager im;


    void Start()
    {
        walkSpeed = (float)(charSpeed + (agility / 5));
        sprintSpeed = walkSpeed + (walkSpeed / 2);
        anim = GetComponent<Animator>();
        state = States.Idle;
        healthClone = Instantiate(heartObject, new Vector3(0, 0, 0), Quaternion.identity);
        originalLocalScale = healthClone.transform.localScale;
        im = FindObjectOfType<InventoryManager>();
        audio = GameObject.FindGameObjectWithTag("AudioManager");


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
        if (playerHealth >= 0.0f)
        {
            timerForAttackAlive -= Time.deltaTime;

        checkStatesForAnimator();
        attack();

        positionHealth();
        im.keysText.text = "Keys Collected:\r\n" + im.keysStored;

        }
        else if (playerHealth <= 0.0f)
        {
            gameObject.GetComponent<Renderer>().enabled = false;
            if (Input.GetKey(KeyCode.R))
            {
                SceneManager.LoadScene("Main Menu");
                Destroy(im.gameObject);
            }
        }


        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

    }

    private void positionHealth()
    {
        float distanceFromCamera = Camera.main.nearClipPlane; // Change this value if you want
        Vector3 topLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, distanceFromCamera));
     
        healthClone.transform.position = new Vector3(topLeft.x + 1, topLeft.y - 0.8f, 0);
        if (playerHealth >= 0.0f)
        {
            healthClone.gameObject.transform.localScale = new Vector3(playerHealth, playerHealth, 0);
        }


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
                attackDirection = 3;
            }

            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                state = States.RunningRight;
                attackDirection = 1;

            }

            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                state = States.RunningUp;
                attackDirection = 4;

            }

            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                state = States.RunningDown;
                attackDirection = 2;

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
            im.levelText.enabled = false;
        }
        if (collision.gameObject.CompareTag("Save Bench"))
        {
            im.saveText.enabled = false;
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Level1"))
        {
            im.levelText.enabled = true;
            im.levelText.text = "Press 'E' to Enter:\r\nLevel 1";
            if (Input.GetKey(KeyCode.E))
            {
                if (im.keysStored < 1)
                {
                    SceneManager.LoadScene("Level1");
                    im.levelText.enabled = false;
                }
            }

        }
        else if(collision.gameObject.CompareTag("Level2"))
        {
            im.levelText.enabled = true;

            if (im.keysStored >= 1)
            {
                im.levelText.text = "Press 'E' to Enter:\r\nLevel 2";
                if (Input.GetKey(KeyCode.E))
                {
                    if (im.keysStored < 2)
                    {
                        SceneManager.LoadScene("Level2");
                        im.levelText.enabled = false;
                    }
                }
            }
            else
            {
                im.levelText.text = "P̸͓̺͎͍̹͖̣̦̠̯͇̜̱̗̹͕̟̮̳̤͎͕̪̹̮̎́͜ŕ̴̢̥͈̭̟̤͍͋͗̅͌͛̀́͂̐̈́̂̈́̀͐̓̓̐́̎̍̓̀̀͊̏̑͒̉̇́̑̔͌͘͘͘̕̚̕͜͠͠ͅẽ̸̡̡̛̛̳̜̝͍̜̝̹̦̔̍̋̀̊͌̃̎͗̾̌̂̇̅̿̈̏͂̓̎͂̑̽̃̄̋̓̑̃̅͗͐̃̀̋̕͜͝ș̷̡̧̡̧̡̢̛̗̘͓̱̤͇͎̮͖͙͍̞̲͖͙͉͚̟̻̘̘̉̀̌̐̓̓͊̀̀͆́͜͝ͅș̸̢̧̨̨̡̧̡̨̛̼̥̘̤͈̤̳͖̻̘͚͈͔̘̲̰̬͈̣͔͔̬̠̹̻̟̤͂̏̔͆̔̉̿̊̊͊͜͝ ̷̨̧̛̦̣̤͕̳̤̗̙͕̱̠͚̭͙̩̼̌́̎͗͛͗͐͛̏͑̆̃̉̈́̍̎̐͋͠͝S̵̨̧̛̳̱̗̤̣̺̲̩̩͔̻̩̯͙̪͇̘̟̟̝͋͗̿͌͛̒̽͌̌̒̇́͗̅̇̀̿̌̈́̅̇̍̃̈́͛̑́̓͒̈͊̀̇̈́͂̃́̋͒̀̕͘͘̚͝͠͝͠͠ͅP̸̟͈̈́͛̅̾̀̒̒̊͗̀͆̊̎̋̈́͐̍̔̇́̈́̅͗̋̓̈̀̍͋̿̽͗̈́̏̈́͗͗̈̐̀̈́̏͐̉͆́̅̓̕͝A̸̡̢̡̡̢̨̨͉̤̭͎̜͔̖̮͔̝͕̩͚̭̭̹̬͚͈̜̗͔̪̅̓̈̔̽̄̀͐̓̋͊̔̎̎̑̔̓̀̅͑́͘͘C̵̡͍̺̞̰͈̟̰̪̲͆̈̋̃͐͛̍̍̑̏͊͂̓̉̕͘̚͜ͅȨ̶̡̧̧̛̛͉͇̹̪͕͚̪̘̜̦̿̂̔̅̆̈́̌̊̌̍̈͋̎̾̆̂͊͗̉̀̎͒͐̈́̊͛̀̊͋̿͛̊͑͐͊̈̏͛̕͘̚͘̚̕̚͝͝ͅ ̸̨̛̼̭͔̜̠̤̯̘̰̗̅̆̓̐̓͂̄̊͊̎̏̍̎̀̿̍̈͐̒̅̇̓̏͒̆͝ṫ̸̛̺̲̭̪̻͎̬̻̬̱̭̤͓̫̻̫͔͓̘̬̠̫̹̫͛̈͑͌̐̏̏͊̒͒̐̽̊̒̀̄̉̽͊̋̆̏̀͐̀̅͛̈́́̎̒̎͐̽͗̓̎̍̈̕͘͜͠͠͝ͅŏ̶̡̧̮͔̤̣͙̩͖̲̮̲̬͙̣̪̣̦̟̥̱͓̖̯̬̯̖̼͓͉̭̫̹̃̊͌͗̀̅͗̽̀̄̎͌ͅͅ ̴̣̭̝̩͔̗̪̳̱͉̟̣̀̿̈̆͂́͋͗͊̕͝͝͠Ě̶̡̡̨̛̟̺̻͈̥̜̼̼̪͍̝͚̬̲̠̟̭̼̖̭̝͑̓̕n̸̨̢̨̨̳̦̱̱̰̞̖̹͍͚̞̳̯̘̲͕̳̗̣̹̯̫̺͚̥̙̟̫̒̈́͛̀̀͐̾̓̑̈́̉͆̈̑̔͌́͘̚̕͝ͅͅt̴̨̨̨̡̛̛͉̟̜͖̲̮͓̗̬̟͎͇͙͚̹̬̻̲̗̲̹̭̙̩͇̮̰̗̲̞͙͕̼̮̪̳͈̬̮͐͊̾̿̑̍̃͗̊̉́́̓̊͛̀̌͘̕͜ͅͅe̶̠̱̗̟̞͉̼͎̭̮̬̼͇̞̜̝͚̻̍̑́̏̌́̔͗̐͗̿͠ŗ̵̡̬̦̞͈̹͎̖̲͓̥̳̆̀̓̇͆̆̿͌̐̈́̅͊̇ͅ:̸̢̯̗̳̯̬͖̖̦̼͆͗̐̽̈́͠͝ \r\n ̸̛̛̞̆̀̎͋̽̑͗͑͋̂̐̈́̀̀̽͒̾͌L̷̢̡̛̝̖͍͔̜̗͈̪̈̓̈́̽̉̉̏͌͐̅̉͂̂͊̀͐͂̎̏͗̒̈́̇̽͌̚͘͝͝͝͠ȇ̶̢̢̨̨̹̗̻̦̬̼͈̥̯̻̰̯͙̳̙̭͚͕͙͖̙̞̩͕̖͕̞̖͎̦̮̪̲͕͙̫̝̹͎͓̰͎̺̼̽̏̎͂̍̕͜v̴̢̡̢̢̡̢̢̪̲̩̖̩̜̹͙̤̹̣̥̬̟͔͇̦͔͔͙̩̤͎̻̹͇͐̿̈͋̾̐̾̈̊͛̈́́͛̈́͛̈́̊͂̃́̇͒͆̿̽̽̍̊̀̈͆̀̉̆̈́̿̿̂̒͒͘͘͘̕̕͜͝͝ȩ̶̡̨͙̙͉̺͔̤̠̬̦̤̻̟̥̺͙̮̼̰̪̪̹̹͍͎͎͔̖͕̻̮̫͖̗͔͉̼̯̣͖̪̲͚̤̑̅̊̇̀̈́̓̈̎̏̆͊̈̎̓̚͜l̸̨̛̛͓̩͇͍̭̘͖̠̭̤̘̘͇̹͗̈́́̊̄̾̔̓̀͑̏̅͋̋͐́̂̿̋̑̊̑̐̔̋̉̌̀̆́͛̆̔̃͂̂̔̀̅͆͝͝͝͝ ̵̢̢̻̠͇̬̖̬̼̪̈́̔̋̎̓̽͋̽̉̓̐̈̈́̓̉̌̓͗̽̿͑́̏̍̓͂̾̍̊̓͒̓̄̃̅̑̚͠͠͝͠͠͠2̷̢̨̨͉̝̼͇͎̗̦͎̫̻͉͎̑̉̾̇̀͊̾͒̉̒͋͛͗͘̕";
            }
        }
        else if (collision.gameObject.CompareTag("Level3"))
        {
            im.levelText.enabled = true;

            if (im.keysStored >= 2)
            {
                im.levelText.text = "Press 'E' to Enter:\r\nLevel 3";
                if (Input.GetKey(KeyCode.E))
                {
                    if (im.keysStored < 3)
                    {
                        SceneManager.LoadScene("Level3");
                        im.levelText.enabled = false;
                    }
                }
            }
            else
            {
                im.levelText.text = "P̵̧̨̡̢̮̝̱̪̼̖̝̙͙͍͉͕͎̱͓̟̱̜͖͎̜̲̯͕͈͉̯̫̞̪̤̱͇͉͎͎̟͔͎̖̱͙̹͔̋̐̈́̓̅͊̈́͗̐̓̋͑̊̓͌̽͒̽͑͌̆͋̌̄̚͜͝͠͠͠ȓ̵̡̢̧̢̢̡̹̙̮͚̘̺͖͍͔͍̣͚̱̩̯̙̪̫͉͓̭̜̭͕͓̰͍͈̩̭̖͉̮̘̙͚͚̞̮̇̀̈́̽̃͆̋͗͌͌̿̈̏̈́̓̀͊̽͗̑̂͛͂̿́͑̐̆͗̓̽̈͛̃͗̾́͘͘͜͠ẻ̴̛̲͖̌̅͋͐̋̌̈́̓͋̃͆̑͊͐̐̔͊͆͌̊̐̀͌͗͊̔̀̒̿̾̅̓͐͛̓̀́̈́́͌̂͝͠͝͝͠s̸̛̛̛̼̗̯͔̮̰͓̜̠̙͚̭͍̝̖̰̱̹͙̺̲̯̰̭̘͒͐͂̂̅͌͊̽̅̑̇̀̅͛͌̄͂̃͋̈́̇̀̇͐̅̒̓͌͌͝͝͝͝͝͠͝ͅs̸̗̻̺̺̙̼͈̯͍̯̤̭͔̩̟̪͚̟͕͚̭̺͇͉͔͚̬̈́́̃̀̉͋͊̃͑͂͂̈́͆̿̇̔͜͜͠ ̵̢̢͖̝͓̝͓̫̩͈͕̭̮̦̟̞̪̜̯̘̱͉͇̯̳͇̲̱̙̯̫̰̯̱͚̔̓̈́̅̅͆̀̄̌̀̄͘͘̕͠͝͝ͅŞ̸̢̢̡̧̨̛̛̗̻̮̟̹̰͎͉̳̞̪̜̟̭͎̼̣̦̞̙̤̙͎̝͍̜͓͙̺̞̙͕͕̲̮̤͖́́̊̆̂̒͐̋́͊̔̀̍́̅̾̀̀͐̂̀͌͑̇̃͗̈́̐̚͘͝͠͠͝͝͠͝ͅP̶̢̢̡̱̖̱̝̻̝̦̤̘͍͍͈͙̦̮͇̫͈̯͈̜̟̥̖̲̩̞͍̮̠͈̹̝̮̅͑̐̇̅͒́͆̉̍̿̾͆͆̑́̈́́́̏͑́̈͗̑̋̄̅̒̎̓̐̑̆̿͊̍̄̏̄̉̕͜͜͝͠͝ͅĄ̸̢̛̲̰͈̪͎̣̳̮͉͎̳̼͎̼͈̲̹̭̾͒̑͊̀̈͐̈͛̓̀̿̿̅̏̈́̿̏͛͋̌̄̀̄͂̎͑̄͘͜͜͠͠͝ͅC̷͍̞̘͔̮̫̗̹̻͕̱͙̙̹̩̫͕̹̺̙̉̑̏͊̏̈́̈̀̋̌͊̽͗̍̅̋̀̿̂́̽̈́͗̊̓̃̚͘͘͝͝͝È̸̛͎̇́̿̿͂͒͊̊̈͐͒͂̉̍͒̏̈́̇̉͗̐̾̄̐̊̃͌̆̏́͊͗͊̎̃̆̈́̍̋̏̚̚̚͝͝͝͝͝ ̵̨̨̛͖̳̳͕͕̬̪͔̰̯͎̟̩̙͉̬̞̘̹̠͉̱̳́̐̎̈́͂̇̄́́̀͊̾̑̅͆̍̽̑̽̈́̾̕͘͜͠͠ͅẗ̶̛͔͕́̈̎̇̉̅̓̐̽̀̽́̈́̒̾̆̈́͂̑̂̄́͂̅͒̋͛̃̚̕̚̚̚͠͠͝ơ̴̥̥̟̿́̂͒̔͐͂͂̋̔͆͆͛́̈̃͗͛̐̅̓̐͐͒̐͘͘͝͝͝͠ ̵̧̢̛̛̝̪̫̗͈̞͕̹̻̥̱̟̦͓͉͉͖̞̯͂̈̇͆̆̄͌̋̌̇͌̅͂̓̏̒͑̈̇̇̿̽̈́̄͊̒̿̾̄͑̄̌͒̂̅̏̋̽͛̿͘̕͘͝Ę̵̢͓̟̳͍̞̤̭͋̊ņ̴̫̙͍̜̳͙̻͖̭̦̳͕̬̞̻̰̪̝͎̪̻̜͇͚͙̭͉̣͙̗̯̩͈̠̋͌̋́͋̓̆̇̎͜͠t̶̡̨̧̛̘̭͖͖̼͔̱̘̪̯̰̦̞̝͚͉̰͙̼͖̤͙̲̗̩̜̹̰͚͈̠̜̦̝͙̠̭̤͓̼͖̠͓̂̄̈́̄̉͋̀̎͛̆́͊̂̒͑̚͜ͅȩ̶̛̲̺̤͙͕̻̗͍̣̟̟͚̦͍͕͎͉̻͚͇̯͈̬͈̻̦̼͚͔̳͉̻̻̝͈̺͙̦̈́̋̒̈̐̉̈́́͑̽̎͛̌́̓̀̉̾͐̃͒̊́̈́̎̊̀́̈́̔̐͛̚̕̕̕͝͠ͅŕ̷̢̢̛̛̝̹̪̯͈̫͉̩̼̘̯̝̭͓̲͎̝̠͕̖͈̦̮̪͙̯̟̝͇̝̯̮̝̪͙̬͕͕͛̃̃̄̈́̆̋̊̋͗̅͛̓͗͛̐͒̀͗̓́̂̄͗̽̇̅̍̀̆͐͑̊͊̔̂̾̇̑̈̐͘̚̕͘͜͝ͅͅͅ:̵̧̡̨̢̛̰̼̤͕̝̖̹̟̯̩͓̺͓͓͕̩͓̳͖͍͖̦͔̣̠̻̝̹̱̯̳͉̪̻̺̖̦͖̣̣̲̎̔͌̊̈́̋͂͊̀̽̌̓̾̇̽͘͠ͅ \r\n ̷̛͕̦̘̭̜͈̱̯͎̬̦̱̩̰̲͖͕̫͙̻̥͇͈͓̠̭͈̲̘̥̽͆̏͗̏̿̀̋͋͑̌͗̌̋̎̀͌̌̔̒͐̈̌̃͒̂̐͂̈̂̐̃͋͆̓̋̂͒̀̋̽͛̐́̈́̚͜͜͠͝ͅͅĻ̵̧͈̜͎̗̥͇͎̼̮̪̞͉̦͓̣̤͑̐̔̔̑̑͊͋̎́̈́̑̆̈́̉̋̐̈́̃̄͛̅ę̶̨̢̡̛̭̬̗͍̰͓̹͕̻̜̟͇͎̞͚̺̱͛̅͛̐̍̋̇̄̀͌͒̋̀̊͌̈̒̒̾͑͗̒̀̔͒̊̑̓̌͛̐̆͋̿̈̍͑͛͘̚͜͠͝ṿ̷̨̢̧̢̨̛̛͎͕̼̫̦̩͎͍̱̼̳̹̠̯̥̝̹͙̞͎̰̳͉̯̦͇̱͚͔͓̦̼͍͉͉̼̰̳̑̋̄̌̆̌͒̉̌͆̎͛̉̈́̊̐̄̍̓̀̏̈́̈́̓̀͆̑̐̾͂̕͝͝͠ͅͅę̴̧̨̛͎̗̼̤͓͎͎͍̩̲͓̜̩͕̪̱̩̠͍̮͕͇̙̞̟̥̪͚́̀̎̾̿͒̂̏͛̈́̃̃͑̃̈́̂̈́͋͊̊̑̿͂͊̌͌̑̉͂̀̍̎̌͂́̓̂̈̇͘̕͘͜͝ͅḷ̷̛̛̣̖̖̳͕͊̿͐̈́̏̐̃́͒̊̓̉̽̍͌̌̌͑͌̏͝͝ͅ ̷̧̖̣̠̠̱͖̤̣̯̟̅͂̐̈́́̆̃̇́̍̐̎͐̏̓̊̎͐͗̎̓̃̚̚͘͠͠͝3̸̨̤̘̰̱̺̭͈͈̱̙̳͕̫͕̖̠̰̲͆͌̉͊̀͊͜";
            }
        }
        else if (collision.gameObject.CompareTag("Level4"))
        {
            im.levelText.enabled = true;

            if (im.keysStored >= 3)
            {
                im.levelText.text = "Press 'E' to Enter:\r\nLevel 4";
                if (Input.GetKey(KeyCode.E))
                {
                    if (im.keysStored < 4)
                    {
                        SceneManager.LoadScene("Level4");
                        im.levelText.enabled = false;
                    }
                }
            }
            else
            {
                im.levelText.text = "P̶̨̧̧̢̢̛̗̦͍̩͔͉͍̳̥̣̪͎͇̘̝͍̠̳̩̗̳̳̙̪̻̩̺̀̿̆̇͌͛́̿̀̂͗̃̈́̽̂͋͂̾̄̕͜͝r̵̢̢̪͓͕̝̞̺͔͖̻̹͖͕͔̻͕̳̱͎̝̜̘̼̯̭̤͖͙̪̩̯̩̱̯̘̻͖͌͑̈́͊̍́͌̈́̉̂̿̿̿́̏͑̏̎̌͋́̇̋̋̔͐̒͑̕̚͠͠͝͝ͅͅe̴̢̨̢̘̱̞̯̥̮̙̯̳̪͓̮͈̼̻͍̥̜̬̙̬̦̬̫͙̜̬͇̺̥̦͍̫̪͒̃̃̈́̌̽̃̂̑̿̎́̂̂̚̕̕͜͝͝ͅs̸̡̞̳̟̪̳̣͇͕̦̯͚̗͍̪̜̹̞̮̰̔͛͊̈̓͋̈́̎͛͆̀̔̎͛̉̾̋̂̒͆̏́͊͒̊̓͛̽͆͗̌̆̓̍́̈̿͛̈̓͘̕̚̕͝͠ͅͅs̷̨̻̳̝̜̲̉̊̆̋̅̌̀͌̌̾͂̿́̇̈̆̚͝͝͝ ̸̢̡̢̨̢̛̛̪̟͕͈̘̳̳̯̣̥̜̣̠̼͕̹̗͇̠͔̪̮̦̙̣̟͓̖͈͔͈̯͍̯̘͉̻̤̜͖̙̳̋̈́́̍̇̾̓̿̇̈́̽̒͒̂̎̂̈́̐͂̍̈́̔̐͋̓̇̎͐̄̓̆̀̕̕͘̚͝͝͝͠ͅS̶̢̧̼̜̫̘͖̭̪̦̳̱̩̖̳͉̱͇͕̹̞̯͈̼̭̘̺̖̪̱̘͉̠͓̯͈̗͕̀́̓̅͑̈́̾͌͂́͗̏̀̓͋̋̅̅͑̈́͊̃͆̇̇̍̌̆̾̈́̒̃̒͐̂̄͛̕̚͠͝͝͠ͅP̶̝̠̲̤̤̼̠̗̰̱͙͔͍̗̭̻͙͚̻͇̠̯̐̀͌́̊̿̀̒͒̃̊͑͆̌̆̈́̇̃̈́̀̎͆̒̈̌̀͋̐̎̓̂̃̈́̍̈́͐̕̕̚͜͠͠͝ͅĄ̶̛̛͙̼̱̥͎̖͍͚͎͒̎̌͐̿̅́̂̈́̀̏́̂̆̔͒͛͂̇̈́͆̀̑͑̑̒̏̈́̎̀̀͗̾̔̍͂̐̇̓͌̒͛̄̚̕͜͜͝͝͠ͅC̶̨̡̨̡̧̤̲̩̫̬͚̺̠̖͎͖̪̤̫͖̞͓̭̦͚͕̥̱̩͓̰͐̓͊̏̂͆͊̊͋̓́̅͛̿͛̾̑̆͊́̋̄̆̊̊̄͋̾̇͌̃̍̄͐̌̾̈̇̑̏̕̚͘͜͠͝͠͝͝͠ͅẺ̸̢̧̛̛͖̠̤͍̣̫̝̖̰̜̺̯̖͚͈͔̫̣̻̇̾̑̀͐̑͐̅̅̓̀͑̉͌̾̇̀̓̄̓̃̀̆̈́͒̆̔͂͆͊̋̽͐̀̇͛͆͘͘͘͜͜͝ͅ ̴͚̩̻̱͔͙͉̱̂͗̽̄͗̃̍́̋̀̽̃̉̒̽͐̾͛̑̽͌̋̔͝͝t̵̛̛̺̯̺͈̻̬̫̬̳̆̇̋͒̎̓̈́̀̍̈̉̿̈́́͐̀̋̊͒͒̿͒͗͘͘͝ơ̶̢̛̠̩͎̱͈̯͍͔̻̣̥̻̹͐̿̆͗͗̎̏͂͒̉̅̀̓̈́̓̑̓́̐̉͛̿́̓̐̀̐͌̚̚͝͝͠ ̶̨̨̛͓̥̟̳̤̯͇͔͉͍͕̪̭͈̻̺̪̗̰̺̖̣̗̙̉̀͒͑͂̓̑̂̓́͋̂̔̊́͂̄̒̌͗͐͊͋̒̐̒͊͛̈́͘̚̚̚͘̚̕͜͝͝͝Ė̶̢̢̤̖͔͙̺̤̪̆̈͐̇̈̚ͅn̴̡̢͖̲̬͚̜̗̾̍́̊͌̓̋̆̒͆̈̉̈́̌̿͂̇͊͋̿̍̈́̀̏̍̌̇̆̐͘͝͝͝t̶̢̧̢̡̞̺̥̫̞̘̤̠̤̦̰̲͕̻̹̹̱̪͖͖͈͇̲̗̰̩̼͒̈̍̈̃̍̐̽̂̓͊̿̀̊̾̎̋̏́͌́̽͜͜͠͝͝͝ͅȩ̷̧̡̨̧̡̡̡͎̺̬͙͓̦̝̠̱̰̦̖̬̤͕̬̳͈͖͉̳̖̹̤̹̪͕͇͎̩̬̩͓̩̯̤̭͌̉̑͜͜ͅr̵̨̨̧̨̭͕̝͇̻̫̭̟̹̪͕̻̖̣̠̗̠̖̠̻̠̖̂̐́̈́̈͂̄̔͌͗͆̑̈́̋̃̑̍́͛͒̈́̔̑̏̆̓͆̕͝͝:̸̨̧̢̢̢̛͍̪̻̣̟̠͔̝̲̥͇̞̠͎̱͚̜̠͙̹̰̠̖̜̰̘͔̟͍̞̩͖͚̖̗̤̇̈́̃̈̐͜͠͝ͅͅͅ \r\n ̷̡̡̧̧̨̩͚̲̹̫̠̠̱̣̥̼̮̘͍̳͓͎̟͖̠͈͕͇̞͓̹͉͖͔͚̳̟̯́̄̄̕͜͜͝͠ͅL̵̛̰̯͈̩̫̙͎̦̲̪̽̇̀͌́̏̾͗͑̏͑́̎̏̋̿̋͂̑̄̏͋́̓̄̌͂̀͂̑͒͆̅̕͘͜͝͝͝͠ͅȩ̸̹̫̼̣̪͍͔̻͖̖̰̠̙̗̺͛̿̈́̀͗̆͊͋̂̄͂͑̄̓̎̓̄͌̓͒̾̓̉͑̃̓̾͘͘v̷̤͍̺̬̯̲̫͆̿͐̽̈́̾͂̋̅͐̇̋͌͆̋͂́͌͗͆̏̀͌̈̒͘̚͘͘̕͠ë̷̡̢̡̼̙͙̞̗̦̲̠͈̻̖͙͎͚̥̭͓̤͎͉̜̝̼̩̯͙͓͆̅̌͗͑̿̾͆͜͜͜͠ͅͅļ̵̨̧̛͇͚͈͚̳̯͕̜̪̺̤̝̜̗̥̙͚̯̲̩̤̳̬͕̺̺͖͚̬̐͑̿̐̌͌̉̾̇̍̅͋͐́̏͑̋̀̃̃̆͂̐̆̾̉̓̐̕̕͠͝͝ ̶̧̢̨̛̖͈̠͔̜̠͕̘̟̻̹̳͇̮̱̣͓͍̗͙̤̳͍̬̱͚̪̞̣̙̯̬̯̖̪̯̪̲͉͖̙̤̀͑̈́̒̽̔̄̃̐͜͝4̸̧̧̨̦͔͙̹̳̣̤̲̣̗̥͉̲̠̙͓͕͚͎͚̮̻͓͎̮̠́͠͠";
            }
        }
        else if (collision.gameObject.CompareTag("LevelBoss"))
        {
            im.levelText.enabled = true;

            if (im.keysStored >= 4)
            {
                im.levelText.text = "Press 'E' to Enter:\r\nCentral Chamber";
                if (Input.GetKey(KeyCode.E))
                {
                    GameObject obj = GameObject.FindGameObjectWithTag("AudioManager");
                    obj.GetComponent<AudioManager>().changeToBossMusic();
                    SceneManager.LoadScene("BossLevel");
                    im.levelText.enabled = false;
                }
            }
            else
            {
                im.levelText.text = "P̵̧̳̼̪͙̬̘͔̯͖̱͍̥͇͖͍̻̼͖͙̫̬͕̾̍͂̅͊͑̏̅͒̓̌̉́̔̃́̎͋̕ͅŗ̴̻̯͕̼̱͈̩͙̤̜̤̹͔̫̗͚̲̳̺̞̯̮̣̼͓͉͉̫̺͍̳͓̄͂̽̆͂̐͒͂͒͗͛͗̔̎̎̽̈́̌͌͛̎͘̚͝͠e̵̢̲̠̟̩͙̝̹̫̼͊̓̐͆͆̀͛̾̂̾͊̈́̈́̒̋̎͂̓͌͘͘̕̕͠͝ͅs̶̢̢̡̨̡̨͙͚̜̤̰̰̺̖͔̘͕͍̠͈͈̥̰̲̥͎̹͍͖̭̹͉͈̱͖̻̱̫͔̞̣̪̣̦̩̦͐̏̔̓̔̈̌̃̓͒͂͌͗̍́̾͆͌͗̉̓̉̈́̋̌́͑͆̓̈̋̽̊͘s̷̢̧̙̠̪̼̫̲̻̱̑̈́̾͒͒̽̃̾̈́̿̈́̃̀͑͠ ̶̨̧̛̰̪̼̣̰̥̪̪̮̣̬̺̖̹̱̭̖̺̙̝̖̬̤͖̪͇̟̽̀̇̐͑̀̐̋́̌́̽́̓̏̍͋̒̿̌͌̍̓͂̆̿͆̓̽̉͂͑̏̄̂͗̌̆̀͘̕̚̚̕͝͠͠Ş̸̡̛̛̝͈͍͍̖̞̭͉̬͓͇͔̤͓̟̟̮̬͙͇͇̖̦̳͎̳̜̯̺̘̜͎͈̼͙̖̮̘̥͌̑̆́̂̔̐͗͆̓̽̓̉͌̿̋͐͑͛̇̄́͛̔͆͂͘̕͜͠͠͝ͅͅP̶͕͎̖̖̪͕̱͔̼̺̰͂͑̌͋͒̾͂̈̉̈́̽͑͊͛̊̎̀̿̄̒͊͒͋͐̈̆͘͘͝͠͠Ā̶̱̪̍̌͂̃̾̈́̆͐͊̈̉́͛̓̉͋̇̓̈́̾̉̽͐̀̍̈̂̈̋̎͋̚̚̕͝Ç̴̡̼͍͕̻͖͇͉̜̘̣̦̞̗͕̼͍̝͚͍̟̯̥͖̱̰̪͎̭̞̟̥̺̦̥̻̯̝̘͕͍͚̲̐̏̽̀ͅͅE̵̡̢̨̢̧̢̛̼̝̹̮̠̞̣̣̯̜͎̼̙̼̻̰̙̳̦̦̝̤͈̱̩̞̣̝̻̬̺̦͔̪̟̲͇͔̐̌̓̃̃͌̂̅̅̎̄͂̈́͜͜͜͜͠ͅͅ ̸̢̫̠͎̻͕͗̾̄̍̋̊̀̉̊̄͌͌͊͑̑̀͝ť̶̡̛̛̩̪͈̺͔̪̪̪̤̪̗͚̲͚͍̙͎͊͒̿͗̔͊̍͝͝ơ̸̢̢̨̢̡̛͓̘̗͕̻̺̪̤͓͉͍͓̣̞͎̪̥̹̮͎̱͈͉̭̠̻̹̗͓̭͙̳̥͈̥̥̖̂̋̓͗̀̋̓̍̌̐̈̾͌ͅͅ ̶̧̢̡̢̡̡̡̠̩͍̳̟̼͈̣̳̫̝͙̼̠͎̙͇̭͎̟̟̣̤͇̱̭̦̤͓͚̦͊̿̈́̈͐̃̏̊̈́͐͋͛̍͋̔͌͒̽̐̃̎̍̑̆̋͑̓̌̂̃̉̃͆̿̏̈́͑̐̄͌͒̌̀͒͂͂͘͜͝ͅͅÈ̴̲̔̿͊̿̃͌̀̀̈́̈́͑͂́͋̈͂̀̆̊̾̒̉̉͒̋̅̐̋̋́̾͒̽͗͐̈́͛͋̽͝͝n̷̛͍̱̰̝͉̣̻̞̣͕͈͙̞͔̲̲̠̹̦̥̟͚̹̜͔̟̮̗͉̲̘̮̬̞̼͒͆̓͑̒̈́͐͐̉̆̏̂̽̈́͑̋̇̈́̈́́͊̎̅̐̾͆̑̿͂̎̂͋̊̕͘̕͝͝͝͝ͅt̶̡̠̱̬̱͚̮̤̝̳̪̝̲̺͚̻͍͔͇̺̜͉̫̬͔̕͝͝ͅe̵̡̦̗̘͉͉̙̘̭͕̲̝͎̺̳͖̬͔̳̯̯̬̬͈͇͚̙͍̔͊̓͆̚̚̚͝͝ŗ̷̢̢̯̖͔̙̼̞̦͚̮̰͎͔̠͔̼͇͇̖͓͎͉̬͇̻̪̪̞̮͍̬̦͊̈́̆͌̃̽̎̆̌͂̋̉͊̄͊͐̈́̓̀͐͛̀̏̈́͗͂͋́̏̒͐̋̈́̇̀̕̕͘͜͠͝ͅ:̶̢̢̢̡̱̲̦͚͓͎̲̠̞̫̗̪̫̹̩͎̖̠̪͐̏͂̐̍̀̒̇̀̽͌̾̀̎͐̓̂͛͘͜͜ \r\n ̸̢̡̨̢̛̘͙̲͉̘͖͙̖͇̠̻͓̹̻̖̹̻̦̙̮͈̼͓͔̳̦̗̽̏̑́̏̆̉̐̋̓͂̈́̃̈̎͘̕͠͝C̴͖͐̋̄̈́͒͑͛̇ë̵̡̪̬̪̤͈̗͙̳͚̳̩̝̘̘̗́̈̇̍̃̽̈́̈́͐̄̅́́́̎̿͛́̍́̋̏̎̈́͒͗̓̒̈́̏̈́̽̑͐̊͘͝͠͠͝͝͝͠ͅn̷̢̡͍͎̞͍͔͎̙̘̭̙͓̺̉͐̂̊̌̽̏̒̅͌̂̆̂͊̾̕̚ṫ̸̢̧̡͓̪̣̪̞̙͈̼͙͓̝͎̰̥͍̺̗̮̼̟̰͙̘̜͈̼͕̤͚̳͖̤̞̗̯͍͍̟̤̭͎͈̻͙͖́́̊̇͊͆̓̇̌͆͂̊͒̓̏̀̒̀̾̑͋͌̃̏͆̍̑̆̕̚͜͝͝͠͝͝͝ṙ̶̡̛̫̗͈̥̬͎͙͉̝͎̖̟̼͍͕͉̯͉̦̤̹̘͇̙̼̹̩̙͇̯͑̀̑̑̈́̆̆̽̃̄́̈́́̀̌̕͜͠͝ͅͅa̷̳̜̯͐͛̒̄́̔́̈̅͌̀̒̊̍̀͝͝l̴̛̛̦͎̻̙̮̣͕̣̜̺̪̲͓̘̱̙͈̰͈͕̬̞̥̳̘̩̱͖̙͚͖͗͌̏̎̒̒͗̔̓̃̈̃̓̍͒͂̊̈͋̋̉̆̀̓ͅ ̸̡̧̨̢̢̺͚̲̼̗͔̬͖̼͉͕̞̣̥̦̺̥͍̦̭̳̱̩̥̫̘͉̤̱͓̯̯̦̤̹͖̱̉͗̽̍̀̔͛̅̈̉̿̆̏̓̾̃̄̀̽͗͗̈̈́͊́́̈́͂͂́̇̔͆͐̕̕͘͘͜͜͝͠ͅͅC̴̰̙̪̬̮̪̣̞͇͓͓̘̺͉̭̲̤̝̳͍̟̺̤̗͍̖̠̳̬̻̄̀̏͛̇͊͆̈́̅̌̒̈́̍̍͌͌̇̌́̊̄͂͗̄͐̿̓̔́̎̀̊̕̕̚̕͜͠͝͠͠͝͝h̴̨̡̨̢̡̛̫͙͓̣̫̜͎̱̠̙̦̦͚̘͎̹̪̼͚͉̖̣̪̣̱͕̲̼̜̭̭̲̰̪͊̔̇̀͂̽́͗̀͑̒́͑̓͗̽̄̾̈͆̎́̌̽́͆̔̓̈́̓̈́̈́̾̓̈͒͘͘͘͘͝͝͝͠ͅã̸̧̢̢̡̛̛̞͉̜͕͎̥̟̱̱̜̥̟͍͍̱͙̭̻͉̱̳͈͎̘̗͇̫̱͛́͌̂̉́͑̽̅̉͛͌̂͊́͊̋̀̓͆͛̌̌̕̕͝͝͝ͅͅm̸̻͕̙̱̝͓̜͖͔͖̟͗̌̀̑́͌̈́̍͆͂̆́̈̌̉͘͘b̴̡͔̗̤̠̣̹̤̯̠͕̥̠̤͔͎͔̞̉̅̇͒̄̊ȩ̴̡̛̭͕̪͚̘̈̃̈́̓̂̎̈́̀̎̋̈́̉̎͋͊͛̉̍̈́̈́̈́̉̈́͗͆́͂̅̆̀͆̈́̒̕͘͘̕͘͠͠͝ŗ̴̛̱̫̝̘̫͔͎̇̅̉̔́͒́̿̈́͗̈́̂̍̈͗̒̐̇͗͊̑̽̍̈́͑̉̄̿̈́́͌̓͋͋̾̒͂̋͗͐̇̃̊̉͝͠͠";
            }
        }
        else if (collision.gameObject.CompareTag("Save Bench"))
        {
            im.saveText.enabled = true;

            im.saveText.text = "Press 'E' to Save Game";
            if (Input.GetKey(KeyCode.E))
            {
                //Save Game
                SavePrefs s = saveObject.GetComponent<SavePrefs>();
                s.setSaveValues();
                s.SaveGame();
            }          
        }
        else
        {
            im.levelText.enabled = false;
            im.saveText.enabled = false;
        }
    }

 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("spit"))
        {
            audio.GetComponent<AudioManager>().playHurt();
            playerHealth = playerHealth - 0.2f;
            Destroy(collision.gameObject);
            Debug.Log("spit hit player ");
        }

        if (collision.gameObject.CompareTag("catAttack"))
        {
            audio.GetComponent<AudioManager>().playHurt();
            playerHealth = playerHealth - 0.1f;
            //Destroy(collision.gameObject);
            Debug.Log("CatAttack hit player ");
        }
        if (collision.gameObject.CompareTag("Rock"))
        {
            audio.GetComponent<AudioManager>().playHurt();
            playerHealth = playerHealth - 0.5f;
            Destroy(collision.gameObject);
            Debug.Log("CatAttack hit player ");
        }


    }

    private void attack()
    {


        if (Input.GetKey(KeyCode.Space) && !playerHasAttacked && im.equippedWeapon == Weapons.Claws)
        {
            audio.GetComponent<AudioManager>().playAttack();
            cloneAttack = Instantiate(attackObject, gameObject.transform.position, Quaternion.identity);
            playerHasAttacked = true;
            timerForAttackAlive = 0.5f;
            Destroy(cloneAttack, 0.5f);

        }

		if (Input.GetKey(KeyCode.Space) && !playerHasAttacked && im.equippedWeapon == Weapons.Sword)
		{
            audio.GetComponent<AudioManager>().playAttack();
            cloneAttack = Instantiate(swordPrefab, gameObject.transform.position, Quaternion.identity);
			playerHasAttacked = true;
			timerForAttackAlive = 0.5f;
			Destroy(cloneAttack, 0.5f);

		}


		if (Input.GetKey(KeyCode.Space) && !playerHasAttacked && im.equippedWeapon == Weapons.Axe)
		{
            audio.GetComponent<AudioManager>().playAttack();
            cloneAttack = Instantiate(axePrefab, gameObject.transform.position, Quaternion.identity);
			playerHasAttacked = true;
			timerForAttackAlive = 0.5f;
			Destroy(cloneAttack, 0.5f);

		}



		if (timerForAttackAlive <= 0.03f)
        {
            playerHasAttacked = false;
        }

        if (playerHasAttacked)
        {
            if (attackDirection == 3)
            {
                cloneAttack.transform.position = new Vector2(gameObject.transform.position.x - 1, gameObject.transform.position.y);
            }
            if (attackDirection == 1)
            {
                cloneAttack.transform.position = new Vector2(gameObject.transform.position.x + 1, gameObject.transform.position.y);
            }
            if (attackDirection == 4)
            {
                cloneAttack.transform.position = new Vector2(gameObject.transform.position.x , gameObject.transform.position.y + 1);
            }
            if (attackDirection == 2)
            {
                cloneAttack.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - 1);
            }
            
        }

    }

    public void healthIncreaseForHealthPowerup()
    {
        playerHealth += 0.5f;
    }

}