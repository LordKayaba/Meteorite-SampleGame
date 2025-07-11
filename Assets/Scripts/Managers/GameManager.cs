using UnityEngine;

public class GameManager : MonoBehaviour
{
    static public GameManager Instance { private set; get; }
    public GameObject savedPoint { private set; get; }
    void Awake()
    {
        if (!Instance)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    public void SavePoint(GameObject point) => savedPoint = point;

    void Update()
    {

    }
}
