﻿using System;
using UnityEngine;

public class RTSCamera : MonoBehaviour
{
    private Vector3 camVel = Vector3.zero;
    private Vector3 currentMousePos = Vector3.zero;

    //vectors used while moving the camera around
    private Vector3 destination = Vector3.zero;
    //this is the laerMask used to autocontrol the camera height
    public LayerMask groundLayer;
    public InputSettings input = new InputSettings();
    public OrbitSettings orbit = new OrbitSettings();
    //int to change the way the dragging move is performed
    private int panDirection;

    //floats to control if there is an input
    private float panInput, orbitInput, zoomInput;

    //initializing the new classes
    public PositionSettings position = new PositionSettings();
    private Vector3 previousMousePos = Vector3.zero;
    private float terrainHeight;

    //floats to get the size of the field to avoid going further
    private float terrainWidth;

    // Use this for initialization
    private void Start()
    {
        //initializing inputs to 0
        panInput = 0.0f;
        orbitInput = 0.0f;
        zoomInput = 0.0f;

        //getting the terrain size initialized
        Vector3 terrainSize;
        var GameTerrain = GameObject.Find("Terrain");
        terrainSize = GameTerrain.GetComponent<Terrain>().terrainData.size;

        terrainWidth = terrainSize.x;
        terrainHeight = terrainSize.z;

        //Debug.Log (terrainSize);
    }

    //responsible for setting input variables
    private void GetInput()
    {
        panInput = Input.GetAxis(input.PAN);
        orbitInput = Input.GetAxis(input.ORBIT_Y);
        zoomInput = Input.GetAxis(input.ZOOM);

        //while input, update the mouse positions
        previousMousePos = currentMousePos;
        currentMousePos = Input.mousePosition;
    }

    // Update is called once per frame
    private void Update()
    {
        //update input
        GetInput();
        //calling zoom function
        if (position.allowZoom)
            Zoom();
        //calling orbit function
        if (orbit.allowOrbit)
            Rotate();
        //calling movement by dragging function
        PanWorld();
    }

    /*void FixedUpdate(){
        //calling a function that checks for the camera to stay at the same distance to the ground
        HandleCameraDistance ();
    }*/

    private void PanWorld()
    {
        //getting our camera position
        var targetPos = transform.position;

        //setting the direction of the movement
        if (position.invertPan)
            panDirection = -1;
        else
            panDirection = 1;

        //checking if we have an input to move the camera
        if (panInput > 0)
        {
            //this sets the "horizontal" movement because we can use the transform right
            targetPos += transform.right*(currentMousePos.x - previousMousePos.x)*position.panSmooth*panDirection*
                         Time.deltaTime;
            //can't use transform.up because of the camera rotation, use Vector3.Cross (transform.right, Vector3.up)
            // to find our transform.up
            targetPos += Vector3.Cross(transform.right, Vector3.up)*(currentMousePos.y - previousMousePos.y)*
                         position.panSmooth*panDirection*Time.deltaTime;
        }
        //check if our nextPos is outside the terrain, if the next position is inside, change camera position
        if ((targetPos.x >= -3500) && (targetPos.x <= terrainWidth) && (targetPos.z >= -3500) &&
            (targetPos.z <= terrainHeight))
            transform.position = targetPos;
    }

    //this function makes the camera stay at the same distance with the groundLayer
    //using ray and rayCastHit can know where the camera is looking at and making it stay the same distance
    private void HandleCameraDistance()
    {
        var ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100, groundLayer))
        {
            destination = Vector3.Normalize(transform.position - hit.point)*position.distanceFromGround;
            destination += hit.point;
            transform.position = Vector3.SmoothDamp(transform.position, destination, ref camVel, 0.3f);
        }
    }

    //Zoom() is a function that changes the camera distance with the scene. This is betwen the max and min zoom set
    private void Zoom()
    {
        position.newDistance += position.zoomStep*-zoomInput;
        //calling Lerp to make a smoother transition betwen camera pos and nextPos
        position.distanceFromGround = Mathf.Lerp(position.distanceFromGround, position.newDistance,
            position.zoomSmoth*Time.deltaTime);

        if (position.distanceFromGround < position.maxZoom)
        {
            position.distanceFromGround = position.maxZoom;
            position.newDistance = position.maxZoom;
        }
        if (position.distanceFromGround > position.minZoom)
        {
            position.distanceFromGround = position.minZoom;
            position.newDistance = position.minZoom;
        }
    }

    //Rotate() allows the camera to rotate in the 'y' axis. Can be disabled with its checkbox
    private void Rotate()
    {
        if (orbitInput > 0)
            orbit.yRotation += (currentMousePos.x - previousMousePos.x)*orbit.yOrbitSmooth*Time.deltaTime;
        transform.rotation = Quaternion.Euler(orbit.xRotation, orbit.yRotation, 0.0f);
    }

    //Class that have all variables used to move the camera
    [Serializable]
    public class PositionSettings
    {
        public bool allowZoom = true; // bool to allow zoom with the mouse wheel
        public float distanceFromGround = 60.0f; // base height used for the camera to "avoid" obstacles
        public bool invertPan = true; // bool used to move in the oposite way you are dragging the mouse
        public float maxZoom = 40.0f; // how close you can be to the scene
        public float minZoom = 80.0f; // how far you can be to the scene

        //newDistance is used to check before zoom movement
        [HideInInspector] public float newDistance = 60.0f; //Same as distanceFromGround
        public float panSmooth = 7.0f; // speed of the dragging movement
        public float zoomSmoth = 5.0f; // speed of the zoom movement
        public float zoomStep = 5.0f; // how much the distance is increased while you are turning the mouse wheel
    }

    //class that have variables involved in camera rotation
    [Serializable]
    public class OrbitSettings
    {
        public bool allowOrbit = true; // bool used to allow orbit or not
        public float xRotation = 50.0f; // used to initialize the x rotation of the camera
        public float yOrbitSmooth = 5.0f; // orbit speed
        public float yRotation; // used to initialixe the y rotation of the camera
    }

    // Class that have the custom inputs to use the camera
    [Serializable]
    public class InputSettings
    {
        public string ORBIT_Y = "MouseTurn"; // used to check the camera rotation
        public string PAN = "MousePan"; // used to check the camera dragging
        public string ZOOM = "Mouse ScrollWheel"; // used to check the zoom
    }
}