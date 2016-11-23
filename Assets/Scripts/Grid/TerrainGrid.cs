using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TerrainGrid : MonoBehaviour {
	// layer to take in consideration to choose a grid to be valid or not
	public LayerMask buildingsMask;
	// varibles used to make the grid including the materials
	public float cellSize = 1;
	public int gridWidth = 50;
	public int gridHeight = 50;
	public float yOffset = 0.5f;
	public Material cellMaterialValid;
	public Material cellMaterialInvalid;
	// boolean to know if the grid is toggled or not
	bool gridVisible;

	//gameobject with the prefab of the tower
	public GameObject tower;

	//this object will be modified and used to show where the tower will be placed
	public GameObject towerShadow;
	GameObject shadow;

	// basic matrix structure that will have all the cells in it
	Square [,] grid;

	// gameobject which will be following the camera and containing the grid
	GameObject gridBox;
	Vector3 boxPos;

	// start to initiate everything
	void Start() {

		gridBox = GameObject.Find ("Grid");
		boxPos = gridBox.transform.position;

		gridVisible = false;

		grid = new Square[gridWidth, gridHeight];
		Vector3 gridBottomLeft = transform.position - Vector3.right * gridWidth / 2 - Vector3.forward * gridHeight / 2;

		// this lines create the grid and initiates all the variables of the cells
		for (int x = 0; x < gridWidth; ++x) {
			for (int y = 0; y < gridHeight; ++y) {
				Vector3 worldPoint = gridBottomLeft + (Vector3.right * x) + (Vector3.forward * y);
				bool building = !(Physics.CheckSphere (worldPoint, cellSize));
				GameObject cell = CreateChild (x,y,gridBottomLeft);
				cell.SetActive (gridVisible);
				grid [x, y] = new Square (building, worldPoint, cell);

			}
		}

		//initiate the towerShadow, to do so, we have to change the layers and disable the colliders
		shadow = (GameObject)Instantiate (towerShadow, Input.mousePosition,transform.rotation);
		shadow.GetComponentInChildren<MeshRenderer> ().enabled = false;
		shadow.layer = LayerMask.NameToLayer ("Default");
		shadow.transform.GetChild (0).gameObject.layer = LayerMask.NameToLayer ("Default");

		//shadow.GetComponentInChildren<Weapon> ().enabled = false;
		//shadow.GetComponentInChildren<Tower> ().enabled = false;


	}

	/* 	every frame want to:
		-update the box position
		-update the cells including the materials
		-check if there are inputs
		-check if the grid is visible or not to show the shadow
		-move the shadow
	*/
	void Update () {

		boxPos = gridBox.transform.position;

		UpdateCells();

		GetInputs ();

		if (gridVisible) {
			shadow.GetComponentInChildren<MeshRenderer> ().enabled = true;
			moveShadow ();
		}
		else shadow.GetComponentInChildren<MeshRenderer> ().enabled = false;
	}

	/*	to move the shadow we just check if the mouse is inside the grid
		after that, checking with raycasthit the position where to locate the
		shadow
	*/
	void moveShadow(){
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		if (Physics.Raycast (ray, out hit)) {
			if (hit.transform != null && hit.transform.gameObject.layer == LayerMask.NameToLayer ("Grid")) {
				Vector3 aux = Vector3.zero;
				aux.x = (int)hit.point.x;
				aux.y = (int)hit.point.y;
				aux.z = (int)hit.point.z;
				shadow.transform.position = aux;
			}
		}
	}

	/*
		This function checks 2 kinds of inputs:
		-Pressing A to toggle the grid/building mode
		-Left-Click to place the tower in the position
	*/
	void GetInputs(){
		if (Input.GetMouseButtonDown (0)) {
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			if (Physics.Raycast (ray, out hit)) {
				if (hit.transform != null && hit.transform.gameObject.layer == LayerMask.NameToLayer("Grid")) {
					//Debug.Log (hit.transform.gameObject.layer);
					Vector3 aux = Vector3.zero;
					aux.x = (int)hit.point.x;
					aux.y = (int)hit.point.y;
					aux.z = (int)hit.point.z;

					bool valid = IsCellValid ((int)aux.x, (int)aux.z);
					Debug.Log (valid);

					if (gridVisible && valid) {
						aux.y = 0;
						GameObject t = (GameObject)Instantiate (tower, aux,transform.rotation);
						t.GetComponentInChildren<CapsuleCollider> ().radius = 20;
						t.GetComponentInChildren<CapsuleCollider> ().height = 30;
						
						t.GetComponentInChildren<Weapon> ().setSourceDeath(GameObject.Find ("Death Audio Source").GetComponent<AudioSource>());
						t.GetComponentInChildren<Weapon> ().setSourceShoot(GameObject.Find ("Shoot Audio Source").GetComponent<AudioSource>());

						toggleGrid ();
					}
				}
			}
		}

		if (Input.GetKeyDown (KeyCode.A)) {
			toggleGrid ();
		}
	}

	/*
	 * this function toggle on/off every cell of the grid
	 */
	void toggleGrid(){
		gridVisible = !gridVisible;
		for (int i = 0; i < gridWidth; ++i) {
			for (int j = 0; j < gridHeight; ++j) {
				grid [i, j].cell.SetActive (gridVisible);
			}
		}
	}

	/*
		this function creates the gameObject that will represent the cell
		and places it in the grid layer
	*/
	GameObject CreateChild(int x, int z, Vector3 gridLeft) {
		GameObject go = new GameObject();

		go.name = "Grid Cell";
		go.transform.parent = transform;
		go.transform.localPosition = Vector3.zero;
		go.AddComponent<MeshRenderer>();
		go.AddComponent<MeshFilter>().mesh = CreateMesh();
		go.layer = LayerMask.NameToLayer("Grid");

		return go;
	}

	/*
	 * this function updates cells information, which includes changing the material
	 * if needed
	 */
	void UpdateCells() {
		for (int z = 0; z < gridHeight; z++) {
			for (int x = 0; x < gridWidth; x++) {
				GameObject cell = grid[x,z].cell;
				MeshRenderer meshRenderer = cell.GetComponent<MeshRenderer>();
				MeshFilter meshFilter = cell.GetComponent<MeshFilter>();

				//meshRenderer.material = IsCellValid(x, z) ? cellMaterialValid : cellMaterialInvalid;
				if (IsCellValid (x, z)) {
					meshRenderer.material = cellMaterialValid;
					grid [x, z].valid = true;
				} else {
					meshRenderer.material = cellMaterialInvalid;
					grid [x, z].valid = false;
				}

				UpdateMesh(meshFilter.mesh, x, z);
			}
		}
	}

	/*
		Makes a rayCast to check if there is a building in that cell position.
		Returns a boolean that will change or not the material of the cell.
	*/
	bool IsCellValid(int x, int z) {
		Vector3 cellPos = Vector3.zero;
		cellPos.x = boxPos.x - (gridWidth/2 - x);
		cellPos.y = 200;
		cellPos.z = boxPos.z - (gridHeight/2 - z);

		RaycastHit hitInfo;
		//Vector3 origin = new Vector3(x * cellSize + cellSize/2, 200, z * cellSize + cellSize/2);
		//Debug.Log (transform.TransformPoint(origin));
		Physics.Raycast(cellPos, Vector3.down, out hitInfo, Mathf.Infinity, LayerMask.GetMask("Buildings"));

		return (hitInfo.collider == null);
	}

	/*
		The following functions are used to make the mesh of the cells
	*/
	Mesh CreateMesh() {
		Mesh mesh = new Mesh();

		mesh.name = "Grid Cell";
		mesh.vertices = new Vector3[] { Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero };
		mesh.triangles = new int[] { 0, 1, 2, 2, 1, 3 };
		mesh.normals = new Vector3[] { Vector3.up, Vector3.up, Vector3.up, Vector3.up };
		mesh.uv = new Vector2[] { new Vector2(1, 1), new Vector2(1, 0), new Vector2(0, 1), new Vector2(0, 0) };

		return mesh;
	}

	void UpdateMesh(Mesh mesh, int x, int z) {
		mesh.vertices = new Vector3[] {
			MeshVertex(x, z),
			MeshVertex(x, z + 1),
			MeshVertex(x + 1, z),
			MeshVertex(x + 1, z + 1),
		};
	}
	
	Vector3 MeshVertex(int x, int z) {
		return new Vector3(x * cellSize - (gridWidth/2), yOffset, z * cellSize - (gridHeight/2));
	}		

}