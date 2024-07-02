using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SelectCharacterManager : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> playerPanels;
    [SerializeField]
    private List<Transform> renderCameras;
    [SerializeField]
    private int[] selectedCharsIndexes;
    [SerializeField]
    private bool[] playersReady;
    [SerializeField]
    private List<List<GameObject>> charactersUI; //seran Animators en el futuro

    [SerializeField]
    private int maxCharacters;

    [SerializeField]
    private GameObject[] multiplayerManagers;
    [SerializeField]
    private GameObject panel2P4P;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivatePlayerPanel(int i)
    {
        playerPanels[i].SetActive(true);
    }

    public void DectivatePlayerPanel(int i)
    {
        playerPanels[i].SetActive(false);
    }

    public void ActivatePlayerParentPanel(int i)
    {
        playerPanels[i].transform.parent.gameObject.SetActive(true);
    }

    public void DectivatePlayerParentPanel(int i)
    {
        playerPanels[i].transform.parent.gameObject.SetActive(false);
    }

    public void OnPlayerLeft(PlayerInput playerInput)
    {
        Debug.Log("se fue");
    }

    public void ChangeCharacterSelected(int playerIndex, int rotateDirection)  //rotateDirection = -1 if rotate left, or = 1 if rotate right
    {
        if (playersReady[playerIndex]) //don't change characters if already selected
        {
            return;
        }
        selectedCharsIndexes[playerIndex] += (maxCharacters + rotateDirection);
        selectedCharsIndexes[playerIndex] %= maxCharacters;
        renderCameras[playerIndex].eulerAngles += Vector3.up * 90 * rotateDirection;
    }

    public bool SelectCharacter(int playerIndex)
    {
        int selected = selectedCharsIndexes[playerIndex];

        for (int i = 0; i < selectedCharsIndexes.Length; i++)
        {
            if (playersReady[i] && selectedCharsIndexes[i] == selected) //another player has already selected this character
            {
                //play error sound
                Debug.Log("Personaje ya elegido");
                return false;
            }
        }

        //trigger character animation

        //charactersUI[playerIndex][selectedCharsIndexes[playerIndex]].SetTrigger("Animate");
        playersReady[playerIndex] = true;
        return true;
    }

    public void DeselectCharacter(int playerIndex)
    {
        playersReady[playerIndex] = false;
    }

    public void Back()
    {
        for (int i = 0; i < multiplayerManagers.Length; i++)
        {
            multiplayerManagers[i].SetActive(false);
            //multiplayerManagers[i].GetComponent<CustomMultiplayerManager>().EnableJoining();
        }
        int count = GameManager.Instance.players.Count;
        for (int i = 0; i < count; i++)
        {
            Destroy(GameManager.Instance.players[0].gameObject);
            GameManager.Instance.players.RemoveAt(0);
        }
        for (int i = 0;i < playerPanels.Count; i++)
        {
            playerPanels[i].SetActive(false);
        }
        selectedCharsIndexes = new int[4];
        playersReady = new bool[4];
        for (int i = 0; i < renderCameras.Count; i++)
        {
            renderCameras[i].eulerAngles = Vector3.zero;
        }
        GameManager.Instance.players = new List<Player>();

        multiplayerManagers[0].transform.parent.gameObject.SetActive(false);
        panel2P4P.SetActive(true);
    }
}
