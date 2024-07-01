using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//[Serializable]
public class Player : MonoBehaviour
{
    public int playerIndex;
    //public InputDevice device;
    private SelectCharacterManager selectCharacterManager;

    private void Start()
    {
        selectCharacterManager = GameObject.Find("SelectCharacterManager").GetComponent<SelectCharacterManager>();
        if (GameManager.Instance.players.Contains(this))
        {
            //ya estamos
            //Debug.Log("ya estoy");
        }
        else
        {
            GameManager.Instance.players.Add(this);
            playerIndex = GameManager.Instance.players.Count - 1;
            //device = playerInput.devices[0];
            //Debug.Log("My ID is: " + device.deviceId);
            //Debug.Log("no estoy. Ahora si");
            selectCharacterManager.ActivatePlayerPanel(playerIndex);
        }
    }

    private void Update()
    {
        
    }
}
