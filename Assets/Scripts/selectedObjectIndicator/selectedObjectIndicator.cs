using UnityEngine;

public class selectedObjectIndicator : MonoBehaviour
{
    private GameObject gameObject;
    private float heightObject;

    // Use this for initialization
    private void Start()
    {
        transform.position = new Vector3(0, -1000000, 0);
    }

    // Update is called once per frame
    private void Update()
    {
        transform.Rotate(new Vector3(0, Time.deltaTime*50, 0));

        if (gameObject != null)
            transform.position = new Vector3(gameObject.GetComponent<Transform>().transform.position.x, heightObject,
                gameObject.GetComponent<Transform>().transform.position.z);
    }

    public void setSelectedObject(GameObject gameObject, float heightObject)
    {
        this.gameObject = gameObject;
        this.heightObject = heightObject;
    }

    public GameObject getSelectedObject()
    {
        return gameObject;
    }
}