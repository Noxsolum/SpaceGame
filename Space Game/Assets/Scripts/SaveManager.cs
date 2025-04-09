using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance { get; set; }
    public SaveScript state;

    public int finalScore;

    private void Awake()
    {
        GameObject[] settingsObj = GameObject.FindGameObjectsWithTag("PersistSettings");
        if (settingsObj.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        Instance = this;
        Load();

        Debug.Log(HelperScript.Serialize<SaveScript>(state));
    }

    // Save the whole state of this saveState script to the player pref
    public void Save()
    {
        saveScore();
        saveVolume();
        PlayerPrefs.SetString("save", HelperScript.Serialize<SaveScript>(state));
        Debug.Log("Game Saved!");
    }

    // Load the previous save state from the player prefs
    public void Load()
    {
        if(PlayerPrefs.HasKey("save"))
        {
            state = HelperScript.Deserialize<SaveScript>(PlayerPrefs.GetString("save"));

            finalScore = state.score;
        }
        else
        {
            state = new SaveScript();
            Save();
            Debug.Log("No Save File Found, Creating a New One!");
        }
    }

    public void saveScore()
    {
        if (finalScore > state.score)
        {
            state.score = finalScore;
        }
    }

    public void saveVolume()
    {

    }
}
