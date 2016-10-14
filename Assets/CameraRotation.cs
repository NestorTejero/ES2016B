using UnityEngine;
using System.Collections;

public class CameraRotation : MonoBehaviour {

	public float moveSpeedV = 10f;
	public float moveSpeedH = 30f;

	public Transform ubCenter;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		Vector3 position = transform.position;

		if (Input.GetKey (KeyCode.UpArrow)) {
			position = position + (Vector3.up * moveSpeedV * Time.deltaTime);
			if (position.y <= 40)
				transform.Translate (Vector3.up * moveSpeedV * Time.deltaTime);
		}
		if (Input.GetKey (KeyCode.DownArrow)) {
			position = position + (Vector3.up * moveSpeedV * Time.deltaTime);
			if (position.y >= 20)
				transform.Translate (-Vector3.up * moveSpeedV * Time.deltaTime);
		}
		if (Input.GetKey (KeyCode.LeftArrow))
			transform.Translate (Vector3.left*moveSpeedV*Time.deltaTime);
		if (Input.GetKey (KeyCode.RightArrow))
			transform.Translate (-Vector3.left*moveSpeedV*Time.deltaTime);

		transform.LookAt (ubCenter.transform.position);
	}
}
