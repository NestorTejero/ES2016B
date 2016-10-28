using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class loadingScreen : MonoBehaviour {
	public float time;
	public Text percent;
	public Image bar;

	void Start () {

		//loadingBar.fillAmount = 0;

	}


	void Update () {

		if (time == 100) {
			SceneManager.LoadScene(1); 
		}


		if (time < 100) {

			time += Time.deltaTime * 7;

		}

		if (time >= 100) {
			time = 100;
		}

		percent.text=""+(int)time+"%";
		bar.transform.localScale = new Vector3 (time/100, 1, 1);


	}


}
	