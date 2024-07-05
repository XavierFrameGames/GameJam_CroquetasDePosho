using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.UI;

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

    [SerializeField] private InputDetector inputDetector;

    public PlayableDirector timelineDirector;



        private void Start()
        {
        
        timelineDirector = gameObject.transform.GetChild(0).transform.GetComponentInChildren<PlayableDirector>();
        input = GetComponent<PlayerInput>();
        inputDetector = gameObject.transform.GetChild(0).transform.GetChild(2).GetComponentInChildren<InputDetector>();
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



    public void SongInputUp(InputAction.CallbackContext callback)
    {
        //float value = input.actions["Button Up"].ReadValue<float>();
        //Poner Inputs de los botones y comprobar que comida esta más cerca (inputdetector)
        //Detectar que tipo de comida es para ver si le das al boton correcto; /1-Up 2-Down 3-Left 4-Right 5-L 6-R
        float distance = 1000000000000;
        int posInList = 0;
        for(int i = 0;i<inputDetector.foodTrans.Count;i++)
        {
            float tempdistance = inputDetector.gameObject.transform.position.x - inputDetector.foodTrans[i].transform.position.x;
            
            if(tempdistance < distance)
            {
                distance = tempdistance;
                posInList = i;
            }

            

        }
        if(distance < 0.1 && inputDetector.foodTrans[posInList].GetComponent<FoodBehaviour>().correctInput == 1)//mirar si es lo optimo
        {
            // inputDetector.foodTrans[posInList]. añadir metodo del objeto para su destruccion en el propio script del objeto;  
            //Añadir puntuación

        }





    }
    
    public void SongInputDown(InputAction.CallbackContext callback)
    {
        //float value = input.actions["Button Up"].ReadValue<float>();
        //Poner Inputs de los botones y comprobar que comida esta más cerca (inputdetector)
        //Detectar que tipo de comida es para ver si le das al boton correcto; /1-Up 2-Down 3-Left 4-Right 5-L 6-R






    }
    public void SongInputLeft(InputAction.CallbackContext callback)
    {
        //float value = input.actions["Button Up"].ReadValue<float>();
        //Poner Inputs de los botones y comprobar que comida esta más cerca (inputdetector)
        //Detectar que tipo de comida es para ver si le das al boton correcto; /1-Up 2-Down 3-Left 4-Right 5-L 6-R






    }
    public void SongInputright(InputAction.CallbackContext callback)
    {
        //float value = input.actions["Button Up"].ReadValue<float>();
        //Poner Inputs de los botones y comprobar que comida esta más cerca (inputdetector)
        //Detectar que tipo de comida es para ver si le das al boton correcto; /1-Up 2-Down 3-Left 4-Right 5-L 6-R






    }
    public void SongInputL(InputAction.CallbackContext callback)
    {
        //float value = input.actions["Button Up"].ReadValue<float>();
        //Poner Inputs de los botones y comprobar que comida esta más cerca (inputdetector)
        //Detectar que tipo de comida es para ver si le das al boton correcto; /1-Up 2-Down 3-Left 4-Right 5-L 6-R






    }
    public void SongInputR(InputAction.CallbackContext callback)
    {
        //float value = input.actions["Button Up"].ReadValue<float>();
        //Poner Inputs de los botones y comprobar que comida esta más cerca (inputdetector)
        //Detectar que tipo de comida es para ver si le das al boton correcto; /1-Up 2-Down 3-Left 4-Right 5-L 6-R






    }
}
