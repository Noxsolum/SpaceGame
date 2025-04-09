/*
Notes:


*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    #region Public Variables
    public float PerspectivezoomSpeed;
    public float OrthoZoomSpeed;
    public bool isTouched = false;
    public bool isLaunched = false;
    public bool isEndless = false;
    public bool hasAnimated = false;
    public int launchPower = 0, maxPower = 500, boost = 1000;
    public Vector2 origin;
    public GameObject rocketShip, updateObj, settingObj;
    public Animation cameraAnim;
    #endregion

    #region Private Variables
    private UpdateScript _updateScr;
    private Vector2 startPos = new Vector2(0.0f, 1.35f);
    private Vector3 launchDir = new Vector3(0.0f, 10.0f, 0.0f); // Making public briefly
    private Vector3 cameraPos =  new Vector3(0.0f, 0.0f, -10.0f);
    #endregion

    void Awake()
    {
        settingObj = GameObject.FindGameObjectWithTag("PersistSettings");
        updateObj = GameObject.FindGameObjectWithTag("Home");
        rocketShip = GameObject.FindGameObjectWithTag("Player");

        _updateScr = updateObj.GetComponent<UpdateScript>();
        cameraAnim = this.GetComponent<Animation>();
    }

    void Update()
    {
        // =====================
        // Assorted Debug
        // =====================
        //Debug.Log(Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position));
        //Debug.Log("Velocity = " + rocketShip.GetComponent<Rigidbody2D>().velocity);
        //Debug.Log("Boost = " + boost);


        // ===================
        //  --- Use Boost ---
        // ===================
        #region Boost

        if (Input.touchCount == 1 && isLaunched == true)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began && boost > 0)
            {
                origin = rocketShip.GetComponent<Rigidbody2D>().linearVelocity;
                rocketShip.GetComponent<Rigidbody2D>().AddForce(rocketShip.GetComponent<Rigidbody2D>().linearVelocity.normalized * 10f, ForceMode2D.Impulse);
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Stationary & boost > 0)
            {
                Debug.Log(boost);
                boost--;
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Ended && boost > 0)
            {
                rocketShip.GetComponent<Rigidbody2D>().AddForce(rocketShip.GetComponent<Rigidbody2D>().linearVelocity.normalized * -10f, ForceMode2D.Impulse);
            }
        }

        #endregion

        // =====================
        //  --- Launch Ship ---
        // =====================
        #region  Launch Ship

        if ((Input.touchCount == 1) && (Input.GetTouch(0).phase == TouchPhase.Began))
        {
            Vector2 raycast = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            RaycastHit2D rayCastHit = Physics2D.Raycast(raycast, Input.GetTouch(0).position);

            if (rayCastHit.collider)
            {
                Debug.Log("Hit Something");
                if (rayCastHit.collider.CompareTag("Player"))
                {
                    Debug.Log("Touched the player");
                    startPos = Input.GetTouch(0).position;
                    launchPower = Mathf.RoundToInt((startPos - Input.GetTouch(0).position).magnitude);
                    isTouched = true;
                }
                else { launchPower = 0; }
            }
        }

        if (isTouched == true && Input.GetTouch(0).phase != TouchPhase.Ended)
        {
            launchPower = Mathf.RoundToInt((startPos - Input.GetTouch(0).position).magnitude);
        }
        else if (isTouched == true && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            if(Input.GetTouch(0).position.y >= -2)
            {
                launchPower = Mathf.RoundToInt((startPos - Input.GetTouch(0).position).magnitude);
                launchDir = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position) - rocketShip.transform.position;

                if (launchPower > 100 && launchPower < 500)
                {
                    rocketShip.GetComponent<Rigidbody2D>().AddForce(launchDir * (launchPower / 10));
                    isLaunched = true;
                }
                else if (launchPower > 100 && launchPower > 500)
                {
                    rocketShip.GetComponent<Rigidbody2D>().AddForce(launchDir * (maxPower / 10));
                    isLaunched = true;
                }
                else
                {
                    isTouched = false;
                }
            }
            isTouched = false;
        }

        #endregion

        // =====================
        // --- Check if the Player has been Touched ---
        // =====================
        CheckPlayerTouch();
        // =====================
        // --- Zoom In & Out ---
        // =====================
        #region Zoom

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
                Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, 5.0f, 16.875f);
                cameraPos.y = Camera.main.orthographicSize;
            }
            else
            {
                Camera.main.fieldOfView += deltaMagnitudeDiff * PerspectivezoomSpeed;
                Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView, 0.1f, 179.9f);
            }

            Camera.main.transform.position = cameraPos;
        }
#endregion

        // =====================
        // --- Autozoom Out ---
        // =====================
        #region AutoZoom

        if (isLaunched == true && rocketShip.transform.position.y >= 5.5f && hasAnimated == false)
        {
            this.transform.parent = rocketShip.transform;
            this.transform.rotation = rocketShip.transform.rotation;
            //this.transform.localPosition = new Vector3(0.0f, 0.0f, -14.6f);
            cameraAnim["CameraZoomOut"].wrapMode = WrapMode.Once;
            cameraAnim.Play("CameraZoomOut");
            hasAnimated = true;
        }

        #endregion
    }

    public bool CheckPlayerTouch()
    {
        if ((Input.touchCount == 1) && (Input.GetTouch(0).phase == TouchPhase.Began))
        {
            Vector2 raycast = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            RaycastHit2D rayCastHit = Physics2D.Raycast(raycast, Input.GetTouch(0).position);

            if (rayCastHit.collider)
            {
                Debug.Log("Hit Something");
                if (rayCastHit.collider.CompareTag("Player"))
                {
                    Debug.Log("Touched the player");
                    return true;
                }
            }
        }
        return false;
    }
}
