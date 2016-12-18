using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinalInfo : MonoBehaviour {

	public Text points;
	public Text time;
	public Text waves;


	// Use this for initialization
	void Start () {
		points.text = PersistentValues.points.ToString();
		time.text = PersistentValues.time.ToString();
		waves.text = PersistentValues.waves.ToString();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
