using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform target;
    public Transform farBackground, middleBackground;

    public float minHeight, maxHeight;


    //private float lastXPos;
    private Vector2 lastPos;


    // Start is called before the first frame update
    void Start()
    {
        // Keeping track of what our previous x value was so we can apply it accordingly to the camera system
        //lastXPos = transform.position.x;

        lastPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);

        /*
        // setting a value we want to 'clamp' our screen between
        float clampedY = Mathf.Clamp(transform.position.y, minHeight, maxHeight);

        transform.position = new Vector3(transform.position.x, clampedY, transform.position.z);
        */


        transform.position = new Vector3(target.position.x, Mathf.Clamp(target.position.y, minHeight, maxHeight),
            transform.position.z);


        //float amountToMoveX = transform.position.x - lastXPos;
        Vector2 amountToMove = new Vector2(transform.position.x - lastPos.x, transform.position.y - lastPos.y);

        // adding on to the position we currently have with the amount to move X with amountToMoveX
        farBackground.position = farBackground.position + new Vector3(amountToMove.x, amountToMove.y, 0f);
        
        // move middleBackground half the distance of which we'd mvoe the farBackground (static following)
        middleBackground.position += new Vector3(amountToMove.x, amountToMove.y, 0f) * 0.5f;

        // updating our lastXPos variable to the current x position
        //lastXPos = transform.position.x;

        lastPos = transform.position;
    }
}
