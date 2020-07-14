using UnityEngine;
using System.Collections;

public class Pause_Menu : MonoBehaviour
{

    //Obiekt Menu pauzy
    public GameObject PauseUI;

    //Obiekt GM
    private Game_Master gm;

    //Zmienne boolean określajace stan
    public bool can_save;
    public bool change;
    public bool paused = false;

    //Odniesienia
    public GameObject SettingsUI;
    private KeyMenager keyMenager;

    void Start()
    {
        //Ustawienie komponentów
        keyMenager = GameObject.Find("KeyMenager").GetComponent<KeyMenager>();
        gm = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<Game_Master>();

        //Poczatkowe ustawienia
        PauseUI.SetActive(false);
        paused = false;

    }


    void Update()
    {
        //Wywołanie menu i zatrzymanie czasu
        if (Input.GetKeyDown(keyMenager.keys["Pause"]))
        {
            if(gm.active_menu == 0 || gm.active_menu==1)
            {
                if (paused == true)
                {
                    gm.ChangeToZero = true;
                }
                paused = !paused;
            }
        }

        if (paused && gm.active_menu==0)
        {
            PauseUI.SetActive(true);
            gm.active_menu = 1;
        }

        if (!paused)
        {
            if(SettingsUI.active==true)
            {
                SettingsUI.GetComponent<Settings>().SettingUI.SetActive(false);
                SettingsUI.GetComponent<Settings>().load();
            }
            PauseUI.SetActive(false);
        }
    }

    //Ustawienia przycisków
    public void Resume()
    {
        gm.ChangeToZero = true;
        paused = false;
    }
    //Menu ustawien
    public void Settings()
    {
        SettingsUI.GetComponent<Settings>().SettingUI.SetActive(true);
    }

    //Zresetowanie sceny
    public void Restart()
    {
        paused = !paused;
        gm.ChangeToZero = true;
        gm.SceneIdToLoad = Application.loadedLevel;
        gm.loadScene = true;
    }

    //Wyłaczenie aplikacji bez zapisu
    public void Quit()
    {
        Application.Quit();
    }

    //Powrot do menu głównego
    public void MainMenu()
    {
        paused = !paused;
        gm.ChangeToZero = true;
        gm.SceneIdToLoad = 0;
        gm.loadScene = true;
    }
}
