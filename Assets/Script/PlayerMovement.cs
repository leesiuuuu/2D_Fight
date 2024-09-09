using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float Speed;
    public float JumpPower;

    public static bool AnimationStart = false;
    public static bool Skill2 = false;
    public static bool Skill1 = false;

    private bool isGround = false;
    private Rigidbody2D rb;
    private Animator animator;

    private bool isDouble = false;
    private bool isDouble2 = false;
    private float timer = 0.2f;
    private float timer2 = 0.2f;
    private bool timerActive = false;
    private bool timer2Active = false;
    private int PressCount = 0;
    private int PressCount2 = 0;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Vector3 moveVelocity = Vector3.zero;
        if(!Skill2 && !AnimationStart && !Skill1)
        {
            if (Input.GetKey(KeyCode.A))
            {
                animator.SetBool("IsRunning", true);
                PressCount2 = 0;
                timer2Active = false;
                isDouble2 = false;
                KeyFunction();
                moveVelocity = Vector3.left * (isDouble ? 1.5f : 1.0f);
                transform.localScale = new Vector3(-1, 1, 1);
            }
            if (Input.GetKey(KeyCode.D))
            {
                animator.SetBool("IsRunning", true);
                PressCount = 0;
                timerActive = false;
                isDouble = false;
                KeyFunction2();
                moveVelocity = Vector3.right * (isDouble2 ? 1.5f : 1.0f);
                transform.localScale = new Vector3(1, 1, 1);
            }
            if (Input.GetKeyUp(KeyCode.A))
            {
                animator.SetBool("IsRunning", false);
                isDouble = false;
                timerActive = false;
            }
            if (Input.GetKeyUp(KeyCode.D))
            {
                animator.SetBool("IsRunning", false);
                isDouble2 = false;
                timer2Active = false;
            }
        }

        Move(moveVelocity);
        Jump();

    }
    void KeyFunction()
    {
        if (timerActive)
        {
            timer -= Time.deltaTime;
            if(timer <= 0)
            {
                timerActive = false;
                timer = 0.2f;
                PressCount = 0;
            }
        }
        if(!timerActive && Input.GetKeyDown(KeyCode.A))
        {
            timerActive = true;
            PressCount++;
            if(PressCount >= 2)
            {
                isDouble = true;
            }
        }
    }
    void KeyFunction2()
    {
        if (timer2Active)
        {
            timer2 -= Time.deltaTime;
            if (timer2 <= 0)
            {
                timer2Active = false;
                timer2 = 0.2f;
                PressCount2 = 0;
            }
        }
        if (!timer2Active && Input.GetKeyDown(KeyCode.D))
        {
            timer2Active = true;
            PressCount2++;
            if (PressCount2 >= 2)
            {
                isDouble2 = true;
            }
        }
    }
    void Move(Vector3 moveVelocity)
    {
        transform.position += moveVelocity * Speed * Time.deltaTime;
    }
    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.W) && isGround && !AnimationStart && !Skill2 && !Skill1)
        {
            animator.SetBool("IsJumping", true);
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
            animator.SetBool("IsJumping", false);
        }
    }
}
