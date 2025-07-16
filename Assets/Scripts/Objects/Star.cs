using System.Collections;
using UnityEngine;

public class Star : MonoBehaviour
{

    SpriteRenderer spriteRenderer;
    Collider2D collider2;
    AudioSource audioSource;
    bool isActivated = false;

    void Start()
    {
        if (GameManager.Instance.ShouldBeDisabled(gameObject))
        {
            gameObject.SetActive(false);
            return;
        }

        spriteRenderer = GetComponent<SpriteRenderer>();
        collider2 = GetComponent<Collider2D>();
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isActivated && collision.gameObject.CompareTag("Player"))
        {
            isActivated = true;
            GameManager.Instance.AddStar();
            GameManager.Instance.AddToDisabledGameObjects(gameObject);
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
