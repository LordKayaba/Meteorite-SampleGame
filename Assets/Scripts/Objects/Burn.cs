using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burn : MonoBehaviour
{
    [SerializeField] float time = 0.5f;

    SpriteRenderer spriteRenderer;
    Collider2D collider2;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider2 = GetComponent<Collider2D>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            spriteRenderer.color = Color.red;
            StartCoroutine(burning());
        }
    }

    IEnumerator burning()
    {
        yield return new WaitForSeconds(time);
        spriteRenderer.enabled = false;
        collider2.enabled = false;
    }
}
