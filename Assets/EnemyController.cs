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

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(player.transform.position.x - gameObject.transform.position.x) > 0.01f)
        {
        float vectorDirection = (player.transform.position.x - gameObject.transform.position.x) / Mathf.Abs(player.transform.position.x - gameObject.transform.position.x);
        Vector2 moveX = new Vector2(vectorDirection, 0);
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

    public void Killed(){
        Destroy(this.gameObject);
    }
}
