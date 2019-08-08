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
    public Text scoreText;
    public int launchDist;
    public GameObject cameraScript, mainMenuPanel, rocketShip, winBoxUI;

    public winTrigger winTrigger;
    private Vector2 startPos = new Vector2(0.0f, 1.35f);

    public void Update()
    {
        // --- Getting GameObjects ---
        rocketShip = GameObject.FindGameObjectWithTag("Player");
        cameraScript = GameObject.FindGameObjectWithTag("MainCamera");

        // --- Getting UI Gameobjects ---
        mainMenuPanel = GameObject.FindGameObjectWithTag("LevelMenu");
        winBoxUI = GameObject.FindGameObjectWithTag("WinBoxUI");

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
    public void winBoxOpen()
    {
        Debug.Log("It's all connected");

        winBoxUI.SetActive(true);
    }

    public void winBoxClose()
    {
        winBoxUI.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    public void HideMenu()
    {
        mainMenuPanel.SetActive(false);
    }
}
