﻿using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShowInfo : MonoBehaviour
{
    public GameObject InfoDesk;

    public GameObject[] Pattern;

    private bool isOver = false;

    private Animator animator;

    private SkillUI skillUI;

    public Color32 FireColor;
    public Color32 ElectColor;
    public Color32 ChaosColor;
    public Color32 AnkhColor;
    public Color32 SwordColor;

    public Text text;

    // Use this for initialization
    void Start () {
     //   InfoDesk.SetActive(false);
       animator = InfoDesk.GetComponent<Animator>();
       InfoDesk.SetActive(false);
       skillUI = GameObject.Find("SkillUI").GetComponent<SkillUI>();

    }
    void Update()
    {
        if (isOver)
        {
            animator.SetBool("Show", true);
        }
        else
        {
            animator.SetBool("Show", false);
        }
        if (!Pattern[0].activeSelf&& !Pattern[1].activeSelf && !Pattern[2].activeSelf && !Pattern[3].activeSelf && !Pattern[4].activeSelf)
        {
            InfoDesk.SetActive(false);
            Debug.Log("paterny wylaczone");
        }
    }

    public void WhenMouseOver()
    {
        if (skillUI.fire)
        {
            text.text = "Fire Element";
            text.color = FireColor;
        }
        if (skillUI.elect)
        {
            text.text = "Electricity Element";
            text.color = ElectColor;

        }
        if (skillUI.ankh)
        {
            text.text = "ankh Element";
            text.color = AnkhColor;

        }
        if (skillUI.chaos)
        {
            text.text = "chaos Element";
            text.color = ChaosColor;

        }
        if (skillUI.sword)
        {
            text.text = "melee fighting";
            text.color = SwordColor;

        }

        Debug.Log("Mouse enter");
        isOver = true;
        if (!InfoDesk.activeSelf)
        {
            InfoDesk.SetActive(true);
        }
        // InfoDesk.SetActive(true);
    }

    public void WhenMouseExit()
    {
        isOver = false;
        Debug.Log("Mouse exit");
    }
}
