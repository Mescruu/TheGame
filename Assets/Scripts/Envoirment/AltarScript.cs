using UnityEngine;
using System.Collections;

public class AltarScript : MonoBehaviour
{
    private float AltarTime;
    public float AltarTimeCD;

    public bool altaring;

    public bool Enter;
    private Player_Controller player;
    public ParticleSystem[] particle;
    public bool PlayerNear;
    public float ActiveRange;
    public LayerMask playerMask;
    private KeyMenager keyMenager;
    public GameObject Sign;
    public Transform SignPos;
    private bool MakeItOnce;
    private Game_Master gm;
    public Color HpColor;
    public Color MpColor;
    // Use this for initialization
    void Start()
    {
        Enter = false;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Controller>();
        keyMenager = GameObject.Find("KeyMenager").GetComponent<KeyMenager>();
        AltarTime = AltarTimeCD;
        gm = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<Game_Master>();

    }

    // Update is called once per frame
    void Update()
    {
        PlayerNear = Physics2D.OverlapCircle(transform.position, ActiveRange, playerMask);

        if(PlayerNear)
        {
            for (int i = 0; i < particle.Length; i++)
            {
                particle[i].enableEmission = true;
            }
        }
        else
        {
            for(int i =0;i<particle.Length;i++)
            {
            particle[i].enableEmission = false;
            }

        }
        if (Enter && Input.GetKey(keyMenager.keys["Action"])&&!altaring)
        {
            Debug.Log("Altar Action");
            altaring = true;
            AltarTime = AltarTimeCD;
            player.onKnee = true;


        }
        if (altaring)
        {
            if(AltarTime>0)
            {
                    AltarTime -= Time.deltaTime;

                if(player.onKnee)
                {
                    if (AltarTime <= AltarTimeCD / 2 && MakeItOnce)
                    {
                        Instantiate(Sign, SignPos.position, SignPos.rotation);
                        MakeItOnce = false;
                        float count = Mathf.Ceil(Random.Range(25, 200));
                        player.curHP += count;
                        gm.dmgTxtController.CreateHealTxt(count.ToString(), player.transform, HpColor, false);
                        count = Mathf.Ceil(Random.Range(25, 200));
                        player.curMana += count;
                        gm.dmgTxtController.CreateHealTxt(count.ToString(), player.transform, MpColor, false);
                    }
                }
                   
                
            }
            else
            {
                player.onKnee = false;

                altaring = false;
                AltarTime = AltarTimeCD;
            }
        }
        else
        {
            MakeItOnce = true;
        }
      
    }

    void OnTriggerEnter2D(Collider2D col)
    {


        if (col.gameObject.tag == "Player")
        {
            Enter = true;

        }
    }
    void OnTriggerExit2D(Collider2D col)
    {


        if (col.gameObject.tag == "Player")
        {
            Enter = false;

        }
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0.1f, 1f, 0.1f,0.7f);
        Gizmos.DrawSphere(transform.position, ActiveRange);//rysowanie kolka

    }
    }