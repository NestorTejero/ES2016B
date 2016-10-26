using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class loadingScreen : MonoBehaviour {
	public float sayi;
	public Text sayiyazi,ipucuyari;
	public Image bar;

	void Start () {

		//loadingBar.fillAmount = 0;

	}


	void Update () {

		if (sayi == 100) {
			SceneManager.LoadScene(1); 
		}


		if (sayi < 100) {

			sayi += Time.deltaTime * 7;

		}

		if (sayi >= 100) {
			sayi = 100;
		}

		sayiyazi.text=""+(int)sayi+"%";
		bar.transform.localScale = new Vector3 (sayi/100, 1, 1);


	}


}
	