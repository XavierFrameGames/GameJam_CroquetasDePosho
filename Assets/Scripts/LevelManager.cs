using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;

public class LevelManager : MonoBehaviour
{

    [SerializeField] private PlayableAsset timeline;
    [SerializeField] private GameObject countdown; 


    
    void Start()
    {
        StartCoroutine(CountdownCoroutine());
    }

    
    void Update()
    {
        
    }

    IEnumerator CountdownCoroutine()
    {
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
       
    }
}
