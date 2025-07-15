using UnityEngine;

public class GameManager : MonoBehaviour
{
    static public GameManager Instance { private set; get; }

    public GameState State { get; private set; }

    public int CurrentSeasonIndex { get; private set; }
    public int CurrentLevelIndex { get; private set; }

    public GameObject SavedPoint { private set; get; }
    public int LifeCount { private set; get; }

    [SerializeField]
    SeasonTableObject[] seasons;

    void Awake()
    {
        if (!Instance)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    public void SavePoint(GameObject point) => SavedPoint = point;

    public void LoadLevel(int seasonIndex, int levelIndex)
    {
        if (seasonIndex < 0 || seasonIndex >= seasons.Length)
        {
            Debug.LogError("Season index out of range");
            return;
        }

        var season = seasons[seasonIndex];
        if (levelIndex < 0 || levelIndex >= season.levelPrefabs.Length)
        {
            Debug.LogError("Level index out of range");
            return;
        }

        GameObject levelPrefab = season.levelPrefabs[levelIndex];
        GameObject levelInstance = Instantiate(levelPrefab);

        var levelData = levelInstance.GetComponent<LevelData>();
        if (levelData != null)
        {
            SetLevelData(levelData);
            State = GameState.Playing;
        }
    }

    void SetLevelData(LevelData data)
    {
        SavePoint(data.StartPoint);
        LifeCount = data.LifeCount;
    }
}

public enum GameState
{
    InMenu,
    Playing,
    RestartingLevel,
    LevelCompleted
}