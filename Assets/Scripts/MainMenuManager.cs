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
        Time.timeScale = 1;
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

    public void SelectSlider(Slider slid)
    {
        slid.Select();
    }

    public void LoadCharacterPanel(GameObject multiplayerManager)
    {
        multiplayerManager.SetActive(true);
        multiplayerManager.GetComponent<CustomMultiplayerManager>().EnableJoining();
        multiplayerManager.GetComponent<CustomMultiplayerManager>().BeginStart();
        multiplayerManager.transform.parent.gameObject.SetActive(true);
    }

    public void SelectLevel(int level)
    {
        if (level == 1) //tutorial
        {
            GameManager.Instance.tutorialDone = true;
        }

        for (int i = 0; i < GameManager.Instance.players.Count; i++)
        {
            GameManager.Instance.players[i].transform.GetChild(0).gameObject.SetActive(true);
            //GameManager.Instance.players[i].transform.GetChild(1).gameObject.SetActive(true);
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + level);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
