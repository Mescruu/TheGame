using UnityEngine;
using System.Collections;

public class Player_Controller : MonoBehaviour {

    public bool blockMove;

    public float wsp=0.05f;   //jako private

    public bool facingRight = true;

    public bool grounded = false;
    public bool UsingStamina=false;

    //how big the circle is going to be when wwe check disstance to the ground;
    //Sprint
    public bool canSprint;
    public bool sprint;
    public float sprintSpeed;
    public float basicSprintSpeed= 250f; //jako private
    //Wall Sliding
    public Transform wallCheckPoint;
    public LayerMask wallLayerMask;
    public bool wallCheck;
    public bool wallSliding;
    public bool sliding;
    public float wallBoucingForce;
    public Transform wallJumpTargetBasic;
    public Transform wallJumpTarget;
    private bool wallJumping;
    public float wallJumpingTimeCD;
    private float wallJumpingTime;
    //skakanie
    public float jumpForce;
    public float basicJumpForce = 150f;  //jako private
    public float JumpingTime;
    public float jumpingTimeCD=1f;
    public bool isJumping;
    public bool firstTime;
    public bool endJumping;
    public bool JumpfromSprint;
    public GameObject DustJump;
    public Transform DustPos;
    //bieganie
    public float moveSpeed;
    public float runSpeed;
    public float basicRunSpeed= 175f; //jako private
    public float currentSpeed;
    public float acceleration;
    //Leader
    public bool onLadder;
    public float leaderSpeed;
    private float climbVelocity;
    public float climbSpeed;
    public GameObject cape;
    //BackDash
    public bool backDash;
    public float DashSpeed;
    public float backDashTime;
    public float backDashTimeCD;
    public Transform dashPoint;
    public GameObject DashPrefab;
    public GameObject ParticleDust;
    //odwolania
    public float timeToStaminaRegeneration;
    public float timeToStaminaRegenerationCD;
    private float timeToStaminaRegenerationCDBasic = 2f;
    public float SpeedStaminaRegeneration;
    private float SpeedStaminaRegenerationBasic = 10f;

    public float timeToManaRegeneration;
    public float timeToManaRegenerationCD;
    private float timeToManaRegenerationCDBasic=5f;
    public float SpeedManaRegeneration;
    private float SpeedManaRegenerationBasic = 5f;
    public bool UsingMana;

    public float curHP;
    public float curMana;
    public int curStamina;
    public float maxHP;
    public float MaxMana;
    public bool dead;
    public float curSTM;
    public bool Defend;

    private KeyMenager keyMenager;
    private Game_Master gm;
    private Rigidbody2D rgb2d;
    private Animator anim;
    public PlayerAttack pLayerAttack;
    public AudioPlayer audioPlayer;
    public AudioClip runSound;
    public AudioClip sprintSound;
    public AudioClip dashSound;
    private AudioSource audioSource;
    public AudioClip newLevelSound;


    public int CriticRange;
    public DmgType[] dmgTypeSensitive;
    public DmgType[] dmgTypeHardness;
    public GameObject Blood;
    public GameObject BloodBack;
    public GameObject BloodDown;
    public Transform feet;
    private bool DeadFacingRight;
    private bool knockbacking;


    public bool onKnee;

    public GameObject BurnParticle;
    public float BurnTime;
    public float BurnTimeCD;
    public float BurnTimeCDBasic;

    public GameObject AcidParticle;
    public float PoisonTime;
    public float PoisonTimeCD;
    public float PoisonTimeCDBasic;

    public float DefendTimeCD;
    public float DefendTime;
    //Altar
    private int seconds;
    private bool invulnerable;
    private float invulnerableTimeBasic = 0.5f;
    private float invulnerableTimeCD;
    private float invulnerableTime;
    private bool getHurt;
    public Color DmgTxtColor;
    public Color CriticTxtColor;
    void Start () {
        getHurt = false;
        onKnee = false;
        knockbacking = false;
        Defend = false;
        rgb2d = gameObject.GetComponent<Rigidbody2D>();
		dead = false;
        cape.SetActive(false);

        keyMenager = GameObject.Find("KeyMenager").GetComponent<KeyMenager>();
        gm = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<Game_Master>();
        anim = gameObject.GetComponent<Animator>();
        pLayerAttack = gameObject.GetComponent<PlayerAttack>();
        audioPlayer = gameObject.GetComponent<AudioPlayer>();
        audioSource = gameObject.GetComponent<AudioSource>();

        //atck = gameObject.GetComponent<PlayerAttack> ();

        curHP = maxHP;
        curMana = MaxMana;


        //skakanie
        isJumping = false;
        JumpingTime = jumpingTimeCD;
    }

