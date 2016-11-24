using UnityEngine;
using System.Collections;

public class CameraRotation : MonoBehaviour
{
    //This are the speeds the camera will turn arround
    public float moveSpeedV = 10f;
    public float moveSpeedH = 30f;

    //This is the point the camera always will look at
    public Transform ubCenter;

    // Use this for initialization
    void Start()
    {
    }

    // Using FixedUpdate because we are messing with the camera
    void FixedUpdate()
    {
        //Creating a vectorposition to limit the height max and min of the camera
        Vector3 position = transform.position;

        //Using the keys to control the camera
        if (Input.GetKey(KeyCode.UpArrow))
        {
            //noticing which will be the future height position to compare it later
            position = position + (Vector3.up*moveSpeedV*Time.deltaTime);
            //We just want the camera to move if our next height is below the max
            if (position.y <= 40)
                transform.Translate(Vector3.up*moveSpeedV*Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            //noticing which will be the future height position to compare it later
            position = position + (Vector3.up*moveSpeedV*Time.deltaTime);
            //We just want the camera to move if our next height is over the min
            if (position.y >= 20)
                transform.Translate(-Vector3.up*moveSpeedV*Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
            transform.Translate(Vector3.left*moveSpeedV*Time.deltaTime);
        if (Input.GetKey(KeyCode.RightArrow))
            transform.Translate(-Vector3.left*moveSpeedV*Time.deltaTime);

        //To make sure the camera points the right way
        transform.LookAt(ubCenter.transform.position);
    }
}