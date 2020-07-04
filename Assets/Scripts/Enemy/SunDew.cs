using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunDew : MonoBehaviour
{

    public float AttackBreakCDBasic;
    public float AttackBreakCD;
    private float AttackBreak;
    public float AttackDurationCD;
    private float AttackDuration;

    private bool attacking;

    public bool shouldattack_left;
    public bool shouldattack_right;

    public Transform attack1l;
    public Transform attack2l;
    public Transform attack1r;
    public Transform attack2r;

    public float ActiveRange;
    private bool Active;

    private Rigidbody2D rgb2d;
    private GameObject target;
    private Player_Controller player;
    private Game_Master gm;
    private Enemy enemy;
    private Animator anim;
    private AudioPlayer audioPlayer;
    public float attackSoundTime = 0.3f;
    public AudioClip[] AttackSound;
    public AudioClip Defendound;

    public LayerMask maskPlayer;

    private bool groundAttacking = false;
    private bool Defending = false;

    // Use this for initialization
    void Start()
    {


        gm = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<Game_Master>();

        anim = gameObject.GetComponent<Animator>();

        enemy = gameObject.GetComponent<Enemy>();
        audioPlayer = gameObject.GetComponent<AudioPlayer>();



        target = GameObject.FindGameObjectWithTag("Player");
        player = target.GetComponent<Player_Controller>();
        rgb2d = gameObject.GetComponent<Rigidbody2D>();


    }

    // Update is called once per frame
    void Update()
    {
        AttackBreakCD = AttackBreakCDBasic * (1f - ((gm.difficultLevel - 1) * 2f) / 10f);
        Active = Physics2D.OverlapCircle(transform.position, ActiveRange, maskPlayer);



        if (Active && player.curHP>0)
        {
            shouldattack_left = Physics2D.OverlapArea(attack1l.position, attack2l.position, maskPlayer);
            shouldattack_right = Physics2D.OverlapArea(attack1r.position, attack2r.position, maskPlayer);

            if (enemy.CurrentHp > 0)
            {

                if (shouldattack_right || shouldattack_left)
                {
                    if (AttackBreak <= 0 && !groundAttacking && !Defending && !attacking)
                    {
                        attacking = true;
                    }
                }
                if (AttackBreak > 0)
                {
                    AttackBreak -= Time.deltaTime;
                }
                if (attacking)
                {
                    AttackBreak = AttackBreakCD;


                    if (AttackDuration > 0 && !groundAttacking)
                    {
                        if (shouldattack_left)
                        {
                            anim.SetBool("AttackingLeft", true);
                        }
                        if (shouldattack_right)
                        {
                            anim.SetBool("AttackingRight", true);
                        }
                        if (audioPlayer.CanPlay)
                        {
                            audioPlayer.PlayOnce(AttackSound[(int)Random.Range(0, AttackSound.Length)], attackSoundTime);
                        }

                        AttackDuration -= Time.deltaTime;
                    }
                    else
                    {
                        AttackBreak = AttackBreakCD;
                        attacking = false;
                    }

                }
                else
                {
                    anim.SetBool("AttackingLeft", false);
                    anim.SetBool("AttackingRight", false);

                    AttackDuration = AttackDurationCD;
                }
            }
        }
        else
        {
            attacking = false;
            anim.SetBool("AttackingLeft", false);
            anim.SetBool("AttackingRight", false);
        }


    }
}