using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class CustomMultiplayerManager : PlayerInputManager
{
    [SerializeField]
    private List<GameObject> playerPrefabs;
    [SerializeField]
    private int playerPrefabsIndex;
    [SerializeField]
    private GameObject[] playerPanels;
    [SerializeField]
    private Transform[] characterLocations;
    // Start is called before the first frame update
    void Start()
    {
        //JoinPlayer(default, default, default, InputSystem.devices[0]);
    }
    
    public void BeginStart()
    {
        Debug.Log("start: " + maxPlayerCount);
        for (int i = 0; i < InputSystem.devices.Count; i++)
        {
            //Debug.Log(InputSystem.devices[i].deviceId);
        }

        for (int i = 0; i < playerPanels.Length; i++)
        {
            if (i < maxPlayerCount)
            {
                playerPanels[i].SetActive(true);
            }
            else
            {
                playerPanels[i].SetActive(false);
            }
        }
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPlayerJoined(PlayerInput playerInput)
    {
        Debug.Log("joined player");
        /*
        playerPrefab = playerPrefabs[playerPrefabsIndex];
        playerPrefabsIndex++;
        playerPrefabsIndex %= playerPrefabs.Count;
        */
    }

    public void Meow(PlayerInput playerInput)
    {
        Debug.Log("Meow");
        playerInput.SwitchCurrentActionMap("Game");
        int character = playerInput.GetComponent<Player>().playerSkin;

        playerInput.transform.position = characterLocations[character].position;
        playerInput.transform.rotation = characterLocations[character].rotation;

        if(SceneManager.GetActiveScene().buildIndex>=2)
        {
            playerInput.GetComponent<Player>().levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        }
    }
}

