using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat : Wood
{
    Animator animator;
    override protected void Start()
    {
        particle = transform.GetChild(0).GetChild(0).gameObject;
        spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        collider2 = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    bool burn = false;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            animator.SetBool("IsDown", true);

            if (!burn)
            {
                burn = true;
                StartCoroutine(Burning());
            }
        }
    }


    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            animator.SetBool("IsDown", false);
        }
    }
}
