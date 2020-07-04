using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using System; //Serializable.
using System.Runtime.Serialization.Formatters.Binary;//Serializes i deserializes obiektu lub cały wykres z połączonych obiektów w formacie binarnym.
using System.IO;//Do operacji na plikach (czytanie, pisanie do pliku).


public class BindKey : MonoBehaviour {

    public Dictionary<string, KeyCode> keys = new Dictionary<string, KeyCode>();
    public Text left, right, jump,sprint,backdash;
    public Text attack, specialAttack, throwing, releaseMagic, powerMagic;
    public Text pause, equipment, experience, action;
    public Text fire, electricity, darkness, ankh;

    private GameObject currentKey;

    public GameObject theSameUI;
    public Color32 normal;// = new Color32(39, 171, 249, 255);
    public Color32 selected;// = new Color32(239, 116, 39, 255);

    private Settings settings;
    private KeyMenager keyMenager;

    public Text warning;

    // Use this for initialization
    void Start()
    {

        settings = gameObject.GetComponent<Settings>();
        keyMenager = GameObject.Find("KeyMenager").GetComponent<KeyMenager>();


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


        left.text = keys["Left"].ToString();
        right.text = keys["Right"].ToString();
        jump.text = keys["Jump"].ToString();
        sprint.text = keys["Sprint"].ToString();
        backdash.text = keys["BackDash"].ToString();

        attack.text = keys["Attack"].ToString();
        specialAttack.text = keys["SpecialAttack"].ToString();
        throwing.text = keys["Throwing"].ToString();
        releaseMagic.text = keys["ReleaseMagic"].ToString();
        powerMagic.text = keys["PowerMagic"].ToString();

        pause.text = keys["Pause"].ToString();
        equipment.text = keys["Equipment"].ToString();
        experience.text = keys["Experience"].ToString();
        action.text = keys["Action"].ToString();

        fire.text = keys["Fire"].ToString();
        electricity.text = keys["Electricity"].ToString();
        darkness.text = keys["Darkness"].ToString();
        ankh.text = keys["Ankh"].ToString();

        theSameUI.SetActive(false);



    }
    public void Refresh()
    {
        keys.Clear();
        PlayerPrefs.DeleteKey("Left");
        PlayerPrefs.DeleteKey("Right");
        PlayerPrefs.DeleteKey("Jump");
        PlayerPrefs.DeleteKey("Sprint");
        PlayerPrefs.DeleteKey("BackDash");

        PlayerPrefs.DeleteKey("Attack");
        PlayerPrefs.DeleteKey("SpecialAttack");
        PlayerPrefs.DeleteKey("Throwing");
        PlayerPrefs.DeleteKey("ReleaseMagic");
        PlayerPrefs.DeleteKey("PowerMagic");

        PlayerPrefs.DeleteKey("Pause");
        PlayerPrefs.DeleteKey("Equipment");
        PlayerPrefs.DeleteKey("Experience");
        PlayerPrefs.DeleteKey("Action");

        PlayerPrefs.DeleteKey("Fire");
        PlayerPrefs.DeleteKey("Electricity");
        PlayerPrefs.DeleteKey("Darkness");
        PlayerPrefs.DeleteKey("Ankh");

       



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

        left.text = keys["Left"].ToString();
        right.text = keys["Right"].ToString();
        jump.text = keys["Jump"].ToString();
        sprint.text = keys["Sprint"].ToString();
        backdash.text = keys["BackDash"].ToString();

        attack.text = keys["Attack"].ToString();
        specialAttack.text = keys["SpecialAttack"].ToString();
        throwing.text = keys["Throwing"].ToString();
        releaseMagic.text = keys["ReleaseMagic"].ToString();
        powerMagic.text = keys["PowerMagic"].ToString();

        pause.text = keys["Pause"].ToString();
        equipment.text = keys["Equipment"].ToString();
        experience.text = keys["Experience"].ToString();
        action.text = keys["Action"].ToString();

        fire.text = keys["Fire"].ToString();
        electricity.text = keys["Electricity"].ToString();
        darkness.text = keys["Darkness"].ToString();
        ankh.text = keys["Ankh"].ToString();

        keyMenager.Refresh();
    }

    // Update is called once per frame
    void Update()
    {

        /* Przyklad uzywania
        if (Input.GetKeyDown(keys["Left"]))
        {
            Debug.Log("Goo leffft");
        }
        */

    }
    public void OK()
    {
        theSameUI.SetActive(false);

    }
  
    void OnGUI()
    {


        if (currentKey!=null)
        {
            Event e = Event.current;
            if(e.isKey)
            {

                checkRepeat(e);

                currentKey.transform.GetChild(0).GetComponent<Text>().text = e.keyCode.ToString();
                currentKey.GetComponent<Image>().color = normal;
                
                    keys[currentKey.name] = e.keyCode;
               
               
                currentKey = null;

                Save();
            }
        }
    }
    public void ChangeKey(GameObject clicked)
    {
        if(currentKey!=null)
        {
            currentKey.GetComponent<Image>().color = normal;
        }
        currentKey = clicked;
        currentKey.GetComponent<Image>().color = selected; 
    }

    public void checkRepeat(Event e)
    {
        Debug.Log("check");

        foreach (var key in keys)
        {
            if (e.keyCode == key.Value && currentKey.name != key.Key)
            {
                Debug.Log(keys[currentKey.name]);
                Debug.Log(key.Value);
                Debug.Log("The same");
                theSameUI.SetActive(true);
                warning.text = "Key: '" + currentKey.name + "' and key: '" + key.Key + "'\n are  the  same.. \n It may be inconvenient";
                Debug.Log("SAVE");
            }
        }
        PlayerPrefs.Save();
    }
    public void Save()
    {
       foreach(var key in keys)
        {
            PlayerPrefs.SetString(key.Key, key.Value.ToString());
            Debug.Log("SAVE");
        }
        PlayerPrefs.Save();
    }
   
    /**
    * Metoda odczytuje stan gry z pliku.
*/
  

}

