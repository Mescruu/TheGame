using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Read : MonoBehaviour {
	public bool Reading;//czy gracz czyta

    //Obiekty dotyczace ksiązki
	public GameObject BackPicInputText;
    public Text textToRead;
	public GameObject BookUI;
    private Game_Master gm;
    public GameObject[] Pages;
    public int OpenedPage;

    public GameObject PrevButton;
    private bool refresh;
    public Text text;

    public GameObject NextButton;
    private KeyMenager keyMenager;

    //Wyłaczenie podpowiedzi
    private bool ExitDesk;
    private float ExitTimeCd = 1f;
    private float ExitTime;

    void Start () 
	{
        //Począatkowe ustawienia
		BackPicInputText.SetActive (false);
		BookUI.SetActive (false);
		Reading = false;
        OpenedPage = 0;
        refresh = true;

        gm = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<Game_Master>();
        keyMenager = GameObject.Find("KeyMenager").GetComponent<KeyMenager>();
    }
    void Update()
    {
        text.text = (OpenedPage + "/" + Pages.Length); //wyswietlanie liczby stron

        if (ExitDesk) //znikanie podpowiedzi
        {
            BackPicInputText.GetComponent<Animator>().SetBool("disappear", true);
            ExitTime -= Time.deltaTime;
            if (ExitTime < 0)
            {
                BackPicInputText.SetActive(false);
                ExitDesk = false;
            }
        }
        else
        {
            ExitTime = 0;
        }

        //odpowiednie przyciski
        if (OpenedPage==Pages.Length-1)
        {
            NextButton.SetActive(false);
        }
        else
        {
            NextButton.SetActive(true);
        }

        if (OpenedPage == 0)
        {
            PrevButton.SetActive(false);
        }
        else
        {
            PrevButton.SetActive(true);
        }
        if(refresh)
        {
            for(int i=0;i<Pages.Length;i++)
            {
                if(i==OpenedPage)
                {
                    Pages[i].SetActive(true);
                }
                else
                {
                    Pages[i].SetActive(false);    
                }
            }
            refresh = false;
        }

        if (Input.GetKeyDown(keyMenager.keys["Pause"]) && Reading) //gracz kończy czytać
        {
           BookUI.SetActive(false);
           gm.ChangeToZero = true;
           Reading = !Reading;
        }
        

    }
    //przeskakiwanie miedzy stronami
    public void NextPage()
    {
        OpenedPage++;
        refresh=true;
    }
   public void PreviousPage()
    {
        OpenedPage--;
        refresh = true;
    }

    void OnTriggerEnter2D(Collider2D col)
	{
		if (col.CompareTag ("Player"))
        {
            BackPicInputText.SetActive(true);
            textToRead.text = "[" + keyMenager.keys["Action"] + "] To Read";
            ExitDesk = false;
        }
	}
	
	void OnTriggerStay2D(Collider2D col) //gracz zaczyna czytać
	{
		if (col.CompareTag ("Player"))
        {
            BackPicInputText.SetActive(true);
            textToRead.text = "[" + keyMenager.keys["Action"] + "] To Read";

            if (!Reading)
             {
			    BackPicInputText.SetActive (true);									
				if (Input.GetKeyDown(keyMenager.keys["Action"])&&gm.dead==false) 
				{
				   if (gm.active_menu == 0)
                    {
                        BookUI.SetActive(true);
                        gm.active_menu = 6;
                        Reading = !Reading;
                    }		
				}
			}
		}
	}
	void OnTriggerExit2D(Collider2D col) //gracz wychodzi z kolizji umożliwiającej czytanie
	{		
		if(col.CompareTag("Player"))
		{
			Reading=false;
            BackPicInputText.SetActive(false);
            //   textToRead.text = " ";
            ExitDesk = true;
            ExitTime = ExitTimeCd;
        }
		
	}
	
	public void Exit() //wyłaczenie UI książki
	{
		Reading = false;
		BookUI.SetActive(false);
	}
}
