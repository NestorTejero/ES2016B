using UnityEngine;
using UnityEngine.UI;

public class MouseSelect : MonoBehaviour
{
    private static Transform transformSelected;
    private bool isSelected;

    // Tooltip field
    public Text TooltipText;

    // Use this for initialization
    private void Start()
    {
        transformSelected = null;
        isSelected = false;

        TooltipText = GameObject.Find("TooltipText").GetComponent<Text>();
    }

    // Update is called once per frame
    private void Update()
    {
        //for debug and testing use renderer.material.color = Color.red 
        //to check if object is not selected or Debug.Log
        if (isSelected && (transform != transformSelected))
            isSelected = false;
    }

    public void OnMouseDown()
    {
        isSelected = true;
        transformSelected = transform;

        APIHUD.instance.setGameObjectSelected(gameObject);

        if (tag == "Building")
        {
            APIHUD.instance.setHealth(GetComponent<Building>().getCurrentHealth(),
                GetComponent<Building>().getTotalHealth());
            APIHUD.instance.setAttackSpeed("-");
            APIHUD.instance.setDamage("-");
            APIHUD.instance.setRange("-");
            APIHUD.instance.setVisibleUpgradeButton(true);
        }

        if (tag == "Unit")
        {
            APIHUD.instance.setHealth(GetComponent<Unit>().getCurrentHealth(),
                GetComponent<Unit>().getTotalHealth());
            APIHUD.instance.setAttackSpeed(GetComponent<Unit>().moveSpeed.ToString());
            APIHUD.instance.setDamage(GetComponent<Weapon>().baseDamage.ToString());
            APIHUD.instance.setRange(GetComponent<Weapon>().baseRange.ToString());
            APIHUD.instance.setVisibleUpgradeButton(false);
        }
    }

    //Here we can edit tooltips
    public void OnMouseOver()
    {
        if ((tag == "Tower") || (name == "TowerN") || (name == "TowerE") || (name == "TowerW") ||
            (name == "TowerS") || (name == "EdificiUB"))
            TooltipText.text = "Protect this building!";

        if ((tag == "Unit") || (name == "Freshman_Roba_freshman"))
            TooltipText.text = "Kill them with fire!";
    }

    public void OnMouseExit()
    {
        TooltipText.text = "";
    }
}