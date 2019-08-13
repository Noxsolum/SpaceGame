/*
Notes:
 - Win State has now been made although hasn't be tested yet
 - Need to start making the levels
 - Need to find a way to re-activate UI!!!!!!!!!!!!!!!!!!!!!!!!
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuAndUIScript : MonoBehaviour
{
    // --- Variables ---
    public int launchDist;
    private int levelUnlock = 0;
    private Vector2 startPos = new Vector2(0.0f, 1.35f);

    // --- UI ---
    public Text scoreText;
    public winTrigger winTrigger;
    public GameObject[] levelButtons;
    public GameObject cameraScript, mainMenuPanel, winBoxUI, LoadingScreenUI, ResetButton;

    // --- GameObjects ---
    public GameObject rocketShip;
    public GameObject[] _planetS, _planetM, _planetL;

    public void Start()
    {
        // --- Getting GameObjects ---
        rocketShip = GameObject.FindGameObjectWithTag("Player");
        cameraScript = GameObject.FindGameObjectWithTag("MainCamera");
        LoadingScreenUI = GameObject.FindGameObjectWithTag("LoadingScreen");

        // --- Getting UI Gameobjects ---
        mainMenuPanel = GameObject.FindGameObjectWithTag("LevelMenu");
        winBoxUI = GameObject.FindGameObjectWithTag("WinBoxUI");
        levelButtons = GameObject.FindGameObjectsWithTag("LevelButtons");
        ResetButton = GameObject.FindGameObjectWithTag("ResetButton");

        for(int x = 1; x < levelButtons.Length; x++)
        {
            levelButtons[x].SetActive(false);
        }

        ResetButton.SetActive(false);
        winBoxUI.SetActive(false);
        LoadingScreenUI.SetActive(false);
    }

    public void Update()
    {
        // --- UI Methods ---
        Text();

        // --- Planets ---
        _planetS = GameObject.FindGameObjectsWithTag("PlanetS");
        _planetM = GameObject.FindGameObjectsWithTag("PlanetM");
        _planetL = GameObject.FindGameObjectsWithTag("PlanetL");
    }

    // --- UI For the Power ---
    public void Text()
    {
        CameraScript cameraScript_x = cameraScript.GetComponent<CameraScript>();
        scoreText.text = "Power: " + cameraScript_x.launchDist_y;
    }

    // --- Button to Reset Level ---
    public void ResetLevel()
    {
        GameObject rocketShip = GameObject.FindGameObjectWithTag("Player");
        rocketShip.transform.position = startPos;
        rocketShip.GetComponent<Rigidbody>().velocity = Vector3.zero;
        rocketShip.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

        Debug.Log("RESET");
    }

    private void clearLevel()
    {
        if (_planetS.Length != 0)
        {
            for (int i = 0; i < _planetS.Length; i++)
            {
                Destroy(_planetS[i]);
            }
        }

        if (_planetM.Length != 0)
        {
            for (int i = 0; i < _planetM.Length; i++)
            {
                Destroy(_planetM[i]);
            }
        }

        if (_planetL.Length != 0)
        {
            for (int i = 0; i < _planetL.Length; i++)
            {
                Destroy(_planetL[i]);
            }
        }
    }

    // --- The Win State ---
    public void openWinBox()
    {
        Debug.Log("It's all connected");
        levelUnlock++;
        winBoxUI.SetActive(true);
    }


    public void hideWinBox()
    {
        LoadingScreenUI.SetActive(true);

        clearLevel();
        winBoxUI.SetActive(false);
        mainMenuPanel.SetActive(true);
        levelButtons[levelUnlock].SetActive(true);
        ResetButton.SetActive(false);
        ResetLevel();

        LoadingScreenUI.SetActive(false);
    }

    // --- Hides the Level Select ---
    public void hideMenu()
    {
        LoadingScreenUI.SetActive(true);

        mainMenuPanel.SetActive(false);
        ResetButton.SetActive(true);

        LoadingScreenUI.SetActive(false);
    }

    // --- Brings Up The Loading Screen ---
    public void openLoadingScreen()
    {
        LoadingScreenUI.SetActive(true);
    }

    // --- Hides the Loading Screen ---
    public void hideLoadingScreen()
    {
        LoadingScreenUI.SetActive(false);
    }
}
