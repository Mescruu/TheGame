using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePostProcesingSetting : MonoBehaviour {

    public CameraProfile camProfile;
    private float chAberration;
    private float bloom;

    public bool ChangeCamSet;
    public bool ChangeAberration;
    public bool ChangeMusic;
    public bool ChangeBloom;
    public float BloomSpeed=4f;
    public float bloomBorder=10f;
    public float Timer;
    public float TimerCD;
    private bool StartChange;
    // Use this for initialization
   void Start()
    {
        StartChange = false;
    }

    // Update is called once per frame
    void Update () {

        if (ChangeCamSet)
        {
            Timer = TimerCD;
            chAberration = camProfile.ChAberrationIntensity;
            bloom = camProfile.bloomIntensity;

            if (!camProfile.audioSource.isPlaying)
            {
                camProfile.audioSource.clip = camProfile.ChAberrationaudioClip;
                camProfile.audioSource.Play();
            }
            StartChange = true;
            ChangeCamSet = false;

        }
        if (StartChange)
        {
            Timer -= Time.deltaTime;

          
                if (Timer <= 0)
                {
                    if (ChangeAberration)
                    {
                        camProfile.ChangeChAberrationAtRuntimeToPreviousSetting();
                    }
                    if (ChangeBloom)
                    {
                        camProfile.ChangeBloomAtRuntimeToPreviousSetting();
                    }
                StartChange = false;    
                }
                else
                {
                     if (ChangeAberration)
                     {
                        chAberration += Time.deltaTime / 3;
                    }
                if (ChangeBloom)
                {
                    bloom += Time.deltaTime*BloomSpeed;
                }
                if (ChangeMusic)
                       {
                       camProfile.audioSource.volume = chAberration;
                     }

                if (chAberration <= 1)
                    {
                        camProfile.ChangeChAberrationAtRuntime(chAberration, ChangeMusic);
                    }
                    else
                    {
                        camProfile.ChangeChAberrationAtRuntime(1,ChangeMusic);

                    }
                if (bloom <= bloomBorder)
                {
                    camProfile.ChangeBloomAtRuntime(bloom);
                }
                else
                {
                    camProfile.ChangeBloomAtRuntime(bloomBorder);
                }
            }

            }
            else
            {
                chAberration = 0;
            }
        }
       


    }

