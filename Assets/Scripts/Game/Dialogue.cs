using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class Dialogue{ //klasa przechowywująca schemat danych

    public string name;
    public Sprite Portrait;
    [TextArea(1,4)]
    public string[] sentences;
}
