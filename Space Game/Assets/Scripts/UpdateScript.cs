/*
Notes:


*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateScript : MonoBehaviour
{
    Vector3 _spinning = new Vector3(0.0f, 0.0f, 10.0f);
    Vector3 startPos;
    Vector2 force;
    public int endlessScore = 0;
    public bool isInGrav = false;
    GameObject[] planetS, planetM, planetL;
    GameObject rocketShip;

    private void Start()
    {
        rocketShip = GameObject.FindGameObjectWithTag("Player");
        startPos = rocketShip.transform.position;
    }

    void Update()
    {
        planetS = GameObject.FindGameObjectsWithTag("PlanetS");
        planetM = GameObject.FindGameObjectsWithTag("PlanetM");
        planetL = GameObject.FindGameObjectsWithTag("PlanetL");
        rocketShip = GameObject.FindGameObjectWithTag("Player");
        
        endlessScore = Mathf.RoundToInt((rocketShip.transform.position - startPos).magnitude);

        rotatePlanet(planetS);
        rotatePlanet(planetM);
        rotatePlanet(planetL);
        faceDirection(rocketShip);
    }

    #region Rotate Planet

    /// <summary>
    /// Makes the planet rotate around its z axis
    /// </summary>
    /// <param name="_planets"></param>
    public void rotatePlanet(GameObject[] _planets)
    {
        for(int z = 0; z < _planets.Length; z++)
        {
            _planets[z].transform.Rotate(_spinning * Time.deltaTime);
        }
    }

    #endregion

    #region SpaceShip Face Direction

    /// <summary>
    /// Makes the SpaceShip face the direction of travel
    /// </summary>
    /// <param name="_player"></param>
    public void faceDirection(GameObject _player)
    {
        Vector2 currDir = _player.GetComponent<Rigidbody2D>().linearVelocity;

        if (currDir != Vector2.zero)
        {
            float angle = (Mathf.Atan2(currDir.y, currDir.x) * Mathf.Rad2Deg) - 90.0f;
            _player.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    #endregion
}
