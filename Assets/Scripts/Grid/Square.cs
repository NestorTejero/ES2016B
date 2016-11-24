using UnityEngine;
using System.Collections;

public class Square
{
    //variables that each cell will have.
    public bool valid;
    public Vector3 worldPosition;
    public GameObject cell;

    //constructor
    public Square(bool _valid, Vector3 _worldPos, GameObject _cell)
    {
        valid = _valid;
        worldPosition = _worldPos;
        cell = _cell;
    }
}