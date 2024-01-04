using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void PickUp(GameObject player)
    {
        if (gameObject.tag == "HeartPowerup")
        {
            player.GetComponent<PlayerController>().Heart();
        }
        else if (gameObject.tag == "JumpPowerup")
        {
            player.GetComponent<PlayerController>().ExtraJump();
        }

        Destroy(this.gameObject);
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Vector2 hitbox = new Vector2(1, 1);
        Gizmos.DrawWireCube(gameObject.transform.position, hitbox);
    }
}
