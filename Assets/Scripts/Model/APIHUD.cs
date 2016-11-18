using UnityEngine;
using System.Collections;

public class APIHUD : MonoBehaviour {

	public static APIHUD instance;
	private bool selectedItem = false;

	void Awake()
	{
		if (instance == null) {
			instance = this;
			return;
		}
	}

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
		setTime(timer.instance.getTime ());
	}

	public void setHealth(string health){
		if (selectedItem == false) {
			setSelectedItemLabel ();	
		}
		transform.FindChild("containerStats").FindChild("container_info").FindChild("imgLifeBar").FindChild("txtLife").GetComponent<UnityEngine.UI.Text>().text = health;
	}

	public void setAttackSpeed(string atackSpeed){
		transform.FindChild("containerStats").FindChild("container_info").FindChild("lblAttackSpeed").FindChild("txtAttackSpeed").GetComponent<UnityEngine.UI.Text>().text = atackSpeed;
	}

	public void setDamage(string damage){
		transform.FindChild("containerStats").FindChild("container_info").FindChild("lblDamage").FindChild("txtDamage").GetComponent<UnityEngine.UI.Text>().text = damage;
	}

	public void setRange(string damage){
		transform.FindChild("containerStats").FindChild("container_info").FindChild("lblRange").FindChild("txtRange").GetComponent<UnityEngine.UI.Text>().text = damage;
	}

	public void setWave(string wave){
		transform.FindChild("containerGameStats").FindChild("lblWave").FindChild("txtWave").GetComponent<UnityEngine.UI.Text>().text = wave;
	}

	public void setTime(string time){
		transform.FindChild("containerGameStats").FindChild("lblTime").FindChild("txtTime").GetComponent<UnityEngine.UI.Text>().text = time;
	}

	public void setDificulty(string dificulty){
		transform.FindChild("containerGameStats").FindChild("lblDificulty").FindChild("txtDificulty").GetComponent<UnityEngine.UI.Text>().text = dificulty;
	}

	public void setPoints(string points){
		transform.FindChild("containerGameStats").FindChild("lblPoints").FindChild("txtPoints").GetComponent<UnityEngine.UI.Text>().text = points;
	}

	public void setVisibleUpgradeButton(bool visible){
		transform.FindChild ("buttons").FindChild ("container_buttons").gameObject.active = visible;
	}

	private void setSelectedItemLabel(){
		transform.FindChild ("containerStats").FindChild ("container_info").gameObject.active = true;
	//	transform.FindChild ("buttons").FindChild ("container_buttons").gameObject.active = true;
		//transform.FindChild ("containerStats").FindChild ("container_NoSelectItem").GetComponent<UnityEngine.UI.Text>().text = "";
		selectedItem = true;
	}
}
