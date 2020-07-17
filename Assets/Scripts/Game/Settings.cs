using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;


public class Settings : MonoBehaviour
{
    //UI
    public GameObject BindSettings;
    public GameObject SettingUI;

    //Przycisk odpowiadający za rozdzielczość
    public GameObject lightOn;
    public GameObject lightOff;
    public int[] resolutionsX; //rozdzielczości X
    public int[] resolutionsY; //rozdzielczosci Y

    //Fullscreen i pasek głosności
    public bool fullScreen;
    public Slider volumeSlider;
    public bool ResolutionScroll;
    public GameObject ResolutionScrollObj;

    public bool QualityScroll;
    public GameObject QualityScrollObj;

    private SoundHolder soundholder;

    public Text ResolutionButtonText;
    public GameObject[] ResolutionPoint;


    public Text QualityButtonText;
    public GameObject[] QualityPoint;


    private int StartQuality;
    private int StartResolution;

    private bool ScrollROpen;
    private bool ScrollGOpen;
    private bool bindkeyUI;

    private int cat;
    public GameObject[] category;

    private BindKey bindkey;

    public GameObject DefaultUI;
    private KeyMenager keyMenager;
    private int resolutionIndex;
    public int qualityindex;
    private GameObject gm;

    // Use this for initialization
    void Start ()
    {
        keyMenager = GameObject.Find("KeyMenager").GetComponent<KeyMenager>();

        gm = GameObject.FindGameObjectWithTag("GameMaster");
        soundholder = GameObject.Find("SoundHolder").GetComponent<SoundHolder>();
        bindkey = gameObject.GetComponent<BindKey>();


        DefaultUI.SetActive(false);

        bindkeyUI = false;
   
        ResolutionScroll = false;
        QualityScroll = false;
        //kategorie poziomu trundości
        category[0].SetActive(true);
        category[1].SetActive(false);
        category[2].SetActive(false);
        category[3].SetActive(false);

        load();

        if (gm!=null)
        {
            gm.GetComponent<Game_Master>().settings = this;
        }
        SettingUI.SetActive(false);

    }
    void Update()
    {
        if (Input.GetKeyDown(keyMenager.keys["Pause"])) //wyłaczenie ustawień
        {
            load();
            SettingUI.SetActive(false);
        }

                    QualityScrollObj.SetActive(ScrollGOpen);
            ResolutionScrollObj.SetActive(ScrollROpen);
            BindSettings.SetActive(bindkeyUI);
            soundholder.volume = volumeSlider.value;
    }
    //właczenie/wyłaczenie UI ustawiania przycisków
    public void BindKeyButton()
    {
        bindkeyUI = !bindkeyUI;
    }
    //Ustawienia rozdzielczości
    public void ResolutionButton()
    {
        ResolutionScroll = !ResolutionScroll;
        ResolutionScrollObj.SetActive(ResolutionScroll);
        ScrollROpen = true;
    }

    public void resolution1()
    {
        ResolutionButton();
        SetResolutionGame(0);
        resolutionIndex = 0;

        ResolutionPoint[0].SetActive(true);
        ResolutionPoint[1].SetActive(false);
        ResolutionPoint[2].SetActive(false);

        ResolutionButtonText.text = resolutionsX[0] + "x" + resolutionsY[0];

    }
    public void resolution2()
    {
        ResolutionButton();
        SetResolutionGame(1);
        resolutionIndex = 1;

        ResolutionPoint[0].SetActive(false);
        ResolutionPoint[1].SetActive(true);
        ResolutionPoint[2].SetActive(false);
        ResolutionButtonText.text = resolutionsX[1] + "x" + resolutionsY[1];

    }
    public void resolution3()
    {
        ResolutionButton();
        SetResolutionGame(2);
        resolutionIndex = 2;
        ResolutionPoint[0].SetActive(false);
        ResolutionPoint[1].SetActive(false);
        ResolutionPoint[2].SetActive(true);
        ResolutionButtonText.text = resolutionsX[2] + "x" + resolutionsY[2];
    }

