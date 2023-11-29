using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float speed;

    [SerializeField]
    float jumpForce = 100;

    [SerializeField]
    Transform groundCheck;

    [SerializeField]
    float groundRadius = 0.1f;

    [SerializeField]
    LayerMask groundLayer;

    [SerializeField]
    LayerMask enemyLayer;

    [SerializeField]
    LayerMask ladderLayer;

    [SerializeField]
    SpriteRenderer hobbeSprite;

    [SerializeField]
    int jumpMax;
    int jumpCount;

    bool mayJump = true;


    [SerializeField]
    Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        float moveX = Input.GetAxisRaw("Horizontal");
        Vector2 movementX = new Vector2(moveX, 0);
        transform.Translate(movementX * speed * Time.deltaTime);
        animator.SetFloat("Horizontal", moveX);
        if (moveX > 0f)
        {
            animator.Play("WalkLeft");
            hobbeSprite.flipX = true;
        }
        else if (moveX < 0f)
        {
            animator.Play("WalkLeft");
            hobbeSprite.flipX = false;
        }
        else{
            animator.Play("Standing");  
        }


        

        bool isClimbing = Physics2D.OverlapCircle(gameObject.transform.position, 0.4f, ladderLayer);
        if (isClimbing == true)
        {
            float moveY = Input.GetAxisRaw("Vertical");
            Vector2 movementY = new Vector2(0, moveY);
            transform.Translate(movementY * speed * Time.deltaTime);
            gameObject.GetComponent<Rigidbody2D>().simulated = false;

        }
        else
        {

            gameObject.GetComponent<Rigidbody2D>().simulated = true;
        }

        bool isGrounded = Physics2D.OverlapBox(groundCheck.position, MakeGroundcheckSize(), 0, groundLayer);
        if (!isGrounded)
        {
            isGrounded = Physics2D.OverlapBox(groundCheck.position, MakeGroundcheckSize(), 0, enemyLayer);
        }
        else
        {
            jumpCount = jumpMax;
        }

        if (Input.GetAxisRaw("Jump") > 0 && mayJump == true && jumpCount > 0)
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            Vector2 jump = Vector2.up * jumpForce;
            rb.AddForce(jump);

            jumpCount--;
            mayJump = false;

        }

        if (Input.GetAxisRaw("Jump") == 0)
        {
            mayJump = true;
        }


    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            Debug.Log("Hit");
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(groundCheck.position, MakeGroundcheckSize());
        Gizmos.DrawWireSphere(gameObject.transform.position, 0.4f);
    }

    private Vector3 MakeGroundcheckSize() => new Vector3(1, groundRadius);
}
