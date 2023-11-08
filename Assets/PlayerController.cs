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

    bool mayJump = true;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        Vector2 movementX = new Vector2(moveX, 0);
        transform.Translate(movementX * speed * Time.deltaTime);

        bool isGrounded = Physics2D.OverlapBox(groundCheck.position, MakeGroundcheckSize(), 0, groundLayer);
        if(!isGrounded){
            isGrounded = Physics2D.OverlapBox(groundCheck.position, MakeGroundcheckSize(), 0, enemyLayer);
        }

        if (Input.GetAxisRaw("Jump") > 0 && mayJump == true && isGrounded == true)
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            Vector2 jump = Vector2.up * jumpForce;
            rb.AddForce(jump);

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
    }

    private Vector3 MakeGroundcheckSize() => new Vector3(1, groundRadius);
}
