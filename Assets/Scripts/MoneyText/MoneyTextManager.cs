using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MoneyTextManager : MonoBehaviour {

	private static MoneyTextManager instance;

	public GameObject textPrefab;
	public RectTransform canvasTransform;

	public Vector3 direction;
	public float speed;
	public float fadeTime;

	public static MoneyTextManager Instance{
		get{
			if (instance == null) {
				instance = GameObject.FindObjectOfType<MoneyTextManager> ();
			}
			return instance;
		}
	}

	public void CreateText(Vector3 position, string text, bool incoming){
		Vector3 finalPosition = new Vector3 (position.x, position.y + 20, position.z);

		GameObject mst = (GameObject)Instantiate(textPrefab, finalPosition, Quaternion.identity);

		mst.transform.SetParent (canvasTransform);
		mst.GetComponent<RectTransform> ().localScale = new Vector3 (2,2,2);
		mst.GetComponent<MoneyText> ().Initialize (speed, direction, fadeTime);

		if (incoming) {
			mst.GetComponent<Text> ().text = "+" + text;
			mst.GetComponent<Text> ().color = Color.green;
		} else {
			mst.GetComponent<Text> ().text = "-" + text;
			mst.GetComponent<Text> ().color = Color.red;
		}
	}

}
