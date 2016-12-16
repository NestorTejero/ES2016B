using UnityEngine;
using UnityEngine.UI;

public class MouseSelect : MonoBehaviour
{
    private static Transform transformSelected;
    private bool isSelected;
    ////private GameObject selectedObjectIndicator; 

    // Tooltip field
    public Text TooltipText;

    // Use this for initialization
    private void Start()
    {
        transformSelected = null;
        isSelected = false;

        TooltipText = GameObject.Find("FeedbackText").GetComponent<Text>();
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

        if (GameObject.Find("selectedObjectIndicator") != null)
            switch (tag)
            {
                case "Building":
                    GameObject.Find("selectedObjectIndicator")
                        .GetComponent<selectedObjectIndicator>()
                        .setSelectedObject(gameObject, gameObject.transform.localScale.y*2);
                    break;

                case "Unit":
                    GameObject.Find("selectedObjectIndicator")
                        .GetComponent<selectedObjectIndicator>()
                        .setSelectedObject(gameObject, gameObject.transform.localScale.y*8);
                    break;

                case "Tower":
                    GameObject.Find("selectedObjectIndicator")
                        .GetComponent<selectedObjectIndicator>()
                        .setSelectedObject(gameObject,
                            gameObject.transform.localScale.y*25 + gameObject.transform.position.y);
                    break;
            }

        APIHUD.instance.setGameObjectSelected(gameObject);
        gameObject.GetComponent<HUDSubject>().NotifyHUD();
    }

    //Here we can edit tooltips
    public void OnMouseOver()
    {
        if (tag == "Building")
            TooltipText.text = "You must protect this building!";

        if (tag == "Unit")
            TooltipText.text = "Kill them all!";
    }

    public void OnMouseExit()
    {
        TooltipText.text = "";
    }
}