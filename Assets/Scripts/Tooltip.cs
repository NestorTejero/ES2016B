using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
    // Text field that will show the tooltip:
    public Text TooltipText;

    // Use this for initialization
    void Start()
    {
        TooltipText = GameObject.Find("TooltipText").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnMouseOver()
    {
        TooltipText.text = "Text we want to show";
    }

    void OnMouseExit()
    {
        TooltipText.text = "";
    }
}