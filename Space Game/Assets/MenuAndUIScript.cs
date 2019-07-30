using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuAndUIScript : MonoBehaviour
{
    public Text scoreText;
    public int launchDist;
    public GameObject cameraScript;
    public GameObject rocketShip;
    public GameObject winBox;
    public winTrigger winTrigger;

    private Vector2 startPos = new Vector2(0.0f, 1.35f);

    public void Update()
    {
        rocketShip = GameObject.FindGameObjectWithTag("Player");
        cameraScript = GameObject.FindGameObjectWithTag("MainCamera");
        winBox = GameObject.FindGameObjectWithTag("WinBox");
        winTrigger = winBox.GetComponent<winTrigger>();
        Text();
        winState();
    }

    public void Text()
    {
        CameraScript cameraScript_x = cameraScript.GetComponent<CameraScript>();
        scoreText.text = "Power: " + cameraScript_x.launchDist_y;
    }

    public void ResetLevel()
    {
        GameObject rocketShip = GameObject.FindGameObjectWithTag("Player");
        rocketShip.transform.position = startPos;
        rocketShip.GetComponent<Rigidbody>().velocity = Vector3.zero;
        rocketShip.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

        Debug.Log("RESET");
    }

    public void winState()
    {
        if (winTrigger._win == true)
        {
            Debug.Log("It's all connected");

            // Do some hocus pocus to bring up a button prompt to return to menu

            winTrigger._win = false;
        }
    }

    public void HomeMenuWin()
    {

    }
}
