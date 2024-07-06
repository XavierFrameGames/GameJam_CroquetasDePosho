using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodBehaviour : MonoBehaviour
{
    private RectTransform rect;
    [SerializeField] private float speed;
    [SerializeField] private InputDetector inputDetector;
    private Vector2 initialPos;
    public int correctInput;

    void Start()
    {
        rect = GetComponent<RectTransform>();
        initialPos = rect.anchoredPosition;
        inputDetector = transform.parent.GetComponent<InputDetector>();
        inputDetector.foodTrans.Add(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        rect.Translate(Vector3.right * speed  * Time.deltaTime);
        float distanceToCenter = Mathf.Abs(initialPos.x  - rect.anchoredPosition.x);
        float totalDistance = Mathf.Abs(inputDetector.GetComponent<RectTransform>().anchoredPosition.x  - initialPos.x);
        if(distanceToCenter > (totalDistance - 1f))
        {
            
            DestroyFood(false);
    
        }
    }

    public void DestroyFood(bool goodInput)
    {
        inputDetector.foodTrans.Remove(gameObject);
        //Destroy(gameObject, 0.5f);
        if (goodInput)
        {
            StartCoroutine(FadeFillCorutine());
        }
        else
        {
            StartCoroutine(OnlyFadeCorutine());
        }
       
    }

    IEnumerator OnlyFadeCorutine()
    {
        float duration = 0.13f;
        float timePass = 0;
        Image img = GetComponent<Image>();
        while (timePass < duration)
        {
            timePass += Time.deltaTime;
            img.color = new Color(img.color.r, img.color.g, img.color.b, 1 - (timePass / duration));
            yield return null;
        }

        Destroy(gameObject);
    }

    IEnumerator FadeFillCorutine()
    {
        float duration = 0.25f;
        float timePass = 0;
        Image img = GetComponent<Image>();
        while (timePass < duration)
        {
            timePass += Time.deltaTime;
            
            img.color = new Color(img.color.r, img.color.g, img.color.b, 1 - (timePass / duration));
            yield return null;
        }

        Destroy(gameObject);
    }
}