    //Ustawienia jakości
    public void QualityButton()
    {
        QualityScroll = !QualityScroll;
        ScrollGOpen = true;
    }
    public void Qulity1()
    {
        QualityButton();
       SetQuality(1);
        qualityindex = 1;
        QualityPoint[0].SetActive(true);
        QualityPoint[1].SetActive(false);
        QualityPoint[2].SetActive(false);
        QualityPoint[3].SetActive(false);

        QualityButtonText.text = "easy";
    }
    public void Qulity2()
    {
        QualityButton();
        SetQuality(2);
        qualityindex = 2;

        QualityPoint[0].SetActive(false);
        QualityPoint[1].SetActive(true);
        QualityPoint[2].SetActive(false);
        QualityPoint[3].SetActive(false);
        QualityButtonText.text = "normal";

    }
    public void Qulity3()
    {
        QualityButton();
        SetQuality(3);
        qualityindex = 3;

        QualityPoint[0].SetActive(false);
        QualityPoint[1].SetActive(false);
        QualityPoint[2].SetActive(true);
        QualityPoint[3].SetActive(false);
        QualityButtonText.text = "hard";

    }
    public void Qulity4()
    {
        QualityButton();
       SetQuality(4);
        qualityindex = 4;

        QualityPoint[0].SetActive(false);
        QualityPoint[1].SetActive(false);
        QualityPoint[2].SetActive(false);
        QualityPoint[3].SetActive(true);
        QualityButtonText.text = "hell";

    }
    //zapisanie ustawień
    public void Exit()
    {
        PlayerPrefs.SetFloat("Volume", volumeSlider.value);
        PlayerPrefs.SetInt("Quality", qualityindex);
        PlayerPrefs.SetInt("difficultLevel", qualityindex);

        PlayerPrefs.SetInt("Resolution", resolutionIndex);

        if (fullScreen)
        {
            PlayerPrefs.SetInt("FullScreen", 1);
        }
        else
        {
            PlayerPrefs.SetInt("FullScreen", 0);

        }

        bindkeyUI = false;

        SettingUI.SetActive(false);

        bindkey.Save();
        keyMenager.Refresh();


    }
    //Wywołanie zapytania czy na pewno
    public void DefaultOptions()
    {
            DefaultUI.SetActive(true);
    }
    //ustawienia domyślne
    public void Yes()
    {
     bindkey.keys.Clear();

        Debug.Log("Default");


       PlayerPrefs.DeleteKey("Volume");
       PlayerPrefs.DeleteKey("Quality");
       PlayerPrefs.DeleteKey("Resolution");
        PlayerPrefs.DeleteKey("FullScreen");

        PlayerPrefs.DeleteKey("difficultLevel");

        DefaultUI.SetActive(false);

       bindkey.Refresh();
        load();

    }
    public void No()
    {
        DefaultUI.SetActive(false);
    }
    public void CloseScrool()
    {
        ScrollROpen = false;
        ScrollGOpen = false;

    }
    //rozdzielczości
    public void setResolutionTable()
    {
        resolutionsX[0] = 1366;
        resolutionsY[0] = 768;

        resolutionsX[1] = 1280;
        resolutionsY[1] = 720;

        resolutionsX[2] = 1600;
        resolutionsY[2] = 900;
    }
    //ustawienia jakości
    public void SetQuality(int index)
    {
     //   QualitySettings.SetQualityLevel(index);
        ScrollGOpen = false;
    }
    public void SetResolutionGame(int index)
    {
        Screen.SetResolution((int)resolutionsX[index], (int)resolutionsY[index], fullScreen);
        ScrollROpen = false;
    }
    public void FullScreenButton()
    {
        fullScreen = !fullScreen;
        if (fullScreen)
        {
            Screen.fullScreen = true;
            lightOn.SetActive(true);
            lightOff.SetActive(false);
        }
        else
        {
            Screen.fullScreen = false;
            lightOn.SetActive(false);
            lightOff.SetActive(true);

        }


    }

    //ładowanie poprzednich ustawień
    public void load()
    {
        if(PlayerPrefs.HasKey("FullScreen"))
        {
            if(PlayerPrefs.GetInt("FullScreen") ==1)
            {
                Screen.fullScreen = true;
                lightOn.SetActive(true);
                lightOff.SetActive(false);

                fullScreen = true;
            }
            else
            {
                Screen.fullScreen = false;
                lightOn.SetActive(false);
                lightOff.SetActive(true);

                fullScreen = false;
            }
        }
        else
        {
            Screen.fullScreen = true;
            lightOn.SetActive(true);
            lightOff.SetActive(false);

            fullScreen = true;
        }

     

        setResolutionTable();

        ScrollROpen = false;
        ScrollGOpen = false;

        CloseScrool();

        if (PlayerPrefs.HasKey("difficultLevel"))
        {
            StartQuality = PlayerPrefs.GetInt("difficultLevel");
            if (StartQuality == 1)
            {
                Qulity1();
            }
            if (StartQuality == 2)
            {
                Qulity2();
            }
            if (StartQuality == 3)
            {
                Qulity3();
            }
            if (StartQuality == 4)
            {
                Qulity4();
            }
        }
        else
        {
            Qulity2();
        }

        if (PlayerPrefs.HasKey("Resolution"))
        {
            StartResolution = PlayerPrefs.GetInt("Resolution");
            if (StartResolution == 0)
            {
                resolution1();
            }
            if (StartResolution == 1)
            {
                resolution2();
            }
            if (StartResolution == 2)
            {
                resolution3();
            }

        }
        else
        {
            resolution1();
        }
        if (PlayerPrefs.HasKey("Volume"))
        {
            volumeSlider.value = PlayerPrefs.GetFloat("Volume");
            soundholder.volume = volumeSlider.value;
        }
        else
        {
            soundholder.volume = 0.75f;
            volumeSlider.value = 0.75f;
            PlayerPrefs.SetFloat("Volume", 0.75f);
        }

    }
    //poziomy trudnosci
    public void Cat1()
    {
        category[0].SetActive(true);
        category[1].SetActive(false);
        category[2].SetActive(false);
        category[3].SetActive(false);

    }
    public void Cat2()
    {
        category[0].SetActive(false);
        category[1].SetActive(true);
        category[2].SetActive(false);
        category[3].SetActive(false);

    }
    public void Cat3()
    {
        category[0].SetActive(false);
        category[1].SetActive(false);
        category[2].SetActive(true);
        category[3].SetActive(false);

    }
    public void Cat4()
    {
        category[0].SetActive(false);
        category[1].SetActive(false);
        category[2].SetActive(false);
        category[3].SetActive(true);

    }
}
