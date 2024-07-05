using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
            img.fillAmount = 1 - (timePass / duration);
            img.color = new Color(img.color.r, img.color.g, img.color.b, 1 - (timePass / duration));
            yield return null;
        }

        Destroy(gameObject);
    }
}
