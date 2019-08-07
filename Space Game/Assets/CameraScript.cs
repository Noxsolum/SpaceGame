/*
Notes:


*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    public float PerspectivezoomSpeed;
    public float OrthoZoomSpeed;
    public bool isTouched = false;
    public int launchDist_x = 0, launchDist_y = 0;
    public GameObject rocketShip;

    private Vector2 startPos = new Vector2(0.0f, 1.35f);
    private Vector3 launchDir = new Vector3(0.0f, 0.0f, 0.0f);
    private Vector3 cameraPos =  new Vector3(0.0f, 0.0f, -10.0f);

    void Update()
    {
        GameObject rocketShip = GameObject.FindGameObjectWithTag("Player");

        if ((Input.touchCount == 1) && (Input.GetTouch(0).phase == TouchPhase.Began))
        {
            Ray raycast = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit rayCastHit;

            if (Physics.Raycast(raycast, out rayCastHit))
            {
                if (rayCastHit.collider.CompareTag("Player"))
                {
                    Debug.Log("Touched the player");
                    startPos = Input.GetTouch(0).position;
                    launchDist_x = Mathf.RoundToInt((startPos - Input.GetTouch(0).position).magnitude);
                    isTouched = true;
                }
                else { launchDist_x = 0; }
            }
        }

        if (isTouched == true && Input.GetTouch(0).phase != TouchPhase.Ended)
        {
            launchDist_y = Mathf.RoundToInt((startPos - Input.GetTouch(0).position).magnitude);
        }
        else if (isTouched == true && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            launchDist_y = Mathf.RoundToInt((startPos - Input.GetTouch(0).position).magnitude);
            launchDir = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            //Debug.Log(launchDist_y);
            if(launchDist_y > 20 && launchDist_y < 140)
            {
                rocketShip.GetComponent<Rigidbody>().AddForce(launchDir * (launchDist_y / 2));
                //Debug.Log(launchDir);
            }
            isTouched = false;
        }


        if (Input.touchCount == 2)
        {
            // Gets the first two touchs in the touch array
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            // Need to find the positions of the touchs in the previous frame (A = CurrentPos - DeltaPos)
            Vector2 touchZeroPrevPos = touchZero.position /*Current Position of the touch*/ - touchZero.deltaPosition /*the difference in position between the touchs current position and position last frame*/;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude; // Distance between points on the previous frame
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag; // The change in difference (Positive Results - Zoom Out, Negative Result - Zoom in)

            if(Camera.main.orthographic == true)
            {
                Camera.main.orthographicSize += deltaMagnitudeDiff * OrthoZoomSpeed;
                Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, 5.0f, 15.0f);
                cameraPos.y = Camera.main.orthographicSize;
            }
            else
            {
                Camera.main.fieldOfView += deltaMagnitudeDiff * PerspectivezoomSpeed;
                Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView, 0.1f, 179.9f);
            }

            Camera.main.transform.position = cameraPos;
        }
    }
}
