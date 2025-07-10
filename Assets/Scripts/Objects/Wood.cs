using System.Collections;
using UnityEngine;

public class Wood : MonoBehaviour, IAction
{
    [SerializeField] protected float time = 0.5f;
    [SerializeField] protected bool canBurn = true;
    [SerializeField]
    protected GameObject[] Falls;

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
        fall();

        if (collider2) collider2.enabled = false;

        spriteRenderer.enabled = false;
        particle.SetActive(true);
        StartCoroutine(DisableRoutine());
    }

    bool isFall = false;
    public void fall()
    {
        if (!isFall && Falls.Length > 0)
        {
            for (int i = 0; i < Falls.Length; i++)
            {
                if (Falls[i])
                {
                    if (Falls[i].GetComponent<Wood>())
                    {
                        Falls[i].GetComponent<Wood>().fall();
                    }
                    Falls[i].AddComponent<Fall>();
                }
            }
            isFall = true;
        }
    }
}
