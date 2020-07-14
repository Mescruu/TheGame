using UnityEngine;
using System.Collections;

public class Elements : MonoBehaviour
{
    //numer żywiołu
    static public int ElementNumber;

    //zmienne boolean określające, który żywioł jest używany
    public bool fire;
    public bool electricity;
    public bool dark;
    public bool white;

    //komponenty
    private Animator anim;
    private Game_Master gameMaster;
    private KeyMenager keyMenager;

    //komponenty dźwięku
    private AudioSource audioSource;
    public AudioClip[] audioClip;

    // Use this for initialization
    void Start()
    {
        //dołączanie komponentów
        anim = gameObject.GetComponent<Animator>();
        gameMaster = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<Game_Master>();
        keyMenager = GameObject.Find("KeyMenager").GetComponent<KeyMenager>();
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //ustawianie w gamemasterze numeru elementu
        gameMaster.Element = ElementNumber;

        //ziana elementu po wcisnięciu odpowiedniego przycisku
        if (Input.GetKeyDown(keyMenager.keys["Fire"]))
        {
            ElementNumber = 1;
            audioSource.clip = audioClip[0];
            audioSource.Play();
        }

        if (Input.GetKeyDown(keyMenager.keys["Electricity"]))
        {
            ElementNumber = 2;
            audioSource.clip = audioClip[1];
            audioSource.Play();
        }

        if (Input.GetKeyDown(keyMenager.keys["Darkness"]))
        {
            ElementNumber = 3;
            audioSource.clip = audioClip[2];
            audioSource.Play();
        }

        if (Input.GetKeyDown(keyMenager.keys["Ankh"]))
        {
            ElementNumber = 4;
            audioSource.clip = audioClip[3];
            audioSource.Play();
        }

        //ustawienie booleanów określajacych elementy
        if (ElementNumber == 1)
        {
            fire = true;
            electricity = false;
            dark = false;
            white = false;
        }
        if (ElementNumber == 2)
        {
            fire = false;
            electricity = true;
            dark = false;
            white = false;
        }
        if (ElementNumber == 3)
        {
            fire = false;
            electricity = false;
            dark = true;
            white = false;
        }
        if (ElementNumber == 4)
        {
            fire = false;
            electricity = false;
            dark = false;
            white = true;
        }

        //ustawienie animacji
        anim.SetBool("White", white);
        anim.SetBool("Dark", dark);
        anim.SetBool("Electricity", electricity);
        anim.SetBool("Fire", fire);
    }
}
