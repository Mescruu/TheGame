﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioPlayer : MonoBehaviour
{

    private AudioSource audioSource;
    private Animator anim;
    private float SoundTime;
    private bool tillAnimation;
    private bool TimeAnimation;
    private bool JustPlay;
    private float AnimationTime;
    private string animationName;
   // private Animation animation;
    public float smoothTime;
    private bool stopPlaying;
    private float timeToStop;

    private float OncePlayTime;
    public bool CanPlay;
    private float distance;

    // Use this for initialization
    void Start()
    {
        CanPlay = true;
        anim = gameObject.GetComponent<Animator>();
        audioSource = gameObject.GetComponent<AudioSource>();
        stopPlaying = false;
        audioSource.loop = false;
        tillAnimation = false;
        TimeAnimation = false;
        JustPlay = false;
        timeToStop = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //Głośność zależna od odległości na ekranie
        distance = Vector3.Distance( GameObject.FindGameObjectWithTag("Player").transform.position, transform.position);
        if (distance < 500)
        {
            audioSource.volume = 1;

        }
        if (distance >= 500 && distance < 1500)
        {
            audioSource.volume = 1/((distance-500)/1000);
        }
        if (distance >= 1500)
        {
            audioSource.volume = 0;
        }

        if (TimeAnimation)
        {
            if(SoundTime>0)
            {
                SoundTime -= Time.deltaTime;
            }
            else
            {
                audioSource.Stop();
                audioSource.clip = null;
                TimeAnimation = false;
            }
        }

        if(stopPlaying)
        {
            float StopTime = timeToStop;
            CanPlay = true;

            if (audioSource.clip!= null) 
            {
                if (timeToStop > 0)
                {
                    timeToStop -= Time.deltaTime;
                    audioSource.volume = timeToStop*(1/smoothTime);
                }
                else
                {
                    timeToStop = 0;
                    audioSource.clip = null;
                }

            }
            else
            {
                stopPlaying = false;
            }
        }
     if(OncePlayTime>=0)
        {
            OncePlayTime -= Time.deltaTime;
            CanPlay = false;
        }else
        {
            CanPlay = true;
        }
     

    }

    public void Play(AudioClip audioClip, float SoundTime) //graj utwór przez jakiś czas
    {
        this.audioSource.clip = audioClip;
        this.SoundTime = SoundTime;
        TimeAnimation = true;
        audioSource.Play();

    }
    public void Play(AudioClip audioClip) //graj utwór
    {
        this.audioSource.clip = audioClip;
        JustPlay = true;
        AnimationTime = 0;
        audioSource.Play();

    }
    public void StopPlaying() //wyłacz utwór
    {
        timeToStop = smoothTime;
        stopPlaying = true;
    }
    public void PlayOnce(AudioClip audioClip, float SoundTime) //zagraj utwór
    {
            this.audioSource.clip = audioClip;
            this.audioSource.Play();
            OncePlayTime = SoundTime;
        

    }
}
