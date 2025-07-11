using UnityEngine;

public class Melt : MonoBehaviour
{
    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            audioSource.Play();
            Meteorite meteorite = collision.gameObject.GetComponent<Meteorite>();
            meteorite.isDamage = false;
            meteorite.AddHealth(100);
            meteorite.AddMass(0.6f);
            meteorite.Trail(false);
            SavePoint();
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            audioSource.Play();
            Meteorite meteorite = collision.gameObject.GetComponent<Meteorite>();
            meteorite.Trail(true);
            meteorite.AddMass(-0.6f);
            meteorite.isDamage = true;
        }
    }

    void SavePoint()
    {
        if (GameManager.Instance.savedPoint != gameObject)
        {
            GameManager.Instance.SavePoint(gameObject);
        }
    }
}
