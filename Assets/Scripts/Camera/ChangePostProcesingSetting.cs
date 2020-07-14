using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePostProcesingSetting : MonoBehaviour {

    //Ustawienia postprocesingu (profil)
    public CameraProfile camProfile;

    private float chAberration; //wartość aberacji
    private float bloom; //wartość efeketu bloom

    //zmienne boolean odpowiadajace określające aktualną zmianę
    public bool ChangeCamSet;
    public bool ChangeAberration;
    public bool ChangeMusic;
    public bool ChangeBloom;
    private bool StartChange;

    //Wartości intensywności i czasu trwania zmiany
    public float BloomSpeed=4f;
    public float bloomBorder=10f;
    public float Timer;
    public float TimerCD;

   void Start()
    {
        StartChange = false;
    }

    // Update is called once per frame
    void Update () {

        //Przygotowanie do zmian ustawień kamery, pobranie odpowiednich danych
        if (ChangeCamSet)
        {
            Timer = TimerCD;
            chAberration = camProfile.ChAberrationIntensity;
            bloom = camProfile.bloomIntensity;

            if (!camProfile.audioSource.isPlaying)
            {
                camProfile.audioSource.clip = camProfile.ChAberrationaudioClip; //włączenie dźwięku odpowiadającego za zmianę efektu
                camProfile.audioSource.Play();
            }
            StartChange = true;
            ChangeCamSet = false;
        }

        //Rozpoczęcie zmian
        if (StartChange)
        {
            Timer -= Time.deltaTime; 
            
            //Powrót do poprzednich ustawień
                if (Timer <= 0)
                {
                    if (ChangeAberration)
                        camProfile.ChangeChAberrationAtRuntimeToPreviousSetting();

                    if (ChangeBloom)
                        camProfile.ChangeBloomAtRuntimeToPreviousSetting();

                StartChange = false;    
                }
                else
                {
                    if (ChangeAberration)
                        chAberration += Time.deltaTime / 3;
                     
                     if (ChangeBloom)
                        bloom += Time.deltaTime*BloomSpeed;
                     
                     if (ChangeMusic)
                         camProfile.audioSource.volume = chAberration;
                     

                     if (chAberration <= 1)
                        camProfile.ChangeChAberrationAtRuntime(chAberration, ChangeMusic);
                     else
                        camProfile.ChangeChAberrationAtRuntime(1,ChangeMusic);
                   
                    if (bloom <= bloomBorder)
                     camProfile.ChangeBloomAtRuntime(bloom);
                    else
                    camProfile.ChangeBloomAtRuntime(bloomBorder);
                }
        }
        else
        {
        chAberration = 0;
        }
     }
}

