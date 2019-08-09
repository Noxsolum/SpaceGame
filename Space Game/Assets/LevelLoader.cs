/*
Notes:
 - 

*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelLoader : PlanetScript
{
    private GameObject _canvas;
    public MenuAndUIScript menuUI;

    public void Update()
    {
        _canvas = GameObject.FindGameObjectWithTag("CanvasUI");
        menuUI = _canvas.GetComponent<MenuAndUIScript>();
    }


    public void loadLevel(int x)
    {
        switch (x)
        {
            case 0:
                // Spawn Level 1
                //PlanetScript planet = new PlanetScript(0, new Vector3(0.0f, 5.0f, 0.0f));
                Debug.Log("Clicked");
                menuUI.hideMenu();
                
                break;
            case 1:
                // Spawn Level 2
                break;
            case 2:
                // Spawn Level 2
                break;
            default:
                break;
        }
    }
}
