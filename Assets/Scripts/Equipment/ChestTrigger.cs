﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChestTrigger : MonoBehaviour {

    public ChestUI chestUI;
    private KeyMenager keyMenager;
    private Animator anim;
    public GameObject desk;
    public Text BackPicInputText;
    public bool visited;
    public GameObject particle;
    public AudioClip[] audio;
    public AudioSource audioSource;

    private bool Exit;
    private float ExitTimeCd = 1f;
    private float ExitTime;

    // Use this for initialization
    void Start () {
        anim = gameObject.GetComponent<Animator>();
        keyMenager = GameObject.Find("KeyMenager").GetComponent<KeyMenager>();
        desk.SetActive(false);
        audioSource = gameObject.GetComponent<AudioSource>();
        particle.SetActive(false);
        Debug.Log("setActive particle system false");

        if (visited == false)
        {
            anim.SetBool("NoVisited", false);
        }
    }

    // Update is called once per frame
    void Update () {

        if (Exit)
        {
            desk.GetComponent<Animator>().SetBool("disappear", true);

            ExitTime -= Time.deltaTime;
            if (ExitTime < 0)
            {
                desk.SetActive(false);
                Exit = false;
            }
        }
        else
        {
            ExitTime = 0;
        }
    }
  
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            audioSource.clip = audio[0];
            audioSource.Play();
            Exit = false;
        }
    }
        void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {

            if (chestUI)
            {
                desk.SetActive(true);
                BackPicInputText.text = "[" + keyMenager.keys["Action"] + "] To Open";
            }
            if (visited == false)
            {
                anim.SetBool("NoVisited", true);
                particle.SetActive(true);
            }

            anim.SetBool("Open", true);
          

        }
     


        if (col.CompareTag("Player"))
        {
            if (Input.GetKeyDown(keyMenager.keys["Action"]))
            {

                chestUI.OpenChest();
                visited = true;
                anim.SetBool("NoVisited", false);

                if (particle.active)
                {
                    particle.SetActive(false);
                }
            }
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
     

        if (col.CompareTag("Player"))
        {
            Exit = true;
            ExitTime = ExitTimeCd;
            anim.SetBool("Open", false);
            if (particle.active)
            {
                particle.SetActive(false);
            }
            audioSource.clip = audio[1];
            audioSource.Play();
        }
 

    }
}
