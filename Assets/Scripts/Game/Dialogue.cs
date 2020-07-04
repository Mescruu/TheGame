using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class Dialogue{

    // Use this for initialization
    public string name;
    public Sprite Portrait;
    [TextArea(1,4)]
    public string[] sentences;
	
	
}
