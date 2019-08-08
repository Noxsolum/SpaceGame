/*
Notes:
 - Basic script to create the winstate.
 - Could be moved into a different script at somepoint but wanted to keep all the elements seperate for the time being.
 - Detects when the player collider goes through the collider at the top of screen, basic but works pretty well.
 - Currently no UI to reflect the win but has debug output.
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class winTrigger : MonoBehaviour
{
    public GameObject rocketShip;
    public Collider winCollider;

    private GameObject _canvas;
    public MenuAndUIScript menuUI;

    public void Update()
    {

        winCollider = this.GetComponent<Collider>();
        winCollider.isTrigger = true;
        rocketShip = GameObject.FindGameObjectWithTag("Player");

        _canvas = GameObject.FindGameObjectWithTag("CanvasUI");
        menuUI = _canvas.GetComponent<MenuAndUIScript>();
    }

    public void OnTriggerExit(Collider other)
    {
        if(other = rocketShip.GetComponent<Collider>())
        {
            Debug.Log("WIN!!");
            menuUI.winBoxOpen();
        }
    }
}
