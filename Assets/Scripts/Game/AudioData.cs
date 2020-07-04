using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioData : MonoBehaviour {

    public bool FightMusic;
    public AudioClip PreFightClip;
    public bool FightMusicRandom;
    private int fightid;

    public AudioClip[] FightClip;
    public AudioClip AfterFightClip;
    public AudioClip[] NormalClip;
    public bool NormalMusicRandom;
    private int normalid;

    private SoundHolder soundHolder;
    private AudioSource soundHolderAudioSource;
    public bool canUse;
    private Game_Master gm;

    private float timeToCheck;
    public AudioClip ChangeSound;
    public AudioSource HelpAudioSource;

	// Use this for initialization
    void Awake()
    {
        soundHolder = GameObject.Find("SoundHolder").GetComponent<SoundHolder>();
        soundHolderAudioSource = soundHolder.gameObject.GetComponent<AudioSource>();

    }
    void Start () {

        if(ChangeSound!=null)
        {
            HelpAudioSource.clip = ChangeSound;
            HelpAudioSource.volume = soundHolder.volume;

        }
        gm = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<Game_Master>();

        if (soundHolder!=null)
        {
            canUse = true;

            soundHolderAudioSource.loop = false;
        }
        fightid = 69;
    }

    // Update is called once per frame
    void Update ()
    {
        FightMusic = gm.PlayerFighting;

        if(canUse && gm!=null)
        {
            if (ChangeSound != null)
            {
                HelpAudioSource.volume = soundHolder.volume*1.5f;

            }

            soundHolderAudioSource.loop = false;

            if (!soundHolderAudioSource.isPlaying)
            {
                if(gm.PlayerFighting)
                {
                    if(FightMusic)
                    {
                        normalid = 69;

                        if (fightid == 69)
                        {
                            ChangeToFight();
                         
                        }
                        else
                        {
                            if (FightMusicRandom)
                            {
                                if (ChangeSound != null)
                                {
                                    HelpAudioSource.Play();
                                }

                                soundHolderAudioSource.clip = FightClip[Random.Range(0, FightClip.Length)];
                                timeToCheck = soundHolderAudioSource.clip.length;
                                soundHolderAudioSource.Play();
                                fightid = 0;
                            

                            }
                            else
                            {

                                if (fightid == -1)
                                {
                                    if (ChangeSound != null)
                                    {
                                        HelpAudioSource.Play();
                                    }

                                    soundHolderAudioSource.clip = FightClip[0];
                                    fightid = 0;
                                    soundHolderAudioSource.Play();
                                 
                                }
                                else
                                {
                                    if (ChangeSound != null)
                                    {
                                        HelpAudioSource.Play();
                                    }
                                    fightid++;

                                    if (fightid == FightClip.Length)
                                    {
                                        fightid = 0;

                                    }
                                    soundHolderAudioSource.clip = FightClip[fightid];
                                    timeToCheck = soundHolderAudioSource.clip.length;
                                    soundHolderAudioSource.Play();
                                 
                                }



                            }
                        }
                        
                    }
                 
                }
                else
                {
                  
                    if(!FightMusic)
                    {
                        if (normalid == 69)
                        {
                            ChangeToNormal();
                          
                        }
                        else
                        {
                            fightid = 69;

                            if (NormalMusicRandom)
                            {
                                if (ChangeSound != null)
                                {
                                    HelpAudioSource.Play();
                                }
                                soundHolderAudioSource.clip = NormalClip[Random.Range(0, FightClip.Length)];
                                timeToCheck = soundHolderAudioSource.clip.length;
                                soundHolderAudioSource.Play();
                             
                            }
                            else
                            {
                                if (ChangeSound != null)
                                {
                                    HelpAudioSource.Play();
                                }
                                if (normalid==-1)
                                {
                                    soundHolderAudioSource.clip = NormalClip[0];
                                    soundHolderAudioSource.Play();
                                    normalid = 0;
                                 
                                }
                                else
                                {
                                    if (ChangeSound != null)
                                    {
                                        HelpAudioSource.Play();
                                    }
                                    normalid++;

                                    if (normalid == NormalClip.Length)
                                    {
                                        normalid = 0;
                                    }

                                    soundHolderAudioSource.clip = NormalClip[normalid];
                                    timeToCheck = soundHolderAudioSource.clip.length;
                                    soundHolderAudioSource.Play();
                                  

                                }


                            }
                        }
                       
                    }
                }
            }
                else
            {
                timeToCheck -= Time.deltaTime;
                if(timeToCheck<=2f)
                {
                    gm.CheckFighting();

                }
            }
        }
        else
        {
            soundHolderAudioSource.loop = true;
        }
      
            
    }
    void ChangeToFight()
    {
        if (canUse)
        {
            if (ChangeSound != null)
            {
                HelpAudioSource.Play();

            }
            soundHolderAudioSource.clip = PreFightClip;
          FightMusic = true;
          fightid = -1;
            soundHolderAudioSource.Play();

        }
    }
    void ChangeToNormal()
    {
        if (canUse)
        {
            if (ChangeSound != null)
            {
                HelpAudioSource.Play();
            }
            normalid = -1;
            soundHolderAudioSource.clip = AfterFightClip;
            FightMusic = false;
            soundHolderAudioSource.Play();

        }
    }
}
