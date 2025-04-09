/*
Notes:
- Creates the different planets.
- Probably going to use this to spawn different planets/ meteors in the future.
- Uses the planet class as well as some constructors to make spawning the levels a lot easier.
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetScript : PlanetClass {

    public GameObject canvasObj;
    public MenuAndUIScript UIScript;
    private GameObject rocketShip;

    private void Start()
    {
        rocketShip = GameObject.FindGameObjectWithTag("Player");
        canvasObj = GameObject.FindGameObjectWithTag("CanvasUI");
        UIScript = canvasObj.GetComponent<MenuAndUIScript>();
    }

    private void Update()
    {
        #region Gravity For Story
        if (UIScript.currentScene.buildIndex == 1)
        {
            HandleGravity();
        }
        #endregion
        #region Gravity for Endless
        else if (UIScript.currentScene.buildIndex == 2 || UIScript.currentScene.buildIndex == 3)
        {
            HandleEndlessGravity();
        }
        #endregion
    }

    private void HandleGravity()
    {
        PlanetGravity(this.GravityRadius, this.GravityStrenght);
        if (this.name == "Planet S(Clone)")
        {
            PlanetGravity(this.GravityRadius, this.GravityStrenght);
        }
        else if (this.name == "Planet M(Clone)")
        {
            PlanetGravity(this.GravityRadius, this.GravityStrenght);
        }
        else if (this.name == "Planet L(Clone)")
        {
            PlanetGravity(this.GravityRadius, this.GravityStrenght);
        }
    }

    void PlanetGravity(float gravitySize, float gravityForce)
    {
        Vector3 rocketPos = rocketShip.transform.position;
        Vector3 planetPos = this.transform.position;

        float rocketPlanetMag = (planetPos - rocketPos).magnitude;

        if (rocketPlanetMag < gravitySize)
        {
            rocketShip.GetComponent<Rigidbody2D>().AddForce((planetPos - rocketPos) * gravityForce);
            IsInGrav = true;
        }
        else { IsInGrav = false; }
    }

    private void HandleEndlessGravity()
    {
        Vector3 rocketPos = rocketShip.transform.position;
        Vector3 planetPos = this.transform.position;

        float rocketPlanetMag = (planetPos - rocketPos).magnitude;

        if (rocketPlanetMag < 5 && UIScript.hasDied != true)
        {
            rocketShip.GetComponent<Rigidbody2D>().AddForce((planetPos - rocketPos) * 1.75f);
            IsInGrav = true;
            if (UIScript.cameraScript.boost < 1000)
            {
                UIScript.cameraScript.boost++;
            }
            UIScript.cameraScript.boost++;
            Debug.Log("Adding Force");
        }
        else { IsInGrav = false; }
    }
}
