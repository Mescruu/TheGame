using UnityEngine;
using System.Collections;

public class SlimeScript : MonoBehaviour
{



    //Poruszanie się - wartości
    public bool StopMove;

    public bool NormalMove;
    public float normalMoveSpeed;
    public float normalMoveSpeedBasic;

    public float acceleration = 1f;
    public float accelerationBasic;

    public float currentMoveSpeed;

    public float Speed;

    public bool turnAround;

    public bool moveRight;

    //Atak z ziemi
    public bool CanGroundAttack;
    public bool shouldGroundAttack;
    public Transform GroundAttackCheck1;
    public Transform GroundAttackCheck2;
    public float GroundAttackDurationCd;
    private float GroundAttackDuration;
    public float GroundAttackBreakCd;
    private float GroundAttackBreak;
    public float GroundAttackBreakCDBasic;

    public GameObject GroundAttackObjLeft;
    public GameObject GroundAttackObjRight;

    public Transform GroundAttackPosition;
    private bool groundAttacking;

    //Obrona obiektu
    public bool CanDefend;
    public bool ShouldDefend;
    private bool Defending;
    public float DefendTimeCD;
    private float DefendTime;
    public float DefendTimeBreakCD;
    public float DefendTimeBreak;
    private int SucceedDefend;
    public float reactionTimeCD;
    private float reactionTime;
    private bool Response;

    public Transform DefendCheck1;
    public Transform DefendCheck2;
    public LayerMask DefendLayerLowStuff;
    public LayerMask DefendLayerFastStuff;
    public bool PlayerCloseRange;
    public Transform CloseRange1;
    public Transform CloseRange2;



    //Walka
    public bool StopToAttack;
    public float AttackBreakCD;
    private float AttackBreak;
    public float AttackBreakCDBasic;

    public float AttackDurationCD;
    private float AttackDuration;

    private bool attacking;
    public bool shouldattack;
    public Transform attack1;
    public Transform attack2;

    //Aktywność
    public float ActiveRange;
    private bool Active;

    //Poruszanie się
    public LayerMask Walls;
    private bool hittingWall;
    public Transform wallCheck2;
    public Transform wallCheck1;

    public Transform EdgeCheck;
    public float EdgeRadius;
    public bool Edge;

    private bool grounded;

    public LayerMask maskPlayer;
    public float backRadius;
    public bool targetBehind;
    public Transform backProtecting1;
    public Transform backProtecting2;

    //Komponenty
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




    void Start()
    {
       //Początkowe wartości
        if (CanDefend)
        {
            ShouldDefend = false;
            Response = false;
            reactionTime = reactionTimeCD;
        }
        if (CanGroundAttack)
        {
            GroundAttackDuration = GroundAttackDurationCd;
            GroundAttackBreak = GroundAttackBreakCd;
            groundAttacking = false;
        }

        AttackDuration = AttackDurationCD;
        AttackBreak = AttackBreakCD;
        attacking = false;

        //Dołączenie komponentów
        gm = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<Game_Master>();
        anim = gameObject.GetComponent<Animator>();
        enemy = gameObject.GetComponent<Enemy>();
        audioPlayer = gameObject.GetComponent<AudioPlayer>();
        target = GameObject.FindGameObjectWithTag("Player");
        player = target.GetComponent<Player_Controller>();
        rgb2d = gameObject.GetComponent<Rigidbody2D>();
        NormalMove = true;
    }

    void Update()

