using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetScript : MonoBehaviour {

    private int planetSize = 0;
    private Vector3 location = new Vector3(0.0f, 0.0f, 0.0f);
    private Quaternion rotation = new Quaternion(0.0f, 0.0f, 0.0f, 0.0f);

    //public GameObject planetS;
    //public GameObject planetM;
    //public GameObject planetL;

    public PlanetScript()
    {
        planetSize = 0;
        //SpawnPlanet(planetSize, location);
    }

    public PlanetScript(int _sizeX)
    {
        planetSize = _sizeX;
    }

    public PlanetScript(int _size, Vector3 _location)
    {
        planetSize = _size;
        location = _location;
        SpawnPlanet(planetSize, location, rotation);
    }

    void SpawnPlanet(int _size, Vector3 _location, Quaternion _rotation)
    {
        switch (_size)
        {
            case 0:
                Instantiate(Resources.Load("PlanetS"), _location, _rotation);
                break;
            case 1:
                Instantiate(Resources.Load("PlanetM"), _location, _rotation);
                break;
            case 2:
                Instantiate(Resources.Load("PlanetL"), _location, _rotation);
                break;
            default:
                Instantiate(Resources.Load("PlanetS"), _location, _rotation);
                break;
        }
    }
}
