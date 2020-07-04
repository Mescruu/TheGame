using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour {




    //shooting
    
    public Transform firePoint;
    public GameObject[] knife;
    public bool Throwing;
    public int countOfKnife;

    public float ThrowingTimer = 0;
    public float ThrowingCd = 0.7f;

    public bool Rune;
    public float RuneTimer = 0;
    public float RuneTimerCD = 0.7f;
    public int countOfRune;

    private KeyMenager keyMenager;
    public bool ShowDesk;
    public float neededMana;

    //FIRE//	//FIRE//	//FIRE//	//FIRE//
    public GameObject FireBall;
    public GameObject BigFireBall;
    public Transform BigFireBallPoint;
    public GameObject BigFlame;
    public Transform BigFlameFront;

    //FIRE//	//FIRE//	//FIRE//	//FIRE//


    //ELECT//	//ELECT// 	//ELECT// 	//ELECT// 
    public GameObject Thunder;
    public Transform ThunderPoint;

    public GameObject Discharge;

    public GameObject SwwordElect;
    public Transform SwwordElectPoint;

    //ELECT//	//ELECT// 	//ELECT// 	//ELECT// 




    //DEATH//    	//DEATH//    	//DEATH//    
    public GameObject DeathBomb;
    public float basicCurseTime=10f;

    public GameObject DeathChop;
    public Transform DeathChopPoint;
    public float basicHpStealth=20f;

    public float DeathModeTimeCDBasic;
    public float DeathModeTimeCD;
    public float DeathModeTime;
    public bool DeathMode;
    public Transform DeathMaskPoint;
    public GameObject DeathMask;
    public AudioClip DeathModeSound;

    //DEATH//    	//DEATH//    	//DEATH//    



    //WHITE//  		//WHITE//  		//WHITE//  
    public GameObject Healing;
    public Color HpColor;
    public float AddHp=25;
    public GameObject StunGameObj;
    public Transform StunGamePoint;
    public float basicStunTime;

    public GameObject Shield;
    public Transform ShieldPoint;


    //WHITE//  		//WHITE//  		//WHITE//  

    //DASH SPECIAL ATTACK// 
    public float dashSpeed;
    public bool DashSpecialAttack;
    public float TimeDashAttack;
    public float TimeDashAttackCD=0.4f;
    private bool faceingRight;
    public float DashSpeciAttackDmg;
    public Transform DashSpecialAttackPoint;
    public bool attacking = false;
    //DASH SPECIAL ATTACK// 

    //DownSpecialAttack// 
    private float DownSpecialAttackTimer;
    private float DownSpecialAttackCD=3f;
    public float DownSpecialAttackDmg;
    public bool DownSpecialAttack;
    public float DownSpecialAttackSpeed;
    public Transform DownSpecialAttackPoint;
    //DownSpecialAttack// 

    //SpecialJumpAttackInAir//
    private bool SpecialJumpAttackInAir;
    public float SpecialJumpAttackTime;
    public float SpecialJumpAttackTimeCd;
    public float SpecialJumpAttackInAirDmg;
    //SpecialJumpAttackInAir//

    public bool SpecialAttacking;

    public float attackTimer;
    public float attackTimerCD;  //calm down ;>
    public float attackBreakTime;
    public float attackBreakTimeCD;
    public float AttackBreakTimeBasic;
    public int CountOFAttack;
    private bool addSwordEffect;



    private Animator anim;
    private Game_Master gm;
    private InvertMaterial invMat;
    public Rigidbody2D rb2d;
    private Player_Controller player;
    public GameObject AttackObj;
    private BoxCollider2D attackTrigger;
    private AttackTrigger atckTrig;
    public AudioClip[] SwordSound;
    public AudioClip InAirSwordSound;
    public AudioClip Falling;
    public AudioClip DashAttackSound;
    private AudioSource audioSource;
    private AudioPlayer audioPlayer;
    public AudioClip SwordElectSound;
    public AudioClip SmallFireBallSound;
    public AudioClip BigFireBallSound;
    public AudioClip DeathBombSound;
    public AudioClip StunBulletStart;

    void Awake()
    {

        audioPlayer = gameObject.GetComponent<AudioPlayer>();
        gm = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<Game_Master>();

        invMat = gameObject.GetComponent<InvertMaterial>();
        player = gameObject.GetComponent<Player_Controller>();
        anim = gameObject.GetComponent<Animator>();
        audioSource = gameObject.GetComponent<AudioSource>();
        attackTrigger = AttackObj.GetComponent<BoxCollider2D>();
        attackTrigger.enabled = false;
        atckTrig = AttackObj.GetComponent<AttackTrigger>();
        
        

    }

    void Start()
    {
        DashSpecialAttack = false;
        TimeDashAttack = TimeDashAttackCD;
        rb2d = gameObject.GetComponent<Rigidbody2D>();

        SpecialJumpAttackTime = SpecialJumpAttackTimeCd;
        keyMenager = GameObject.Find("KeyMenager").GetComponent<KeyMenager>();

        DeathModeTimeCD = (DeathModeTimeCDBasic*gm.PlayerMagic*0.1f+DeathModeTimeCDBasic);
        CountOFAttack = 0;
    }
    void Update()
    {
        countOfRune = gm.runes;
        if (gm.usedWeaponType == 0)
        {
            attackTimerCD = 0.5f;
        }
        if (gm.usedWeaponType == 1)
        {
            attackTimerCD = 0.5f-gm.usedWeaponType/5;
        }
        if (gm.usedWeaponType == 2)
        {
            attackTimerCD = 0.5f + gm.usedWeaponType / 10;
        }

        if (attackBreakTimeCD < 0.1f)
        {
            attackBreakTimeCD = 0.1f;
        }
        attackBreakTimeCD = (AttackBreakTimeBasic - (gm.PlayerSpeed * gm.PlayerSwordLevel)/100f+gm.usedWeaponType/10);

        //// RUNE /////      		//// RUNE /////      		//// RUNE /////      		//// RUNE /////      		//// RUNE /////      		//// RUNE /////   


        if (Input.GetKeyDown(keyMenager.keys["ReleaseMagic"]) && !player.dead && !Throwing && !player.onLadder && !player.wallSliding && countOfRune > 0 && !player.blockMove)
        {
            if (firePoint.position.x > transform.position.x)
            {
                player.facingRight = true;
            }
            else
            {
                player.facingRight = false;
            }
            if (player.curMana > neededMana && gm.Element>0)
                {

                    if (gm.Element == 1)
                    {

                        if (gm.powerLevel == 1)
                        {
                        if(gm.PlayerFireLevel >= 1)
                        {
                            Instantiate(FireBall, firePoint.position, firePoint.rotation);
                            audioPlayer.Play(SmallFireBallSound);
                            ReduceRuneAndMana(1);
                        }
                        else
                        {
                            gm.ShowInfo(2f, "You can't pronounce that spell");

                        }
                     

                        }
                        
                    if (gm.powerLevel == 2)
                        {
                        if(gm.PlayerFireLevel >= 2)
                        {
                            Instantiate(BigFireBall, BigFireBallPoint.position, BigFireBallPoint.rotation);
                            audioPlayer.Play(BigFireBallSound);

                            ReduceRuneAndMana(1);
                        }
                        else
                        {
                            gm.ShowInfo(2f, "You can't pronounce that spell");

                        }


                    }
                  
                    if (gm.powerLevel == 3)
                        {
                        if (gm.PlayerFireLevel >= 3)
                        {
                            Instantiate(BigFlame, BigFlameFront.position, BigFlameFront.rotation);
                            ReduceRuneAndMana(1);
                        }
                        else
                        {
                            gm.ShowInfo(2f, "You can't pronounce that spell");

                        }
                    

                        }
                 
                }
                if (gm.Element == 2)
                {

                    if (gm.powerLevel == 1)
                    {
                        if(gm.PlayerElectLevel >= 1)
                        {
                            Instantiate(Thunder, ThunderPoint.position, ThunderPoint.rotation);
                            ReduceRuneAndMana(2);
                        }
                        else
                        {
                            gm.ShowInfo(2f, "You can't pronounce that spell");

                        }
                      

                    }
                  
                    if (gm.powerLevel == 2)
                    {
                        if(gm.PlayerElectLevel >= 2)
                        {
                            Instantiate(Discharge, transform.position, transform.rotation,transform);
                            ReduceRuneAndMana(2);
                        }
                        else
                        {
                            gm.ShowInfo(2f, "You can't pronounce that spell");

                        }

                      

                    }
                    else
                    {
                        gm.ShowInfo(2f, "You can't pronounce that spell");

                    }
                    if (gm.powerLevel == 3)
                    {
                      
                        if(gm.PlayerElectLevel>=3)
                        {
                            Instantiate(SwwordElect, SwwordElectPoint.position, SwwordElectPoint.rotation);
                            ReduceRuneAndMana(3);
                            audioPlayer.Play(SwordElectSound);

                        }
                        else
                          {
                            gm.ShowInfo(2f, "You can't pronounce that spell");

                        }
                    }
                  
                }
                if (gm.Element == 3)
                {

                    if (gm.powerLevel == 1)
                    {
                        if(gm.PlayerDeathLevel >= 1)
                        {
                            Instantiate(DeathBomb, firePoint.position, transform.rotation);
                            ReduceRuneAndMana(3);
                            audioPlayer.Play(DeathBombSound);
                        }
                        else
                        {
                            gm.ShowInfo(2f, "You can't pronounce that spell");

                        }

                    }
                  
                    if (gm.powerLevel == 2)
                    {
                        if(gm.PlayerDeathLevel >= 2)
                        {
                            Instantiate(DeathChop, DeathChopPoint.position, DeathChopPoint.rotation);
                            ReduceRuneAndMana(3);
                        }
                        else
                        {
                            gm.ShowInfo(2f, "You can't pronounce that spell");

                        }

                    }
                  
                    if (gm.powerLevel == 3)
                    {
                        if(gm.PlayerDeathLevel >= 3 && !DeathMode)
                        {
                            DeathModeTime = DeathModeTimeCD;

                            Instantiate(DeathMask, firePoint.position, firePoint.rotation);
                            invMat.InvertTime = DeathModeTime;
                            invMat.playOnce = true;
                            invMat.playOnceMiddle = true;
                            invMat.invert = true;
                            if (!gm.Player_DeathMode)
                            {
                                gm.Player_DeathMode = true;
                                gm.AddEffect(2);
                            }
                            ReduceRuneAndMana(3);
                            DeathMode = true;
                            audioPlayer.Play(DeathModeSound);
                        }
                        else
                        {
                            gm.ShowInfo(2f, "You can't pronounce that spell");

                        }

                    }
                  
                }
                if (gm.Element == 4)
                {

                    if (gm.powerLevel == 1)
                    {
                       if(gm.PlayerWhiteLevel>=1)
                        {
                            if (player.curHP < player.maxHP)
                            {
                                float hp = Mathf.Ceil(Random.Range(gm.PlayerWhiteLevel * AddHp*0.7f + AddHp * 0.3f * gm.PlayerMagic, gm.PlayerWhiteLevel * AddHp + AddHp * 0.6f * gm.PlayerMagic));
                                gm.dmgTxtController.CreateHealTxt(hp.ToString(), player.transform, HpColor, false);
                                player.curHP += hp;
                                Instantiate(Healing, transform.position, transform.rotation, transform);
                                ReduceRuneAndMana(4);
                            }
                            else
                            {
                                gm.ShowInfo(2f, "You are in good health");
                            }
                        }
                        else
                        {
                            gm.ShowInfo(2f, "You can't pronounce that spell");

                        }


                    }
                   
                    if (gm.powerLevel == 2)
                    {
                        if(gm.PlayerWhiteLevel>=2)
                        {
                            Instantiate(Shield, ShieldPoint.position, ShieldPoint.rotation,transform);
                           
                            ReduceRuneAndMana(4);
                        }
                        else
                        {
                            gm.ShowInfo(2f, "You can't pronounce that spell");

                        }
                  

                    }
                   
                    if (gm.powerLevel == 3)
                    {
                        if (gm.PlayerWhiteLevel >= 3)
                        {

                            Instantiate(StunGameObj, StunGamePoint.position, StunGamePoint.rotation);
                            ReduceRuneAndMana(4);
                            audioPlayer.Play(StunBulletStart);

                        }
                        else
                        {
                            gm.ShowInfo(2f, "You can't pronounce that spell");

                        }
                    }
                   
                }
            }
                if(gm.Element==0)
            {
             
                    gm.ShowInfo(2f, "focus on one of the elements");
            
                
            }

                

        }
        else
        {
            anim.SetBool("Running", false);

        }

    if(DeathMode&&DeathModeTime>0)
        {
            DeathModeTime -= Time.deltaTime;
            atckTrig.DeathMode = true;

            if (DeathModeTime<=0)
            {
                DeathMode = false;
            }
        }
    if(!DeathMode)
        {
            gm.Player_DeathMode = false;

            atckTrig.DeathMode = false;
        }

   

   

        if (gm.Element == 1)
        {
            if (gm.powerLevel == 1)
            {
                neededMana = gm.NeededManaFire[0];
            }
            if (gm.powerLevel == 2)
            {
                neededMana = gm.NeededManaFire[1];
            }
            if (gm.powerLevel == 3)
            {
                neededMana = gm.NeededManaFire[2];
            }
        }


        if (gm.Element == 2)
        {
            if (gm.powerLevel == 1)
            {
                neededMana = gm.NeededManaElect[0];
            }
            if (gm.powerLevel == 2)
            {
                neededMana = gm.NeededManaElect[1];
            }
            if (gm.powerLevel == 3)
            {
                neededMana = gm.NeededManaElect[2];
            }
        }

        if (gm.Element == 3)
        {
            if (gm.powerLevel == 1)
            {
                neededMana = gm.NeededManaChaos[0];
            }
            if (gm.powerLevel == 2)
            {
                neededMana = gm.NeededManaChaos[1];
            }
            if (gm.powerLevel == 3)
            {
                neededMana = gm.NeededManaChaos[2];
            }
        }

        if (gm.Element == 4)
        {
            if (gm.powerLevel == 1)
            {
                neededMana = gm.NeededManaAnkh[0];
            }
            if (gm.powerLevel == 2)
            {
                neededMana = gm.NeededManaAnkh[1];
            }
            if (gm.powerLevel == 3)
            {
                neededMana = gm.NeededManaAnkh[2];
            }
        }

/*
        if (DeathMode)
        {
            gameObject.GetComponent<Animation>().Play("DeathModeStart");


            DeathModeTime -= Time.deltaTime;

            if (DeathModeTime <= 0)
            {
                gameObject.GetComponent<Animation>().Stop("DeathModeStart");


                gameObject.GetComponent<Animation>().Play("DeathModeEnd");

                DeathModeTime = DeathModeTimeCD;

                AttackTrigger.dmg = 2;


                DeathMode = false;

            }


        } */
   



        /////////knifethrowing//////////         			/////////knifethrowing//////////         			/////////knifethrowing//////////   
        if(!player.blockMove)
        {

            if (Input.GetKeyDown(keyMenager.keys["SpecialAttack"]) && !player.dead && player.grounded && !DashSpecialAttack && player.curSTM > gm.RunSpecialAttackCost && !Throwing && !Rune && player.sprint && !SpecialAttacking && !attacking && !player.onLadder && !player.wallSliding && gm.PlayerSwordLevel >= 3)
            {


                anim.SetBool("SpecialRunAttack", true);


                player.curSTM -= gm.RunSpecialAttackCost;
                gm.SwordUsedStamina -= gm.RunSpecialAttackCost;

                audioPlayer.Play(DashAttackSound);
                DashSpecialAttack = true;
                TimeDashAttack = TimeDashAttackCD;
                faceingRight = player.facingRight;
                atckTrig.SetDmg(DashSpeciAttackDmg);
                player.UsingStamina = true;

            }
            if (Input.GetKeyDown(keyMenager.keys["SpecialAttack"]) && !player.dead && player.grounded && player.sprint)
            {
                if (player.curSTM < gm.RunSpecialAttackCost)
                {
                    gm.ShowInfo(2f, "you don't have enought stamina points!");
                }
                if (gm.PlayerSwordLevel < 3)
                {
                    gm.ShowInfo(2f, "you can't do this!");
                }
            }


            if (DashSpecialAttack)
            {

                if (TimeDashAttack > 0)
                {
                    TimeDashAttack -= Time.deltaTime;

                    transform.position = Vector3.MoveTowards(transform.position, DashSpecialAttackPoint.transform.position, dashSpeed * Time.deltaTime);


                }
                else
                {
                    DashSpecialAttack = false;
                }
            }
            else
            {
                TimeDashAttack = TimeDashAttackCD;
                anim.SetBool("SpecialRunAttack", false);
            }


            if (Input.GetKeyDown(keyMenager.keys["SpecialAttack"]) && !player.dead && !player.grounded && player.curSTM > gm.JumpDownSpecialAttackCost && !attacking && !SpecialAttacking && !Throwing && !Rune && !player.onLadder && !player.wallSliding && gm.PlayerSwordLevel >= 2)
            {

                DownSpecialAttack = true;
                atckTrig.SetDmg(DownSpecialAttackDmg);
                anim.SetBool("SpecialJumpAttack", true);
                player.curSTM -= gm.JumpDownSpecialAttackCost;
                gm.SwordUsedStamina -= gm.JumpDownSpecialAttackCost;
                //audioPlayer.Play(Falling);

                player.UsingStamina = true;


            }
            if (Input.GetKeyDown(keyMenager.keys["SpecialAttack"]) && !player.dead && !player.grounded)
            {
                if (player.curSTM < gm.JumpDownSpecialAttackCost)
                {
                    gm.ShowInfo(2f, "you don't have enought stamina points!");
                }
                if (gm.PlayerSwordLevel < 2)
                {
                    gm.ShowInfo(2f, "you can't do this!");
                }
            }
            if (DownSpecialAttack)
            {


                if (DownSpecialAttackTimer < 0)
                {
                    if (!player.grounded)
                    {


                        if (rb2d.velocity.y < 40f)
                        {
                            transform.position = Vector3.MoveTowards(transform.position, DownSpecialAttackPoint.transform.position, DownSpecialAttackSpeed + Time.deltaTime * 10);
                        }
                        else
                        {
                            transform.position = Vector3.MoveTowards(transform.position, DownSpecialAttackPoint.transform.position, DownSpecialAttackSpeed);
                        }
                    }
                    else
                    {
                        audioSource.clip = SwordSound[(int)Random.Range(0, SwordSound.Length)];
                        audioSource.Play();
                        DownSpecialAttack = false;
                    }
                }
                else
                {
                    DownSpecialAttackTimer -= Time.deltaTime;

                }

            }
            else
            {
                anim.SetBool("SpecialJumpAttack", false);
            }


            if (Input.GetKeyDown(keyMenager.keys["BackDash"]) && !player.dead && !SpecialAttacking && !player.grounded && player.curSTM > gm.JumpDownSpecialAttackCost && !attacking && !Throwing && !Rune && !player.onLadder && !player.wallSliding && gm.PlayerSwordLevel >= 2)
            {

                SpecialJumpAttackInAir = true;
                atckTrig.SetDmg(SpecialJumpAttackInAirDmg);

                audioSource.clip = InAirSwordSound;
                audioSource.Play();

                player.curSTM -= gm.JumpSpecialAttackCost;
                gm.SwordUsedStamina -= gm.JumpSpecialAttackCost;
                SpecialJumpAttackTime = SpecialJumpAttackTimeCd;
                anim.SetBool("SpecialJumpAttackInAir", true);
                player.UsingStamina = true;

            }
            if (Input.GetKeyDown(keyMenager.keys["BackDash"]) && !player.grounded && !player.dead)
            {
                if (player.curSTM < gm.JumpSpecialAttackCost)
                {
                    gm.ShowInfo(2f, "you don't have enought stamina points!");
                }
                if (gm.PlayerSwordLevel < 1)
                {
                    gm.ShowInfo(2f, "you can't do this!");
                }
            }
            if (SpecialJumpAttackInAir)
            {
                if (SpecialJumpAttackTime <= 0 || player.grounded)
                {
                    audioPlayer.StopPlaying();
                    SpecialJumpAttackInAir = false;
                }
                if (SpecialJumpAttackTime > 0)
                {
                    SpecialJumpAttackTime -= Time.deltaTime;
                }

            }
            else
            {
                anim.SetBool("SpecialJumpAttackInAir", false);
            }



            if (Input.GetKeyDown(keyMenager.keys["Attack"]) && !player.dead && CountOFAttack <= gm.PlayerSwordLevel && !Throwing && !attacking && !player.backDash && !SpecialAttacking && !player.onLadder && !player.wallSliding && !SpecialAttacking && !player.sprint)
            {
                bool attack = false;
                if (player.currentSpeed > 10 && player.grounded)
                {
                    if (player.curSTM <gm.AttackRunCost)
                    {
                        gm.ShowInfo(2f, "you don't have enought stamina points!");
                    }
                    else
                    {
                        attack = true;
                        player.curSTM -= gm.AttackRunCost;
                        player.UsingStamina = true;

                    }

                }
                else
                {
                    if (player.curSTM < gm.AttackCost)
                    {
                        gm.ShowInfo(2f, "you don't have enought stamina points!");
                    }
                    else
                    {
                        attack = true;
                        player.curSTM -= gm.AttackCost;
                        player.UsingStamina = true;

                    }
                }
                if(attack)
                {
                    atckTrig.SetBasicDmg();
                    attacking = true;
                    attackTimer = attackTimerCD;
                    audioPlayer.Play(SwordSound[(int)Random.Range(0, SwordSound.Length)]);
                    CountOFAttack += 1;
                }

            }

            if (attacking)
            {
                if (attackTimer > 0)
                {
                    attackTimer -= Time.deltaTime;
                }
                else
                {
                    attackBreakTime = attackBreakTimeCD;
                    attacking = false;
                    attackTrigger.enabled = false;
                }

            }


            if (attackBreakTime <= 0)
            {
                CountOFAttack = 0;
                addSwordEffect = true;
            }
            else
            {
                if (addSwordEffect)
                {
                    gm.AddEffect(4);
                    addSwordEffect = false;
                }
                if (!attacking)
                {
                    attackBreakTime -= Time.deltaTime;
                }
            }

            anim.SetBool("Attacking", attacking);

            countOfKnife = gm.knifeCount;
            if (Input.GetKeyDown(keyMenager.keys["Throwing"]) && !player.dead && !SpecialAttacking && !player.backDash &&!Throwing && !player.sprint && !player.onLadder && !player.wallSliding && countOfKnife > 0)
            {
                if (firePoint.position.x > transform.position.x)
                {
                    player.facingRight = true;
                }
                else
                {
                    player.facingRight = false;
                }

                anim.SetBool("Throwing", true);

                Throwing = true;
                ThrowingTimer = ThrowingCd;
                Instantiate(knife[gm.knifeId], firePoint.position, firePoint.rotation);
                gm.ThrowKnife();
            }
            else
            {
                anim.SetBool("Throwing", false);

            }
            if (Input.GetKeyDown(keyMenager.keys["Throwing"]) && countOfKnife <= 0)
            {
                gm.ShowInfo(2f, "You don't have any knives!");
            }
            if (Throwing)
            {
                if (ThrowingTimer > 0)
                {
                    ThrowingTimer -= Time.deltaTime;
                }
                else
                {
                    Throwing = false;
                }

            }




            if (Input.GetKeyDown(keyMenager.keys["PowerMagic"]) && countOfRune <= 0 && !player.sprint)
            {
                gm.ShowInfo(2f, "You don't have any runes!");
            }
            if (Input.GetKeyDown(keyMenager.keys["ReleaseMagic"]) && player.curMana < neededMana)
            {
                gm.ShowInfo(2f, "you don't have enought mana points!");
            }

            if (Rune)
            {
                player.UsingMana = true;
                if (RuneTimer > 0)
                {
                    RuneTimer -= Time.deltaTime;
                }
                else
                {
                    Rune = false;
                }

            }
            else
            {
                player.UsingMana = false;
            }


            if (!SpecialJumpAttackInAir && !DownSpecialAttack && !DashSpecialAttack)
            {
                SpecialAttacking = false;
            }
            if (SpecialJumpAttackInAir || DownSpecialAttack || DashSpecialAttack)
            {
                SpecialAttacking = true;
            }


        }



        //	anim.SetBool ("Running", Run);

    }
    void ReduceRuneAndMana(int element)
    {
        gm.runes -= 1;
        player.curMana -= neededMana;
        anim.SetBool("Running", true);
        Rune = true;
        RuneTimer = RuneTimerCD;
        if(element==1)
        {
            gm.FireUsedMana += neededMana;
        }
        if (element == 2)
        {
            gm.ElectUsedMana += neededMana;
        }
        if (element == 4)
        {
            gm.AnkhUsedMana += neededMana;
        }
        if (element == 3)
        {
            gm.ChaosUsedMana += neededMana;
        }
    }
		//////////mele attacking////////           			//////////mele attacking////////           			//////////mele attacking////////           
	}
	