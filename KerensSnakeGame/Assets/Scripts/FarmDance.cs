using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class FarmDance : MonoBehaviour
{
    public static FarmDance Instance;
    [SerializeField] private GameObject _farmChicken;
    //TODO
    //add different animals to the dance
    //[SerializeField] private GameObject _farmAnimal2;
    
    private List<GameObject> _farmAnimals = new List<GameObject>();
    
    public event System.Action OnEatingFruit;
    public event System.Action OnGameOver;
    
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

    public void AddAnimalToFarmDance(Vector3 location)
    {
        GameObject animal = Instantiate(_farmChicken ,location, Quaternion.identity);
        _farmAnimals.Insert(0, animal);
        Debug.Log("added animal! Current count: " + _farmAnimals.Count);
    }

    public void Dance(Vector3 direction)
    {
        //move the last animal to the new position
        if (_farmAnimals.Count == 0)
        {
            Debug.LogWarning("No farm animals to move.");
            return;
        }
        
        Vector3 newPosition = _farmAnimals[0].transform.position + direction;

        //TODO
        // check if new position is not: 

        if (IsAWall(newPosition) || IsAnAnimal(newPosition))
        {
            Debug.Log("Game Over! You hit a wall or another animal.");
            // Trigger game over event
            OnGameOver?.Invoke();
            return;
        }

        if (IsAFruit(newPosition))
        {
            OnEatingFruit?.Invoke();
            AddAnimalToFarmDance(newPosition);
            return;
        }
        
        // If the new position is valid, move the last animal to the new position
        MoveLastAnimalToNewPosition(newPosition);
    }
    
    public bool IsAnAnimal(Vector3 position)
    {
        return _farmAnimals.Any(part => 
            Vector3.Distance(part.transform.position, position) < 0.1f);
    }

    private bool IsAWall(Vector3 position)
    {
        return position.y >= GameLogic.Instance._levelHeight -2 || 
               position.y < 0 ||
               position.x >= GameLogic.Instance._levelWidth -2 ||
               position.x < 0;
    }

    private bool IsAFruit(Vector3 position)
    {
        return (Vector3.Distance(GameLogic.Instance._fruit.transform.position, position) < 0.1f);
    }

    void MoveLastAnimalToNewPosition(Vector3 newPosition)
    {
        if (_farmAnimals.Count == 0)
        {
            Debug.LogWarning("No farm animals to remove.");
            return;
        }
        
        int lastIndex = _farmAnimals.Count - 1;
        GameObject lastAnimal = _farmAnimals[lastIndex];
        _farmAnimals.RemoveAt(lastIndex);
        
        lastAnimal.transform.position = newPosition;
        _farmAnimals.Insert(0, lastAnimal);
    }
}
