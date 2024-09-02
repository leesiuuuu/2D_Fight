using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    public float Speed;
    public float JumpPower;
    public float PressDelayTime = 0.3f;
    private bool isGround = false;
    private Rigidbody2D rb;

    private bool isDouble = false;
    private float timer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Vector3 moveVelocity = Vector3.zero;
        if (!isDouble)
        {
            if (Input.GetKey(KeyCode.A))
            {
                transform.localScale = new Vector3(-1, 1, 1);
                moveVelocity = Vector3.left;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                transform.localScale = new Vector3(1, 1, 1);
                moveVelocity = Vector3.right;
            }
        }
        Move(moveVelocity);
        Jump();
    }
    void Move(Vector3 moveVelocity)
    {
        transform.position += moveVelocity * Speed * Time.deltaTime;
    }
    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.W) && isGround)
        {
            isGround = false;
            rb.velocity = Vector3.zero;
            Vector3 JumpVelocity = new Vector3(0, JumpPower, 0);
            rb.AddForce(JumpVelocity, ForceMode2D.Impulse);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGround = true;
        }
    }
}
