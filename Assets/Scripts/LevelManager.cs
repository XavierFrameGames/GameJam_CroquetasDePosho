using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.Playables;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{

    [SerializeField] private PlayableAsset timeline;
    [SerializeField] private GameObject countdown;
    [SerializeField] private GameObject pausepanel;
    [SerializeField] private GameObject optionsPanel;
    [SerializeField] private GameObject rankingPanel;
    [SerializeField] private TextMeshProUGUI rankingText;
    [SerializeField] private GameObject[] food;
    public bool playing;
    [SerializeField] private AudioClip song;
    [SerializeField] private EventSystem globalEventSystem;


    private IEnumerator finalCoroutine;

    
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

    IEnumerator FinalLevelCoroutine()
    {
        //Audiomanager.Instance.PlaySFX()Piiiiii
        for(int i = 0; i < GameManager.Instance.players.Count;i++)
        {
            GameObject canvas = GameManager.Instance.players[i].gameObject.transform.GetChild(0).GetChild(2).gameObject;
            for (int j = 0; j < canvas.transform.childCount; j++)
            {
                canvas.transform.GetChild(j).gameObject.SetActive(false);
            }
        }
        yield return null;
        // sfx tambores
        countdown.SetActive(true);
        countdown.GetComponentInChildren<TextMeshProUGUI>().text = string.Empty;
        yield return new WaitForSecondsRealtime(1);
        countdown.GetComponentInChildren<TextMeshProUGUI>().text = "3";
       

        yield return new WaitForSecondsRealtime(1);
        countdown.GetComponentInChildren<TextMeshProUGUI>().text = "2";
        

        yield return new WaitForSecondsRealtime(1);
        countdown.GetComponentInChildren<TextMeshProUGUI>().text = "1";
        

        yield return new WaitForSecondsRealtime(1);
        countdown.SetActive(false);

        //AudioManager.Instance.PlaySFX(); confetti
        List<Player> ordered = OrderPlayers();
        rankingText.text = "";
        for(int i = 0; i < ordered.Count; i++)
        {
            string skinName = "";
            switch (ordered[i].playerSkin)
            {
                case 0:
                    skinName = "Cavernícola";
                    break;
                case 1:
                    skinName = "Chica";
                    break;
                case 2:
                    skinName = "Chico";
                    break;
                case 3:
                    skinName = "Robot";
                    break;
                default:
                    break;
            }
            rankingText.text += (i + 1).ToString() + " " + skinName + ": " + ordered[i].points.ToString() + "\n";
        }
        
        rankingPanel.SetActive(true);
        




        for (int i = 0; i < ordered.Count; i++)
        {
            
            Debug.Log("Entered for ordered");
            if (i == 0)
            {
                GameObject finalfood = Instantiate(food[2], ordered[i].finalFoodTrans[ordered[i].playerSkin].position, 
                    ordered[i].finalFoodTrans[ordered[i].playerSkin].rotation);   //food buena
                ordered[i].scenes[ordered[i].playerSkin].transform.GetChild(0).GetComponent<Animator>().SetTrigger("Victory");
                Debug.Log("Entered and victory");
            }
            else if (i == ordered.Count - 1)
            {
                GameObject finalfood = Instantiate(food[0], ordered[i].finalFoodTrans[ordered[i].playerSkin].position,
                  ordered[i].finalFoodTrans[ordered[i].playerSkin].rotation);  //food mala
                ordered[i].scenes[ordered[i].playerSkin].transform.GetChild(0).GetComponent<Animator>().SetTrigger("Defeat");
                Debug.Log("Entered and def");
            }
            else
            {
                GameObject finalfood = Instantiate(food[1], ordered[i].finalFoodTrans[ordered[i].playerSkin].position,
                  ordered[i].finalFoodTrans[ordered[i].playerSkin].rotation);   //food media
                ordered[i].scenes[ordered[i].playerSkin].transform.GetChild(0).GetComponent<Animator>().SetTrigger("Defeat");
                Debug.Log("Entered and def");
            }
        }

        EventSystem.current = globalEventSystem;
        rankingPanel.transform.GetChild(0).GetChild(0).GetComponent<Button>().Select();



        
      
        

    }

    public void FinishLevel()
    {
        if (finalCoroutine != null)
        {
            StopCoroutine(finalCoroutine);
        }
        finalCoroutine = FinalLevelCoroutine();
        StartCoroutine(finalCoroutine);
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
        for (int i = 0; i < GameManager.Instance.players.Count; i++)
        {
            GameObject canvas = GameManager.Instance.players[i].gameObject.transform.GetChild(0).GetChild(2).gameObject;
            for (int j = 0; j < canvas.transform.childCount; j++)
            {
                canvas.transform.GetChild(j).gameObject.SetActive(true);
            }
            canvas.transform.GetChild(4).gameObject.SetActive(false);
            canvas.transform.GetChild(5).gameObject.SetActive(false);
            canvas.transform.GetChild(6).gameObject.SetActive(false);
            canvas.transform.GetChild(7).gameObject.SetActive(false);
            canvas.transform.GetChild(8).gameObject.SetActive(false);
            canvas.transform.GetChild(9).gameObject.SetActive(false);
            canvas.transform.GetChild(10).gameObject.SetActive(false);
            canvas.transform.GetChild(11).gameObject.SetActive(false);
            canvas.transform.GetChild(12).gameObject.SetActive(false);
            canvas.transform.GetChild(13).gameObject.SetActive(false);
        }
        AudioManager.Instance.StopMusic();
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
            pausepanel.transform.GetChild(0).gameObject.GetComponent<Button>().Select();
        }
        else
        {
            pausepanel.SetActive(false);
            optionsPanel.SetActive(true);
            //optionsPanel.transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<Button>().Select();
        }
    }

    public void SelectSlider(Slider slid)
    {
        slid.Select();
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
            
            Button butt = pausepanel.transform.GetChild(0).gameObject.GetComponent<Button>();
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
    

    public List<Player> OrderPlayers()
    {
        List<Player> orderedPlayers = new List<Player>();

        for (int i = 0; i < GameManager.Instance.players.Count; i++)
        {
            orderedPlayers.Add(GameManager.Instance.players[i]);
        }
        for (int i = 0; i < orderedPlayers.Count; i++)
        {
            for (int j = 0; j < orderedPlayers.Count; j++)
            {
                if (orderedPlayers[j].points < orderedPlayers[i].points)
                {
                    Player aux = orderedPlayers[j];
                    orderedPlayers[j] = orderedPlayers[i];
                    orderedPlayers[i] = aux;
                }
            }
        }
        for (int i = 0; i < orderedPlayers.Count; i++)
        {
            Debug.Log("Player " + orderedPlayers[i].playerIndex + ": " + orderedPlayers[i].points);
        }

        return orderedPlayers;
    }
}
