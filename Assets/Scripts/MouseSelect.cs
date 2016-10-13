using UnityEngine;
using System.Collections;

public class MouseSelect : MonoBehaviour {

    static private Transform transformSelected;
    private bool isSelected;


    // Use this for initialization
    void Start()
    {
        transformSelected = null;
        isSelected = false;
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

        //print("selected + this.name");
    }

}