    {
        //Poruszanie się i wyliczanie zmiennych na podstawie poziomu trudności
        currentMoveSpeed = Mathf.Abs(rgb2d.velocity.x);

        Active = Physics2D.OverlapCircle(transform.position, ActiveRange, maskPlayer);

        anim.SetFloat("Speed", Mathf.Abs(rgb2d.velocity.x));

        AttackBreakCD = AttackBreakCDBasic * (1f - ((gm.difficultLevel-1)*2f) / 10f);

        GroundAttackBreakCd = GroundAttackBreakCDBasic * (1f - ((gm.difficultLevel - 1) * 2f) / 10f);

        normalMoveSpeed = normalMoveSpeedBasic + (acceleration / (7 - gm.difficultLevel));
        acceleration = accelerationBasic + accelerationBasic * gm.difficultLevel / 10f;

        if (Active && player.curHP>0) //Jeżeli obiekt jest aktywny i gracz ma więcej niż 0 hp
        {
            if (StopMove)
            {
                Speed = 0;
            }
            if (!StopMove)
            {
                if (Speed < normalMoveSpeed)
                {
                    Speed += Time.deltaTime * acceleration;
                }
                else
                {
                    Speed = normalMoveSpeed;
                }
            }
            //Atak z ziemi//

            shouldattack = Physics2D.OverlapArea(attack1.position, attack2.position, maskPlayer);

            if (CanGroundAttack)
            {
                shouldGroundAttack = Physics2D.OverlapArea(GroundAttackCheck1.position, GroundAttackCheck2.position, maskPlayer);
            }
            targetBehind = Physics2D.OverlapArea(backProtecting1.position, backProtecting2.position, maskPlayer);
            grounded = enemy.Grounded;


            //Poruszanie się
            Edge = Physics2D.OverlapCircle(EdgeCheck.position, EdgeRadius, Walls);
            hittingWall = Physics2D.OverlapArea(wallCheck1.position, wallCheck2.position, Walls);
            if (!enemy.Dead && !enemy.StunEnemy)
            {
                if (hittingWall || !Edge)
                {
                    moveRight = !moveRight;
                }
                if (grounded)
                {
                    if (moveRight)
                    {
                        transform.localScale = new Vector3(1f, 1f, 1f); //zmiana kierunków
                        rgb2d.velocity = new Vector2(Speed, rgb2d.velocity.y);
                    }
                    else
                    {
                        transform.localScale = new Vector3(-1f, 1f, 1f); //zmiana kierunków
                        rgb2d.velocity = new Vector2(-Speed, rgb2d.velocity.y);
                    }
                    if (turnAround)
                    {
                        if (targetBehind && target.transform.position.x + 4 < transform.position.x)
                        {
                            moveRight = false;
                        }
                        if (targetBehind && target.transform.position.x - 4 > transform.position.x + 2)
                        {
                            moveRight = true;
                        }
                    }
                    if (shouldattack)
                    {
                        if (StopToAttack)
                        {
                            StopMove = true;
                        }
                        if (AttackBreak <= 0 && !groundAttacking && !Defending)
                        {
                            attacking = true;
                        }
                    }

                    if (StopToAttack && !CanGroundAttack && AttackBreak <= 0 && !Defending)
                    {
                        StopMove = false;
                    }

                    //Atakowanie
                    if (AttackBreak > 0)
                    {
                        AttackBreak -= Time.deltaTime;
                    }
                    if (attacking)
                    {
                        AttackBreak = AttackBreakCD;

                        if (AttackDuration > 0 && !groundAttacking)
                        {
                            anim.SetBool("Attack", true);
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
                        anim.SetBool("Attack", false);
                        AttackDuration = AttackDurationCD;
                    }
                }

                //Defensywa obiektu
                if (CanDefend)
                {
                    PlayerCloseRange = Physics2D.OverlapArea(CloseRange1.position, CloseRange2.position, maskPlayer);
                    if (Physics2D.OverlapArea(DefendCheck1.position, DefendCheck2.position, DefendLayerFastStuff))
                    {
                        if (Response)
                        {
                            if (Random.Range(0, SucceedDefend) == SucceedDefend)
                            {
                                ShouldDefend = true;
                                reactionTime = reactionTimeCD;
                            }
                            reactionTime = reactionTimeCD;
                            Response = false;
                        }
                        else
                        {
                            reactionTime -= Time.deltaTime;
                            if (reactionTime <= 0)
                            {
                                Response = true;
                            }
                        }
                    }
                    if (Physics2D.OverlapArea(DefendCheck1.position, DefendCheck2.position, DefendLayerLowStuff))
                    {
                        ShouldDefend = true;
                    }
                    if (!Physics2D.OverlapArea(DefendCheck1.position, DefendCheck2.position, DefendLayerLowStuff) && !Physics2D.OverlapArea(DefendCheck1.position, DefendCheck2.position, DefendLayerFastStuff))
                    {
                        ShouldDefend = false;
                    }
                    if (ShouldDefend)
                    {
                        if (DefendTimeBreak <= 0 && !attacking && !groundAttacking)
                        {
                            Defending = true;
                            StopMove = true;
                        }
                    }

                    if (DefendTimeBreak > 0)
                    {
                        if (PlayerCloseRange)
                        {
                            DefendTimeBreak -= Time.deltaTime;
                        }
                        else
                        {
                            DefendTimeBreak -= Time.deltaTime * 10f;
                        }
                    }

                    if (Defending)
                    {
                        enemy.defend = true;
                        DefendTimeBreak = DefendTimeBreakCD;
                        if (DefendTime > 0 && !groundAttacking)
                        {
                            anim.SetBool("Defend", true);
                            if (audioPlayer.CanPlay)
                            {
                                audioPlayer.PlayOnce(Defendound, DefendTime);
                            }
                            DefendTime -= Time.deltaTime;
                        }
                        else
                        {
                            DefendTimeBreak = DefendTimeBreakCD;
                            Defending = false;
                        }
                    }
                    else
                    {
                        anim.SetBool("Defend", false);
                        DefendTime = DefendTimeCD;
                        enemy.defend = false;
                    }
                }


                //Atak z ziemi
                if (CanGroundAttack)
                {
                    if (!attacking && !groundAttacking && !Defending)
                    {
                        StopMove = false;
                    }
                    if (shouldGroundAttack && !attacking && GroundAttackBreak <= 0)
                    {
                        if (GroundAttackBreak <= 0)
                        {
                            groundAttacking = true;
                        }
                    }

                    if (GroundAttackBreak > 0)
                    {

                        GroundAttackBreak -= Time.deltaTime;
                    }
                    if (groundAttacking)
                    {
                        if (GroundAttackDuration > 0)
                        {
                            anim.SetBool("GroundAttack", true);
                            GroundAttackDuration -= Time.deltaTime;
                            if (StopToAttack)
                            {
                                StopMove = true;
                            }
                        }
                        else
                        {
                            if (moveRight)
                            {
                                Instantiate(GroundAttackObjRight, GroundAttackPosition.position, transform.rotation);
                            }
                            else
                            {
                                Instantiate(GroundAttackObjLeft, GroundAttackPosition.position, transform.rotation);
                            }
                            GroundAttackBreak = GroundAttackBreakCd;
                            AttackBreak = AttackBreakCD;
                            groundAttacking = false;
                        }
                    }
                    else
                    {
                        anim.SetBool("GroundAttack", false);
                        GroundAttackDuration = GroundAttackDurationCd;
                    }
                }
            }
        }
        else //zatrzymanie animacji 
        {
            anim.SetBool("Attack", false);
            anim.SetBool("GroundAttack", false);
            anim.SetBool("Defend", false);
            rgb2d.velocity = new Vector2(0, rgb2d.velocity.y);
        }
    }

    //Rysowanie okręgów
    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0.5f, 0.1f, 0.5f, 0.5f);
        Gizmos.DrawSphere(transform.position, ActiveRange);//rysowanie kolka
 
        Gizmos.color = new Color(0.7f, 0.6f, 0.5f, 0.2f);
        Gizmos.DrawSphere(EdgeCheck.position, EdgeRadius);//rysowanie kolka
    }
}


