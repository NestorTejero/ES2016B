using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform Tarjet; // This will be the main camera (and minimap) position


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
        // Position:
        transform.position = new Vector3(Tarjet.position.x, transform.position.y, Tarjet.position.z);
        // we set the x and z of our minimap camera equals to x and z of main manera

        // Orientation:
        var eulerAngles = Tarjet.eulerAngles; // We copy the main camera horientarion
        eulerAngles.x = 90; // We set the X orientation of out copy as 90 (perpendicular to terrain)

        transform.eulerAngles = eulerAngles; // we set the new orientation to our minimap camera
    }
}