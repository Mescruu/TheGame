using UnityEngine;
using System.Collections;

public class FlyerEnemy : MonoBehaviour {


    public float Gravity;

    public bool boom;

	public bool attack;

	public bool NeedGroundToDie;

	private bool grounded;

	public GameObject Boom;

    public bool StopMove;
    public float normalMoveSpeed;
    public float normalMoveSpeedBasic;

    public float moveSpeed;
    public float acceleration;
    public float accelerationBasic;
    public float Speed;


    public bool canGoOut;

	public Transform GoOutPoint;
	public bool goOut;
	public float runningTimeCD;
    public float timetorunCD;
	public float timetorun;
    public float runningTime;
    public float runningTimeCDBasic;

    public float AttackRadius;
	public bool shouldattack;
    public bool StopToAttack;
    public float AttackBreakCD;
    public float AttackBreakCDBasic;
    private float AttackBreak;
    public float AttackDurationCD;
    private float AttackDuration;

    private bool attacking;
    public bool canAttack;


    public float PlayerRange;

	public LayerMask playerLayer;

	public bool facingAway;
	public bool followOnLookAway;

	public bool moveRight;




	public bool playerInRange;

    private Player_Controller player;
    public Animator anim;
    private Game_Master gm;
    private Enemy enemy;
    private Rigidbody2D rgb2;
    private AudioPlayer audioPlayer;

    public AudioClip[] AttackSound;
    public float attackSoundTime=0.4f;
    public int ChanceToRunOutWhileAttacking;

    private bool Active;
    public float ActiveRange;

    // Use this for initialization
    void Start () {

		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player_Controller> ();


		timetorun=timetorunCD;

		anim = gameObject.GetComponent<Animator> ();

		goOut=false;

		gm = GameObject.FindGameObjectWithTag ("GameMaster").GetComponent<Game_Master> ();

        enemy = gameObject.GetComponent<Enemy>();

        rgb2 = gameObject.GetComponent<Rigidbody2D>();

        audioPlayer = gameObject.GetComponent<AudioPlayer>();


	}

    // Update is called once per frame
    void Update() {


        AttackBreakCD = AttackBreakCDBasic * (1f - ((gm.difficultLevel - 1) * 2f) / 10f);


        normalMoveSpeed = normalMoveSpeedBasic + (acceleration / (7 - gm.difficultLevel));
        acceleration = accelerationBasic + accelerationBasic * gm.difficultLevel / 10f;
        runningTimeCD = runningTimeCDBasic+(runningTimeCDBasic * 2 / gm.difficultLevel)/5f;

        Active = Physics2D.OverlapCircle(transform.position, ActiveRange, playerLayer);

        if(Active && player.curHP>0)
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

        if (enemy.StunEnemy)
        {
            StopMove = true;

        } else {

      

            if (!goOut && playerInRange && enemy.CurrentHp > 0) {

                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, Speed * Time.deltaTime);

                runningTime = runningTimeCD;

            }

            if(canGoOut)
            {
                if (!attacking && goOut && playerInRange && enemy.CurrentHp > 0)
                {

                    runningTime -= Time.deltaTime;


                    transform.position = Vector3.MoveTowards(transform.position, GoOutPoint.transform.position, Speed * Time.deltaTime);

                }
                if (!attacking && runningTime > 0 && playerInRange)
                {
                    goOut = true;

                }
                if (timetorun >= 0)
                {
                    timetorun -= Time.deltaTime;
                    goOut = false;
                }
                else
                {
                    goOut = true;
                    if (runningTime <= 0)
                    {
                        timetorun = timetorunCD;
                        runningTime = runningTimeCD;
                    }
                }   
            }
        


            if (player.transform.position.x < transform.position.x) {

                moveRight = false;

            }
            if (player.transform.position.x > transform.position.x) {

                moveRight = true;

            }

            playerInRange = Physics2D.OverlapCircle(transform.position, PlayerRange, playerLayer);
            shouldattack = Physics2D.OverlapCircle(transform.position, AttackRadius, playerLayer);


            //Atttacking
       
            if (shouldattack)
            {
                if (StopToAttack)
                {
                    StopMove = true;
                }
                if (AttackBreak <= 0 && !goOut)
                {
                    attacking = true;
                }

            }

            if (StopToAttack && AttackBreak <= 0 && goOut)
            {
                StopMove = false;
            }

            if (AttackBreak > 0)
            {
                AttackBreak -= Time.deltaTime;
            }
            if (attacking && enemy.CurrentHp>0)
            {
                AttackBreak = AttackBreakCD;


                if (AttackDuration > 0 && !goOut)
                {
                    anim.SetBool("Attack", true);
                   
                    if(canGoOut && AttackDuration == AttackDurationCD)
                    {
                      if(Random.Range(0, ChanceToRunOutWhileAttacking)== 0)
                        {
                            timetorun = 0;
                            runningTime = runningTimeCD;
                        }
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
                anim.SetBool("Attack", false);
                AttackDuration = AttackDurationCD;

            }







            if ((player.transform.position.x < transform.position.x && player.transform.localScale.x < 0) || (player.transform.position.x > transform.position.x && player.transform.localScale.x > 0)) {
                facingAway = true;
            } else {
                facingAway = false;
            }

            //	if (playerInRange && facingAway) {

            //		transform.position = Vector3.MoveTowards (transform.position, thePlayer.transform.position, moveSpeed * Time.deltaTime);

            //	}

            if (moveRight) {
                if (enemy.CurrentHp > 0) {
                    transform.localScale = new Vector3(-1f, 1f, 1f); //zmiana kierunków
                }

            } else {
                if (enemy.CurrentHp > 0) {
                    transform.localScale = new Vector3(1f, 1f, 1f); //zmiana kierunków
                }

            }

        }



        grounded = enemy.Grounded;

    
            anim.SetBool("Grounded", grounded);
        


      

        }
        else
        {
            anim.SetBool("Attack", false);

        }


        if (enemy.CurrentHp <= 0)
        {

            moveSpeed = 0;
            rgb2.constraints = RigidbodyConstraints2D.FreezePositionX;
            rgb2.mass = 20;
            rgb2.gravityScale = Gravity;

            if (boom)
            {


                Instantiate(Boom, transform.position, transform.rotation);
                enemy.NeededGroundToDie = false;
                enemy.Destroy();

            }

        }

    }
            void OnDrawGizmosSelected()
            {

                Gizmos.DrawSphere (transform.position, PlayerRange);//rysowanie kolka
   
            }

}
