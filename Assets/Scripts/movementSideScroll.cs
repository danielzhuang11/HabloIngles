﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class movementSideScroll : MonoBehaviour
{
    public Animator anim;

    public float moveSpeed = 20f;
    public FixedJoystick joystick;
    float horizontalMove = 0f;
    public bool isGrounded = false;
    public bool isGrounded1 = false;
    public bool isGrounded2 = false;
    public float jumpForce = 5f;
    public float healthMax = 5f;
    public Transform player;
    public Image healthBar;
    public Image img;

    private float health;
    public GameObject GameOver;
    public GameObject playerz;
    public Text coinTxt;
    public GameObject mi;
    public Transform gamPos;
    public GameObject ui;
    private float hMove;
    private float vMove;
    private Rigidbody2D rigid;
    private float jumpCoolDown = .1f;
    private float timeTill = 0f;
    public TextMeshProUGUI results;
    public static bool movin;
    public Rigidbody2D pla;
    void Start()
    {
        globalScore.coins = 0;
        health = healthMax;
        Time.timeScale = 1;
        rigid = playerz.GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        timeTill += Time.deltaTime;
        if((isGrounded1 && isGrounded2))
        {
            if(timeTill>jumpCoolDown)
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
        
    }
    void FixedUpdate()
    {
        
        if ( ui.transform.position.z < -2000)
        {
            pla.constraints = RigidbodyConstraints2D.None;
            pla.constraints = RigidbodyConstraints2D.FreezeRotation;
            pla.gravityScale = 2;
            movin = true;
            hMove = Input.GetAxisRaw("Horizontal");
            vMove = Input.GetAxisRaw("Vertical");
            if (player.position.y < -30)
            {
                health = 0;
                healthBar.fillAmount = 0;

            }
            if (health <= 0)
            {
                GameOver.SetActive(true);
                playerz.SetActive(false);
                globalScore.coins = 0;
            }
            if (joystick.Horizontal >= 0.2f || hMove >= 0.1f)
            {
                coinTxt.text = "Coin: " + globalScore.coins;
                horizontalMove = moveSpeed;
                anim.SetFloat("Speed", Mathf.Abs(horizontalMove));
            
                GetComponent<Rigidbody2D>().velocity = new Vector2(horizontalMove, GetComponent<Rigidbody2D>().velocity.y);
                transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y);
            }
            else if (joystick.Horizontal <= -0.2f || hMove <= -0.1f)
            {
                coinTxt.text = "Coin: " + globalScore.coins;
                horizontalMove = -moveSpeed;
                
                GetComponent<Rigidbody2D>().velocity = new Vector2(horizontalMove, GetComponent<Rigidbody2D>().velocity.y);
                transform.localScale = new Vector2(-Mathf.Abs(transform.localScale.x), transform.localScale.y);
                anim.SetFloat("Speed", Mathf.Abs(horizontalMove));
            }
            else
            {
                coinTxt.text = "Coin: " + globalScore.coins;
                horizontalMove = 0f;
                GetComponent<Rigidbody2D>().velocity = new Vector2(horizontalMove, GetComponent<Rigidbody2D>().velocity.y);
                anim.SetFloat("Speed", Mathf.Abs(horizontalMove));
            }
            if (timeTill > jumpCoolDown && (joystick.Vertical >= 0.7f || vMove >= 0.1f))
            {
                if (isGrounded)
                {
                    if (timeTill > jumpCoolDown)
                    {
                        Debug.Log("Youre doijng it wrong");
                        Debug.Log(isGrounded);
                        isGrounded = false;
                        isGrounded2 = false;
                        isGrounded1 = false;

                        timeTill = 0;
                        Jump();
                    }
                }
                
            }
        }
        else
        {
            movin = false;
            pla.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            pla.velocity = new Vector2(0,0);
            pla.gravityScale = 0;
            //pla.gravityScale = 0;
        }

    }
    void Jump()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x,jumpForce);
        //gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Health" && health < healthMax)
        {
            health += 1;
            healthBar.fillAmount = health / healthMax;
        }
        if (collision.collider.tag == "Coin")
        {

            ui.transform.position = new Vector3(ui.transform.position.x, GetWords.y, 0);
            img.sprite = null;
            results.text = "Press the New Word Button";

        }
        if (collision.collider.tag == "Enemy")
        {
            health -= 1;
            healthBar.fillAmount = health / healthMax;
        }
    }
}