    void Update()
    {

        invulnerableTimeCD = invulnerableTimeBasic - gm.difficultLevel*0.1f;
        PoisonTimeCD = PoisonTimeCDBasic * gm.difficultLevel;
        BurnTimeCD= BurnTimeCDBasic + BurnTimeCDBasic * gm.difficultLevel/2;

        if (curHP> maxHP)
        {
            curHP = maxHP;
        }
        if (curMana >= MaxMana)
        {
            curMana = MaxMana;
        }

        if (curSTM >= 100)
            {
                curStamina = 10;
            }
         

            if (curSTM > 90 && curSTM < 100)
            {
                curStamina = 9;

            }
            if (curSTM > 80 && curSTM < 90)
            {
                curStamina = 8;

            }
            if (curSTM > 70 && curSTM < 80)
            {
                curStamina = 7;

            }
            if (curSTM > 60 && curSTM < 70)
            {
                curStamina = 6;

            }
            if (curSTM > 50 && curSTM < 60)
            {
                curStamina = 5;

            }
            if (curSTM > 40 && curSTM < 50)
            {
                curStamina = 4;

            }
            if (curSTM > 30 && curSTM < 40)
            {
                curStamina = 3;

            }
            if (curSTM > 20 && curSTM < 30)
            {
                curStamina = 2;

            }
            if (curSTM > 10 && curSTM < 20)
            {
                curStamina = 1;

            }
            if (curSTM < 10)
            {
                curStamina = 0;

            }


            if (curSTM < 100 && !UsingStamina)
            {
                if (timeToStaminaRegeneration <= 0)
                {
                    curSTM = curSTM + Time.deltaTime * SpeedStaminaRegeneration;
                }
                else
                {
                    timeToStaminaRegeneration -= Time.deltaTime;
                }
            }
            else
            {
                timeToStaminaRegeneration = timeToStaminaRegenerationCD;
            }


            if (!UsingMana && curMana < MaxMana)
            {
                if (timeToManaRegeneration <= 0)
                {
                    curMana = curMana + Time.deltaTime * SpeedManaRegeneration;
                }
                else
                {
                    timeToManaRegeneration -= Time.deltaTime;
                }
            }
            else
            {
                timeToManaRegeneration = timeToManaRegenerationCD;
            }


        //BackDash
        if (!blockMove)
        {
            if (Input.GetKeyDown(keyMenager.keys["BackDash"]) && grounded && !backDash && curSTM >= gm.BackDashCost && !pLayerAttack.SpecialAttacking)
            {
              //  Instantiate(DustDash, DustPos.position, transform.rotation);

                ParticleDust.GetComponent<ParticleSystem>().enableEmission = true;
                ParticleDust.transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y,1f);

                curSTM -= gm.BackDashCost;
                backDash = true;
                if (pLayerAttack.DeathMode)
                {
                    Instantiate(DashPrefab, transform.position, transform.rotation);

                }
                //	anim.SetBool ("BackDash", true);

            }
        }
         

