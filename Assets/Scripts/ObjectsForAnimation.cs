using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsForAnimation : MonoBehaviour
{
    public GameObject objectt;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActiveTrueFalse(int activate)
    {
        if(activate == 1)
        {
            objectt.SetActive(true);
        }
        else
        {
            objectt.SetActive(false);
        }
    }
}
