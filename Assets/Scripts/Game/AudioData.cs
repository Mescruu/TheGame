using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioData : MonoBehaviour {

    //muzyka pod walkę
    public bool FightMusic;
    public AudioClip PreFightClip;
    public bool FightMusicRandom;
    private int fightid;

    //przejście miedzy walką
    public AudioClip[] FightClip;
    public AudioClip AfterFightClip;
    public AudioClip[] NormalClip;
    public bool NormalMusicRandom;
    private int normalid;

    //obiekt odtwarzający muzykę
    private SoundHolder soundHolder;
    private AudioSource soundHolderAudioSource;
    public bool canUse;

    private float timeToCheck;
    public AudioClip ChangeSound;
    public AudioSource HelpAudioSource;

    private Game_Master gm;

    void Awake() //Na samym początku odszukiwany jest komponent SoundHolder i jego odtwarzacz
    {
        soundHolder = GameObject.Find("SoundHolder").GetComponent<SoundHolder>();
        soundHolderAudioSource = soundHolder.gameObject.GetComponent<AudioSource>();

    }
    void Start () {

        gm = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<Game_Master>();

        if (ChangeSound!=null) //ustalenie początkowego utworu
        {
            HelpAudioSource.clip = ChangeSound;
            HelpAudioSource.volume = soundHolder.volume;
        }
        if (soundHolder!=null) //może zostać użyty do walki
        {
            canUse = true;
            soundHolderAudioSource.loop = false;
        }
        fightid = 100; //ustalenie poczatkowego id (liczba oznaczająca gotowość)
    }

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
                        normalid = 100; //normal jest gotowy

                        if (fightid == 100) //jeżeli fightid jest gotowy zmień na muzykę do walki
                        {
                            ChangeToFight();
                        }
                        else
                        {
                            if (FightMusicRandom) //przeskakuj między ścieżkami losowo
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
                        if (normalid == 100)
                        {
                            ChangeToNormal();
                        }
                        else
                        {
                            fightid = 100;

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
    void ChangeToFight() //zmień muzykę na tę do walki
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
    void ChangeToNormal() //zmień na normalną muzykę
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
