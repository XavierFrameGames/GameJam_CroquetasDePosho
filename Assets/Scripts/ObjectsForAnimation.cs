using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsForAnimation : MonoBehaviour
{
    public GameObject objectt;
    public GameObject[] objectts;

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

    public void ActivateOneObject(int activate)
    {
        for (int i = 0; i < objectts.Length; i++)
        {
            objectts[i].SetActive(false);
        }
        if (activate == 0)
        {
            return;
        }
        else
        {
            objectts[activate - 1].SetActive(true);
        }
    }
}
