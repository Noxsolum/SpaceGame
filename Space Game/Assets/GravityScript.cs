/*
Notes:


*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityScript : MonoBehaviour
{
    void Update()
    {
        GameObject[] planetS = GameObject.FindGameObjectsWithTag("PlanetS");
        GameObject rocketShip = GameObject.FindGameObjectWithTag("Player");

        for (int x = 0; x < planetS.Length; x++)
        {
            Vector3 rocketPos = rocketShip.transform.position;
            Vector3 planetPos = planetS[x].transform.position;

            float rocketPlanetMag = (planetPos - rocketPos).magnitude;
            Debug.Log(rocketPlanetMag);

            if (rocketPlanetMag < 3)
            {
                rocketShip.GetComponent<Rigidbody>().AddForce(planetPos - rocketPos);
                Debug.Log("Adding Force");
            }
        }
    }
}
