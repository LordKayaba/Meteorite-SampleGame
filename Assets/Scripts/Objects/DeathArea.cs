using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathArea : MonoBehaviour
{
    public float time;
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
        meteorite.GetComponent<Meteorite>().Die();
    }
}
