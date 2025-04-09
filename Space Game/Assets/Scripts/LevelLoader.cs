/*
Notes:
 - 

*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelLoader : PlanetScript
{
    // --- UI Elements ---

    public GameObject _canvas;
    public MenuAndUIScript _menuUI;

    // --- Objects ---
    public GameObject wintrigger, exitSign, arrow, rocketShip;
    private GameObject [] planet;
    private LineRenderer _line;
    
    public Vector3 playerPos, startPos, currentPos, previousPos, planetSpawnPoint;
    private Vector3 location = new Vector3(0.0f, 0.0f, 0.0f);
    int amountPlanets;
    private int frames;

    private void Start()
    {
        rocketShip = GameObject.FindGameObjectWithTag("Player");
        wintrigger = GameObject.FindGameObjectWithTag("WinBox");
        exitSign = GameObject.FindGameObjectWithTag("ExitSign");

        arrow = GameObject.FindGameObjectWithTag("Arrow");
        //_line = arrow.GetComponent<LineRendererXD>();
        _canvas = GameObject.FindGameObjectWithTag("CanvasUI");
        _menuUI = _canvas.GetComponent<MenuAndUIScript>();


        startPos = rocketShip.transform.position;
        previousPos = new Vector3(0.0f, 0.0f, 0.0f);

        frames = 0;
    }

    public void Update()
    {
        // Try and make the planets consistantly spawn based on the player position
        // if statement for if their are less than the correct amount on the map
        // continue spawning the planets while the player is moving and change the spawn point 

        frames++;

        planet = GameObject.FindGameObjectsWithTag("Orbs");
        amountPlanets = planet.Length;
        currentPos = rocketShip.transform.position;

        if (frames % 5 == 0)
        {
            if (_menuUI.currentScene.buildIndex == 2 || _menuUI.currentScene.buildIndex == 3)
            {
                if (currentPos != previousPos && _menuUI.cameraScript.isLaunched)
                {
                    Vector2 tempPos;
                    planetSpawnPoint = findCentrePoint(currentPos, previousPos);

                    tempPos = (new Vector2(planetSpawnPoint.x, planetSpawnPoint.y) + Random.insideUnitCircle * 200);
                    if (checkSpawn(tempPos) == true)
                    {
                        SpawnPlanet(Random.Range(0, 3), tempPos, new Quaternion(0, 0, 0, 0));
                    }

                }
                destroyPlanets(false);
                previousPos = currentPos;
            }
        }
    }

    #region Find Spawn Point

    /// <summary>
    /// 
    /// </summary>
    /// <param name="cPos"></param>
    /// <param name="pPos"></param>
    /// <returns></returns>

    public Vector2 findCentrePoint(Vector3 cPos, Vector3 pPos)
    {
        Vector2 shipDirection = cPos - pPos;

        Vector2 finaldirection = shipDirection + (shipDirection.normalized * 350);

        Vector2 targetPos = new Vector2(pPos.x, pPos.y) + finaldirection;

        return targetPos;
    }

    #endregion

    #region Destroy Planets

    public void destroyPlanets(bool b)
    {
        playerPos = rocketShip.transform.position;
        planet = GameObject.FindGameObjectsWithTag("Orbs");

        if (b == true)
        {
            foreach(GameObject _planet in planet)
            {
                Destroy(_planet);
            }
        }
        else if(b == false)
        {
            foreach (GameObject _planet in planet)
            {
                if ((playerPos.y - 250) > _planet.transform.position.y)
                {
                    Destroy(_planet);
                }
            }
        }
    }

    #endregion

    #region Spawn Planets

    /// <summary>
    /// Method that takes a planet type, spawn location and it's rotation
    /// </summary>
    /// <param name="_size"></param>
    /// <param name="_location"></param>
    /// <param name="rotation"></param>

    public void SpawnPlanet(int _size, Vector3 _location, Quaternion rotation)
    {
        switch (_size)
        {
            case 0:
                Instantiate(Resources.Load("Planet S"), _location, rotation);
                break;
            case 1:
                Instantiate(Resources.Load("Planet M"), _location, rotation);
                break;
            case 2:
                Instantiate(Resources.Load("Planet L"), _location, rotation);
                break;
            case 5:
                Instantiate(Resources.Load("DebugSpriteS"), _location, rotation);
                break;
            case 6:
                Instantiate(Resources.Load("DebugSpriteM"), _location, rotation);
                break;
            case 7:
                Instantiate(Resources.Load("DebugSpriteL"), _location, rotation);
                break;
            default:
                Instantiate(Resources.Load("PlanetS"), _location, rotation);
                break;
        }
    }

    #endregion

    #region Check Spawn

    /// <summary>
    /// Checks the spawn location for planets so it can check if it's spawning correctly
    /// </summary>
    /// <returns></returns>
    /// 

    public bool checkSpawn(Vector3 planetLoc)
    {
        planet = GameObject.FindGameObjectsWithTag("Orbs");

        foreach (GameObject _planet in planet)
        {
            int magni = Mathf.RoundToInt(Vector3.Distance(planetLoc, _planet.transform.position));
            
            if (magni <= 7 && magni >= 0)
            {
                return false;
            }
        }
        
        return true;
    }

    #endregion

    #region Load Endless

    public void LoadLevel(float spawnLoc, bool fPlanet)
    {
        int aOP = 175;


        if (fPlanet == true)
            SpawnPlanet(0, new Vector3(0, 13, 0), new Quaternion(0, 0, 0, 0));


        for (int x = 0; x < aOP; x++)
        {
            Vector3 vector3;
            do
            {
                vector3 = (new Vector2(0, spawnLoc) + Random.insideUnitCircle * 200);
            } while (checkSpawn(vector3) == false);

            SpawnPlanet(Random.Range(0, 3), vector3, new Quaternion(0, 0, 0, 0));
        }
    }

    #endregion

    #region Load Story

    /// <summary>
    /// 
    /// </summary>
    /// <param name="x"></param>

    public void loadLevel(int x)
    {

        switch (x)
        {
            case 0:     // Spawn Level 1

                //int amountPlanets = 200;

                //SpawnPlanet(0, new Vector3(0, 13, 0), new Quaternion(0, 0, 0, 0));

                //for (x = 0; x < amountPlanets; x++)
                //{
                //    Vector3 vector3;

                //    do
                //    {
                //        vector3 = (new Vector2(0, 213) + Random.insideUnitCircle * 200);
                //    } while (checkSpawn(vector3) == false);

                //    SpawnPlanet(Random.Range(0, 3), vector3, new Quaternion(0, 0, 0, 0));
                //}


                //#region Old Level 1
                //Debug.Log("Level 1");

                //// --- Planets ---
                //planet[0] = new PlanetScript(0, new Vector3(0.0f, 5.0f, 0.0f));
                //planet[1] = new PlanetScript(1, new Vector3(0.0f, 7.0f, 0.0f));
                //planet[2] = new PlanetScript(1, new Vector3(0.0f, 7.0f, 0.0f));

                //// --- Win Point ---
                //wintrigger.transform.position = new Vector3(0.0f, 14.7f, 0.0f);
                //exitSign.transform.position = new Vector3(0.0f, 14.6f, 0.0f);

                //// --- Line Renderer ---
                //_line.enableLine = true;

                //_menuUI.hideMenu();

                break;
                #region Temp
                /*   case 1:     // Spawn Level 2
                       Debug.Log("Level 2");

                       // --- Planets ---
                       planet[0] = new PlanetScript(1, new Vector3(0.0f, 15.0f, 0.0f));
                       planet[1] = new PlanetScript(6, new Vector3(0.0f, 15.0f, 0.0f));

                       // --- Win Point ---
                       wintrigger.transform.position = new Vector3(0.0f, 33.875f, 0.0f);
                       exitSign.transform.position = new Vector3(0.0f, 31.875f, 0.0f);

                       _menuUI.hideMenu();
                       break;
                   case 2:     // Spawn Level 3
                       Debug.Log("Level 3");

                       // --- Planets ---
                       planet[0] = new PlanetScript(2, new Vector3(0.0f, 15.0f, 0.0f));
                       planet[1] = new PlanetScript(7, new Vector3(0.0f, 15.0f, 0.0f));

                       // --- Win Point ---
                       wintrigger.transform.position = new Vector3(0.0f, 33.875f, 0.0f);
                       exitSign.transform.position = new Vector3(0.0f, 31.875f, 0.0f);

                       _menuUI.hideMenu();
                       break;
                   case 3:     // Spawn Level 4
                       Debug.Log("Level 4");

                       // --- Planets ---
                       planet[0] = new PlanetScript(0, new Vector3(0.0f, 15.0f, 0.0f));
                       planet[1] = new PlanetScript(0, new Vector3(10.0f, 25.0f, 0.0f));
                       planet[2] = new PlanetScript(0, new Vector3(-10.0f, 5.0f, 0.0f));

                       // --- Win Point ---
                       wintrigger.transform.position = new Vector3(0.0f, 33.875f, 0.0f);
                       exitSign.transform.position = new Vector3(0.0f, 31.875f, 0.0f);

                       _menuUI.hideMenu();
                       break;
                   case 4:     // Spawn Level 5
                       Debug.Log("Level 5");

                       // --- Planets ---
                       planet[0] = new PlanetScript(0, new Vector3(-10.0f, 10.0f, 0.0f));
                       planet[1] = new PlanetScript(0, new Vector3(10.0f, 10.0f, 0.0f));
                       planet[2] = new PlanetScript(0, new Vector3(-20.0f, 25.0f, 0.0f));
                       planet[3] = new PlanetScript(0, new Vector3(20.0f, 25.0f, 0.0f));

                       // --- Win Point ---
                       wintrigger.transform.position = new Vector3(0.0f, 33.875f, 0.0f);
                       exitSign.transform.position = new Vector3(0.0f, 31.875f, 0.0f);

                       _menuUI.hideMenu();
                       break;
                   case 5:     // Spawn Level 6
                       Debug.Log("Level 6");

                       // --- Planets ---
                       planet[0] = new PlanetScript(0, new Vector3(0.0f, 15.0f, 0.0f));
                       planet[1] = new PlanetScript(0, new Vector3(10.0f, 25.0f, 0.0f));
                       planet[2] = new PlanetScript(0, new Vector3(-10.0f, 5.0f, 0.0f));

                       // --- Win Point ---
                       wintrigger.transform.position = new Vector3(0.0f, 33.875f, 0.0f);
                       exitSign.transform.position = new Vector3(0.0f, 31.875f, 0.0f);

                       _menuUI.hideMenu();
                       break;
                   case 6:     // Spawn Level 7
                       Debug.Log("Level 7");

                       // --- Planets ---
                       planet[0] = new PlanetScript(0, new Vector3(0.0f, 15.0f, 0.0f));
                       planet[1] = new PlanetScript(0, new Vector3(10.0f, 25.0f, 0.0f));
                       planet[2] = new PlanetScript(0, new Vector3(-10.0f, 5.0f, 0.0f));

                       // --- Win Point ---
                       wintrigger.transform.position = new Vector3(0.0f, 33.875f, 0.0f);
                       exitSign.transform.position = new Vector3(0.0f, 31.875f, 0.0f);

                       _menuUI.hideMenu();
                       break;
                   case 7:     // Spawn Level 8
                       Debug.Log("Level 8");

                       // --- Planets ---
                       planet[0] = new PlanetScript(0, new Vector3(0.0f, 15.0f, 0.0f));
                       planet[1] = new PlanetScript(0, new Vector3(10.0f, 25.0f, 0.0f));
                       planet[2] = new PlanetScript(0, new Vector3(-10.0f, 5.0f, 0.0f));

                       // --- Win Point ---
                       wintrigger.transform.position = new Vector3(0.0f, 33.875f, 0.0f);
                       exitSign.transform.position = new Vector3(0.0f, 31.875f, 0.0f);

                       _menuUI.hideMenu();
                       break;
                   case 8:     // Spawn Level 9
                       Debug.Log("Level 9");

                       // --- Planets ---
                       planet[0] = new PlanetScript(0, new Vector3(0.0f, 15.0f, 0.0f));
                       planet[1] = new PlanetScript(0, new Vector3(10.0f, 25.0f, 0.0f));
                       planet[2] = new PlanetScript(0, new Vector3(-10.0f, 5.0f, 0.0f));

                       // --- Win Point ---
                       wintrigger.transform.position = new Vector3(0.0f, 33.875f, 0.0f);
                       exitSign.transform.position = new Vector3(0.0f, 31.875f, 0.0f);

                       _menuUI.hideMenu();
                       break;
                   case 9:     // Spawn Level 10
                       Debug.Log("Level 10");

                       // --- Planets ---
                       planet[0] = new PlanetScript(0, new Vector3(0.0f, 15.0f, 0.0f));
                       planet[1] = new PlanetScript(0, new Vector3(10.0f, 25.0f, 0.0f));
                       planet[2] = new PlanetScript(0, new Vector3(-10.0f, 5.0f, 0.0f));

                       // --- Win Point ---
                       wintrigger.transform.position = new Vector3(0.0f, 33.875f, 0.0f);
                       exitSign.transform.position = new Vector3(0.0f, 31.875f, 0.0f);

                       _menuUI.hideMenu();
                       break;
                   case 10:     // Spawn Level 11
                       Debug.Log("Level 11");

                       // --- Planets ---
                       planet[0] = new PlanetScript(0, new Vector3(0.0f, 15.0f, 0.0f));
                       planet[1] = new PlanetScript(0, new Vector3(10.0f, 25.0f, 0.0f));
                       planet[2] = new PlanetScript(0, new Vector3(-10.0f, 5.0f, 0.0f));

                       // --- Win Point ---
                       wintrigger.transform.position = new Vector3(0.0f, 33.875f, 0.0f);
                       exitSign.transform.position = new Vector3(0.0f, 31.875f, 0.0f);

                       _menuUI.hideMenu();
                       break;
                   case 11:     // Spawn Level 12
                       Debug.Log("Level 12");

                       // --- Planets ---
                       planet[0] = new PlanetScript(0, new Vector3(0.0f, 15.0f, 0.0f));
                       planet[1] = new PlanetScript(0, new Vector3(10.0f, 25.0f, 0.0f));
                       planet[2] = new PlanetScript(0, new Vector3(-10.0f, 5.0f, 0.0f));

                       // --- Win Point ---
                       wintrigger.transform.position = new Vector3(0.0f, 33.875f, 0.0f);
                       exitSign.transform.position = new Vector3(0.0f, 31.875f, 0.0f);

                       _menuUI.hideMenu();
                       break;
                   case 12:     // Spawn Level 13
                       Debug.Log("Level 1");

                       // --- Planets ---
                       planet[0] = new PlanetScript(0, new Vector3(0.0f, 5.0f, 0.0f));
                       planet[1] = new PlanetScript(1, new Vector3(0.0f, 7.0f, 0.0f));
                       planet[2] = new PlanetScript(1, new Vector3(0.0f, 7.0f, 0.0f));

                       // --- Win Point ---
                       wintrigger.transform.position = new Vector3(0.0f, 14.7f, 0.0f);
                       exitSign.transform.position = new Vector3(0.0f, 14.6f, 0.0f);

                       // --- Line Renderer ---
                       _line.enableLine = true;

                       _menuUI.hideMenu();
                       break;
                   case 13:     // Spawn Level 14
                       Debug.Log("Level 1");

                       // --- Planets ---
                       planet[0] = new PlanetScript(0, new Vector3(0.0f, 5.0f, 0.0f));
                       planet[1] = new PlanetScript(1, new Vector3(0.0f, 7.0f, 0.0f));
                       planet[2] = new PlanetScript(1, new Vector3(0.0f, 7.0f, 0.0f));

                       // --- Win Point ---
                       wintrigger.transform.position = new Vector3(0.0f, 14.7f, 0.0f);
                       exitSign.transform.position = new Vector3(0.0f, 14.6f, 0.0f);

                       // --- Line Renderer ---
                       _line.enableLine = true;

                       _menuUI.hideMenu();
                       break;
                   case 14:     // Spawn Level 15
                       Debug.Log("Level 1");

                       // --- Planets ---
                       planet[0] = new PlanetScript(0, new Vector3(0.0f, 5.0f, 0.0f));
                       planet[1] = new PlanetScript(1, new Vector3(0.0f, 7.0f, 0.0f));
                       planet[2] = new PlanetScript(1, new Vector3(0.0f, 7.0f, 0.0f));

                       // --- Win Point ---
                       wintrigger.transform.position = new Vector3(0.0f, 14.7f, 0.0f);
                       exitSign.transform.position = new Vector3(0.0f, 14.6f, 0.0f);

                       // --- Line Renderer ---
                       _line.enableLine = true;

                       _menuUI.hideMenu();
                       break;
                   default:
                       break;*/
                #endregion
        }
    }

    #endregion

}