using DefaultNamespace;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;
    [Header("Movement Settings")]
    [SerializeField] private float _timeToMove = 0.5f;
    
    public event System.Action OnPlayerInput;
    
    private Vector3? _direction = null;
    private float _moveCountdown = 0;
    
    void Awake()
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
    
    // Update is called once per frame
    void Update()
    {
        HandleInput();

        if (GameLogic.Instance._gameState == GameState.NotStarted || 
            GameLogic.Instance._gameState == GameState.GameOver)
        {
            // If the game is not started or is over, do nothing
            return;
        }
        
        _moveCountdown += Time.deltaTime;
        
        if(_moveCountdown >= _timeToMove)
        {
            _moveCountdown = 0;
            MoveFarmAnimal();
        }
    }
    
    void HandleInput()
    {
        if (Input.anyKeyDown)
        {
            OnPlayerInput?.Invoke();
        }
        else
        {
            return;
        }
        
        if (Input.GetKeyDown(KeyCode.S))
        {
            _direction = Vector3.down;
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            _direction = Vector3.up;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            _direction = Vector3.left;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            _direction = Vector3.right;
        }
    }
    
    void MoveFarmAnimal()
    {
        if (_direction.HasValue)
        {
            FarmDance.Instance.Dance(_direction.Value);
        }
    }
}
