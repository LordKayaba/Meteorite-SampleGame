using System.Collections;
using UnityEngine;

public class Star : MonoBehaviour
{

    SpriteRenderer spriteRenderer;
    Collider2D collider2;
    AudioSource audioSource;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider2 = GetComponent<Collider2D>();
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collider2.enabled = false;
            audioSource.Play();
            transform.GetChild(0).gameObject.SetActive(true);
            spriteRenderer.enabled = false;
            StartCoroutine(DisableRoutine());
        }
    }

    IEnumerator DisableRoutine()
    {
        yield return new WaitForSeconds(5);
        gameObject.SetActive(false);
    }
}
