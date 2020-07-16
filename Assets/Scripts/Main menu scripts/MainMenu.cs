using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;//Do operacji na plikach (czytanie, pisanie do pliku).
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
	
	
	public int LastLevel;
	public GameObject MenuUI;

	public GameObject ExitUI;

	public GameObject continueGameUI;

	public GameObject NewGameUI;

    public int levels = 1;

    public LoadTargetScript sc;

    public bool fade;
    public int gotolevel;
    public float TimeToFade=1f;
    public Animator anim;

    public GameObject Setting;

    public GameObject InfoDesk;
    private bool infoDeskActive = false;

    public Saveing save;

    public Sprite spriteToFade;

    private KeyMenager keyMenager;


    void Start()
	{
        anim.GetComponent<Image>().sprite = spriteToFade;
        anim.gameObject.SetActive(true);

        save = gameObject.GetComponent<Saveing>();
        save.notmenu = false;
        LastLevel = save.LastLevel;
        PlayerPrefs.DeleteKey("beforeBorders0");

        anim.SetBool("FadeOut", false);

        gotolevel = -1;

        fade = false;

		ExitUI.SetActive (false);

        NewGameUI.SetActive(false);

       sc = gameObject.GetComponent<LoadTargetScript>();

        Cursor.lockState = CursorLockMode.None;//Odblokowanie kursora myszy.
        Cursor.visible = true;//Pokazanie kursora.

        infoDeskActive = false; //tablica z informacją o projekcie jest wyłączona
        InfoDesk.SetActive(false);

        keyMenager = GameObject.Find("KeyMenager").GetComponent<KeyMenager>();


    }
    void Update()
    {
        if(fade)
        {
            Cursor.lockState = CursorLockMode.Locked; //Zablokowanie kursora myszy.
            Cursor.visible = false;//Ukrycie kursora.
            anim.SetBool("FadeOut", true);
           TimeToFade -= Time.deltaTime;
           if (TimeToFade <= 0)
            {                               //50 line
                sc.LoadScreenNum(gotolevel, Application.loadedLevel);
            }
        }

        if(levels < 2)
             {
            continueGameUI.SetActive(false);
             }

        if (Input.GetKeyDown(keyMenager.keys["Pause"]))
        {
            if (infoDeskActive)
            {
                infoDeskActive = false; //zamknięcie tablicy z informacjami o projekcie
                InfoDesk.SetActive(false);
            }

        }
    }


	public void NewGame() //wczytanie nowej gry
	{
        if (LastLevel > 2)
        {
		NewGameUI.SetActive (true);
		}
        else
        {
            gotolevel = 1;
            fade = true;
            Debug.Log("new game");
		}
    }

	public void Continue() //kontynuowanie rozgrywki
	{
        Debug.Log("Play");
        gotolevel = LastLevel;
        fade = true;
        Debug.Log("new game");
    }

    public void Options()
    {
        Setting.SetActive(true);
    }
	
    public void Credits()
    {
        Debug.Log("Credits");
        InfoDesk.SetActive(true);
        infoDeskActive = true;
    }

    public void Reset()
    {
        Debug.Log("Reset");
        if (File.Exists(Application.persistentDataPath + "/Record.data"))
        {
            File.Delete(Application.persistentDataPath + "/Record.data");
            Debug.Log("delete" + "/Record.data");

        }
        for (int i = 0; i <= SceneManager.sceneCountInBuildSettings; i++)
        {
            if (File.Exists(Application.persistentDataPath + "/ChestRecord" + i + ".data"))
            {
                File.Delete(Application.persistentDataPath + "/ChestRecord" + i + ".data");
                Debug.Log("delete" + "/ChestRecord" + i + ".data");
            }
        }
    }

    public void Quit()
    {
        Debug.Log("Quit_sure");
        ExitUI.SetActive (true);
    }

	public void Return ()
	{
    NewGameUI.SetActive (false);
    ExitUI.SetActive(false);

    }

    public void StartSure()
	{
		if (LastLevel > 2)
         {
		    NewGameUI.SetActive (true);
		}
	}

	public void StartNewGame()
	{
        Debug.Log("Start new game");
        gotolevel = 1;
        fade = true;
        Debug.Log("new game");
    }

    public void QuitGame()
    {
        Debug.Log("quit game");
        Application.Quit();
    }

    public void CreditsExit()
    {
        InfoDesk.SetActive(false);
    }
}

