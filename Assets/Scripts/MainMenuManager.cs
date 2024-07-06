using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    private AudioManager aM;
    [SerializeField] private AudioClip selectSFX, choseSFX, musicTheme;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        aM = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        aM.PlayMusic(musicTheme);
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

    public void UISelect()
    {
        aM.PlayUISound(selectSFX);
    }

    public void UIChosen()
    {
        aM.PlayUISound(choseSFX);
    }
}
