using UnityEngine;

public class gridMove : MonoBehaviour
{
    private GameObject grid;
    //used to initiate the grid container
    private void Start()
    {
        grid = GameObject.Find("Grid");
    }

    //once per frame it checks if the camera moves. If so, moves the container.
    private void Update()
    {
        RaycastHit hit;
        var cam = Camera.main.transform;
        var ray = new Ray(cam.position, cam.forward);
        if (Physics.Raycast(ray, out hit))
        {
            var newPosition = new Vector3((int) hit.point.x, 1, (int) hit.point.z);
            grid.transform.position = newPosition;
        }
    }
}