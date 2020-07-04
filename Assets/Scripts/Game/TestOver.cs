using System;
using UnityEngine;
using UnityEngine.UI;

public class TestOver : MonoBehaviour
{
    public bool isOver = false;

    public void WhenMouseOver()
    {
        Debug.Log("Mouse enter");
        isOver = true;
    }

    public void WhenMouseExit()
    {
        Debug.Log("Mouse exit");
        isOver = false;
    }
}