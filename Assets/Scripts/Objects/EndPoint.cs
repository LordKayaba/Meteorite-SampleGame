using UnityEngine;

public class EndPoint : MonoBehaviour
{
    bool isActivated = false;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isActivated && collision.gameObject.CompareTag("Player"))
        {
            isActivated = true;
            GameManager.Instance.OnLevelComplete();
        }
    }
}
