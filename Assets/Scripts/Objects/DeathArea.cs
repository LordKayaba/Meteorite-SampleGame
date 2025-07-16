using System.Collections;
using UnityEngine;

public class DeathArea : MonoBehaviour
{
    [SerializeField]
    float time;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Die(collision.gameObject));
        }
    }

    IEnumerator Die(GameObject meteorite)
    {
        yield return new WaitForSeconds(time);
        meteorite.GetComponent<Meteorite>().Damage(100);
    }
}