            if (backDash && grounded)
            {


                UsingStamina = true;
                backDashTime -= Time.deltaTime;
                if (backDashTime < backDashTimeCD - 0.05f)
                {
                    anim.SetBool("BackDash", true);

                }
            if (backDashTime < backDashTimeCD / 3f)
            {
                ParticleDust.GetComponent<ParticleSystem>().enableEmission = false;
            }

            if (backDashTime > backDashTimeCD)
                {
                    transform.position = Vector3.MoveTowards(transform.position, dashPoint.transform.position, DashSpeed * Time.deltaTime);

                }
                if (backDashTime < backDashTimeCD)
                {
                    transform.position = Vector3.MoveTowards(transform.position, dashPoint.transform.position, DashSpeed * Time.deltaTime);

                }
                if (backDashTime < 0)
                {
                    backDash = false;
                }

            }
            else
            {
                backDashTime = backDashTimeCD;
                anim.SetBool("BackDash", false);
           
                ParticleDust.GetComponent<ParticleSystem>().enableEmission = false;
           
                backDash = false;


            }
            //
            // Leader
            if (onLadder && !dead && !grounded && !pLayerAttack.SpecialAttacking)
            {
                sprint = false;

                //	atck.AirSpecialAttack = false;
                moveSpeed = leaderSpeed;
                rgb2d.gravityScale = -0.00f;
                climbVelocity = climbSpeed * Input.GetAxis("Vertical");
                rgb2d.velocity = new Vector2(rgb2d.velocity.x * 0.85f, climbVelocity);
                anim.SetBool("Climbing", true);
                cape.SetActive(true);

            }
            if (!onLadder)
            {
                cape.SetActive(false);
                anim.SetBool("Climbing", false);

            }
            if (!onLadder && !sprint)
            {

                moveSpeed = runSpeed;
                rgb2d.gravityScale = 1f;


            }

            //LEadaer end

            //Wall Handle
            if (!grounded && !onLadder && !pLayerAttack.SpecialAttacking)
            {
                wallCheck = Physics2D.OverlapCircle(wallCheckPoint.position, 0.1f, wallLayerMask);

                if (facingRight && Input.GetAxis("Horizontal") > 0.1f || !facingRight && Input.GetAxis("Horizontal") < 0.1f)
                {
                    if (wallCheck)
                    {
                        HandleWallSliding();
                    }
                }
            }

            if (!wallSliding)
            {
                //	anim.SetBool ("WallSliding", wallSliding  && !dead);

            }
            if (sliding)
            {
                rgb2d.velocity = new Vector2(rgb2d.velocity.x, -1f);
            }
            else
            {

                rgb2d.velocity = new Vector2(rgb2d.velocity.x, rgb2d.velocity.y);
            }
            if (wallCheck == false || grounded)
            {
                wallSliding = false;
            }
            /////////////

            //Sprintowanie
            if (Input.GetKey(keyMenager.keys["Sprint"]) && grounded && Mathf.Abs(currentSpeed) > 2f && curSTM > 0f && canSprint)
            {
                sprint = true;
                UsingStamina = true;

                //		anim.SetBool ("Sprint", true);
            }
            if (!grounded || Mathf.Abs(currentSpeed) < 2f || curSTM <= 0f || Input.GetKeyUp(keyMenager.keys["Sprint"]))
            {
                sprint = false;
                //	anim.SetBool ("Sprint", false); 
            }

            if (curSTM <= 30)
            {
                canSprint = false;
            }
            ////////////////////////
            if (curSTM > 30)
            {
                canSprint = true;
            }



        if (!blockMove)
        {

            if (Input.GetKeyDown(keyMenager.keys["Jump"]) && grounded && sprint && !backDash)
            {
               // curSTM -= gm.JumpCost;
                UsingStamina = true;

                // rgb2d.velocity = new Vector2(rgb2d.velocity.x, jumpForce);
                JumpfromSprint = true;
                rgb2d.velocity = Vector2.up * jumpForce;
                endJumping = true;
                Debug.Log("is holded");
                Instantiate(DustJump, DustPos.position, transform.rotation);
            }
            if (Input.GetKeyDown(keyMenager.keys["Jump"]) && grounded && !sprint && !backDash)
            {
               // curSTM -= gm.JumpCost;
                UsingStamina = true;

                // rgb2d.velocity = new Vector2(rgb2d.velocity.x, jumpForce);
                JumpfromSprint = false;
                rgb2d.velocity = Vector2.up * jumpForce;
                endJumping = true;
                Debug.Log("is holded");
                Instantiate(DustJump, DustPos.position, transform.rotation);

            }
            if (Input.GetKeyDown(keyMenager.keys["Jump"]) && grounded && !sprint && backDash)
            {
            //    curSTM -= gm.JumpCost;
                UsingStamina = true;

                JumpfromSprint = false;
                if (facingRight)
                {
                    rgb2d.velocity = Vector2.up * jumpForce * 1.2f + Vector2.left * jumpForce;
                    Instantiate(DustJump, DustPos.position, transform.rotation);
                }
                else
                {
                    rgb2d.velocity = Vector2.up * jumpForce * 1.2f + Vector2.right * jumpForce;
                    Instantiate(DustJump, DustPos.position, transform.rotation);
                }
                endJumping = false;
                Debug.Log("is holded");

            }
      
            if (JumpingTime > 0 && Input.GetKey(keyMenager.keys["Jump"]) && !grounded && endJumping)
            {
                JumpingTime -= Time.deltaTime;
                curSTM -= Time.deltaTime*gm.JumpCost;
                rgb2d.velocity = Vector2.up * jumpForce;

            }
            if (Input.GetKeyUp(keyMenager.keys["Jump"]))
            {
                endJumping = false;
            }
        }
          if (grounded&&curSTM>5)
            {
               JumpingTime = jumpingTimeCD;
            }
            else
            {
                sprint = false;



            }

