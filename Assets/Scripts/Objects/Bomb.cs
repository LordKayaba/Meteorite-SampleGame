using UnityEngine;

public class Bomb : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            /*GameData.data.level.AddBomb(1);
            GameData.data.level.GameObjectRemove(gameObject);
            UIManager.ui.SetUI();
            AudioManager.Audio.FX(AudioManager.Audio.data.FXs.Bomb);
            GameObject fx = Instantiate(FX);
            fx.transform.position = transform.position;
            Destroy(gameObject);*/
        }
    }
}
