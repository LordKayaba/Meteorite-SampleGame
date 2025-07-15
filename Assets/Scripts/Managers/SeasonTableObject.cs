using UnityEngine;

[CreateAssetMenu(fileName = "New Season", menuName = "Game/Season Table")]
public class SeasonTableObject : ScriptableObject
{
    public string seasonName;
    public GameObject[] levelPrefabs;
}