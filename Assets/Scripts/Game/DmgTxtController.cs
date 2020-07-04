    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DmgTxtController : MonoBehaviour
{
    public DmgTxt dmgTxtPrefab;
    public DmgTxt CriticTxtPrefab;
    public DmgTxt PlayerdmgTxtPrefab;
    public DmgTxt PlayerCriticTxtPrefab;
    public DmgTxt enhancementTxtPrefab;
    public DmgTxt CriticEnhancementTxtPrefab;

    private static GameObject canvas;

    public int Counter;
    public float Timer;
    private float TimerCD=1f;
    private bool active;
    private float[] Array; 
    void Start()
    {
        active = false;
        Timer = TimerCD;
        Counter = 0;
        Array = new float[] { -10, -5, 10, 5, 0, 0, 0};
    }
    void Update()
    {
        if (Timer > 0)
        {
            Timer -= Time.deltaTime;
            active = true;
        }
        else
        {
            active = false;
            Counter = 0;
        }
        if (Counter >= 4)
        {
            Counter = 0;
        }
    }
    public  void CreateDmgTxt(string txt, Transform location, DmgType dmgType, bool Critic, bool player)
    {
        if (Critic)
        {
            if(player)
            {
                DmgTxt instance = Instantiate(PlayerCriticTxtPrefab);
                Vector2 screenPos = new Vector2(location.position.x + Random.Range(Array[Counter], Array[Counter*2]), location.position.y + Random.Range(Array[Counter], Array[Counter * 2])); instance.transform.SetParent(transform.parent);
                instance.transform.position = screenPos;
                instance.SetTxt(txt, dmgType, true);
                Timer = TimerCD;
                Counter++;
            }
            else
            {
                DmgTxt instance = Instantiate(CriticTxtPrefab);
                Vector2 screenPos = new Vector2(location.position.x + Random.Range(Array[Counter], Array[Counter * 2]), location.position.y + Random.Range(Array[Counter], Array[Counter * 2])); instance.transform.SetParent(transform.parent);
                instance.transform.position = screenPos;
                instance.SetTxt(txt, dmgType, true);
                Timer = TimerCD;
                Counter++;

            }

        }
        else
        {
            if (player)
            {
                DmgTxt instance = Instantiate(PlayerdmgTxtPrefab);
                Vector2 screenPos = new Vector2(location.position.x + Random.Range(Array[Counter], Array[Counter * 2]), location.position.y + Random.Range(Array[Counter], Array[Counter * 2])); instance.transform.SetParent(transform.parent);
                instance.transform.position = screenPos;
                instance.SetTxt(txt, dmgType, false);
                Timer = TimerCD;
                Counter++;

            }
            else
            {
                DmgTxt instance = Instantiate(dmgTxtPrefab);
                Vector2 screenPos = new Vector2(location.position.x + Random.Range(Array[Counter], Array[Counter * 2]), location.position.y + Random.Range(Array[Counter], Array[Counter * 2])); instance.transform.SetParent(transform.parent);
                instance.transform.position = screenPos;
                instance.SetTxt(txt, dmgType, false);
                Timer = TimerCD;
                Counter++;

            }

        }
    }
    public void CreateHealTxt(string txt, Transform location, Color color, bool Critic)
    {
        txt = '+' + txt;
        if (Critic)
        {

            DmgTxt instance = Instantiate(CriticEnhancementTxtPrefab);
            Vector2 screenPos = new Vector2(location.position.x + Random.Range(Array[Counter], Array[Counter * 2]), location.position.y + Random.Range(Array[Counter], Array[Counter * 2])); instance.transform.SetParent(transform.parent);
            instance.transform.position = screenPos;
            instance.SetTxt(txt, color, false);
            Timer = TimerCD;
            Counter++;

        }
        else
        {
            DmgTxt instance = Instantiate(enhancementTxtPrefab);
            Vector2 screenPos = new Vector2(location.position.x + Random.Range(Array[Counter], Array[Counter * 2]), location.position.y + Random.Range(Array[Counter], Array[Counter * 2])); instance.transform.SetParent(transform.parent);
            instance.transform.position = screenPos;
            instance.SetTxt(txt, color, false);
            Timer = TimerCD;
            Counter++;

        }
    }
   }
