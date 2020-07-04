using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Read : MonoBehaviour {
	public bool Reading;
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

    private bool ExitDesk;
    private float ExitTimeCd = 1f;
    private float ExitTime;

    // Use this for initialization
    void Start () 
	{
		
		BackPicInputText.SetActive (false);
		BookUI.SetActive (false);
		Reading = false;
        gm = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<Game_Master>();
        OpenedPage = 1;
        refresh = true;
        keyMenager = GameObject.Find("KeyMenager").GetComponent<KeyMenager>();

    }
    void Update()
    {

        text.text = (OpenedPage + "/" + Pages.Length);


        if (ExitDesk)
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



        if (OpenedPage==Pages.Length)
        {
            NextButton.SetActive(false);
        }
        else
        {
            NextButton.SetActive(true);
        }

        if (OpenedPage == 1)
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

        
            if (Input.GetKeyDown(keyMenager.keys["Pause"]) && Reading)
            {
                BookUI.SetActive(false);
                gm.ChangeToZero = true;
                Reading = !Reading;
            }
        

    }
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
	
	void OnTriggerStay2D(Collider2D col)
	{
		
		if (col.CompareTag ("Player"))
        {
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
	void OnTriggerExit2D(Collider2D col)
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
	
	
	
	public void Exit()
	{
		Reading = false;
		BookUI.SetActive(false);

	}
	
}
