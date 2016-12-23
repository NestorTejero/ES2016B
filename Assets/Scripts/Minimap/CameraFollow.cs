using UnityEngine;
using System;

public class CameraFollow : MonoBehaviour
{
    public Transform Tarjet; // This will be the main camera (and minimap) position
	public LayerMask mask;


    // Use this for initialization
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }

    // This function is called after all updates have been done
    private void LateUpdate()
    {
        setMinimapPosition();
		setMinimapOrientation();
		
    }
	
	private void setMinimapPosition(){
		// Old Position (we dont delete this to understand what we did later):
        //transform.position = new Vector3(Tarjet.position.x, transform.position.y, Tarjet.position.z);
        // we set the x and z of our minimap camera equals to x and z of main manera
		
		
		// New Position:
		RaycastHit hit;
        var ray = new Ray(Tarjet.position, Tarjet.forward);

        if (Physics.Raycast(ray, out hit,150, mask))
        {
            transform.position = new Vector3((int) hit.point.x, transform.position.y, (int) hit.point.z);
        }
		
		// Para conseguir una posición del minimapa más natural, en lugar de coger directamente la X y Z de cámara, hacemos un raycast,
		// y cogemos la interesección con el terreno de juego.
		// la variable mask nos sirve para que el raycast solo tenga en cuenta el terreno.
		
		
		/* Raycast alternativo
		RaycastHit[] hit;
		hit = Physics.RaycastAll(Tarjet.position, Tarjet.forward, 200);
		RaycastHit last = hit[hit.Length -1];
		transform.position = new Vector3((int) (last.point.x), transform.position.y, (int) (last.point.z));
		*/
	}
	
	private void setMinimapOrientation(){
		// Orientation:
        var eulerAngles = Tarjet.eulerAngles; // We copy the main camera horientarion
        eulerAngles.x = 90; // We set the X orientation of out copy as 90 (perpendicular to terrain)

        transform.eulerAngles = eulerAngles; // we set the new orientation to our minimap camera
	}
}