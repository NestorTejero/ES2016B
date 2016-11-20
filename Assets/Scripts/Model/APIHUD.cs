using UnityEngine;
using System.Collections;

public class APIHUD : MonoBehaviour {

	public static APIHUD instance;
	private bool selectedItem = false;
	private GameObject gameObjectSelected;

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

	public void setHealth(float currentHealth, float totalHealth){
		if (selectedItem == false) {
			setSelectedItemLabel ();	
		}

		float maxWidthBarLife = transform.FindChild("containerStats").FindChild("container_info").FindChild("imgLifeBar").GetComponent<RectTransform>().rect.width;
		float maxHeigthBarLife = transform.FindChild("containerStats").FindChild("container_info").FindChild("imgLifeBar").GetComponent<RectTransform>().rect.height;
	
		float widthLifeBar = (currentHealth * maxWidthBarLife) / totalHealth;

		//Rect r = transform.FindChild ("containerStats").FindChild ("container_info").FindChild ("imgLifeBar").FindChild ("imgLife").GetComponent<RectTransform> ().rect;
		//r.width = widthLifeBar;

		//transform.FindChild("containerStats").FindChild("container_info").FindChild("imgLifeBar").FindChild("imgLife").GetComponent<RectTransform>().rect = r;

		transform.FindChild("containerStats").FindChild("container_info").FindChild("imgLifeBar").FindChild("imgLife").GetComponent<RectTransform>().sizeDelta = new Vector2(widthLifeBar, maxHeigthBarLife);

		transform.FindChild("containerStats").FindChild("container_info").FindChild("imgLifeBar").FindChild("txtLife").GetComponent<UnityEngine.UI.Text>().text = currentHealth.ToString();
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

	public void setMoney(string money){
		transform.FindChild("containerGameStats").FindChild("lblMoney").FindChild("txtMoney").GetComponent<UnityEngine.UI.Text>().text = money;
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

	public void setGameObjectSelected(GameObject gameObject){
		this.gameObjectSelected = gameObject;
	}

	public GameObject getGameObjectSelected(){
		return this.gameObjectSelected;
	}
}
