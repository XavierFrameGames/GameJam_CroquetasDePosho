using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{

    [SerializeField] private PlayableAsset timeline;
    [SerializeField] private GameObject countdown;
    [SerializeField] private GameObject pausepanel;
    [SerializeField] private GameObject optionsPanel;
    public bool playing;
    [SerializeField] private AudioClip song;
    
    


    
    void Start()
    {
        for(int i = 0; i < GameManager.Instance.players.Count; i++) 
        {
            GameManager.Instance.players[i].GetComponent<Player>().ResetPoints();

            for (int j = 0; j < GameManager.Instance.players[i].GetComponent<Player>().inputDetector.foodTrans.Count; j++)
            {
                Destroy(GameManager.Instance.players[i].GetComponent<Player>().inputDetector.foodTrans[j].gameObject);
                
            }
            GameManager.Instance.players[i].GetComponent<Player>().inputDetector.foodTrans = new List<GameObject>();

        }
        
      
        StartCoroutine(CountdownCoroutine(false));
    }

    
    void Update()
    {
        
    }

    IEnumerator CountdownCoroutine(bool inPause)
    {
        yield return null;
        for (int i = 0; i < GameManager.Instance.players.Count; i++)
        {

           GameManager.Instance.players[i].GetComponent<Player>().enabled = false;
           GameManager.Instance.players[i].GetComponent<Player>().DisableActions(new string[] { "Pause"});

        }
        if(inPause)
        {
            pausepanel.SetActive(false);

        }
        
        countdown.SetActive(true);   
        countdown.GetComponentInChildren<TextMeshProUGUI>().text = string.Empty;
        yield return new WaitForSecondsRealtime(1);
        countdown.GetComponentInChildren<TextMeshProUGUI>().text = "3";
        //AudioManager.Instance.PlaySFX();

        yield return new WaitForSecondsRealtime(1);
        countdown.GetComponentInChildren<TextMeshProUGUI>().text = "2";
        //AudioManager.Instance.PlaySFX();

        yield return new WaitForSecondsRealtime(1);
        countdown.GetComponentInChildren<TextMeshProUGUI>().text = "1";
        // AudioManager.Instance.PlaySFX();

        yield return new WaitForSecondsRealtime(1);
        countdown.SetActive(false);
        for (int i = 0; i < GameManager.Instance.players.Count; i++)
        {

            GameManager.Instance.players[i].GetComponent<Player>().enabled = true;
            GameManager.Instance.players[i].GetComponent<Player>().EnableActions(new string[] { "Pause" });

        }
        if (!inPause)
        {
            for (int i = 0; i < GameManager.Instance.players.Count; i++)
            {
                if (GameManager.Instance.players[i].timelineDirector.state != PlayState.Playing)
                {
                    GameManager.Instance.players[i].timelineDirector.Play(timeline);
                }

            }
            AudioManager.Instance.PlayMusic(song);
        }
        if(inPause)
        {
            Time.timeScale = 1;
            Debug.Log("inPause corr");
            for (int i = 0; i < GameManager.Instance.players.Count; i++)
            {
                GameManager.Instance.players[i].GetComponent<Player>().enabled = true;
                GameManager.Instance.players[i].GetComponent<PlayerInput>().SwitchCurrentActionMap("Game");



                if (GameManager.Instance.players[i].timelineDirector.state == PlayState.Paused)
                {
                    GameManager.Instance.players[i].timelineDirector.Resume();
                    GameManager.Instance.players[i].GetComponent<Player>().EnableActions(new string[] { "Pause" });
                }
            }
            AudioManager.Instance.ResumeMusic();
            pausepanel.SetActive(false);
            Debug.Log("pause panel false");
            
            
        }
        
        
        
        
       
    }

    public void Retry()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); 
        


    }

    public void ReturnMainMenu()
    {
        Time.timeScale = 1.0f;
        int count = GameManager.Instance.players.Count;
        for (int i = 0; i < count; i++)
        {
            Destroy(GameManager.Instance.players[0].gameObject);
            GameManager.Instance.players.RemoveAt(0);
        }
        GameManager.Instance.players = new List<Player>();
        SceneManager.LoadScene(0);
    }
    public void OptionsPanel(bool inOptions)
    {
        if (inOptions)
        {
            optionsPanel.SetActive(false);
            pausepanel.SetActive(true);
            pausepanel.transform.GetChild(0).GetChild(0).gameObject.GetComponent<Button>().Select();
        }
        else
        {
            
            pausepanel.SetActive(false);
            optionsPanel.SetActive(true);
            optionsPanel.transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<Button>().Select();
        }

    }


    public void Pause(int player)
    {
        
        if(AudioManager.Instance.musicSource.isPlaying == true)
        {
            for (int i = 0; i < GameManager.Instance.players.Count; i++)
            {
                if (i != player)
                {
                    GameManager.Instance.players[i].GetComponent<Player>().enabled = false;
                    GameManager.Instance.players[i].GetComponent<PlayerInput>().SwitchCurrentActionMap("bait");
                }
               
                
                   
                
                if (GameManager.Instance.players[i].timelineDirector.state == PlayState.Playing)
                {
                    GameManager.Instance.players[i].timelineDirector.Pause();
                    GameManager.Instance.players[i].GetComponent<Player>().DisableActions(new string[] { "Pause"});
                }
            }
            AudioManager.Instance.PauseMusic();

            Time.timeScale = 0;
            pausepanel.SetActive(true);
            
            Button butt = pausepanel.transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<Button>();
            GameManager.Instance.players[player].GetComponent<MultiplayerEventSystem>().playerRoot = pausepanel.transform.parent.gameObject;
            GameManager.Instance.players[player].GetComponent<EventSystem>().SetSelectedGameObject(butt.gameObject);
            EventSystem.current = GameManager.Instance.players[player].GetComponent<EventSystem>();



        }
        

        
    }

    public void Resume()
    {
        
            //Time.timeScale = 1;
            StartCoroutine(CountdownCoroutine(true));
        
    }


}
