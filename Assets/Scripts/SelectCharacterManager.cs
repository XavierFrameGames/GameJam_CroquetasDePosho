using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCharacterManager : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> playerPanels;
    [SerializeField]
    private List<Transform> renderCameras;
    [SerializeField]
    private List<int> selectedCharsIndexes;
    [SerializeField]
    private List<bool> playersReady;
    [SerializeField]
    private List<List<GameObject>> charactersUI; //seran Animators en el futuro

    [SerializeField]
    private int maxCharacters;
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

        for (int i = 0; i < selectedCharsIndexes.Count; i++)
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
}
