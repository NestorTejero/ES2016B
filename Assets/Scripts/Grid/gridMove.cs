using UnityEngine;
using System.Collections;

public class gridMove : MonoBehaviour {

	GameObject grid;
	//used to initiate the grid container
	void Start () {
		grid = GameObject.Find ("Grid");
	}
	
	//once per frame it checks if the camera moves. If so, moves the container.
	void Update () {
		RaycastHit hit;
		Transform cam = Camera.main.transform;
		Ray ray = new Ray (cam.position,cam.forward);
		if (Physics.Raycast (ray, out hit)) {
			Vector3 newPosition = new Vector3((int)hit.point.x , 1,(int)hit.point.z);
			grid.transform.position = newPosition;
		}
	}
}
