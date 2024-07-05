using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public List<Player> players;
    public OptionsData options;

    public bool tutorialDone;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        LoadOptions();
    }

    public void SaveOptions()
    {
        string optionsData = JsonUtility.ToJson(options);
        PlayerPrefs.SetString("Options", optionsData);
    }

    public void LoadOptions()
    {
        if (PlayerPrefs.HasKey("Options"))
        {
            string optionsData = PlayerPrefs.GetString("Options");
            options = JsonUtility.FromJson<OptionsData>(optionsData);
        }
        else
        {
            //default values
            options = new OptionsData();
        }
    }
}
