using UnityEngine;
using System.Collections;

public class selectedObjectIndicator : MonoBehaviour {

	private GameObject gameObject;
	private float heightObject;

	// Use this for initialization
	void Start () {
		transform.position = new Vector3(0, -1000000, 0);
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate (new Vector3(0, Time.deltaTime*50, 0));

		if (gameObject != null) {
			//transform.position = new Vector3 (gameObject.GetComponent<Transform> ().transform.position.x, gameObject.GetComponent<Transform> ().transform.position.y + (gameObject.GetComponent<Transform> ().transform.localScale.y * 2), gameObject.GetComponent<Transform> ().transform.position.z);
			transform.position = new Vector3 (gameObject.GetComponent<Transform> ().transform.position.x, heightObject, gameObject.GetComponent<Transform> ().transform.position.z);
		}
	}

	public void setSelectedObject(GameObject gameObject, float heightObject){
		this.gameObject = gameObject;
		this.heightObject = heightObject;
	}
}
