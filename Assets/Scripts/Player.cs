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
    private PlayerInput input;

    private void Start()
    {
        input = GetComponent<PlayerInput>();
        DisableActions(new string[] { "Cancel Character" });
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

    public void ScrollCharacters(InputAction.CallbackContext callback)
    {
        if (callback.performed)
        {
            float value = input.actions["Scroll Characters"].ReadValue<float>();
            if (value >= 0.9f)
            {
                selectCharacterManager.ChangeCharacterSelected(playerIndex, 1);
            }
            else if (value <= -0.9f)
            {
                selectCharacterManager.ChangeCharacterSelected(playerIndex, -1);
            }
        }
    }

    public void SelectCharacter(InputAction.CallbackContext callback)
    {
        if (callback.performed)
        {
            bool successful = selectCharacterManager.SelectCharacter(playerIndex); //should I get the return value?
            if (successful)
            {
                DisableActions(new string[] { "Scroll Characters" });
                EnableActions(new string[] { "Cancel Character" });
            }
        }
    }

    public void DeselectCharacter(InputAction.CallbackContext callback)
    {
        if (callback.performed)
        {
            EnableActions(new string[] { "Scroll Characters" });
            DisableActions(new string[] { "Cancel Character" });
            selectCharacterManager.DeselectCharacter(playerIndex);
        }
    }

    public void DisableActions(string[] actions)
    {
        for (int i = 0; i < actions.Length; i++)
        {
            input.actions[actions[i]].Disable();
        }
    }

    public void EnableActions(string[] actions)
    {
        for (int i = 0; i < actions.Length; i++)
        {
            input.actions[actions[i]].Enable();
        }
    }
}
