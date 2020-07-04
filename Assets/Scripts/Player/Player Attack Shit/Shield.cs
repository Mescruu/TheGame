using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour {

    public DmgType[] dmgTypeSensitive;
    public DmgType[] dmgTypeHardness;

    private Player_Controller playerController;
    private PlayerAttack playerAttack;
    public float TimeToDestroy;
    public float TimetoDestroyCD;
    public float Hp;
    public float HpBasic;
    public GameObject ParticleObj;
    private Game_Master gm;

    private Animator anim;

    private float animTime;
    private float animTimeCd = 0.1f;
    public AudioClip ShieldStart;
    public AudioClip[] ShieldDmg;
    public AudioClip ShieldEnd;
    private AudioSource audios;
    private bool PlayOnce;

    // Use this for initialization
    void Start () {
        gm = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<Game_Master>();
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Controller>();
        playerAttack = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAttack>();

        PlayOnce = true;
        TimeToDestroy = TimetoDestroyCD;
        Hp = HpBasic * gm.PlayerWhiteLevel + HpBasic * 0.1f* gm.PlayerMagic;
        anim = gameObject.GetComponent<Animator>();
        animTime = animTimeCd;
        audios = gameObject.GetComponent<AudioSource>();
        audios.clip = ShieldStart;
        audios.Play();


        if (!gm.Player_Defend)
        {
            gm.Player_Defend = true;
            gm.AddEffect(3);
        }

    }

    // Update is called once per frame
    void Update () {

        if(animTime>0)
        {
            animTime -= Time.deltaTime;
            anim.SetBool("Defend", true);
        }
        else
        {
            playerController.Defend = false;
            Debug.Log("Player Defend False");

            anim.SetBool("Defend", false);
        }
        if (Hp<=0||TimeToDestroy<=0)
        {
            anim.SetBool("End", true);
            if(PlayOnce)
            {
                audios.clip = ShieldEnd;
                audios.Play();
                PlayOnce = false;
            }
            Destroy(gameObject, 2f);
            gm.Player_Defend = false;
            playerController.Defend = false;
            Debug.Log("Player Defend False");
            playerController.DefendTime = 0;

        }
        else
        { 
            anim.SetBool("End", false);
            gm.Player_Defend = true;

            playerController.Defend = true;
            playerController.DefendTimeCD = TimetoDestroyCD;
            playerController.DefendTime = TimeToDestroy;
            TimeToDestroy -= Time.deltaTime;
        }
    }
    public void getDmg(float Dmg, DmgType[] dmgType, float[] percentageDistribution, Vector3 point, Transform attackPos)
    {
        anim.SetBool("Defend", false);
        audios.clip = ShieldDmg[Random.Range(0, ShieldDmg.Length)];

        audios.Play();

        bool dmgTaken = false;
        animTime = animTimeCd;  

        for (int i = 0; i < dmgType.Length; i++)
        {
            dmgTaken = false;

            for (int j = 0; j < dmgTypeSensitive.Length; j++)
            {
                if (dmgType[i] == dmgTypeSensitive[j])
                {
                    Hp -= (Dmg * percentageDistribution[i] * 2f);
                    dmgTaken = true;
                }
            }
            for (int j = 0; j < dmgTypeHardness.Length; j++)
            {
                if (dmgType[i] == dmgTypeHardness[j])
                {
                    Hp -= (Dmg * percentageDistribution[i] * 0.5f);
                    dmgTaken = true;
                }
            }
            for (int j = 0; j < dmgTypeHardness.Length; j++)
            {
                if (dmgType[i] ==DmgType.Ankh)
                {
                    dmgTaken = true;
                }
            }
            if (!dmgTaken)
            {
                Hp -= (Dmg * percentageDistribution[i]);
            }

        }

            Instantiate(ParticleObj, point, attackPos.transform.rotation);


    }
}

