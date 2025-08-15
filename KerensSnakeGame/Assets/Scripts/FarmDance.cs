using System.Drawing;
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
        // 1. a wall (farm fence)
        // 2. another animal
        // OnGameOver()?.Invoke();
        // 3. fruit
        // OnEatingFruit()?.Invoke();
        // AddAnimalToFarmDance(newPosition);
        // for this, I need to add collisions
        // else:
        MoveLastAnimalToNewPosition(newPosition);
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
