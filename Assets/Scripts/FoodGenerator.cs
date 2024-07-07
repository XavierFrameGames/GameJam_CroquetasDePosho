using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FoodCreation : MonoBehaviour
{
  
    [SerializeField] public  GameObject[] foodList;
    [SerializeField] private Transform currentPart;
    [SerializeField] private Transform[] initialParts;
    [SerializeField] private Transform[] spawnPoint;
    [SerializeField] private InputDetector inputDetector;
    [SerializeField] private int spawnList;
    

    // Start is called before the first frame update
    void Awake()
    {
        int part = SceneManager.GetActiveScene().buildIndex - 2;
        currentPart = initialParts[part];
    }
    private void OnEnable()
    {
        SpawnFood();
    }

    private void SpawnFood()
    {
        int currentFood = Random.Range((int)currentPart.localPosition.x, (int)currentPart.localPosition.y); //Scene Object transform x and y
        Debug.Log(currentFood);
        int spawnPosition = Random.Range(0, 2);
        GameObject food = Instantiate(foodList[currentFood], spawnPoint[spawnPosition].transform.position, spawnPoint[spawnPosition].rotation, inputDetector.transform);
        
    }

    // Update is called once per frame
    void Update() 
    {
        
    }

    public void CurrentFoodInterval(Transform part)
    {
        currentPart = part;
    }
    
   

}
