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
    private Vector2 startPos = new Vector2(0.0f, 1.35f);

    // --- UI ---
    public Text scoreText;
    public winTrigger winTrigger;
    public GameObject cameraScript, mainMenuPanel, winBoxUI, LoadingScreenUI;

    // --- GameObjects ---
    public GameObject rocketShip;

    public void Start()
    {
        // --- Getting GameObjects ---
        rocketShip = GameObject.FindGameObjectWithTag("Player");
        cameraScript = GameObject.FindGameObjectWithTag("MainCamera");
        LoadingScreenUI = GameObject.FindGameObjectWithTag("LoadingScreen");

        // --- Getting UI Gameobjects ---
        mainMenuPanel = GameObject.FindGameObjectWithTag("LevelMenu");
        winBoxUI = GameObject.FindGameObjectWithTag("WinBoxUI");


        winBoxUI.SetActive(false);
        LoadingScreenUI.SetActive(false);
    }

    public void Update()
    {
        // --- UI Methods ---
        Text();
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

    // --- The Win State ---
    public void openWinBox()
    {
        Debug.Log("It's all connected");

        winBoxUI.SetActive(true);
    }

    public void hideWinBoxClose()
    {
        LoadingScreenUI.SetActive(true);

        winBoxUI.SetActive(false);
        mainMenuPanel.SetActive(true);
        ResetLevel();

        LoadingScreenUI.SetActive(false);
    }

    public void hideMenu()
    {
        mainMenuPanel.SetActive(false);
    }

    public void openLoadingScreen()
    {
        LoadingScreenUI.SetActive(true);
    }

    public void hideLoadingScreen()
    {
        LoadingScreenUI.SetActive(false);
    }
}
