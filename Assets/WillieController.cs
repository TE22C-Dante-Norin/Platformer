using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WillieController : MonoBehaviour
{
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxisRaw("Fire1") > 0)
        {
            animator.Play("snur");
        }
    }
}
