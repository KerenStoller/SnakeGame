using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject _FarmAnimal;
    [Header("Movement Settings")]
    [SerializeField] private float _timeToMove = 0.5f;
    
    private Vector3? _direction = null;
    private float _moveCountdown = 0;
    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.S))
        {
            _direction = Vector3.down;
        }
        else if (Input.GetKey(KeyCode.W))
        {
            _direction = Vector3.up;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            _direction = Vector3.left;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            _direction = Vector3.right;
        }
        
        _moveCountdown += Time.deltaTime;
        
        if(_moveCountdown >= _timeToMove)
        {
            _moveCountdown = 0;
            MoveFarmAnimal();
        }
    }
    
    void MoveFarmAnimal()
    {
        if (_direction.HasValue)
        {
            _FarmAnimal.transform.position += _direction.Value;
        }
        /*
        if (_direction.HasValue)
        {
            Vector3 newPosition = transform.position + _direction.Value;
            Instantiate(_FarmAnimal, newPosition, Quaternion.identity);
            _direction = null; // Reset direction after moving
        }*/
    }
}
