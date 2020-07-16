using UnityEngine;
using System.Collections;

public class SorcererScrpit : MonoBehaviour
{


    public bool NoticePlayer;

    public bool StopMove;

    //Zmienne dot poruszania się
    public float acceleration;
    public float accelerationBasic;

    public float Speed;
    public float normalMoveSpeed;
    public float normalMoveSpeedBasic;

    public bool canRunAway;
    private bool RunAway;
    public float runAwaySpeed;
    public float runAwaySpeedBasic;


    public bool CanAppearObj;
    public bool HaveToStopToAppearObj;

    //Odwracanie się za graczem jezeli jest za plecami
    public bool CanTurnAround;

    //Odwracanie się za graczem
    public bool CanBetterTurnAround;

    public bool CanTeleport; //Teleportacja obiektu
    public bool CanAppearMonster; //Tworzenie dodatkowych przeciwników
    public bool HaveToStopToAppearMonster; //Musi się zatrzymać żeby tworzyć dodatkowych przeciwników

    public float RunAwayRange;  //pole ucieczki
    public float playerCloseRadius; //sprawdzanie czy gracz jest blisko
    public float EdgeCheckRadius; //sprawdzanie czy jest krawędź 

    public bool HaveToStopToAttack; //czy musi się zatrzymać żeby zaatakować

    //Transform
    //punkty sprawdzające położenie względem innych obiektów
    public Transform wallCheck1; 
    public Transform wallCheck2;

    public Transform backProtecting1;
    public Transform backProtecting2;

    public Transform EdgeCheck;
    public Transform playerBack;

    //Pociski wystrzeliwane przez obiekt
    public GameObject granade;
    public GameObject bullet;
    public Transform firePos; //skąd obiekt strzela

    //Czasy ataku
    private bool attackOnce;

    public bool StopToAttack;
    public float AttackBreakCD;
    private float AttackBreak;
    public float AttackDurationCD;
    private float AttackDuration;
    public float AttackBreakCDBasic;

    private bool attacking;
    public bool shouldattack;
    public Transform attack1;
    public Transform attack2;

    //Czy obiekt "czaruje"
    public bool Spelling;

    //Tworzenie dodatokwych przeciwników//
    public GameObject AppearMonster;
    public float CanMonsterAppearRadius;
    public Transform[] AppearMonsterPoint;
    public float AppearMonsterRadius;
    public bool ShouldAppearMonster;

    public bool stoptoSpelling;

    public float AppearMonsterBreakCD;
    private float AppearMonsterBreak;
    public float AppearMonsterBreakCDBasic;

    public float AppearMonsterDurationCD;
    private float AppearMonsterDuration;

    //Pojawianie się obiektów//
    public GameObject AppearObject;
    public Transform CanObjectAppearCheck;
    public float CanObjectAppearRadius;

    public Transform[] ObjectPoint;
    public float AppearObjectRadius;
    public bool ShouldAppearObject;

    //Scripts rest
    private Animator anim;
    private Game_Master gm;
    private Player_Controller player;
    private Enemy enemy;
    private Rigidbody2D rgb2;

    //Boolean określający pozycję i możliwości obiektu
    private bool Active;
    private bool NotatEdge;
    private bool playerClose;
    private bool ableToRunAway;
    private bool hittingWall;
    private bool moveRight;
    private bool turnAround;
    private bool targetBehind;
    public float PlayerRange;

    //Warstwy
    public LayerMask maskPlayer;
    public LayerMask Walls;
    public LayerMask whatIsWall;

    public AudioClip[] AttackSound;
    public float attackSoundTime;
  
    private AudioPlayer audioPlayer;

    private bool AttackingStopMove;
    private bool SpellingStopMove;
    private bool RunAwayStopMove;

    void Start()
    {
        attackOnce = true;

        enemy = gameObject.GetComponent<Enemy>();
        gm = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<Game_Master>();
        anim = gameObject.GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Controller>();
        rgb2 = gameObject.GetComponent<Rigidbody2D>();
        audioPlayer = gameObject.GetComponent<AudioPlayer>();
    }

