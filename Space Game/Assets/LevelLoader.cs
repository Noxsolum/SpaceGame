using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelLoader : PlanetScript
{
    public GameObject[] menuUI;

    public void Update()
    {
        menuUI = GameObject.FindGameObjectsWithTag("LevelMenu");

        
    }


    public void loadLevel(int x)
    {
        switch (x)
        {
            case 0:
                // Spawn Level 1
                PlanetScript planet = new PlanetScript(0, new Vector3(0.0f, 5.0f, 0.0f));

                
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
