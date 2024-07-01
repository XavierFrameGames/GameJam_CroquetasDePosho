using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCharacterManager : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> playerPanels;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivatePlayerPanel(int i)
    {
        playerPanels[i].SetActive(true);
    }
}
