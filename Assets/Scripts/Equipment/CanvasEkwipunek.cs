using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CanvasEkwipunek : MonoBehaviour {

	//Obiekt (płutno) ekwipunku
	public GameObject ekwipunek;

    //Poszczegolne kategorie (która z nich jest włączona)
    public bool Amor_Cat;
    public bool Rest_Cat;
    public bool Weapon_Cat;

    //Poszczegolne kategorie (Obiekt)
    public GameObject Amor_CatUI;
    public GameObject Rest_CatUI;
    public GameObject Weapon_CatUI;

    public GameObject[] Active; //kategorie

    public int NoteNumber;
    public GameObject[] noteObj;  //teksty notatek
    public GameObject[] noteButtons;  //teksty notatek

    private bool Change;

    public GameObject NotesUI; //UI notesu
    public GameObject StatsUI; //UI statystyk
    public GameObject StatsUIChest; //UI skrzyni
    public int[] status;

    //Teksty
    public Text attackTxt;
    public Text armorTxt;
    public Text magicTxt;
    public Text agilityTxt;
    public Text StatsText;

    public Text GoldTxt;
    public Text RunesTxt;
    public Text HpTxt;
    public Text MpTxt;

    //Przyciski
    public GameObject ButtonHp;
    public GameObject ButtonMana;
    public GameObject ButtonHpHide;
    public GameObject ButtonManaHide;

   //Komponenty
    private AudioSource audioSource;

    //Transferowanie obiektów
    private ElementEq item;
    public GameObject TransferGui;
    public Text StayAmount;
    public Text TransferAmount;
    public Slider slider;
    private bool waitForDecision;
    private bool accept;
    private GameObject transfer_drop_place;
    private int amountToTransfer;

    //Obiekty potrzebne temu skryptowi
    private Game_Master gm;
    private KeyMenager keyMenager;

    void Start ()
    {
        waitForDecision = false;
        StatsText.text = "S  t a  t s";
        ekwipunek.SetActive(false); //Domyślnie ekwipunek wyłączony.
        Armor();
        NoteNumber = 0;
        Change = true;

        NotesUI.SetActive(false);
        StatsUI.SetActive(false);
        accept = false;

        RefreshNotes();

        keyMenager = GameObject.Find("KeyMenager").GetComponent<KeyMenager>();
        gm = GameObject.Find("gameMaster").GetComponent<Game_Master>();
        audioSource = gameObject.GetComponent<AudioSource>();
    }
   
    void Update ()
    {

        if (waitForDecision) //Tranferiwanie obiektów
        {
            StayAmount.text = ((int)slider.maxValue - (int)slider.value).ToString();
            TransferAmount.text = ((int)slider.value).ToString();
            if (accept)
            {
                transfer_drop_place.GetComponent<DropPlace>().TransferKnives(item, amountToTransfer);
                amountToTransfer = 0;
                item = null;
                waitForDecision = false;
            }
        }
        else
        {
            accept = false;
        }
        if (Input.GetKeyDown(keyMenager.keys["Equipment"])&&gm.active_menu==0)
        { //Jeżeli naciśnięto klawisz "I"
        
            if(ekwipunek.active==false)
            {
                ekwipunek.SetActive(true); //Ukrycie/pokazanie ekwipunku.		
                gm.active_menu = 3;

                Cursor.lockState = CursorLockMode.None;//Odblokowanie kursora myszy.
                Cursor.visible = true;//Pokazanie kursora.
            }
        }

        if (ekwipunek.active == true && Input.GetKeyDown(keyMenager.keys["Pause"]))
        {
            gm.ChangeToZero = true;
            ekwipunek.SetActive(false);//Ukrycie/pokazanie ekwipunku.	
            StatsUI.SetActive(false);
        }

        if (Change) //Zmiana notatki
        {
            for(int i=0;i< noteObj.Length;i++)
            {
                if(i==NoteNumber-1)
                {
                    noteObj[i].SetActive(true);
                }
                else
                {
                    noteObj[i].SetActive(false);
                }
            }
            Change = false;
        }

        if(Amor_Cat)
        {
            Active[1].SetActive(true);
        }
        else
        {
            Active[1].SetActive(false);

        }

        if (Weapon_Cat)
        {
            Active[0].SetActive(true);

        }
        else
        {
            Active[0].SetActive(false);

        }

        if (Rest_Cat)
        {
            Active[2].SetActive(true);

        }
        else
        {
            Active[2].SetActive(false);

        }

        //Wyświetlenie statystyk i posiadanych przedmiotów wielekrotnego użytku
        attackTxt.text = gm.PlayerAttack.ToString();
        armorTxt.text = gm.PlayerArmor.ToString();
        magicTxt.text = gm.PlayerMagic.ToString();
        agilityTxt.text = gm.PlayerSpeed.ToString();

        GoldTxt.text = gm.gold.ToString();
        RunesTxt.text = gm.runes.ToString();
        HpTxt.text = gm.hp_potion.ToString();
        MpTxt.text = gm.mana_potion.ToString();

        //Przyciski umożliwiające wykorzystanie obiektów
        if(gm.hp_potion>0)
        {
            ButtonHp.SetActive(true);
            ButtonHpHide.SetActive(false);
        }
        else
        {
            ButtonHp.SetActive(false);
            ButtonHpHide.SetActive(true);
        }
        if (gm.mana_potion > 0)
        {
            ButtonMana.SetActive(true);
            ButtonManaHide.SetActive(false);
        }
        else
        {
            ButtonMana.SetActive(false);
            ButtonManaHide.SetActive(true);
        }
    }
    public void RefreshNotes() //Odświeżenie notatek
    {
        for (int i = 0; i < noteObj.Length; i++)
        {
            if (gm.note[i] == 1)
            {
                status[i] = 1;
                noteButtons[i].SetActive(true);
            }
            else
            {
                status[i] = 0;
                noteButtons[i].SetActive(false);

            }
        }
    }
    public void PlayBag()
    {
        audioSource.Play();
    }
    public void UseHP()
    {
        gm.AddHp();
        gm.hp_potion -= 1;
    }
    public void UseMana()
    {
        gm.AddMana();
        gm.mana_potion -= 1;
    }

    public void Armor()
    {
        Amor_CatUI.SetActive(true);
        Weapon_CatUI.SetActive(false);
        Rest_CatUI.SetActive(false);

        Amor_Cat = true;
        Weapon_Cat = false;
        Rest_Cat = false;
    }
    public void Weapon()
    {
        Amor_CatUI.SetActive(false);
        Weapon_CatUI.SetActive(true);
        Rest_CatUI.SetActive(false);

        Amor_Cat = false;
        Weapon_Cat = true;
        Rest_Cat = false;
    }
    public void Rest()
    {
        Amor_CatUI.SetActive(false);
        Weapon_CatUI.SetActive(false);
        Rest_CatUI.SetActive(true);

        Amor_Cat = false;
        Weapon_Cat = false;
        Rest_Cat = true;
    }
    public void Stats()
    {
        StatsUI.SetActive(true);
        NotesUI.SetActive(false);

    }
    public void Notes()
    {
        NotesUI.SetActive(true);
        StatsUI.SetActive(false);
    }
    public void Eq()
    {
        StatsUI.SetActive(false);
        NotesUI.SetActive(false);

    }
    public void Note1()
    {
        NoteNumber = 1;
        Change = true;
    }
    public void Note2()
    {
        NoteNumber = 2;
        Change = true;

    }
    public void Note3()
    {
        NoteNumber = 3;
        Change = true;

    }
    public void Note4()
    {
        NoteNumber = 4;
        Change = true;

    }
    public void Note5()
    {
        NoteNumber = 5;
        Change = true;

    }
    public void Note6()
    {
        NoteNumber = 6;
        Change = true;

    }
    public void Note7()
    {
        NoteNumber = 7;
        Change = true;

    }
    public void Note8()
    {
        NoteNumber = 8;
        Change = true;

    }
    public void Note9()
    {
        NoteNumber = 9;
        Change = true;

    }
    public void OpenEq()
    {
        ekwipunek.SetActive(true); //Ukrycie/pokazanie ekwipunku.		
    }
    //Przełozenie obiektu
    public void Transfer(ElementEq d, GameObject drop_place)
    {
        item = d;
        TransferGui.SetActive(true);
        slider.maxValue = d.Count;
        waitForDecision = true;
        transfer_drop_place = drop_place;
    }
    public void Decline()
    {
        TransferGui.SetActive(false);
        waitForDecision = false;
        accept = false;
    }

    //zgodzenie się na przetransferowanie obiektu
    public void Accept()
    {
        TransferGui.SetActive(false);
        amountToTransfer = (int)slider.value;
        if (amountToTransfer > slider.maxValue)
        {
            amountToTransfer = (int)slider.maxValue -1;
            accept = true;
        }
        else
        {
            if (amountToTransfer <= 0)
            {
                amountToTransfer = 0;
                TransferGui.SetActive(false);
                waitForDecision = false;
                accept = true;
            }
            else
            {
                amountToTransfer = (int)slider.value;
                accept = true;
            }
        }
    }
}
