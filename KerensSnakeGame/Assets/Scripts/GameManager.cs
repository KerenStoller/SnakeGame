using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private GameObject _food;
    [SerializeField] private GameObject _farmFence;
    [SerializeField] private int _levelWidth = 10;
    [SerializeField] private int _levelHeight = 10;


    private void Awake()
    {
        buildLevel();
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

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            spawnFood();
        }
    }

    private void spawnFood()
    {
        Instantiate(_food, getRandomeSpawnPosition(), Quaternion.identity);
    }

    private Vector3 getRandomeSpawnPosition()
    {
        float newX = UnityEngine.Random.Range(0, _levelWidth - 2);
        float newY = UnityEngine.Random.Range(0, _levelHeight - 2);
        return new Vector3(newX, newY, 0);
    }
}