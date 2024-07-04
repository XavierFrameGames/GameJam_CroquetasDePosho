using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodCreation : MonoBehaviour
{
  
    [SerializeField] private GameObject[] foodList;
    [SerializeField] private int currentPart;
    [SerializeField] private Transform[] spawnPoint;
    [SerializeField] private InputDetector inputDetector;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnEnable()
    {
        SpawnFood();
    }

    private void SpawnFood()
    {
        int currentFood = Random.Range(0, currentPart);
        int spawnPosition = Random.Range(0, 2);
        GameObject food = Instantiate(foodList[currentFood], spawnPoint[spawnPosition].transform.position, spawnPoint[spawnPosition].rotation, inputDetector.transform);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CurrentLastIntFood(int part)
    {
        currentPart = part;
    }
}
