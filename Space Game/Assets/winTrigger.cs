using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class winTrigger : MonoBehaviour
{
    public bool _win = false;
    public GameObject rocketShip;
    public Collider winCollider;

    public void Update()
    {
        winCollider = this.GetComponent<Collider>();
        winCollider.isTrigger = true;
        rocketShip = GameObject.FindGameObjectWithTag("Player");
    }


    public void OnTriggerExit(Collider other)
    {
        if(other = rocketShip.GetComponent<Collider>())
        {
            Debug.Log("WIN!!");
            _win = true;
        }
    }

}
