using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MouseSelect : MonoBehaviour {

    static private Transform transformSelected;
    private bool isSelected;
	
	// Tooltip field
	public Text TooltipText;

    // Use this for initialization
    void Start()
    {
        transformSelected = null;
        isSelected = false;
		
		TooltipText = GameObject.Find("TooltipText").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {

        //for debug and testing use renderer.material.color = Color.red 
        //to check if object is not selected or Debug.Log
        if (isSelected && transform != transformSelected)
        {
            isSelected = false;
            //print("not selected + this.name");
        }
    }

    public void OnMouseDown()
    {
        isSelected = true;
        transformSelected = transform;
		
		if(this.tag == "Building"){
			APIHUD.instance.setHealth(this.GetComponent<Building>().baseHealth.ToString());
			APIHUD.instance.setAttackSpeed("-");
			APIHUD.instance.setDamage("-");
			APIHUD.instance.setRange("-");
		}

		if(this.tag == "Unit"){
			APIHUD.instance.setHealth(this.GetComponent<Unit>().baseHealth.ToString());
			APIHUD.instance.setAttackSpeed("-");
			APIHUD.instance.setDamage(this.GetComponent<Weapon>().baseDamage.ToString());
			APIHUD.instance.setRange(this.GetComponent<Weapon>().baseRange.ToString());
		}
    }
	
	//Here we can edit tooltips
	public void OnMouseOver(){
		
		if(this.tag == "Tower" || this.name == "TowerN" || this.name == "TowerE" || this.name == "TowerW" || this.name == "TowerS" || this.name == "EdificiUB" ){
			TooltipText.text = "Protect this building!";
		}
		
		if(this.tag == "Unit" || this.name == "Freshman_Roba_freshman"){
			TooltipText.text = "Kill them with fire!";
		}
		
		
	}
	
	public void OnMouseExit ()
    {
		TooltipText.text = "";

    }

}
