using System.Drawing;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class FarmDance : MonoBehaviour
{
    public static FarmDance Instance;
    [SerializeField] private GameObject _farmChicken;
    //[SerializeField] private GameObject _farmAnimal2;
    [SerializeField] private Vector3 _startingLocation = new Vector3(0, 0, 0);
    
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

    public void CreateFarmDance()
    {
        GameObject animal = Instantiate(_farmChicken ,_startingLocation, Quaternion.identity);
        _farmAnimals.Add(animal);
    }

    public void Dance(Vector3 direction)
    {
        //move the last animal to the new position
        if (_farmAnimals.Count == 0)
        {
            Debug.LogWarning("No farm animals to move.");
            return;
        }

        GameObject animalToMove = _farmAnimals[0];
        if (_farmAnimals.Count > 1)
        {
            animalToMove = _farmAnimals.FindLast(GameObject => GameObject != null);
        }
        
        
        moveAnimal(animalToMove, _farmAnimals[0].transform.position + direction);
    }

    void moveAnimal(GameObject animalToMove, Vector3 location)
    {
        // check if next position is not: 
        // 1. a wall (farm fence)
        // 2. another animal
        // OnGameOver()?.Invoke();
        // 3. fruit
        // OnEatingFruit()?.Invoke();
        // addAnimalToDance(location);
        // for this, I need to add collisions
        // else:
        animalToMove.transform.position = location;
    }

    void addAnimalToDance(Vector3 location)
    {
        
    }
}
