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

    //[SerializeField]
    public int playerSkin = -1;

    private bool scrolled;
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
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        
    }

    public void OnDeviceLost(PlayerInput playerInput)
    {
        selectCharacterManager.DectivatePlayerPanel(playerIndex);
    }

    public void OnDeviceReconnected(PlayerInput playerInput)
    {
        selectCharacterManager.ActivatePlayerPanel(playerIndex);
    }

    public void ScrollCharacters(InputAction.CallbackContext callback)
    {
        if (callback.performed)
        {
            
            float value = input.actions["Scroll Characters"].ReadValue<float>();
            if (!scrolled && value >= 0.9f)
            {
                selectCharacterManager.ChangeCharacterSelected(playerIndex, 1);
                scrolled = true;
            }
            else if (!scrolled && value <= -0.9f)
            {
                selectCharacterManager.ChangeCharacterSelected(playerIndex, -1);
                scrolled = true;
            }
            else if (scrolled && value >= -0.8f && value <= 0.8f)
            {
                scrolled = false;
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
                playerSkin = selectCharacterManager.selectedCharsIndexes[playerIndex];
                DisableActions(new string[] { "Scroll Characters", "Select Character" });
                EnableActions(new string[] { "Cancel Character" });
            }
        }
    }

    public void DeselectCharacter(InputAction.CallbackContext callback)
    {
        if (callback.performed)
        {
            playerSkin = -1;
            EnableActions(new string[] { "Scroll Characters", "Select Character" });
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

    public void UICancelHeld(InputAction.CallbackContext callback)
    {
        if (callback.performed)
        {
            bool killME = selectCharacterManager.Back();
            if (killME)
            {
                Destroy(gameObject);
            }
            else
            {
                /*
                playerSkin = -1;
                EnableActions(new string[] { "Scroll Characters", "Select Character" });
                DisableActions(new string[] { "Cancel Character" });
                */
            }
        }
    }

    public void ResetCharacterSelection()
    {
        playerSkin = -1;
        EnableActions(new string[] { "Scroll Characters", "Select Character" });
        DisableActions(new string[] { "Cancel Character" });
    }

    public void StartButton(InputAction.CallbackContext callback)
    {
        if (callback.performed)
        {
            bool result = selectCharacterManager.PlayerPressedStart(playerIndex);
            if (result)
            {
                DisableActions(new string[] { "Scroll Characters", "Select Character", "Cancel Character" });
            }
        }
    }
}
