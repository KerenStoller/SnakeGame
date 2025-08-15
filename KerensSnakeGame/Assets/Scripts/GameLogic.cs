using System;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class GameLogic : MonoBehaviour
{
    public static GameLogic Instance;
    [SerializeField] private GameObject _food;
    [SerializeField] private GameObject _farmFence;
    [SerializeField] private int _levelWidth = 10;
    [SerializeField] private int _levelHeight = 10;
    public GameState _gameState { get; private set; } = GameState.NotStarted;
    private GameObject _fruit;

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        Start();
    }

    private void Start()
    {
        buildLevel();
        PlayerController.Instance.OnPlayerInput += OnPlayerInput;
        FarmDance.Instance.OnEatingFruit += OnEatingFruit;
        FarmDance.Instance.OnGameOver += OnGameOver;
        FarmDance.Instance.CreateFarmDance();
        SpawnFruit();
    }
    
    private void buildLevel()
    {
        Debug.Log("Building level with width: " + _levelWidth + " and height: " + _levelHeight);
        for (int i = -1; i < _levelWidth - 1; i++)
        {
            Instantiate(_farmFence, new Vector3(i, -1, 0), Quaternion.identity);
            Instantiate(_farmFence, new Vector3(i, _levelHeight - 2, 0), Quaternion.identity);
        }

        for (int j = 0; j < _levelHeight - 2; j++)
        {
            Instantiate(_farmFence, new Vector3(-1, j, 0), Quaternion.identity);
            Instantiate(_farmFence, new Vector3(_levelWidth - 2, j, 0), Quaternion.identity);
        }
    }

    void OnPlayerInput()
    {
        _gameState = GameState.Playing;
    }

    void OnEatingFruit()
    {
        // raise score
    }
    
    void OnGameOver()
    {
        _gameState = GameState.GameOver;
        // show game over screen
        Debug.Log("Game Over!");
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    private void SpawnFruit()
    {
        Instantiate(_food, getRandomFruitPosition(), Quaternion.identity);
    }

    private Vector3 getRandomFruitPosition()
    {
        // Here we make sure the fruit can spawn anywhere in the level except the walls (farm fence)
        // We need to add collisions to avoid the area where the farm animals are dancing
        // and the area where the fruit is already spawned
        float newX = UnityEngine.Random.Range(0, _levelWidth - 2);
        float newY = UnityEngine.Random.Range(0, _levelHeight - 2);
        return new Vector3(newX, newY, 0);
    }
}