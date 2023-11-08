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


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float vectorDirection = (player.transform.position.x - gameObject.transform.position.x) / Mathf.Abs(player.transform.position.x - gameObject.transform.position.x);
        Vector2 movementX = new Vector2(vectorDirection, 0);
        transform.Translate(movementX * speed * Time.deltaTime);
    }

    
}
