using System.Collections;
using UnityEngine;

public class Wood : MonoBehaviour, IAction
{
    [SerializeField] protected float time = 0.5f;
    [SerializeField] protected bool canBurn = true;
    [SerializeField]
    protected GameObject[] falls;

    protected GameObject particle;
    protected SpriteRenderer spriteRenderer;
    protected Collider2D collider2;
    protected AudioSource audioSource;

    protected virtual void Start()
    {
        particle = transform.GetChild(0).gameObject;
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider2 = GetComponent<Collider2D>();
        audioSource = GetComponent<AudioSource>();
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (canBurn && collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Burning());
        }
    }

    protected IEnumerator Burning()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(time);
        Explode();
    }

    IEnumerator DisableRoutine()
    {
        yield return new WaitForSeconds(5);
        gameObject.SetActive(false);
    }

    public void Explode()
    {
        audioSource.Play();
        Fall();

        if (collider2) collider2.enabled = false;

        spriteRenderer.enabled = false;
        particle.transform.SetParent(null);
        particle.SetActive(true);
        StartCoroutine(DisableRoutine());
    }

    bool isFall = false;
    public void Fall()
    {
        if (isFall || falls.Length == 0) return;

        foreach (var fall in falls)
        {
            if (!fall) continue;
            fall.GetComponent<Wood>()?.Fall();
            fall.AddComponent<Fall>();
        }

        isFall = true;
    }

    void OnDisable()
    {
        particle?.SetActive(false);
    }
}
