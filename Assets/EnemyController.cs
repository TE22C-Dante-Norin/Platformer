using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    GameObject player;

    [SerializeField]
    float speed;

    [SerializeField]
    SpriteRenderer enemySprite;
    [SerializeField]
    LayerMask groundLayer;

    Vector3 ledgeCheckSize = new Vector3(0.25f, 0.25f, 0.25f);
    Vector3 ledgeCheckSize2D = new Vector3(0.1f, 0.1f);
    Vector3 offsetX = new Vector3(0.5f, 0, 0);

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        


        if (Mathf.Abs(player.transform.position.x - gameObject.transform.position.x) > 0.01f && MathF.Abs(player.transform.position.x - gameObject.transform.position.x) < 10)
        {
            float vectorDirection = (player.transform.position.x - gameObject.transform.position.x) / Mathf.Abs(player.transform.position.x - gameObject.transform.position.x);
            Vector2 moveX = new Vector2(vectorDirection, 0);
            LedgeCheck(vectorDirection);
            transform.Translate(moveX * speed * Time.deltaTime);

            if (vectorDirection > 0f)
            {
                enemySprite.flipX = true;
            }
            else if (vectorDirection < 0f)
            {
                enemySprite.flipX = false;
            }
        }
    }

    bool LedgeCheck(float direction)
    {
        Debug.Log(direction);
        Vector3 hitboxPosition = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 0.5f, 0);
        hitboxPosition = hitboxPosition + (offsetX * direction);
        bool isGrounded = Physics2D.OverlapBox(hitboxPosition, ledgeCheckSize, groundLayer);
        
        if (isGrounded)
        {
            return true;
            
        }
        else
        {
            Debug.Log("YIPPIE");
            return false;
        }
    }

    public void Killed()
    {
        Destroy(this.gameObject);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Vector3 hitboxPosition = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 0.5f, 0);
        Gizmos.DrawWireCube(hitboxPosition - offsetX, ledgeCheckSize);
        Gizmos.DrawWireCube(hitboxPosition + offsetX, ledgeCheckSize);
    }
}
