using UnityEngine;

public class TerrainGrid : MonoBehaviour
{
    private Vector3 boxPos;
    // layer to take in consideration to choose a grid to be valid or not
    public LayerMask buildingsMask;
    public Material cellMaterialInvalid;
    public Material cellMaterialValid;
    // varibles used to make the grid including the materials
    public float cellSize = 1;

    // basic matrix structure that will have all the cells in it
    private Square[,] grid;

    // gameobject which will be following the camera and containing the grid
    private GameObject gridBox;
    public int gridHeight = 50;
    // boolean to know if the grid is toggled or not
    private bool gridVisible;
    public int gridWidth = 50;
    private GameObject shadow;

    //gameobject with the prefab of the tower
    public GameObject tower;

    //this object will be modified and used to show where the tower will be placed
    public GameObject towerShadow;
    public float yOffset = 0.5f;

    // start to initiate everything
    private void Start()
    {
        gridBox = GameObject.Find("Grid");
        boxPos = gridBox.transform.position;

        gridVisible = false;

        grid = new Square[gridWidth, gridHeight];
        var gridBottomLeft = transform.position - Vector3.right*gridWidth/2 - Vector3.forward*gridHeight/2;

        // this lines create the grid and initiates all the variables of the cells
        for (var x = 0; x < gridWidth; ++x)
            for (var y = 0; y < gridHeight; ++y)
            {
                var worldPoint = gridBottomLeft + Vector3.right*x + Vector3.forward*y;
                var building = !Physics.CheckSphere(worldPoint, cellSize);
                var cell = CreateChild(x, y, gridBottomLeft);
                cell.SetActive(gridVisible);
                grid[x, y] = new Square(building, worldPoint, cell);
            }

        //initiate the towerShadow, to do so, we have to change the layers and disable the colliders
        shadow = (GameObject) Instantiate(towerShadow, Input.mousePosition, transform.rotation);
        shadow.GetComponentInChildren<MeshRenderer>().enabled = false;
        shadow.layer = LayerMask.NameToLayer("Default");
        shadow.transform.GetChild(0).gameObject.layer = LayerMask.NameToLayer("Default");

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

    private void Update()
    {
        boxPos = gridBox.transform.position;

        GetInputs();

        if (gridVisible)
        {
            shadow.GetComponentInChildren<MeshRenderer>().enabled = true;
            moveShadow();
        }
        else shadow.GetComponentInChildren<MeshRenderer>().enabled = false;
    }

    /*	to move the shadow we just check if the mouse is inside the grid
        after that, checking with raycasthit the position where to locate the
        shadow
    */

    private void moveShadow()
    {
        RaycastHit hit;
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
            if ((hit.transform != null) && (hit.transform.gameObject.layer == LayerMask.NameToLayer("Grid")))
            {
                var aux = Vector3.zero;
                aux.x = (int) hit.point.x;
                aux.y = (int) hit.point.y;
                aux.z = (int) hit.point.z;
                shadow.transform.position = aux;
            }
    }

    /*
        This function checks 2 kinds of inputs:
        -Pressing A to toggle the grid/building mode
        -Left-Click to place the tower in the position
    */

    private void GetInputs()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 200.0f))
                if ((hit.transform != null) && (hit.transform.gameObject.layer == LayerMask.NameToLayer("Grid")))
                {
                    //Debug.Log (hit.transform.gameObject.layer);
                    var aux = Vector3.zero;
                    aux.x = (int) hit.point.x;
                    aux.y = (int) hit.point.y;
                    aux.z = (int) hit.point.z;

                    var valid = IsCellValid((int) aux.x, (int) aux.z);
                    Debug.Log(valid);

                    if (gridVisible && valid)
                    {
                        aux.y = 0;
                        var t = (GameObject) Instantiate(tower, aux, transform.rotation);
                        t.GetComponentInChildren<CapsuleCollider>().radius = 20;
                        t.GetComponentInChildren<CapsuleCollider>().height = 30;

                        t.GetComponentInChildren<Unit>()
                            .setSourceDeath(GameObject.Find("Death Audio Source").GetComponent<AudioSource>());
                        t.GetComponentInChildren<Weapon>()
                            .setSourceShoot(GameObject.Find("Shoot Audio Source").GetComponent<AudioSource>());

						GameObject.Find ("EdificiUB").GetComponent<Building> ().buyTower ();
                        toggleGrid();
                    }
                }
        }

		if (Input.GetKeyDown (KeyCode.A)) {
			if (GameObject.Find ("EdificiUB").GetComponent<Building> ().canBuild ())
				toggleGrid ();
			else
				Debug.Log ("Dont have enough money");
		}        
    }

    /*
     * this function toggle on/off every cell of the grid
     */

    public void toggleGrid()
    {
		GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<RTSCamera> ().changecanMove ();
		UpdateCells();

        gridVisible = !gridVisible;
        for (var i = 0; i < gridWidth; ++i)
            for (var j = 0; j < gridHeight; ++j)
                grid[i, j].cell.SetActive(gridVisible);
    }

    /*
        this function creates the gameObject that will represent the cell
        and places it in the grid layer
    */

    private GameObject CreateChild(int x, int z, Vector3 gridLeft)
    {
        var go = new GameObject();

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

	public void UpdateCells()
    {
        for (var z = 0; z < gridHeight; z++)
            for (var x = 0; x < gridWidth; x++)
            {
                var cell = grid[x, z].cell;
                var meshRenderer = cell.GetComponent<MeshRenderer>();
                var meshFilter = cell.GetComponent<MeshFilter>();

                //meshRenderer.material = IsCellValid(x, z) ? cellMaterialValid : cellMaterialInvalid;
                if (IsCellValid(x, z))
                {
                    meshRenderer.material = cellMaterialValid;
                    grid[x, z].valid = true;
                }
                else
                {
                    meshRenderer.material = cellMaterialInvalid;
                    grid[x, z].valid = false;
                }

                UpdateMesh(meshFilter.mesh, x, z);
            }
    }

    /*
        Makes a rayCast to check if there is a building in that cell position.
        Returns a boolean that will change or not the material of the cell.
    */

    private bool IsCellValid(int x, int z)
    {
        var cellPos = Vector3.zero;
        cellPos.x = boxPos.x - (gridWidth/2 - x);
        cellPos.y = 200;
        cellPos.z = boxPos.z - (gridHeight/2 - z);

        RaycastHit hitInfo;
        //Vector3 origin = new Vector3(x * cellSize + cellSize/2, 200, z * cellSize + cellSize/2);
        //Debug.Log (transform.TransformPoint(origin));
        Physics.Raycast(cellPos, Vector3.down, out hitInfo, Mathf.Infinity, LayerMask.GetMask("Buildings"));

        return hitInfo.collider == null;
    }

    /*
        The following functions are used to make the mesh of the cells
    */

    private Mesh CreateMesh()
    {
        var mesh = new Mesh();

        mesh.name = "Grid Cell";
        mesh.vertices = new[] {Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero};
        mesh.triangles = new[] {0, 1, 2, 2, 1, 3};
        mesh.normals = new[] {Vector3.up, Vector3.up, Vector3.up, Vector3.up};
        mesh.uv = new[] {new Vector2(1, 1), new Vector2(1, 0), new Vector2(0, 1), new Vector2(0, 0)};

        return mesh;
    }

    private void UpdateMesh(Mesh mesh, int x, int z)
    {
        mesh.vertices = new[]
        {
            MeshVertex(x, z),
            MeshVertex(x, z + 1),
            MeshVertex(x + 1, z),
            MeshVertex(x + 1, z + 1)
        };
    }

    private Vector3 MeshVertex(int x, int z)
    {
        return new Vector3(x*cellSize - gridWidth/2, yOffset, z*cellSize - gridHeight/2);
    }
}