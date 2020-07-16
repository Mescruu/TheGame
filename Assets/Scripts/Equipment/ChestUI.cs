using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ChestUI : MonoBehaviour {
    
    //Pozycje slotów
    public Vector3[] EqPosition;
    public Vector3[] ChPosition;

    public GameObject[] Slots; //Sloty

    public bool ChestOpen;//czy skrzynia jest otworzona
    public GameObject ChestObj;

    //Teksty
    public Text GoldTxt;
    public Text RunesTxt;
    public Text HpTxt;
    public Text MpTxt;

    public Text attackTxt;
    public Text armorTxt;
    public Text magicTxt;
    public Text agilityTxt;

    //Odwołania
    public Game_Master gm;
    private KeyMenager keyMenager;
    private CanvasEkwipunek eq;
    public GameObject DropPlaceChest;
    public GameObject background;
    // Use this for initialization
    void Start () {
        gm = GameObject.Find("gameMaster").GetComponent<Game_Master>();
        keyMenager = GameObject.Find("KeyMenager").GetComponent<KeyMenager>();
        eq = gameObject.GetComponent<CanvasEkwipunek>();
        CloseChest();
    }

    void Update ()
    {
        if (Input.GetKeyDown(keyMenager.keys["Equipment"])||Input.GetKeyDown(keyMenager.keys["Pause"]))
        { //Jeżeli naciśnięto klawisz "I"
            if (ChestObj.active == true)
            {
                gm.ChangeToZero = true;
                ChestObj.SetActive(false);//Ukrycie/pokazanie ekwipunku.	
                DropPlaceChest.SetActive(false);
                CloseChest();
            }
        }

        //wyswietlenie statystyk i posiadanych rzeczy wielokrotnego użytku
        attackTxt.text = gm.PlayerAttack.ToString();
        armorTxt.text = gm.PlayerArmor.ToString();
        magicTxt.text = gm.PlayerMagic.ToString();
        agilityTxt.text = gm.PlayerSpeed.ToString();

        GoldTxt.text = gm.gold.ToString();
        RunesTxt.text = gm.runes.ToString();
        HpTxt.text = gm.hp_potion.ToString();
        MpTxt.text = gm.mana_potion.ToString();


    }
    public void OpenChest() //Przerzucenie pozycji slotów
    {
        if (ChestObj.active == false)
        {
            ChestObj.SetActive(true); //Ukrycie/pokazanie ekwipunku.		
            eq.OpenEq();
            DropPlaceChest.SetActive(true);
            ChestOpen = true;
            Slots[0].transform.localPosition = ChPosition[0];
            Slots[1].transform.localPosition = ChPosition[1];
            Slots[2].transform.localPosition = ChPosition[2];
            Slots[3].transform.localPosition = ChPosition[3];
            Slots[4].transform.localPosition = ChPosition[4];
            Slots[5].transform.localPosition = ChPosition[5];
            gm.active_menu = 3;
            background.SetActive(true);
            gm.Chest_Open = true;
        }
    }

    public void CloseChest() //Przerzucenie pozycji slotów
    {
        Slots[0].transform.localPosition = EqPosition[0];
        Slots[1].transform.localPosition = EqPosition[1];
        Slots[2].transform.localPosition = EqPosition[2];
        Slots[3].transform.localPosition = EqPosition[3];
        Slots[4].transform.localPosition = EqPosition[4];
        Slots[5].transform.localPosition = EqPosition[5];
        ChestObj.SetActive(false);//Ukrycie/pokazanie ekwipunku.
        DropPlaceChest.SetActive(false);
        background.SetActive(false);
        gm.SetChestBool(false);
    }
}
