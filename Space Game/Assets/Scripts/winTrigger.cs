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
    public Collider2D winCollider;

    private GameObject _canvas;
    public MenuAndUIScript menuScript;

    public void Update()
    {

        winCollider = this.GetComponent<Collider2D>();
        winCollider.isTrigger = true;
        rocketShip = GameObject.FindGameObjectWithTag("Player");

        _canvas = GameObject.FindGameObjectWithTag("CanvasUI");
        menuScript = _canvas.GetComponent<MenuAndUIScript>();
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if(other = rocketShip.GetComponent<Collider2D>())
        {
            menuScript.openWinBox();
        }
    }
}
