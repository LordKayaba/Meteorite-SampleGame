using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TNT : MonoBehaviour
{
    [SerializeField] GameObject[] destructions;
    List<IAction> actions = new();
    AudioSource audioSource;
    SpriteRenderer spriteRenderer;
    Collider2D collider2d;
    bool isActivated = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider2d = GetComponent<Collider2D>();

        if(destructions.Length > 0)
        foreach (var destruction in destructions)
        {
            actions.Add(destruction.GetComponent<IAction>());
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isActivated && collision.gameObject.CompareTag("Player"))
        {
            isActivated = true;
            audioSource.Play();
            transform.GetChild(0).gameObject.SetActive(true);
            if(actions.Count > 0)
            {
                foreach (var action in actions)
                {
                    action.Explode();
                }
            }
            collider2d.enabled = false;
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
