using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class CustomMultiplayerManager : PlayerInputManager
{
    [SerializeField]
    private List<GameObject> playerPrefabs;
    [SerializeField]
    private int playerPrefabsIndex;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < InputSystem.devices.Count; i++)
        {
            //Debug.Log(InputSystem.devices[i].deviceId);
        }

        JoinPlayer(default, default, default, InputSystem.devices[0]);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnPlayerJoined(PlayerInput playerInput)
    {
        Debug.Log("joined player");
        playerPrefab = playerPrefabs[playerPrefabsIndex];
        playerPrefabsIndex++;
        playerPrefabsIndex %= playerPrefabs.Count;
    }
}