    void Update()
    {

        //Poruszanie się i wyliczanie zmiennych na podstawie poziomu trudności
        AttackBreakCD = AttackBreakCDBasic * (1f - ((gm.difficultLevel - 1) * 2f) / 10f);
        AppearMonsterBreakCD = AppearMonsterBreakCDBasic * (1f - ((gm.difficultLevel - 1) * 2f) / 10f);

        normalMoveSpeed = normalMoveSpeedBasic + (acceleration / (7 - gm.difficultLevel));
        acceleration = accelerationBasic + accelerationBasic * gm.difficultLevel / 10f;
        runAwaySpeed= runAwaySpeedBasic + (acceleration / (7 - gm.difficultLevel));

        anim.SetFloat("Speed", rgb2.velocity.x);

        Active = Physics2D.OverlapCircle(transform.position, PlayerRange, maskPlayer);

        if (enemy.StunEnemy || enemy.CurrentHp < 0) //zamrożenie akcji obiektu
        {
            rgb2.velocity = new Vector2(0, rgb2.velocity.y);
            rgb2.constraints = RigidbodyConstraints2D.FreezePositionX;
        }
        else if(Active && enemy.CurrentHp>0 && player.curHP>0)  //Jeżeli obiekt jest aktywny i gracz ma więcej niż 0 hp
        {
            if (enemy.turnAround)
            {
              moveRight = !moveRight;
                Speed = 0;
              enemy.turnAround = false;
            }

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            //Sprawdzanie pozycji względem innych obiektów
            targetBehind = Physics2D.OverlapArea(backProtecting1.position, backProtecting2.position, maskPlayer);
            playerClose = Physics2D.OverlapCircle(transform.position, playerCloseRadius, maskPlayer);
            NotatEdge = Physics2D.OverlapCircle(EdgeCheck.position, EdgeCheckRadius, whatIsWall);
            hittingWall = Physics2D.OverlapArea(wallCheck1.position, wallCheck2.position, whatIsWall);
            shouldattack = Physics2D.OverlapArea(attack1.position, attack2.position, maskPlayer);

            if (hittingWall || !NotatEdge)
            {
                moveRight = !moveRight;
                Debug.Log("NormalCheckTurnAround");
            }


            //Poruszanie się
            if (StopMove)
            {
                rgb2.constraints = RigidbodyConstraints2D.FreezePositionX;
                Speed = 0;
                rgb2.constraints = RigidbodyConstraints2D.FreezeRotation;
            }
            if (!StopMove)
            {
                rgb2.constraints = RigidbodyConstraints2D.None;
                rgb2.constraints = RigidbodyConstraints2D.FreezeRotation;

                if (ableToRunAway)
                {
                    if (Speed < runAwaySpeed)
                    {
                        Speed += Time.deltaTime * acceleration;
                    }
                    else
                    {
                        Speed = runAwaySpeed;
                    }
                }
                else
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
            }

            if (enemy.Grounded)
            {
                if(canRunAway==false || ableToRunAway==false)
                {
                    if (!attacking || !HaveToStopToAttack)
                    {
                        if (!Spelling)
                        {
                            StopMove = false;
                        }
                    }
                    if (moveRight)
                    {
                        transform.localScale = new Vector3(1f, 1f, 1f); //zmiana kierunków
                        rgb2.velocity = new Vector2(Speed, rgb2.velocity.y);
                    }
                    else
                    {
                        transform.localScale = new Vector3(-1f, 1f, 1f); //zmiana kierunków
                        rgb2.velocity = new Vector2(-Speed, rgb2.velocity.y);
                    }
                }
                if (canRunAway == true)
                {
                    ableToRunAway = Physics2D.OverlapCircle(transform.position, RunAwayRange, maskPlayer);
                    if(ableToRunAway)
                    {
                        if (Mathf.Abs(player.transform.position.x-transform.position.x)<RunAwayRange+10f && Mathf.Abs(player.transform.position.x - transform.position.x) > RunAwayRange - 10f)
                        { 
                           RunAwayStopMove = true;
                           RunAway = false;
                        }
                        else
                        {
                            RunAwayStopMove = false;
                            RunAway = true;
                        }
                    }
                    else
                    {
                        RunAway = false;
                        if (Mathf.Abs(player.transform.position.x - transform.position.x) > RunAwayRange + 10f)
                        {
                            RunAwayStopMove = false;
                        }
                    }

                    if (RunAway)
                    {
                        if (player.transform.position.x> transform.position.x)
                        {

                            transform.localScale = new Vector3(1f, 1f, 1f); //zmiana kierunków

                            rgb2.velocity = new Vector2(-Speed, rgb2.velocity.y);

                        }
                        if (player.transform.position.x< transform.position.x)
                        {
                            transform.localScale = new Vector3(-1f, 1f, 1f); //zmiana kierunków
                            rgb2.velocity = new Vector2(Speed, rgb2.velocity.y);

                        }
                    }
                }

                //Odwracanie się za graczem jezeli jest za plecami
                if (CanTurnAround)
                {
                    if (targetBehind && player.transform.position.x + 4 < transform.position.x)
                    {
                        moveRight = false;
                        Speed = 0;
                    }
                    if (targetBehind && player.transform.position.x - 4 > transform.position.x + 2)
                    {
                        moveRight = true;
                        Speed = 0;
                    }
                }

                //Odwracanie się
                if (CanBetterTurnAround)
                {
                    turnAround = false;
                    if(enemy.noticePlayer)
                    {
                        if ( player.transform.position.x + 4 < transform.position.x)
                        {
                            moveRight = false;
                        }

                        if (player.transform.position.x - 4 > transform.position.x + 2)
                        {
                            moveRight = true;
                        }
                    }
                }


                /////Atakowanie///
                if (shouldattack)
                {
                    if (StopToAttack)
                    {
                        AttackingStopMove = true;
                    }
                    if (AttackBreak <= 0 && !Spelling)
                    {
                        attacking = true;
                    }
                }
                if (StopToAttack && !Spelling && AttackBreak <= 0)
                {
                    AttackingStopMove = false;
                }
                if (AttackBreak > 0)
                {
                    AttackBreak -= Time.deltaTime;
                }
                if (attacking)
                {
                    AttackBreak = AttackBreakCD;
                    if (AttackDuration > 0 &&!Spelling)
                    {
                        anim.SetBool("Attack", true);
                        if(bullet!=null && attackOnce)
                        {
                            Instantiate(bullet, firePos.position, transform.rotation);
                            attackOnce = false;
                        }
                        if (granade!=null && attackOnce)
                        {
                            Instantiate(granade, firePos.position, transform.rotation);
                            attackOnce = false;
                        }
                        if (audioPlayer.CanPlay)
                        {
                            audioPlayer.PlayOnce(AttackSound[(int)Random.Range(0, AttackSound.Length)], attackSoundTime);
                        }
                        AttackDuration -= Time.deltaTime;
                    }
                    else
                    {
                        attackOnce = true;
                        AttackBreak = AttackBreakCD;
                        attacking = false;
                    }
                }
                else
                {
                    anim.SetBool("Attack", false);
                    AttackDuration = AttackDurationCD;
                }

                //Czarowanie
                if(CanAppearMonster)
                {
                    for(int i=0;i<AppearMonsterPoint.Length;i++)
                    {
                        ShouldAppearMonster = !Physics2D.OverlapCircle(AppearMonsterPoint[i].position, AppearMonsterRadius, whatIsWall);
                    }
                    if (ShouldAppearMonster)
                    {
                        if (stoptoSpelling)
                        {
                            SpellingStopMove = true;
                        }
                        if (AppearMonsterBreak <= 0 && !attacking)
                        {
                            Spelling = true;
                        }
                    }

                    if (stoptoSpelling && !attacking && AppearMonsterBreak <= 0)
                    {
                        SpellingStopMove = false;
                    }
                    if (AppearMonsterBreak > 0)
                    {
                        AppearMonsterBreak -= Time.deltaTime;
                    }
                    if (Spelling)
                    {
                        AppearMonsterBreak = AppearMonsterBreakCD;
                        if (AppearMonsterDuration > 0 && !attacking)
                        {
                            anim.SetBool("Magic", true);
                            if (AppearMonster != null && attackOnce)
                            {
                                for (int i = 0; i < AppearMonsterPoint.Length; i++)
                                {
                                    Instantiate(AppearMonster, AppearMonsterPoint[i].position, transform.rotation,transform.parent);
                                }
                                attackOnce = false;
                            }
                            if (audioPlayer.CanPlay)
                            {
                                audioPlayer.PlayOnce(AttackSound[(int)Random.Range(0, AttackSound.Length)], attackSoundTime);
                            }
                            AppearMonsterDuration -= Time.deltaTime;
                        }
                        else
                        {
                            attackOnce = true;
                            AppearMonsterBreak = AppearMonsterBreakCD;
                            Spelling = false;
                        }
                    }
                    else
                    {
                        anim.SetBool("Magic", false);
                        AppearMonsterDuration = AppearMonsterDurationCD;
                    }
                }
        }

            if(AttackingStopMove || RunAwayStopMove || SpellingStopMove)
            {
                StopMove = true;
            }
            if(!AttackingStopMove && !RunAwayStopMove && !SpellingStopMove)
            {
                StopMove = false;
            }
            if (enemy.CurrentHp <= 0)
            {
                anim.SetBool("Die", true);
                Speed = 0;
            }
        }
    }
    //Rysowanie okręgów
    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0.5f,0.1f,0.5f,0.5f);
        Gizmos.DrawSphere(transform.position, PlayerRange);//rysowanie kolka

        Gizmos.color = new Color(0.1f, 0.3f, 0.5f, 0.5f);
        Gizmos.DrawSphere(transform.position, playerCloseRadius);//rysowanie kolka

        Gizmos.color = new Color(0.7f, 0.6f, 0.5f, 0.2f);
        Gizmos.DrawSphere(EdgeCheck.position, EdgeCheckRadius);//rysowanie kolka

    }
}
