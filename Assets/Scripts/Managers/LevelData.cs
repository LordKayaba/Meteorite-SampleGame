using UnityEngine;

public class LevelData : MonoBehaviour
{
    [SerializeField]
    int lifeCount;
    public int LifeCount => lifeCount;

    [SerializeField]
    GameObject startPoint;
    public GameObject StartPoint => startPoint;
}