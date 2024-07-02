using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadPanel(GameObject panel)
    {
        panel.SetActive(!panel.activeInHierarchy);
    }

    public void ButtonToSelect(Button butt)
    {
        butt.Select();
    }

    public void LoadCharacterPanel(GameObject multiplayerManager)
    {
        multiplayerManager.SetActive(true);
        multiplayerManager.GetComponent<CustomMultiplayerManager>().EnableJoining();
        multiplayerManager.GetComponent<CustomMultiplayerManager>().BeginStart();
        multiplayerManager.transform.parent.gameObject.SetActive(true);
    }

    

    public void Exit()
    {
        Application.Quit();
    }
}
