using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField]
    TMP_Text jumpCountText;


    [SerializeField]
    GameObject hitLeft;
    [SerializeField]
    GameObject hitRight;

    bool mayJump = true;
    bool mayAttack = true;


    [SerializeField]
    Animator animator;

    [SerializeField]
    Slider healthbar;
    [SerializeField]
    float healthMax;
    float healthCurrent;


    // Start is called before the first frame update
    void Start()
    {
        healthCurrent = healthMax;
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if (healthCurrent > healthMax)
        {
            healthCurrent = healthMax;
        }

        if (healthCurrent <= 0)
        {
            Destroy(this.gameObject);
        }

        healthbar.value = healthCurrent;

        float moveX = Input.GetAxisRaw("Horizontal");
        Vector2 movementX = new Vector2(moveX, 0);
        transform.Translate(movementX * speed * Time.deltaTime);
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
        else
        {
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
            {
                animator.Play("Standing");
            }
        }


        jumpCountText.text = $"Jumps left: {jumpCount}";

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
        else if (mayJump)
        {
            jumpCount = jumpMax;
        }

        if (Input.GetAxisRaw("Jump") > 0 && mayJump == true && jumpCount > 0)
        {
            Jump();
        }

        if (Input.GetAxisRaw("Jump") == 0)
        {
            mayJump = true;
        }


        if (Input.GetKeyDown("e") && mayAttack == true)
        {
            hobbeSprite.flipX = true;
            mayAttack = false;
            Attack(hitRight);
            animator.Play("Hit");
        }

        if (Input.GetKeyDown("q") && mayAttack == true)
        {
            hobbeSprite.flipX = false;
            mayAttack = false;
            Attack(hitLeft);
            animator.Play("Hit");
        }
        if (Input.GetKeyDown("h"))
        {
            GetComponent<TextMeshPro>().text = "333";
        }

        if (!Input.GetKeyDown("e") && !Input.GetKeyDown("q"))
        {
            mayAttack = true;
        }

    }

    void Jump()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        Vector2 jump = Vector2.up * jumpForce;
        Vector2 tempVector = new Vector2(0, 0);
        rb.velocity = tempVector;
        rb.AddForce(jump);
        jumpCount--;
        mayJump = false;
    }
    void Attack(GameObject direction)
    {
        Collider2D[] HitEnemies = Physics2D.OverlapCircleAll(direction.transform.position, 0.4f, enemyLayer);
        foreach (Collider2D enemy in HitEnemies)
        {
            enemy.GetComponent<EnemyController>().Killed();

        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            healthCurrent -= 20;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 8)
        {
            other.gameObject.GetComponent<PowerupScript>().PickUp(gameObject);
        }
    }

    public void Heart()
    {
        healthCurrent += 20;
    }
    public void ExtraJump()
    {
        jumpMax++;
        jumpCount = jumpMax;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(groundCheck.position, MakeGroundcheckSize());
        Gizmos.DrawWireSphere(gameObject.transform.position, 0.4f);
        Gizmos.DrawWireSphere(hitLeft.transform.position, 0.3f);
        Gizmos.DrawWireSphere(hitRight.transform.position, 0.3f);
    }

    private Vector3 MakeGroundcheckSize() => new Vector3(1, groundRadius);
}
