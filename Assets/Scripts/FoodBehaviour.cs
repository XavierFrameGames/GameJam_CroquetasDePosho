using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodBehaviour : MonoBehaviour
{
    private RectTransform rect;
    [SerializeField] private float speed;
    [SerializeField] private InputDetector inputDetector;
    [SerializeField] private int correctInput;
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
        if(rect.anchoredPosition.x == 0)
        {
            DestroyFood();
    
        }
    }

    private void DestroyFood()
    {
        //inputDetector.foodTrans.RemoveAt()  Detectar en que numero esta el rect para poderlo quitar
        Destroy(gameObject);
    }
}
