﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2 : MonoBehaviour
{
    [SerializeField]
    private float speed = 700f;
    [SerializeField]
    private float jumpSpeed = 400f;
    private Rigidbody2D rb2;
    private bool isGrounded;
    private Vector2 startPosition;


    // Start is called before the first frame update
    void Start()
    {
        rb2 = GetComponent<Rigidbody2D>();
        isGrounded = true;
        startPosition = transform.position;
}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) &&isGrounded)
        {
            rb2.AddForce(new Vector2(0, jumpSpeed));
            isGrounded = false;

            //   animator.SetBool("IsJumping", true);
        }
        if (Input.GetKey(KeyCode.LeftArrow)){
            rb2.AddForce(Vector2.left* speed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(new Vector2(0,180));
        }
        if (Input.GetKey(KeyCode.RightArrow)){
            rb2.AddForce(Vector2.right* speed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(new Vector2(0, 0));
        }
        if (Input.GetKeyDown(KeyCode.RightShift))
        {
            Debug.Log(transform.right);
            Debug.DrawLine(transform.position, transform.position + transform.right * 5);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
        if (collision.gameObject.tag == "Water")
        {
            PlayerReset();
            var uiManager = FindObjectOfType<UIManager>();
            uiManager?.AddScorePlayer(player: 1);
        }

    }
    private void PlayerReset()
    {
        transform.position = startPosition;
        rb2.velocity = Vector2.zero;
        rb2.angularVelocity = 0;
        transform.rotation = Quaternion.identity;
    }
}
