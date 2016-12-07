using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class APIHUD : MonoBehaviour
{
    public static APIHUD instance;
    private GameObject gameObjectSelected;
    private bool selectedItem;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    // Update is called once per frame
    private void Update()
    {
        setTime(timer.instance.getTime());
    }

    public void setHealth(float currentHealth, float totalHealth)
    {
        if (selectedItem == false)
            setSelectedItemLabel();

        var maxWidthBarLife =
            transform.FindChild("containerStats")
                .FindChild("container_info")
                .FindChild("imgLifeBar")
                .GetComponent<RectTransform>()
                .rect.width;
        var maxHeigthBarLife =
            transform.FindChild("containerStats")
                .FindChild("container_info")
                .FindChild("imgLifeBar")
                .GetComponent<RectTransform>()
                .rect.height;

        var widthLifeBar = currentHealth*maxWidthBarLife/totalHealth;

        //Rect r = transform.FindChild ("containerStats").FindChild ("container_info").FindChild ("imgLifeBar").FindChild ("imgLife").GetComponent<RectTransform> ().rect;
        //r.width = widthLifeBar;

        //transform.FindChild("containerStats").FindChild("container_info").FindChild("imgLifeBar").FindChild("imgLife").GetComponent<RectTransform>().rect = r;

        transform.FindChild("containerStats")
            .FindChild("container_info")
            .FindChild("imgLifeBar")
            .FindChild("imgLife")
            .GetComponent<RectTransform>()
            .sizeDelta = new Vector2(widthLifeBar, maxHeigthBarLife);

        transform.FindChild("containerStats")
            .FindChild("container_info")
            .FindChild("imgLifeBar")
            .FindChild("txtLife")
            .GetComponent<Text>()
            .text = currentHealth.ToString();
    }

    public void setAttackSpeed(string atackSpeed)
    {
        transform.FindChild("containerStats")
            .FindChild("container_info")
            .FindChild("lblAttackSpeed")
            .FindChild("txtAttackSpeed")
            .GetComponent<Text>()
            .text = atackSpeed;
    }

    public void setDamage(string damage)
    {
        transform.FindChild("containerStats")
            .FindChild("container_info")
            .FindChild("lblDamage")
            .FindChild("txtDamage")
            .GetComponent<Text>()
            .text = damage;
    }

    public void setRange(string damage)
    {
        transform.FindChild("containerStats")
            .FindChild("container_info")
            .FindChild("lblRange")
            .FindChild("txtRange")
            .GetComponent<Text>()
            .text = damage;
    }

    public void setWave(string wave)
    {
        transform.FindChild("containerGameStats")
            .FindChild("lblWave")
            .FindChild("txtWave")
            .GetComponent<Text>()
            .text = wave;
    }

    public void setTime(string time)
    {
        transform.FindChild("containerGameStats")
            .FindChild("lblTime")
            .FindChild("txtTime")
            .GetComponent<Text>()
            .text = time;
    }

    public void setDifficulty(string dificulty)
    {
        transform.FindChild("containerGameStats")
            .FindChild("lblDificulty")
            .FindChild("txtDificulty")
            .GetComponent<Text>()
            .text = dificulty;
    }

    public void setPoints(string points)
    {
        transform.FindChild("containerGameStats")
            .FindChild("lblPoints")
            .FindChild("txtPoints")
            .GetComponent<Text>()
            .text = points;
    }

    public void setMoney(string money)
    {
        transform.FindChild("containerGameStats")
            .FindChild("lblMoney")
            .FindChild("txtMoney")
            .GetComponent<Text>()
            .text = money;
    }
		
	private void setOnClickFunctionUpgrade()
	{

	    if (this.gameObjectSelected == null) return;

		transform.FindChild ("buttons")
			.FindChild ("container_buttons")
			.FindChild ("btn_Upgrade")
			.GetComponent<Button> ()
			.onClick.RemoveAllListeners ();

	    switch (this.gameObjectSelected.tag)
	    {
	        case "Tower":
	            transform.FindChild("buttons")
	                .FindChild("container_buttons")
	                .FindChild("btn_Upgrade")
	                .GetComponent<Button>()
	                .onClick.AddListener(() =>
	                {
						this.gameObjectSelected
	                        .GetComponent<Tower>()
	                        .Upgrade();
	                });
	            break;
		case "Building":
	            transform.FindChild("buttons")
	                .FindChild("container_buttons")
	                .FindChild("btn_Upgrade")
	                .GetComponent<Button>()
	                .onClick.AddListener(() =>
	                {
						this.gameObjectSelected
	                        .GetComponent<Building>()
	                        .Upgrade();
	                });
	            break;
	    }
	}

	private void setOnClickFunctionRepair(){
		if (gameObjectSelected != null && gameObjectSelected.tag == "Building") {
			transform.FindChild ("buttons").FindChild ("container_buttons").FindChild ("btn_Repair").GetComponent<Button> ().onClick.AddListener (() => {
				gameObjectSelected
					.GetComponent<Building>()
					.Repair();
			});
		}
	}
    
	public void setGameObjectSelected(GameObject gameObject)
	{
		gameObjectSelected = gameObject;
	}

	public GameObject getGameObjectSelected()
	{
		return gameObjectSelected;
	}

    public void setVisibleUpgradeButton(bool visible)
    {
		transform.FindChild("buttons").FindChild("container_buttons").FindChild("btn_Upgrade").gameObject.active = visible;
		setOnClickFunctionUpgrade ();
	}

	public void setVisibleRepairButton(bool visible)
	{
		transform.FindChild("buttons").FindChild("container_buttons").FindChild("btn_Repair").gameObject.active = visible;
		setOnClickFunctionRepair ();
	}

    private void setSelectedItemLabel()
    {
        transform.FindChild("containerStats").FindChild("container_info").gameObject.active = true;
        //	transform.FindChild ("buttons").FindChild ("container_buttons").gameObject.active = true;
        //transform.FindChild ("containerStats").FindChild ("container_NoSelectItem").GetComponent<UnityEngine.UI.Text>().text = "";
        selectedItem = true;
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public void notifyChange(HUDSubject subj, HUDInfo info)
    {
        if (gameObjectSelected == null || gameObjectSelected.GetComponent<HUDSubject>() != subj)
            return;

        setHealth(info.CurrentHealth, info.TotalHealth);
        setAttackSpeed(info.AttackSpeed);
        setDamage(info.Damage);
        setRange(info.Range);
        setVisibleUpgradeButton(info.VisibleUpgradeButton);
		setVisibleRepairButton(info.VisibleRepairButton);
    }
}