using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //ilosc expa za pokonanie
    public int Exp;
    
    //loot jaki wypada z przeciwnika
    public GameObject Loot;

    //słabości i odporność na typ obrażeń
    public DmgType[] dmgTypeSensitive;
    public DmgType[] dmgTypeHardness;

    public float CurrentHp;
    public float MaxHp;

    public bool Grounded;
    public bool NeededGroundToDie;
    public float TimeToDead;
    private float timeToDieCD;
    public bool Dead;
    private bool DieOnce;

    //Animator i krew
    private Animator anim;
    public GameObject blood;
    public GameObject bloodBack;

    //Stun obiektu
    public bool StunEnemy;
    public GameObject StunStar;
    private float StunTime;

    //Klątwa
    public bool CurseEnemy;
    public GameObject CurseParticle;
    private float CurseTime;
    private int MeanOfDeath; // 0 podatny // 1  obojetny //2 odporny

    //Płomień - podpalenie
    public bool BurnEnemy;
    public GameObject BurnParticle;
    private float BurnTime;
    public float BurnTimeCd;
    private int MeanOfBurn; // 0 podatny // 1  obojetny //2 odporny


    private PlayerAttack playertck;
    private Game_Master gm;
    public string StunBulletTag = "StunBullet";
    public string DeathBombTag = "DeathBomb";
    private SpriteRenderer spriteRenderer;
    public Color curseColor;
    public Color NormalColor;
    public Color BurnColor;
    public AudioSource audioSource;

    //Wspolczynnik odporności
    public float MeeleDefendFactor;
    private float MeeleDefendFactorVariable;
    public float MagicDefendFactor;
    private float MagicDefendFactorVariable;

    //Obrona obiektu
    public bool defend;
    public GameObject defendParticle;
    public Transform DefendPoint;
    private Rigidbody2D rgb2;

    //Zauważenie gracza
    public bool turnAround;

    public bool noticePlayer;
    private bool noticePlayerCheck;
    public Transform NoticePlayerPos1;
    public Transform NoticePlayerPos2;
    public LayerMask playerMask;

    public float focusOnPlayerTimeBasic;
    public float focusOnPlayerTimeCd;
    private float focusOnPlayerTime;
    public GameObject PdBullet;

    //Nietykalność
    private float invulnerableTimeBasic = 0.2f;
    private float invulnerableTimeCD;
    private float invulnerableTime;
    private bool getHurt;

    public Color[] DmgColor;
    public int CriticRange=50;
    // Use this for initialization
    void Start()
    {
        //Zainicjowanie początkowych wartości i dodanie komponentów
        getHurt = false;
        timeToDieCD = TimeToDead;
        noticePlayer = false;

        if (audioSource == null)
        {
            audioSource = gameObject.GetComponentInChildren<AudioSource>();
        }
        gm = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<Game_Master>();
        rgb2 = gameObject.GetComponent<Rigidbody2D>();
        playertck = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAttack>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        defend = false;
        StunEnemy = false;
        CurseEnemy = false;
        BurnEnemy = false;
        CurrentHp = MaxHp+MaxHp*(gm.difficultLevel-2f)*0.1f;
        anim = GetComponent<Animator>();
        MeanOfDeath = 1;
        MeanOfBurn = 1;
        DieOnce = true;

        //Ustalenie w jakim stopniu i na co podatny jest obiekt
        for (int i = 0; i < dmgTypeSensitive.Length; i++)
        {
            if (dmgTypeSensitive[i] == DmgType.Chaos)
            {
                MeanOfDeath = 0;
            }
            if (dmgTypeSensitive[i] == DmgType.Fire)
            {
                MeanOfBurn = 0;
            }
        }

        //Ustalenie w jakim stopniu i na co odporny jest obiekt
        for (int i = 0; i < dmgTypeHardness.Length; i++)
        {
            if (dmgTypeHardness[i] == DmgType.Chaos)
            {
                MeanOfDeath = 2;
            }
            if (dmgTypeHardness[i] == DmgType.Fire)
            {
                MeanOfBurn = 2;
            }

        }
    }

    void Update()
    {
        //Zauważanie gracza
        focusOnPlayerTimeCd = focusOnPlayerTimeBasic + (focusOnPlayerTimeBasic * gm.difficultLevel) / 5f;
        noticePlayerCheck = Physics2D.OverlapArea(NoticePlayerPos1.position, NoticePlayerPos2.position, playerMask);

        if(noticePlayerCheck)
        {
            focusOnPlayerTime=focusOnPlayerTimeCd;
            noticePlayer = true;
        }
        if(noticePlayer)
        {
            if(focusOnPlayerTime>0)
            {
                focusOnPlayerTime -= Time.deltaTime;
            }
            else
            {
                noticePlayer = false;
            }
        }
        //Otrzymywanie obrażeń
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

        //W razie śmierci
        if (CurrentHp <= 0)
        {
            StunEnemy = false;
            CurseEnemy = false;
            Dead = true;

            if (BurnEnemy)
            {
                for (int i = 0; i < BurnParticle.transform.GetChildCount(); i++)
                {
                    BurnParticle.transform.GetChild(i).GetComponent<ParticleSystem>().enableEmission = false;
                }
            }
                Destroy();
        }
        else // Jeżeli obiekt żyje
        {
            if (defend)
            {
                MeeleDefendFactorVariable = MeeleDefendFactor;
                MagicDefendFactorVariable = MagicDefendFactor;

            }
            else
            {
                MeeleDefendFactorVariable = 1;
                MagicDefendFactorVariable = 1;
            }
            if (BurnEnemy)
            {
                Burning();
            }
            else
            {
                BurnParticle.SetActive(false);
            }

            if (!BurnEnemy && !CurseEnemy)
            {
                spriteRenderer.color = NormalColor;
            }

            if (StunEnemy && CurrentHp>0)
            {
                StunTime -= Time.deltaTime;
                if (StunTime <= 0)
                {
                    StunEnemy = false;
                }
            }
            else
            {
                StunStar.SetActive(false);
            }
            if (CurseEnemy)
            {
                spriteRenderer.color = curseColor;
                CurseTime -= Time.deltaTime;

                if (MeanOfDeath == 1)
                {
                    CurrentHp -= (Time.deltaTime * gm.PlayerDeathLevel * gm.PlayerMagic) / 5f;
                }
                if (MeanOfDeath == 0)
                {
                    CurrentHp -= (Time.deltaTime * gm.PlayerDeathLevel * gm.PlayerMagic) * 2 / 5f;
                }
                if (MeanOfDeath == 2)
                {
                    CurrentHp -= (Time.deltaTime * gm.PlayerDeathLevel * gm.PlayerMagic) * 0.5f / 5f;
                }

                if (CurseTime <= 0.5f | CurrentHp <= 0)
                {
                    for (int k = 0; k < BurnParticle.transform.childCount; k++)
                    {
                        CurseParticle.transform.GetChild(k).GetComponent<ParticleSystem>().enableEmission = false;
                    }
                }

                if (CurseTime > 0.5f && CurrentHp>0)
                {
                    for (int k = 0; k < BurnParticle.transform.childCount; k++)
                    {
                        CurseParticle.transform.GetChild(k).GetComponent<ParticleSystem>().enableEmission = true;

                    }
                }

                if (CurseTime <= 0)
                {
                    CurseEnemy = false;
                }
            }
            else
            {
                CurseParticle.SetActive(false);
            }
        }
    }

    public void CheckYourBack() //Funkcja odpowiadająca za wywołanie obrotu obiektu
    {
        turnAround = true;
    }

    //Otrzymywanie obrażeń
    public void getDmg(float Dmg, DmgType[] dmgType, float[] percentageDistribution)
    {
        if (!getHurt)
        {
            bool dmgTaken = false;
            if (defend)
            {
                Instantiate(defendParticle, DefendPoint.position, transform.rotation);
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
                    
                        if (dmgType[i] == DmgType.Meele)
                        {
                            if (Critic == 1)
                            {
                                wounds = Mathf.Ceil(Random.Range(Dmg * percentageDistribution[i] * 3f * MeeleDefendFactorVariable, Dmg * percentageDistribution[i] * 5f * MeeleDefendFactorVariable));
                                CurrentHp -= wounds;
                                gm.dmgTxtController.CreateDmgTxt(wounds.ToString(), transform, dmgType[i], true, false);
                            }
                            else
                            {
                                wounds = Mathf.Ceil(Random.Range(Dmg * percentageDistribution[i] * 1f * MeeleDefendFactorVariable, Dmg * percentageDistribution[i] * 3f * MeeleDefendFactorVariable));
                                CurrentHp -= wounds;
                                gm.dmgTxtController.CreateDmgTxt(wounds.ToString(), transform, dmgType[i], false, false);
                            }
                           
                        }
                        else
                        {
                            if (Critic == 1)
                            {
                                wounds = Mathf.Ceil(Random.Range(Dmg * percentageDistribution[i] * 3f * MagicDefendFactorVariable, Dmg * percentageDistribution[i] * 5f * MagicDefendFactorVariable));
                                CurrentHp -= wounds;
                                gm.dmgTxtController.CreateDmgTxt(wounds.ToString(), transform, dmgType[i], true, false);
                            }
                            else
                            {
                                wounds = Mathf.Ceil(Random.Range(Dmg * percentageDistribution[i] * 1 * MagicDefendFactorVariable, Dmg * percentageDistribution[i] * 3f * MagicDefendFactorVariable));
                                CurrentHp -= wounds;
                                gm.dmgTxtController.CreateDmgTxt(wounds.ToString(), transform, dmgType[i], false, false);
                            }
                        }
                        dmgTaken = true;
                    }
                    if (dmgType[i] == DmgType.Fire)
                    {
                        if (Random.Range(0, gm.difficultLevel + 1) == 1)
                        {
                            BurnTime = BurnTimeCd;
                            BurnEnemy = true;
                            for (int k = 0; k < BurnParticle.transform.childCount; k++)
                            {
                                BurnParticle.transform.GetChild(k).GetComponent<ParticleSystem>().enableEmission = true;
                            }
                        }
                    }
                }

                for (int j = 0; j < dmgTypeHardness.Length; j++)
                {
                    int Critic;
                    Critic = Random.Range(0, CriticRange);
                    float wounds;

                    if (dmgType[i] == dmgTypeHardness[j])
                    {
                        if (dmgType[i] == DmgType.Meele)
                        {
                            if (Critic == 1)
                            {
                                wounds = Mathf.Ceil(Random.Range(Dmg * percentageDistribution[i] * 1.5f * MeeleDefendFactorVariable, Dmg * percentageDistribution[i] * 3f * MeeleDefendFactorVariable));
                                CurrentHp -= wounds;
                                gm.dmgTxtController.CreateDmgTxt(wounds.ToString(), transform, dmgType[i], true, false);
                            }
                            else
                            {
                                wounds = Mathf.Ceil(Random.Range(Dmg * percentageDistribution[i] * 0.25f * MeeleDefendFactorVariable, Dmg * percentageDistribution[i] * MeeleDefendFactorVariable));
                                CurrentHp -= wounds;
                                gm.dmgTxtController.CreateDmgTxt(wounds.ToString(), transform, dmgType[i], false, false);
                            }
                        }
                        else
                        {
                            if (Critic == 1)
                            {
                                wounds = Mathf.Ceil(Random.Range(Dmg * percentageDistribution[i] * 1.5f * MagicDefendFactorVariable, Dmg * percentageDistribution[i] * 3f * MagicDefendFactorVariable));
                                CurrentHp -= wounds;
                                gm.dmgTxtController.CreateDmgTxt(wounds.ToString(), transform, dmgType[i], true, false);
                            }
                            else
                            {
                                wounds = Mathf.Ceil(Random.Range(Dmg * percentageDistribution[i] * 0.25f * MagicDefendFactorVariable, Dmg * percentageDistribution[i]  * MagicDefendFactorVariable));
                                CurrentHp -= wounds;
                                gm.dmgTxtController.CreateDmgTxt(wounds.ToString(), transform, dmgType[i], false, false);
                            }
                        }

                        dmgTaken = true;
                    }
                    if (dmgType[i] == DmgType.Fire)
                    {
                        if (Random.Range(0, gm.difficultLevel + 9) == 1)
                        {
                            BurnTime = BurnTimeCd;
                            BurnEnemy = true;
                            for (int k = 0; k < BurnParticle.transform.childCount; k++)
                            {
                                BurnParticle.transform.GetChild(k).GetComponent<ParticleSystem>().enableEmission = true;
                            }
                        }
                    }
                }
                if (!dmgTaken)
                {
                    if (dmgType[i] == DmgType.Fire)
                    {
                        if (Random.Range(0, gm.difficultLevel + 4) == 1)
                        {
                            BurnTime = BurnTimeCd;
                            BurnEnemy = true;
                            for (int k = 0; k < BurnParticle.transform.childCount; k++)
                            {
                                BurnParticle.transform.GetChild(k).GetComponent<ParticleSystem>().enableEmission = true;
                            }
                        }
                    }
                    if (dmgType[i] == DmgType.Meele)
                    { int Critic;
                        float wounds;

                        Critic = Random.Range(0, CriticRange);
                        if (Critic == 1)
                        {
                            wounds = Mathf.Ceil(Random.Range((Dmg * percentageDistribution[i]) *2* MeeleDefendFactorVariable, Dmg * percentageDistribution[i]) *4* MeeleDefendFactorVariable);
                            CurrentHp -= wounds;
                            gm.dmgTxtController.CreateDmgTxt(wounds.ToString(), transform, dmgType[i], true, false);
                        }
                        else
                        {
                            wounds = Mathf.Ceil(Random.Range((Dmg * percentageDistribution[i]) * 0.5f * MeeleDefendFactorVariable, Dmg * percentageDistribution[i]) * 2 * MeeleDefendFactorVariable);
                            CurrentHp -= wounds;
                            gm.dmgTxtController.CreateDmgTxt(wounds.ToString(), transform, dmgType[i], false, false);
                        }
                          
                    }
                    else
                    {
                        int Critic;
                        float wounds;

                        Critic = Random.Range(0, CriticRange);
                        if (Critic == 1)
                        {
                            wounds = Mathf.Ceil(Random.Range((Dmg * percentageDistribution[i]) * 2 * MagicDefendFactorVariable, Dmg * percentageDistribution[i]) * 4 * MagicDefendFactorVariable);
                            CurrentHp -= wounds;
                            gm.dmgTxtController.CreateDmgTxt(wounds.ToString(), transform, dmgType[i], true, false);
                        }
                        else
                        {
                            wounds = Mathf.Ceil(Random.Range((Dmg * percentageDistribution[i]) * 0.5f * MagicDefendFactorVariable, Dmg * percentageDistribution[i]) * 2 * MagicDefendFactorVariable);
                            CurrentHp -= wounds;
                            gm.dmgTxtController.CreateDmgTxt(wounds.ToString(), transform, dmgType[i], false,false);
                        }
                    }
                }
            }
        }
    }
    //Gdy przeciwnik płonie
    public void Burning()
    {
        spriteRenderer.color = BurnColor;
        BurnParticle.SetActive(true);
        BurnTime -= Time.deltaTime;

        if (MeanOfBurn == 1)
        {
            CurrentHp -= (Time.deltaTime * gm.PlayerFireLevel * gm.PlayerMagic) / 10f;

        }
        if (MeanOfBurn == 0)
        {
            CurrentHp -= (Time.deltaTime * gm.PlayerFireLevel * gm.PlayerMagic) * 2 / 10f;

        }
        if (MeanOfBurn == 2)
        {
            CurrentHp -= (Time.deltaTime * gm.PlayerFireLevel * gm.PlayerMagic) * 0.5f / 10f;
        }

        if (BurnTime <= 0.4f)
        {
            for(int i=0; i<BurnParticle.transform.childCount;i++)
            {
                BurnParticle.transform.GetChild(i).GetComponent<ParticleSystem>().enableEmission = false;
            }
        }
       
         if (BurnTime <= 0 || CurrentHp<=0)
        {
            BurnEnemy = false;
        }
    }

    //Niszczenie obiektu
    public void Destroy()
    {
        anim.SetBool("Die", true);

        if (DieOnce) //gdy przeciwnik umiera od razu i nie potrzebuje zadnych dodatkowych czynników
        {
            GameObject expBullet =  Instantiate(PdBullet, transform.position, transform.rotation);
            expBullet.GetComponent<PdBullet>().expPoints= Exp;

            if (NeededGroundToDie)
            {
                rgb2.constraints = RigidbodyConstraints2D.None;
            }

            if (Loot != null)
            {
                Instantiate(Loot, transform.position, transform.rotation);
            }
            DieOnce = false;
        }

        if(NeededGroundToDie) // w przypadku gdy potrzebuje spaść na ziemie by umrzeć
        {
            if(Grounded)
            {
                TimeToDead -= Time.deltaTime;
            }
            rgb2.velocity = new Vector3(rgb2.velocity.x,0);
        }
        else
        {
            rgb2.velocity = Vector3.zero;
            TimeToDead -= Time.deltaTime;
            audioSource.volume = TimeToDead/timeToDieCD;  
        }
        if (TimeToDead <= 0)
        {
            audioSource.volume = 0f;
            audioSource.Stop();
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        //Wejście w kolizje z stunbullet ( stunowanie obiektu)
        if (col.gameObject.tag == StunBulletTag)
        {
            StunEnemy = true;
            StunTime = (gm.PlayerWhiteLevel * playertck.basicStunTime + (gm.PlayerMagic * playertck.basicStunTime / 100f));
            StunStar.SetActive(true);
        }

        //Wejście w kolizje z DeathBomb (Klątwa)
        if (col.gameObject.tag == DeathBombTag)
        {
            CurseEnemy = true;
            CurseTime = (gm.PlayerDeathLevel * playertck.basicCurseTime + (gm.PlayerMagic * playertck.basicCurseTime / 100f));
            CurseParticle.SetActive(true);
        }
    }
}
