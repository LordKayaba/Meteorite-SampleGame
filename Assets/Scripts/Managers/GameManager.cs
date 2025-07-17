using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static public GameManager Instance { get; private set; }

    public GameState State { get; private set; }

    public int CurrentSeasonIndex { get; private set; }
    public int CurrentLevelIndex { get; private set; }

    public Vector3 SavedPoint { get; private set; }
    public int LifeCount { get; private set; }
    public int CollectedStars { get; private set; }

    [SerializeField]
    SeasonTableObject[] seasons;

    List<string> disabledGameObjects = new();

    void Awake()
    {
        if (Instance) { Destroy(gameObject); return; }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
        StartCoroutine(Test());
    }

    IEnumerator Test()
    {
        yield return new WaitForSeconds(1);
        LoadLevel(0, 0);
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode) => Setup();

    void Setup()
    {
        switch (State)
        {
            case GameState.InMenu:
                break;
            case GameState.Playing: LoadLevel(CurrentSeasonIndex, CurrentLevelIndex);
                break;
            case GameState.RestartingLevel:
                break;
            case GameState.LevelCompleted:
                break;
            default:
                break;
        }
    }

    public void ReloadCurrentScene() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    public void SavePoint(Vector3 point) => SavedPoint = point;

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

        GameObject levelInstance = Instantiate(season.levelPrefabs[levelIndex]);

        if (State == GameState.Playing) return;

        var levelData = levelInstance.GetComponent<LevelData>();
        if (levelData != null)
        {
            SetLevelData(levelData);
            State = GameState.Playing;
        }
    }

    public void AddToDisabledGameObjects(GameObject obj) => disabledGameObjects.Add(obj.name);

    public bool ShouldBeDisabled(GameObject obj) => disabledGameObjects.Contains(obj.name);

    public void AddStar() => CollectedStars++;

    void SetLevelData(LevelData data)
    {
        SavePoint(data.StartPoint.transform.position);
        LifeCount = data.LifeCount;
    }

    public void OnLevelComplete()
    {
        State = GameState.LevelCompleted;
    }

    public void OnPlayerDied()
    {
        if (LifeCount <= 0) return;

        LifeCount--;
        StartCoroutine(ReloadCurrentSceneDelay(5));
    }

    IEnumerator ReloadCurrentSceneDelay(float time)
    {
        yield return new WaitForSeconds(time);
        ReloadCurrentScene();
    }
}

public enum GameState
{
    InMenu,
    Playing,
    RestartingLevel,
    LevelCompleted
}