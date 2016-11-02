using UnityEngine;
using System.Collections;

public class RTSCamera : MonoBehaviour {

	//this is the laerMask used to autocontrol the camera height
	public LayerMask groundLayer;

	//Class that have all variables used to move the camera
	[System.Serializable]
	public class PositionSettings{
		public bool invertPan = true; // bool used to move in the oposite way you are dragging the mouse
		public float panSmooth = 7.0f; // speed of the dragging movement
		public float distanceFromGround = 40.0f; // base height used for the camera to "avoid" obstacles
		public bool allowZoom = true; // bool to allow zoom with the mouse wheel
		public float zoomSmoth = 5.0f; // speed of the zoom movement
		public float zoomStep = 5.0f; // how much the distance is increased while you are turning the mouse wheel
		public float maxZoom = 25.0f; // how close you can be to the scene
		public float minZoom = 80.0f; // how far you can be to the scene

		//newDistance is used to check before zoom movement
		[HideInInspector]
		public float newDistance = 40.0f; //Same as distanceFromGround
	}

	//class that have variables involved in camera rotation
	[System.Serializable]
	public class OrbitSettings{
		public float xRotation = 50.0f; // used to initialize the x rotation of the camera
		public float yRotation = 0.0f; // used to initialixe the y rotation of the camera
		public bool allowOrbit = true; // bool used to allow orbit or not
		public float yOrbitSmooth = 5.0f; // orbit speed

	}

	// Class that have the custom inputs to use the camera
	[System.Serializable]
	public class InputSettings{
		public string PAN = "MousePan"; // used to check the camera dragging
		public string ORBIT_Y = "MouseTurn"; // used to check the camera rotation
		public string ZOOM = "Mouse ScrollWheel"; // used to check the zoom
	}

	//initializing the new classes
	public PositionSettings position = new PositionSettings();
	public OrbitSettings orbit = new OrbitSettings();
	public InputSettings input = new InputSettings ();

	//vectors used while moving the camera around
	Vector3 destination = Vector3.zero;
	Vector3 camVel = Vector3.zero;
	Vector3 previousMousePos = Vector3.zero;
	Vector3 currentMousePos = Vector3.zero;

	float panInput, orbitInput, zoomInput;
	int panDirection = 0;

	float terrainWidth = 0.0f;
	float terrainHeight = 0.0f;

	// Use this for initialization
	void Start () {
		panInput = 0.0f;
		orbitInput = 0.0f;
		zoomInput = 0.0f;

		Vector3 terrainSize;
		GameObject GameTerrain = GameObject.Find ("Terrain");
		terrainSize = GameTerrain.GetComponent<Terrain>().terrainData.size;

		terrainWidth = terrainSize.x;
		terrainHeight = terrainSize.z;

		//Debug.Log (terrainSize);
	}

	void GetInput(){
		//responsible for setting input variables
		panInput = Input.GetAxis (input.PAN);
		orbitInput = Input.GetAxis (input.ORBIT_Y);
		zoomInput = Input.GetAxis (input.ZOOM);

		previousMousePos = currentMousePos;
		currentMousePos = Input.mousePosition;
	}
	
	// Update is called once per frame
	void Update () {
		//update input
		GetInput();
		if (position.allowZoom)
			Zoom ();
		if (orbit.allowOrbit)
			Rotate ();
		PanWorld ();
	}

	void FixedUpdate(){
		HandleCameraDistance ();
	}

	void PanWorld(){
		Vector3 targetPos = transform.position;

		if (position.invertPan)
			panDirection = -1;
		else
			panDirection = 1;

		if (panInput > 0) {
			targetPos += transform.right * (currentMousePos.x - previousMousePos.x) * position.panSmooth * panDirection * Time.deltaTime;
			//we can't use transform.up because of the camera rotation, we use Vector3.Cross (transform.right, Vector3.up)
			// to find our transform.up
			targetPos += Vector3.Cross (transform.right, Vector3.up) * (currentMousePos.y - previousMousePos.y) * position.panSmooth * panDirection *Time.deltaTime;
		}
		if(targetPos.x >= 0 && targetPos.x <= terrainWidth && targetPos.z >= 0 && targetPos.z <= terrainHeight)
			transform.position = targetPos;
	}

	void HandleCameraDistance(){
		Ray ray = new Ray (transform.position, transform.forward);
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit, 100, groundLayer)) {
			destination = Vector3.Normalize (transform.position - hit.point) * position.distanceFromGround;
			destination += hit.point;

			transform.position = Vector3.SmoothDamp (transform.position, destination, ref camVel, 0.3f);
		}
	}

	void Zoom(){
		position.newDistance += position.zoomStep * -zoomInput;

		position.distanceFromGround = Mathf.Lerp (position.distanceFromGround, position.newDistance, position.zoomSmoth * Time.deltaTime);

		if (position.distanceFromGround < position.maxZoom) {
			position.distanceFromGround = position.maxZoom;
			position.newDistance = position.maxZoom;
		}
		if (position.distanceFromGround > position.minZoom) {
			position.distanceFromGround = position.minZoom;
			position.newDistance = position.minZoom;
		}
	}

	void Rotate(){
		if (orbitInput > 0) {
			orbit.yRotation += (currentMousePos.x - previousMousePos.x) * orbit.yOrbitSmooth * Time.deltaTime;
		}
		transform.rotation = Quaternion.Euler (orbit.xRotation, orbit.yRotation, 0.0f);
	}
}