        if (!blockMove)
        {

            if (Input.GetKey(keyMenager.keys["Left"]) || Input.GetKey(keyMenager.keys["Right"]))
            {
                backDash = false;

                if (pLayerAttack.attacking && grounded)
                {
                    if (pLayerAttack.attackTimer > pLayerAttack.attackTimerCD * 0.7f)
                    {
                        moveSpeed = sprintSpeed*1.3f;
                    }
                    else
                    {
                        moveSpeed = runSpeed * 0.5f;
                    }
                }
                else
                {
                    if (sprint)
                    {
                        if (moveSpeed <= sprintSpeed)
                        {
                            moveSpeed += Time.deltaTime * acceleration;
                        }
                        else
                        {
                            moveSpeed = sprintSpeed;
                        }
                    }
                    else
                    {
                        if (moveSpeed <= runSpeed)
                        {
                            moveSpeed += Time.deltaTime * acceleration;
                        }
                        else
                        {
                            moveSpeed = runSpeed;
                        }
                    }
                    if (JumpfromSprint && !grounded)
                    {
                        moveSpeed = sprintSpeed;
                    }
                    if (!JumpfromSprint && !grounded)
                    {
                        moveSpeed = runSpeed;
                    }
                }
             
            }
            else
            {
                moveSpeed = 0;
            }
        }

