using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LineRenderer : MonoBehaviour
{
    public LineRenderer launchLine;
    public GameObject player;
    private Vector2 startPos, touchPos;
    public bool enableLine = false;

    // Start is called before the first frame update
//    void Start()
//    {
//        player = GameObject.FindGameObjectWithTag("Player");
//        launchLine = this.GetComponent<LineRenderer>();
//        //launchLine.positionCount = 2;
//        startPos = new Vector2(0.0f, 0.0f);    
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        if (enableLine == true)
//        {
//            if (Input.touchCount == 1)
//            {
//                touchPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
//                launchLine.SetPosition(0, player.transform.position);
//                launchLine.SetPosition(1, new Vector3(touchPos.x, touchPos.y, 0.0f));
//                this.gameObject.transform.position = touchPos;
//                this.gameObject.transform.LookAt(touchPos, Vector3.left);
//                launchLine.startWidth = 0.1f;
//                launchLine.endWidth = 0.2f;
//                launchLine.startColor = Color.red;
//                launchLine.endColor = Color.red;
//                launchLine.sortingLayerName = "Objects Layer";
//                launchLine.sortingOrder = 1;
//            }
//        }
//    }
}
