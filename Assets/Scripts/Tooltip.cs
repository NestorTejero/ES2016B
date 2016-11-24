using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
    // Text field that will show the tooltip:
    public Text TooltipText;

    // Use this for initialization
    private void Start()
    {
        TooltipText = GameObject.Find("TooltipText").GetComponent<Text>();
    }

    private void OnMouseOver()
    {
        TooltipText.text = "Text we want to show";
    }

    private void OnMouseExit()
    {
        TooltipText.text = "";
    }
}