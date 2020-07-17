using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using System; //Serializable.

public class KeyMenager : MonoBehaviour {

    public Dictionary<string, KeyCode> keys = new Dictionary<string, KeyCode>();

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Start ()
    {//początkowe wartości
        keys.Add("Left", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Left", "A")));
        keys.Add("Right", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Right", "D")));
        keys.Add("Jump", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Jump", "W")));
        keys.Add("Sprint", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Sprint", "LeftShift")));
        keys.Add("BackDash", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("BackDash", "Z")));


        keys.Add("Attack", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Attack", "K")));
        keys.Add("SpecialAttack", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("SpecialAttack", "S")));
        keys.Add("Throwing", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Throwing", "F")));
        keys.Add("ReleaseMagic", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("ReleaseMagic", "Tab")));
        keys.Add("PowerMagic", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("PowerMagic", "Q")));


        keys.Add("Pause", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Pause", "Escape")));
        keys.Add("Equipment", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Equipment", "R")));
        keys.Add("Experience", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Experience", "U")));
        keys.Add("Action", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Action", "E")));

        keys.Add("Fire", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Fire", "Alpha1")));
        keys.Add("Electricity", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Electricity", "Alpha2")));
        keys.Add("Darkness", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Darkness", "Alpha3")));
        keys.Add("Ankh", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Ankh", "Alpha4")));
    }
	
	public void Refresh ()
    {//odswieżenie wartości przypisanych przycisków
        keys.Clear();

        keys.Add("Left", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Left", "A")));
        keys.Add("Right", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Right", "D")));
        keys.Add("Jump", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Jump", "W")));
        keys.Add("Sprint", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Sprint", "LeftShift")));
        keys.Add("BackDash", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("BackDash", "Z")));


        keys.Add("Attack", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Attack", "K")));
        keys.Add("SpecialAttack", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("SpecialAttack", "S")));
        keys.Add("Throwing", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Throwing", "F")));
        keys.Add("ReleaseMagic", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("ReleaseMagic", "Tab")));
        keys.Add("PowerMagic", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("PowerMagic", "Q")));


        keys.Add("Pause", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Pause", "Escape")));
        keys.Add("Equipment", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Equipment", "R")));
        keys.Add("Experience", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Experience", "U")));
        keys.Add("Action", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Action", "E")));

        keys.Add("Fire", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Fire", "Alpha1")));
        keys.Add("Electricity", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Electricity", "Alpha2")));
        keys.Add("Darkness", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Darkness", "Alpha3")));
        keys.Add("Ankh", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Ankh", "Alpha4")));

    }
}
