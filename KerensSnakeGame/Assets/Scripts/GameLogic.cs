using DefaultNamespace;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    public static GameLogic Instance;
    [SerializeField] private GameObject _food;
    [SerializeField] private GameObject _farmFence;
    [SerializeField] public int _levelWidth = 10;
    [SerializeField] public int _levelHeight = 10;
    
    [SerializeField] private Vector3 _startingLocation = new Vector3(0, 0, 0);
    public GameState _gameState { get; private set; } = GameState.NotStarted;
    public GameObject _fruit;
    
    private int _foodCount;

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
    }

    private void Start()
    {
        BuildLevel();
        PlayerController.Instance.OnPlayerInput += OnPlayerInput;
        FarmDance.Instance.AddAnimalToFarmDance(_startingLocation);
        FarmDance.Instance.OnEatingFruit += OnEatingFruit;
        FarmDance.Instance.OnGameOver += OnGameOver;
        SpawnFruit();
        _foodCount = 0;
    }
    
    private void BuildLevel()
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
        Debug.Log("Eating fruit!");
        Destroy(_fruit);
        SpawnFruit();
        _foodCount++;
    }
    
    void OnGameOver()
    {
        _gameState = GameState.GameOver;
        //TODO
        // show game over screen
        Debug.Log("Game Over!");
    }

    private void SpawnFruit()
    {
        _fruit = Instantiate(_food, GetRandomFruitPosition(), Quaternion.identity);
    }

    private Vector3 GetRandomFruitPosition()
    {        
        if (_foodCount >= 5)
        {
            Debug.Log("Max food count reached, no more fruit will spawn.");
            return Vector3.zero; // or handle as needed
        }
        
        float newX = Random.Range(0, _levelWidth - 2);
        float newY = Random.Range(0, _levelHeight - 2);
        
        Vector3 suggestedPosition = new Vector3(newX, newY, 0);
        
        if (FarmDance.Instance.IsAnAnimal(suggestedPosition))
        {
            // this won't cause a loop since we have the food count limit
            return GetRandomFruitPosition();
        }
        
        
        return suggestedPosition;
    }
}