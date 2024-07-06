using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuConceptArtGallery : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> conceptArts;

    [SerializeField]
    private List<Button> buttons;

    [SerializeField]
    private Button back;

    private Button lastButtonSelected;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            if (EventSystem.current.currentSelectedGameObject == buttons[i].gameObject)
            {
                conceptArts[i].SetActive(true);
                lastButtonSelected = buttons[i];
            }
            else if (EventSystem.current.currentSelectedGameObject == back.gameObject)
            {
                Navigation nav = back.navigation;
                nav.selectOnUp = lastButtonSelected;
                back.navigation = nav;
                //back.navigation.selectOnUp = lastButtonSelected;
            }
            else
            {
                conceptArts[i].SetActive(false);
            }
        }
    }
}
