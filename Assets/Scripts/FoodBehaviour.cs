using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodBehaviour : MonoBehaviour
{
    private RectTransform rect;
    [SerializeField] private float speed;
    [SerializeField] private InputDetector inputDetector;
    public int correctInput;
    void Start()
    {
        rect = GetComponent<RectTransform>();
        inputDetector = transform.parent.GetComponent<InputDetector>();
        inputDetector.foodTrans.Add(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        rect.Translate(Vector3.right * speed  * Time.deltaTime);
        float distanceToCenter = Mathf.Abs(inputDetector.GetComponent<RectTransform>().anchoredPosition.x  - rect.anchoredPosition.x);
        if(distanceToCenter < 1f)
        {
            inputDetector.foodTrans.Remove(gameObject);
            DestroyFood();
    
        }
    }

    private void DestroyFood()
    {
        Destroy(gameObject);
        //inputDetector.foodTrans.RemoveAt()  Detectar en que numero esta el rect para poderlo quitar
       // Destroy();
    }
}
