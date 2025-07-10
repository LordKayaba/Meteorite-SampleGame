using UnityEngine;

public class GameManager : MonoBehaviour
{
    static public GameManager Instance { private set; get; }
    void Awake()
    {
        if (!Instance)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    void Update()
    {

    }
}
