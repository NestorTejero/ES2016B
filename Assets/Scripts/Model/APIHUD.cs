using UnityEngine;
using UnityEngine.UI;

public class APIHUD : MonoBehaviour
{
    public static APIHUD instance;
    private GameObject gameObjectSelected;
    private bool selectedItem;
	public GameObject lifeBar;
	public GameObject moneyIndicator;
	public GameObject timeIndicator;
	public GameObject scoreIndicator;
	public GameObject waveIndicator;
	public GameObject attackIndicator;
	public GameObject defenseIndicator;
	public GameObject speedIndicator;
	public GameObject rangeIndicator;
	public GameObject actionUpLeft;
	public GameObject actionUpCenter;
	public GameObject actionUpRight;
	public GameObject actionDownLeft;
	public GameObject actionDownCenter;
	public GameObject actionDownRight;

    private void Awake()
    {
        if (instance == null)
            instance = this;
		setUnitInfoVisibility (false);
    }

    // Update is called once per frame
    private void Update()
    {
        setTime(timer.instance.getTime());
    }

	private void setTextIndicator(GameObject indicator, string text)
	{
		if (text != null) {
			indicator.transform.FindChild ("Indicator")
				.GetComponent<Text> ()
				.text = text;
		} else {
			indicator.SetActive (false);
		}

	}

    public void setHealth(float currentHealth, float totalHealth)
    {
		var lifePercent = currentHealth/(float) totalHealth;

		var fillerGeometry = lifeBar.transform.FindChild("Filler").GetComponent<RectTransform>();
		fillerGeometry.anchorMax = new Vector2 (lifePercent, 1.0f);

		setTextIndicator(lifeBar, ((int)(lifePercent * 100)) + "%");
		lifeBar.transform.FindChild ("Indicator")
			.GetComponent<Text>()
			.color = lifePercent > 0.5 ? Color.white : Color.black;
    }

	public void setAttackSpeed(string text)
    {
		setTextIndicator (speedIndicator, text);
    }

	public void setDamage(string text)
    {
		setTextIndicator (attackIndicator, text);
    }

    public void setRange(string text)
    {
		setTextIndicator (rangeIndicator, text);
    }

    public void setWave(string text)
    {
		setTextIndicator (waveIndicator, text);
    }

    public void setTime(string text)
    {
		setTextIndicator (timeIndicator, text);
    }

    public void setDifficulty(string dificulty)
    {
        
    }

    public void setPoints(string text)
    {
		setTextIndicator (scoreIndicator, text);
    }

    public void setMoney(string text)
    {
		setTextIndicator (moneyIndicator, text);
    }

    public void setVisibleUpgradeButton(bool visible)
    {
       // transform.FindChild("buttons").FindChild("container_buttons").gameObject.active = visible;
    }

	private void setUnitInfoVisibility(bool visibility)
	{
		lifeBar.SetActive (visibility);
		attackIndicator.SetActive (visibility);
		defenseIndicator.SetActive (visibility);
		speedIndicator.SetActive (visibility);
		rangeIndicator.SetActive (visibility);
	}

	private void setActionFunctionality(GameObject button, string spritePath, UnityEngine.Events.UnityAction callback)
	{
		button.transform.FindChild("Background")
			.GetComponent<Image>()
			.sprite = Resources.Load<Sprite>("Sprites/" + spritePath);
		if (callback != null) {
			button.transform.GetComponent<Button>()
				.onClick.AddListener(callback);
		} else {
			button.transform.GetComponent<Button> ()
				.onClick.RemoveAllListeners ();
		}

	}

	private void clearAllActions()
	{
		setActionFunctionality (actionUpLeft, "hud_button_disabled", null);
		setActionFunctionality (actionUpCenter, "hud_button_disabled", null);
		setActionFunctionality (actionUpRight, "hud_button_disabled", null);
		setActionFunctionality (actionDownLeft, "hud_button_disabled", null);
		setActionFunctionality (actionDownCenter, "hud_button_disabled", null);
		setActionFunctionality (actionDownRight, "hud_button_disabled", null);
	}

    public void setGameObjectSelected(GameObject gameObject)
    {
        gameObjectSelected = gameObject;
		setUnitInfoVisibility (gameObject != null);
		clearAllActions ();
		if (gameObject.tag == "Building") {
			setActionFunctionality (actionUpLeft, "hud_button_addtower", delegate {
				print("You pressed the button");
			});
		}
    }

    public GameObject getGameObjectSelected()
    {
        return gameObjectSelected;
    }
}