using UnityEngine;

public class Star : MonoBehaviour
{
    public GameObject FX;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Contains("Meteorite"))
        {
            /*GameData.data.level.AddStar(gameObject);
            AudioManager.Audio.FX(AudioManager.Audio.data.FXs.Star);*/
            GameObject fx = Instantiate(FX);
            fx.transform.position = transform.position;
            Destroy(gameObject);
        }
    }
}
