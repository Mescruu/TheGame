using UnityEngine;
using System.Collections;

public class Pause_Menu : MonoBehaviour
{


    public GameObject PauseUI;

    private Game_Master gm;
    private Saving_Script saving_cs;

    public bool can_save;
    public bool change;


    public bool paused = false;
    public GameObject SettingsUI;
    private KeyMenager keyMenager;

    void Start()
    {
        keyMenager = GameObject.Find("KeyMenager").GetComponent<KeyMenager>();

        gm = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<Game_Master>();
        saving_cs = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<Saving_Script>();

        PauseUI.SetActive(false);
        paused = false;

    }


    void Update()
    {
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

    public void Resume()
    {
        gm.ChangeToZero = true;

        paused = false;

    }
    public void Settings()
    {
        SettingsUI.GetComponent<Settings>().SettingUI.SetActive(true);
    }

    public void Restart()
    {
        paused = !paused;

        gm.ChangeToZero = true;

        gm.SceneIdToLoad = Application.loadedLevel;
        gm.loadScene = true;


    }

    public void Quit()
    {


        Application.Quit();


    }

    public void MainMenu()
    {
        paused = !paused;
        gm.ChangeToZero = true;

        gm.SceneIdToLoad = 0;
        gm.loadScene = true;
    }

}
