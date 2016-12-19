using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class APIHUD : MonoBehaviour
{
    public static APIHUD instance;
    private GameObject gameObjectSelected;
    private bool selectedItem;
	private bool visibleTooltipWave = false;
	private float initTimeTooltipWave = 0.0f;
	private int totalWaves = 0;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    // Update is called once per frame
    private void Update()
    {
        setTime(timer.instance.getTime());

		if (visibleTooltipWave == true) {
			showingTooltipWave ();
		}
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
            .FindChild("imgAttackSpeed")
            .FindChild("txtAttackSpeed")
            .GetComponent<Text>()
            .text = atackSpeed;
    }

    public void setDamage(string damage)
    {
        transform.FindChild("containerStats")
            .FindChild("container_info")
            .FindChild("imgDamage")
            .FindChild("txtDamage")
            .GetComponent<Text>()
            .text = damage;
    }

    public void setRange(string damage)
    {
        transform.FindChild("containerStats")
            .FindChild("container_info")
            .FindChild("imgRange")
            .FindChild("txtRange")
            .GetComponent<Text>()
            .text = damage;
    }

    public void setWave(string wave)
    {

		PersistentValues.waves = (int.Parse(wave)-1).ToString();

        transform.FindChild("containerGameStats")
            .FindChild("imgWave")
            .FindChild("txtWave")
            .GetComponent<Text>()
            .text = wave;

		/*totalWaves = transform.FindChild ("GameController(Clone)")
			.GetComponent<GameController> ().totalWaves;*/

		totalWaves = GameController.instance.getTotalWave();

		Debug.Log ("TOTAL WAVES: " + totalWaves);

		if (((int.Parse (wave) - 1) > 0) && ((int.Parse (wave) - 1) < totalWaves)) {
			setVisibleTooltipWave (true);
		}
    }

    public void setTime(string time)
    {
		PersistentValues.time = time;

        transform.FindChild("containerGameStats")
            .FindChild("imgTime")
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
		PersistentValues.points = points;

        transform.FindChild("containerGameStats")
            .FindChild("imgPoints")
            .FindChild("txtPoints")
            .GetComponent<Text>()
            .text = points;
    }

    public void setMoney(string money)
    {
        transform.FindChild("containerGameStats")
            .FindChild("imgMoney")
            .FindChild("txtMoney")
            .GetComponent<Text>()
            .text = money;
    }

    private void setOnClickFunctionUpgrade()
    {
        if (gameObjectSelected == null) return;

        transform.FindChild("buttons")
            .FindChild("container_buttons")
            .FindChild("btn_Upgrade")
            .GetComponent<Button>()
            .onClick.RemoveAllListeners();

        switch (gameObjectSelected.tag)
        {
            case "Tower":
                transform.FindChild("buttons")
                    .FindChild("container_buttons")
                    .FindChild("btn_Upgrade")
                    .GetComponent<Button>()
                    .onClick.AddListener(() =>
                    {
                        gameObjectSelected
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
                        gameObjectSelected
                            .GetComponent<Building>()
                            .Upgrade();
                    });
                break;
        }
    }

    private void setOnClickFunctionRepair()
    {
        if ((gameObjectSelected != null) && (gameObjectSelected.tag == "Building"))
            transform.FindChild("buttons")
                .FindChild("container_buttons")
                .FindChild("btn_Repair")
                .GetComponent<Button>()
                .onClick.AddListener(() =>
                {
                    gameObjectSelected
                        .GetComponent<Building>()
                        .Repair();
                });
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
        transform.FindChild("buttons").FindChild("container_buttons").FindChild("btn_Upgrade").gameObject.active =
            visible;
        setOnClickFunctionUpgrade();
    }

    public void setVisibleRepairButton(bool visible)
    {
        transform.FindChild("buttons").FindChild("container_buttons").FindChild("btn_Repair").gameObject.active =
            visible;
        setOnClickFunctionRepair();
    }

    public void setVisibleBuyTowerButton(bool visible)
    {
        transform.FindChild("buttons").FindChild("container_buttons").FindChild("btn_AddTower").gameObject.active =
            visible;
        setOnClickFunctionRepair();
    }

	public void setVisibleTooltipWave(bool visible)
	{
		transform.FindChild("containerTootips").FindChild("tooltipWave").gameObject.active =
			visible;

		visibleTooltipWave = true;
		initTimeTooltipWave = timer.instance.getTimeFloat ();
	}

	private void showingTooltipWave(){
		float temp = timer.instance.getTimeFloat ();
		float timeShowingTooltipWave = temp - initTimeTooltipWave;

		if (timeShowingTooltipWave > 5.0f) {
			visibleTooltipWave = false;
			setVisibleTooltipWave (false);
		}
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
        if ((gameObjectSelected == null) || (gameObjectSelected.GetComponent<HUDSubject>() != subj))
            return;

        setHealth(info.CurrentHealth, info.TotalHealth);
        setAttackSpeed(info.AttackSpeed);
        setDamage(info.Damage);
        setRange(info.Range);
        setVisibleUpgradeButton(info.VisibleUpgradeButton);
        setVisibleRepairButton(info.VisibleRepairButton);
    }
}