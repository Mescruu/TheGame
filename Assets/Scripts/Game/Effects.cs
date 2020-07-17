using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //można używać UI w skryptach 

public class Effects : MonoBehaviour {

    //Pojawiające się efekty
    public bool SwordStun;
    public bool Burn;
    public bool Poisoned;
    public bool Defend;
    public bool DeathMode;
    public Image Bar;

    //komponenty
    private Game_Master gm;
    private Player_Controller player;
    private PlayerAttack playerAtck;
    private Animator anim;

    void Start () {
        //komponenty
        gm = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<Game_Master>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Controller>();
        playerAtck = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAttack>();
        anim = gameObject.GetComponent<Animator>();
    }

    void Update () {
        //efekty pojawiające się na graczu (wyświetlane pod HUD - hp bar itd)
        if (SwordStun)
        {
            Bar.fillAmount = (playerAtck.attackBreakTime / playerAtck.attackBreakTimeCD);
            if (playerAtck.attackBreakTime <= 0)
            {
                End();
            }
        }
        if (Burn)
        {
            Bar.fillAmount = (player.BurnTime / player.BurnTimeCD);
            if(player.BurnTime<=0)
            {
                End();
            }
        }
        if (Poisoned)
        {
            Bar.fillAmount = (player.PoisonTime / player.PoisonTimeCD);
            if (player.PoisonTime <= 0)
            {
                End();
            }
        }
    
        if (DeathMode)
        {
            Bar.fillAmount = (playerAtck.DeathModeTime / playerAtck.DeathModeTimeCD);
            if (playerAtck.DeathModeTime <= 0)
            {
                End();
            }
        }
        if (Defend)
        {
            Bar.fillAmount = (player.DefendTime / player.DefendTimeCD);
            if (player.DefendTime <= 0)
            {
                End();
            }
        }

    }
    void End() //zniknięcie kwadracików
    {
        anim.SetBool("disappear", true);
        Destroy(gameObject, 0.5f);
    }
}
