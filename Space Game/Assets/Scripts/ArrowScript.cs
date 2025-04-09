using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript: MonoBehaviour
{
    public GameObject cameraMain, Arrow, spaceShip;
    public CameraScript _camScript;

    // The target marker.
    public Vector2 target;

    // Angular speed in radians per sec.
    public float speed = 1.0f;

    private void Awake()
    {
        GameObject cameraMain = GameObject.FindGameObjectWithTag("MainCamera");
        GameObject spaceShip = GameObject.FindGameObjectWithTag("Player");
        GameObject Arrow = GameObject.FindGameObjectWithTag("Arrow");

        _camScript = cameraMain.GetComponent<CameraScript>();
    }

    void Update()
    {
        
    }
}
