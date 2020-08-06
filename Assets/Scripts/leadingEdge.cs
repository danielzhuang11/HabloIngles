﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class leadingEdge : MonoBehaviour
{
    GameObject Player;
    void Start()
    {
        Player = gameObject.transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            Player.GetComponent<movementSideScroll>().isGrounded1 = true;
        }
        if (collision.collider.tag == "Enemy")
        {
            Player.GetComponent<movementSideScroll>().isGrounded2 = true;
        }
    }
    /*private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground" || collision.collider.tag == "Enemy")
        {
            Player.GetComponent<movementSideScroll>().isGrounded = true;
        }
    }*/
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            Player.GetComponent<movementSideScroll>().isGrounded1 = false;
        }
        
    }
}
