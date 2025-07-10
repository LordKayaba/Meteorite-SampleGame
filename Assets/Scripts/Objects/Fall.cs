using UnityEngine;

public class Fall : MonoBehaviour
{
    void Start()
    {
        gameObject.AddComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (transform.position.y < -15)
        {
            gameObject.SetActive(false);
        }
    }
}
