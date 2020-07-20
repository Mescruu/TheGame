using UnityEngine;
using System.Collections;

public class DeathBite : MonoBehaviour
{

    static public float dmg; //zadawany dmg
    public float TimeToDestroy = 2; //jak długo trwa

    private bool can; //czy można zakończyc
    static public int hp; //pozyskane hp
    private Animator anim; //animacja
    public float Speed; //prędkość
    public Transform forward;
    public LayerMask Layer;
    public AudioClip startClip;
    public AudioClip EndClip;

    //Komponenty
    private AudioSource audioSource;
    private Game_Master gm;
    private PlayerAttack playerAttack;
    private Player_Controller player;
    private KeyMenager keyMenager;

    void Start()
    {
        //ustawienia dźwięku
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.loop = true;
        audioSource.clip = startClip;
        audioSource.Play();

        can = true;//Możliwość zamknięcia

        //Komponenty
        gm = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<Game_Master>();
        keyMenager = GameObject.Find("KeyMenager").GetComponent<KeyMenager>();
        anim = gameObject.GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Controller>();
        playerAttack = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAttack>();

        hp = Random.Range(2, 5); //ilość pozyskanego hp

        //Odwrócenie obiektu
        if (player.facingRight)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        anim.SetBool("End", false);
    }

    void Update()
    {
        //koło
        if(Physics2D.OverlapCircle(transform.position, 0.3f, Layer) && can)
        {
            anim.SetBool("End", true);
            TimeToDestroy = 0.2f;
            can = false;
        }

        //Zamknięcie czaru
        if (Input.GetKeyDown(keyMenager.keys["ReleaseMagic"]) && can)
        {
            anim.SetBool("Release", true);
            TimeToDestroy = 4f;
            can = false;
            audioSource.loop = false;
            audioSource.clip = EndClip;
            audioSource.Play();
        }

        //Czas do zniszczenia obiektu
        TimeToDestroy -= Time.deltaTime;
        if (TimeToDestroy <= 4f)
        {
            anim.SetBool("End", true);
            if (TimeToDestroy <= 0)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, forward.transform.position, Speed * Time.deltaTime);
        }

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        //pobranie hp
        if (other.tag == "Enemy")
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (!enemy.Dead)
            {
                player.curHP += playerAttack.basicHpStealth * gm.PlayerDeathLevel + playerAttack.basicHpStealth * gm.PlayerMagic / 100f;
            }
        }

        //niszczenie obiektu
        if (other.tag == "Walls")
        {
            anim.SetBool("End", true);
            TimeToDestroy = 0.2f;
        }

        //niszczenie obiektu
        if (other.tag == "Ground")
        {
            anim.SetBool("End", true);
            TimeToDestroy = 0.2f;
        }
    }
}





