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
    public PlanetScript planet;

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
                planet = new PlanetScript(0, new Vector3(0.0f, 5.0f, 0.0f));
                Debug.Log("Clicked");
                menuUI.hideMenu();
                
                break;
            case 1:
                // Spawn Level 2
                planet = new PlanetScript(0, new Vector3(0.0f, 25.0f, 0.0f));
                Debug.Log("Clicked");
                menuUI.hideMenu();
                break;
            case 2:

                menuUI.hideMenu();
                break;
            case 3:

                menuUI.hideMenu();
                break;
            case 4:

                menuUI.hideMenu();
                break;
            case 5:

                menuUI.hideMenu();
                break;
            case 6:

                menuUI.hideMenu();
                break;
            case 7:

                menuUI.hideMenu();
                break;
            case 8:

                menuUI.hideMenu();
                break;
            case 9:

                menuUI.hideMenu();
                break;
            case 10:

                menuUI.hideMenu();
                break;
            case 11:

                menuUI.hideMenu();
                break;
            case 12:

                menuUI.hideMenu();
                break;
            case 13:

                menuUI.hideMenu();
                break;
            case 14:

                menuUI.hideMenu();
                break;
            default:
                break;
        }
    }
}
