using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FailScript : MonoBehaviour
{
    public GameObject rocketShip;
    public Collider2D failCollider;

    private GameObject _canvas;
    public MenuAndUIScript menuScript;

    public void Update()
    {
        failCollider = this.GetComponent<Collider2D>();
        failCollider.isTrigger = true;
        rocketShip = GameObject.FindGameObjectWithTag("Player");

        _canvas = GameObject.FindGameObjectWithTag("CanvasUI");
        menuScript = _canvas.GetComponent<MenuAndUIScript>();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other = rocketShip.GetComponent<Collider2D>())
        {
            rocketShip.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(0.0f, 0.0f);
            menuScript.hasDied = true;
            menuScript.failResetShow();
 
            //menuScript.ResetLevel();
        }
    }
}