        if (!blockMove)
        {
            if (wallJumping && Input.GetKey(keyMenager.keys["Jump"]) && wallJumpingTime > 0 && !grounded && !pLayerAttack.SpecialAttacking && !pLayerAttack.Throwing && !pLayerAttack.Rune)
            {
                wallJumpingTime -= Time.deltaTime;
                if (wallJumpTarget.position.x < transform.position.x)
                {
                    rgb2d.velocity = new Vector2(-wallBoucingForce, jumpForce / 2f);
                }
                else
                {
                    rgb2d.velocity = new Vector2(wallBoucingForce, jumpForce / 2f);

                }
                if (Input.GetKey(keyMenager.keys["Right"]))
                {
                    transform.localScale = new Vector3(1, 1, 1);
                }
                if (Input.GetKey(keyMenager.keys["Left"]))
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                }
                Debug.Log("Walljumping");
                // transform.position = Vector3.MoveTowards(transform.position, wallJumpTarget.position, wallBoucingForce * Time.deltaTime);
                firstTime = false;
            }
            else
            {
                wallJumping = false;
            }
        }

            currentSpeed = rgb2d.velocity.x;
            if (!backDash && !sprint && !pLayerAttack.attacking)
            {
                UsingStamina = false;

            }

            if (audioSource.isPlaying == false || audioSource.clip == null)
            {
                if (anim.GetCurrentAnimatorStateInfo(0).IsName("Player_Run"))
                {
                    audioPlayer.Play(runSound);
                }
                if (anim.GetCurrentAnimatorStateInfo(0).IsName("Player_sprint"))
                {
                    audioPlayer.Play(sprintSound);
                }
                if (anim.GetCurrentAnimatorStateInfo(0).IsName("BackDash_Player"))
                {
                    audioPlayer.Play(dashSound);
                }
            }
            if (gm.active_menu > 0 && audioSource.clip != null)
            {
                audioPlayer.StopPlaying();
            }






        anim.SetBool ("Sprint", sprint);
       anim.SetBool("WallSliding", wallSliding);
       anim.SetFloat ("Jumping",  rgb2d.velocity.y);
       anim.SetFloat("JumpingModule", Mathf.Abs(rgb2d.velocity.y));

        anim.SetBool("Grounded", grounded);
       anim.SetFloat("Speed", Mathf.Abs(rgb2d.velocity.x));

        if(onKnee && !pLayerAttack.attacking && !pLayerAttack.Throwing && !pLayerAttack.Rune &&! pLayerAttack.SpecialAttacking && Mathf.Abs(rgb2d.velocity.x)<3f&&grounded)
        {
            anim.SetBool("OnKnee", true);

        }
        else
        {
            onKnee = false;
            anim.SetBool("OnKnee", false);

        }


        if (curHP <= 0)
        {
            if(!dead)
            {
                DeadFacingRight = facingRight;
            }
            Die();
            dead = true;
        }
        else
        {
            if (gm.Player_Burn)
            {
                BurnTime -= Time.deltaTime;
                curHP -= Time.deltaTime * 5f * gm.difficultLevel / 2;
                for (int i = 0; i < BurnParticle.transform.GetChildCount(); i++)
                {
                    BurnParticle.transform.GetChild(i).GetComponent<ParticleSystem>().enableEmission = true;
                }
                if(BurnTime<=0)
                {
                    gm.Player_Burn = false;
                }
            }
            if (!gm.Player_Burn || curHP <= 0)
            {
                BurnParticle.SetActive(false);

                for (int i = 0; i < BurnParticle.transform.GetChildCount(); i++)
                {
                    BurnParticle.transform.GetChild(i).GetComponent<ParticleSystem>().enableEmission = false;
                }
            }

            if (gm.Player_Poisoned)
            {
                PoisonTime -= Time.deltaTime;
                curHP -= Time.deltaTime * 2;

                for (int i = 0; i < AcidParticle.transform.GetChildCount(); i++)
                {
                    AcidParticle.transform.GetChild(i).GetComponent<ParticleSystem>().enableEmission = true;
                }
                if (PoisonTime <= 0)
                {
                    gm.Player_Poisoned = false;
                }
            }
            if(!gm.Player_Poisoned||curHP<=0)
            {
                AcidParticle.SetActive(false);

                for (int i = 0; i < AcidParticle.transform.GetChildCount(); i++)
                {
                    AcidParticle.transform.GetChild(i).GetComponent<ParticleSystem>().enableEmission = false;
                }
            }
            if (getHurt)
            {
                if (invulnerableTime > 0)
                {
                    invulnerableTime -= Time.deltaTime;
                }
                else
                {
                    getHurt = false;
                }
            }
        }

    }
    void HandleWallSliding()
    {

        rgb2d.velocity = new Vector2(rgb2d.velocity.x, -0.7f);
        wallSliding = true;



        if (!blockMove)
        {
            if (Input.GetKeyDown(keyMenager.keys["Jump"]))
            {
                wallJumping = true;
                wallJumpTarget.transform.position = wallJumpTargetBasic.transform.position;
                wallJumpingTime = wallJumpingTimeCD;

            }
        }
    }
  
    void FixedUpdate()
    {
       

    
        //moving the player
       
        if(dead)
        {
            float h = 1;
            if (Input.GetKeyDown(keyMenager.keys["Left"]))
            {
                h = -1;
            }
            if (Input.GetKeyDown(keyMenager.keys["Right"]))
            {

                h = 1;

            }
        }
           

            Vector3 easeVeloity = rgb2d.velocity;
            //   easeVeloity.y = rgb2d.velocity.y;
            easeVeloity.z = 0.0f;
            easeVeloity.x *= 0.75f;

            //fake fricition


            if (grounded)
            {
                rgb2d.velocity = easeVeloity;
            }
        if(!blockMove)
        {

       
        if (!knockbacking && curHP>0)
        {

            if (grounded)
            {
                if (sprint)
                {


                    curSTM -= (Time.deltaTime * 25);
                    if (Input.GetKey(keyMenager.keys["Left"]))
                    {
                        rgb2d.velocity = new Vector2(-moveSpeed, rgb2d.velocity.y);

                        transform.localScale = new Vector3(-1, 1, 1);
                        if (!dead)
                        {
                            facingRight = false;

                        }
                    }
                    if (Input.GetKey(keyMenager.keys["Right"]))
                    {
                        rgb2d.velocity = new Vector2(moveSpeed, rgb2d.velocity.y);
                        transform.localScale = new Vector3(1, 1, 1);
                        if (!dead)
                        {
                            facingRight = true;

                        }
                    }


                }
                else
                {
                    if (Input.GetKey(keyMenager.keys["Left"]))
                    {
                        rgb2d.velocity = new Vector2(-moveSpeed, rgb2d.velocity.y);

                        transform.localScale = new Vector3(-1, 1, 1);
                        if (!dead)
                        {
                            facingRight = false;

                        }


                    }
                    else
                    {
                        if (Input.GetKey(keyMenager.keys["Right"]))
                        {
                            rgb2d.velocity = new Vector2(moveSpeed, rgb2d.velocity.y);
                            transform.localScale = new Vector3(1, 1, 1);
                            if (!dead)
                            {
                                facingRight = true;

                            }

                        }

                    }


                }
                if (Mathf.Abs(rgb2d.velocity.x) < 2f)
                {
                    firstTime = false;
                }
                else
                {
                    firstTime = true;
                }
            }
            else if (!wallJumping)
            {


                if (facingRight)
                {
                    if (Input.GetKey(keyMenager.keys["Left"]))
                    {
                        rgb2d.velocity = new Vector2(-moveSpeed * 0.5f, rgb2d.velocity.y);
                        transform.localScale = new Vector3(-1, 1, 1);
                        if (!dead)
                        {
                            facingRight = false;

                        }

                    }
                    if (firstTime)
                    {
                        if (Input.GetKey(keyMenager.keys["Right"]))
                        {
                            rgb2d.velocity = new Vector2(moveSpeed * 0.8f, rgb2d.velocity.y);
                            transform.localScale = new Vector3(1, 1, 1);
                            if (!dead)
                            {
                                facingRight = true;

                            }
                        }
                        if (Input.GetKeyUp(keyMenager.keys["Right"]))
                        {
                            firstTime = false;
                            JumpfromSprint = false;
                        }
                    }
                    else
                    {
                        if (Input.GetKey(keyMenager.keys["Right"]))
                        {
                            rgb2d.velocity = new Vector2(moveSpeed * 0.5f, rgb2d.velocity.y);
                            transform.localScale = new Vector3(1, 1, 1);
                            if (!dead)
                            {
                                facingRight = true;

                            }
                        }
                    }

                }
                else
                {
                    if (Input.GetKey(keyMenager.keys["Right"]))
                    {
                        rgb2d.velocity = new Vector2(moveSpeed * 0.5f, rgb2d.velocity.y);
                        transform.localScale = new Vector3(1, 1, 1);
                        if (!dead)
                        {
                            facingRight = true;

                        }
                    }

                    if (firstTime)
                    {
                        if (Input.GetKey(keyMenager.keys["Left"]))
                        {
                            rgb2d.velocity = new Vector2(-moveSpeed * 0.8f, rgb2d.velocity.y);
                            transform.localScale = new Vector3(-1, 1, 1);
                            if (!dead)
                            {
                                facingRight = false;

                            }
                        }
                        if (Input.GetKeyUp(keyMenager.keys["Left"]))
                        {
                            firstTime = false;
                            JumpfromSprint = false;

                        }
                    }
                    else
                    {
                        if (Input.GetKey(keyMenager.keys["Left"]))
                        {
                            rgb2d.velocity = new Vector2(-moveSpeed * 0.5f, rgb2d.velocity.y);
                            transform.localScale = new Vector3(-1, 1, 1);
                            if (!dead)
                            {
                                facingRight = false;

                            }
                        }
                    }




                }


            }

            //Sprint

        }

        }
    }

        public void getDmg(float Dmg, DmgType[] dmgType, float[] percentageDistribution,Vector3 point, Transform attackPos)
    {

        if (!getHurt)
        {

            bool dmgTaken = false;
            if (gm.difficultLevel == 1)
            {
                Dmg -= (Dmg * gm.PlayerArmor / 100f);
                Dmg -= Dmg * 0.2f;
            }
            if (gm.difficultLevel == 2)
            {
                Dmg -= (Dmg * gm.PlayerArmor / 100f);

            }
            if (gm.difficultLevel == 3)
            {
                Dmg -= (Dmg * gm.PlayerArmor / 100f);
                Dmg += Dmg * 0.1f;

            }
            if (gm.difficultLevel == 4)
            {
                Dmg -= (Dmg * gm.PlayerArmor / 100f);
                Dmg += Dmg * 0.25f;

            }

            if (Defend)
            {
                if (gm.PlayerWhiteLevel == 2)
                {
                    Dmg = Dmg * 0.8f;
                }
                if (gm.PlayerWhiteLevel == 3)
                {
                    Dmg = Dmg * 0.6f;
                }

                for (int i = 0; i < dmgType.Length; i++)
                {
                    if (dmgType[i] == DmgType.Meele)
                    {
                        if (gm.PlayerWhiteLevel == 2)
                        {
                            percentageDistribution[i] = 0.5f;
                        }
                        if (gm.PlayerWhiteLevel == 3)
                        {
                            percentageDistribution[i] = 0.3f;
                        }

                    }
                }
            }


            for (int i = 0; i < dmgType.Length; i++)
            {
                dmgTaken = false;

                for (int j = 0; j < dmgTypeSensitive.Length; j++)
                {
                    if (dmgType[i] == dmgTypeSensitive[j])
                    {
                        int Critic;
                        Critic = Random.Range(0, CriticRange);
                        float wounds;
                           
                        if (Critic == 1)
                        {
                            wounds = Mathf.Ceil(Random.Range(Dmg * percentageDistribution[i] * 3, Dmg * percentageDistribution[i] * 5f));
                            curHP -= wounds;
                            gm.dmgTxtController.CreateDmgTxt((wounds).ToString(), transform, dmgType[i], true, true);
                        }
                        else
                        {
                            wounds = Mathf.Ceil(Random.Range(Dmg * percentageDistribution[i] * 1f, Dmg * percentageDistribution[i] * 3f));
                            curHP -= wounds;
                            gm.dmgTxtController.CreateDmgTxt(wounds.ToString(), transform, dmgType[i], false,true);
                        }
                        dmgTaken = true;

                    }
                }
                for (int j = 0; j < dmgTypeSensitive.Length; j++)
                {
                    float wounds;

                    if (dmgType[i] == dmgTypeHardness[j])
                    {
                        int Critic;
                        Critic = Random.Range(0, CriticRange);
                        if (Critic == 1)
                        {
                            wounds = Mathf.Ceil(Random.Range(Dmg * percentageDistribution[i] * 1.5f, Dmg * percentageDistribution[i]) * 3);
                            curHP -= wounds;
                            gm.dmgTxtController.CreateDmgTxt(wounds.ToString(), transform, dmgType[i], true, true);
                        }
                        else
                        {
                            wounds = Mathf.Ceil(Random.Range(Dmg * percentageDistribution[i] * 0.25f, Dmg * percentageDistribution[i]));
                            curHP -= wounds;
                            gm.dmgTxtController.CreateDmgTxt(wounds.ToString(), transform, dmgType[i], false, true);

                        }
                        dmgTaken = true;
                    }
                }
                if (!dmgTaken)
                {
                    int Critic;
                    Critic = Random.Range(0, CriticRange);
                    float wounds;

                    if (Critic == 1)
                    {
                        wounds= Mathf.Ceil(Random.Range(Dmg * percentageDistribution[i] * 2, Dmg * percentageDistribution[i] * 4));
                        curHP -= wounds;
                        gm.dmgTxtController.CreateDmgTxt(wounds.ToString(), transform, dmgType[i], true, true);
                    }
                    else
                    {
                        wounds = (Mathf.Ceil(Random.Range(Dmg * percentageDistribution[i] * 0.5f, Dmg * percentageDistribution[i] * 2)));
                        curHP -= wounds;
                        gm.dmgTxtController.CreateDmgTxt(wounds.ToString(), transform, dmgType[i], false, true);
                    }

                }

                if (dmgType[i] == DmgType.Fire)
                {
                    if (Random.Range(0, 6 - gm.difficultLevel) == 0)
                    {
                        BurnTime = BurnTimeCD;
                        if (!gm.Player_Burn)
                        {
                            gm.AddEffect(0);
                            gm.Player_Burn = true;

                        }
                        BurnParticle.SetActive(true);
                        for (int k = 0; k < BurnParticle.transform.childCount; k++)
                        {
                            BurnParticle.transform.GetChild(k).GetComponent<ParticleSystem>().enableEmission = true;
                        }
                    }
                }
                if (dmgType[i] == DmgType.Poison)
                {

                    if (Random.Range(0, 4 - gm.difficultLevel) == 0)
                    {
                        PoisonTime = PoisonTimeCD;
                        if (!gm.Player_Poisoned)
                        {
                            gm.AddEffect(1);
                            gm.Player_Poisoned = true;
                        }
                        AcidParticle.SetActive(true);

                        for (int k = 0; k < AcidParticle.transform.childCount; k++)
                        {
                            AcidParticle.transform.GetChild(k).GetComponent<ParticleSystem>().enableEmission = true;
                        }
                    }
                }

            }

            if (attackPos.position.y < feet.position.y)
            {
                Instantiate(BloodDown, point, attackPos.transform.rotation);

            }
            else
            {
                if (transform.position.x > attackPos.transform.position.x)
                {
                    Instantiate(Blood, point, attackPos.transform.rotation);
                }
                else
                {
                    Instantiate(BloodBack, point, attackPos.transform.rotation);
                }
            }

            invulnerableTime = invulnerableTimeCD;
            getHurt =true;
        }
     



    }

    public void SetData(float Hp,float Mana,float Stamina)//Ustawiamy zdrowie mane i stamine trzeba zrobic funkcje w PlayerController  dane zmienic na data powyzej i gracz zmienic na Player.    {
    {
        curHP =Hp;
        curMana = Mana;
        curSTM = Stamina;
    }
    void Die()
	{
        for (int k = 0; k < BurnParticle.transform.childCount; k++)
        {
            BurnParticle.transform.GetChild(k).GetComponent<ParticleSystem>().enableEmission = false;
        }
        for (int k = 0; k < AcidParticle.transform.childCount; k++)
        {
            AcidParticle.transform.GetChild(k).GetComponent<ParticleSystem>().enableEmission = false;
        }

        curHP = 0f;
        runSpeed = 0f;
        sprintSpeed = 0f;
        jumpForce = 0f;
	    anim.SetBool ("Dead", true);
        gm.dead = true;
        DashSpeed = 0;

        if(DeadFacingRight)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);

        }

        //Restart


    }
    public void CutScene(GameObject cutScene)
    {
        Destroy(cutScene);
        anim.applyRootMotion = true;
        Debug.Log("true");
        anim.applyRootMotion = false;
        Debug.Log("false");
        anim.ApplyBuiltinRootMotion();

    }
    public void SetNumbers(int PlayerSpeed, int PlayerMagic,int PlayerArmor)
    {
        
        jumpForce = basicJumpForce + (PlayerSpeed * wsp *  basicJumpForce);

        runSpeed = basicRunSpeed+ (PlayerSpeed * wsp * basicRunSpeed);

        sprintSpeed = basicSprintSpeed + (PlayerSpeed * wsp * basicSprintSpeed);

        SpeedStaminaRegeneration = SpeedStaminaRegenerationBasic - gm.difficultLevel + gm.PlayerSwordLevel+(PlayerSpeed) / 2f;
        timeToStaminaRegenerationCD = timeToStaminaRegenerationCDBasic - gm.PlayerSpeed * 0.01f - gm.PlayerSwordLevel * 0.005f;

        SpeedManaRegeneration = SpeedManaRegenerationBasic - gm.difficultLevel + (gm.PlayerWhiteLevel+gm.PlayerFireLevel+gm.PlayerElectLevel+gm.PlayerDeathLevel)/4f + (PlayerMagic) / 2f;
        timeToManaRegenerationCD = timeToManaRegenerationCDBasic - gm.PlayerMagic * 0.01f - (gm.PlayerWhiteLevel + gm.PlayerFireLevel + gm.PlayerElectLevel + gm.PlayerDeathLevel) / 4f * 0.005f;


    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0.5f, 0.1f, 0.5f, 0.5f);
        Gizmos.DrawSphere(wallJumpTargetBasic.transform.position, 5f);//rysowanie kolka

        Gizmos.color = new Color(0.1f, 0.3f, 0.5f, 0.5f);
        Gizmos.DrawSphere(wallCheckPoint.transform.position, 1f);//rysowanie kolka

    }



}
