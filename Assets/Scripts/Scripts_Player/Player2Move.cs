using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class Player2Move : MonoBehaviour
{
    PlayerStats playerStats;

    Rigidbody2D rb;
    Animator animator;

    [SerializeField] private Joystick joystick;

    public float speed;
    public Vector2 movement;
    public float horizontal = 0;
    public float vertical = 0;
    
    bool isFacingRinght = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerStats = GetComponent<PlayerStats>();

        UpdateSpeed();
        playerStats.OnSpeedChanged += HandleSpeedChange;
    }
    private void FixedUpdate()
    {
        MoveMent();
    }
    protected void UpdateSpeed()
    {
        speed = playerStats.speed;
    }
    protected void HandleSpeedChange(float newSpeed)
    {
        speed = newSpeed;
    }

    public void MoveMent()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        horizontal = joystick.Horizontal;
        vertical = joystick.Vertical;
        movement = new Vector2(horizontal, vertical).normalized * speed;

        rb.MovePosition(rb.position +  movement * Time.fixedDeltaTime);

        if(isFacingRinght == false && movement.x < 0)
        {
            transform.localScale = new Vector3(-0.14f, 0.14f, 1);
            isFacingRinght = true;
        }
        else if (isFacingRinght == true && movement.x > 0)
        {
            transform.localScale = new Vector3(0.14f, 0.14f, 1);
            isFacingRinght = false;
        }

        if(movement == Vector2.zero)
        {
            animator.SetFloat("Move", 0);
        }
        else if (movement != Vector2.zero)
        {
            animator.SetFloat("Move", 1);
        }
    }
}
