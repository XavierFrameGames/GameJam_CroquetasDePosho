using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
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
        StartCoroutine(CountdownCoroutine());
    }

    
    void Update()
    {
        
    }

    IEnumerator CountdownCoroutine()
    {
        yield return null;  
        countdown.SetActive(true);   
        countdown.GetComponentInChildren<TextMeshProUGUI>().text = string.Empty;  
        yield return new WaitForSeconds(1);
        countdown.GetComponentInChildren<TextMeshProUGUI>().text = "3";
        //añadir SFX de cocina (espatula o algo)

        yield return new WaitForSeconds(1);
        countdown.GetComponentInChildren<TextMeshProUGUI>().text = "2";
        //añadir SFX de cocina (espatula o algo)

        yield return new WaitForSeconds(1);
        countdown.GetComponentInChildren<TextMeshProUGUI>().text = "1";
        //añadir SFX de cocina (espatula o algo)

        yield return new WaitForSeconds(1);
        countdown.SetActive(false);

        for (int i = 0; i < GameManager.Instance.players.Count; i++) 
        {
            if (GameManager.Instance.players[i].timelineDirector.state != PlayState.Playing)
            {
                GameManager.Instance.players[i].timelineDirector.Play(timeline);
            }
            
            
               
            


        }
        AudioManager.Instance.PlayMusic(song);
        
       
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
            pausepanel.SetActive(true);
            pausepanel.transform.GetChild(0).GetChild(0).gameObject.GetComponent<Button>().Select();
        }
        else
        {
            
            pausepanel.SetActive(false);
            optionsPanel.SetActive(true);
            optionsPanel.transform.GetChild(0).GetChild(0).gameObject.GetComponent<Button>().Select();
        }

    }


    public void PauseResume(int player)
    {
        
        if(playing)
        {
            for (int i = 0; i < GameManager.Instance.players.Count; i++)
            {
                if (i != player)
                {
                    GameManager.Instance.players[i].GetComponent<PlayerInput>().enabled = false;
                }
                if (GameManager.Instance.players[i].timelineDirector.state == PlayState.Playing)
                {
                    GameManager.Instance.players[i].timelineDirector.Pause();
                }
            }
            AudioManager.Instance.PauseMusic();
            Time.timeScale = 0;
            pausepanel.SetActive(playing);
            pausepanel.transform.GetChild(0).GetChild(0).gameObject.GetComponent<Button>().Select();
            
            playing = false;
        }
            
            

        
        if(!playing)
        {
            
            for (int i = 0; i < GameManager.Instance.players.Count; i++)
            {
                GameManager.Instance.players[i].GetComponent<PlayerInput>().enabled = true;
                if (GameManager.Instance.players[i].timelineDirector.state == PlayState.Paused)
                {
                    GameManager.Instance.players[i].timelineDirector.Resume();
                }
            }
            AudioManager.Instance.ResumeMusic();
            pausepanel.SetActive(!playing);
            Time.timeScale = 1;
            playing = true;
            
        }
    }


}
