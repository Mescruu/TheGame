using UnityEngine;
using System.Collections;

public class DeathBite : MonoBehaviour
{

    static public float dmg;
    private Player_Controller player;
    public float TimeToDestroy = 2;
    private KeyMenager keyMenager;

    private bool can;
    static public int hp;
    private Animator anim;
    public float Speed;
    public Transform forward;
    public LayerMask Layer;
    public AudioClip startClip;
    public AudioClip EndClip;
    private AudioSource audioSource;
    private Game_Master gm;
    private PlayerAttack playerAttack;

    // Use this for initiazization
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.loop = true;
        audioSource.clip = startClip;
        audioSource.Play();

        gm = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<Game_Master>();

        can = true;
        keyMenager = GameObject.Find("KeyMenager").GetComponent<KeyMenager>();

        hp = Random.Range(2, 5);

        anim = gameObject.GetComponent<Animator>();

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Controller>();
        playerAttack = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAttack>();

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

    // Update is called once per frame
    void Update()

    {

        if(Physics2D.OverlapCircle(transform.position, 0.3f, Layer) && can)
        {
            anim.SetBool("End", true);
            TimeToDestroy = 0.2f;
            can = false;
        }

        if (Input.GetKeyDown(keyMenager.keys["ReleaseMagic"]) && can)
        {
            anim.SetBool("Release", true);
            TimeToDestroy = 4f;
            can = false;
            audioSource.loop = false;
            audioSource.clip = EndClip;
            audioSource.Play();
        }


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

        if (other.tag == "Enemy")
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (!enemy.Dead)
            {
                player.curHP += playerAttack.basicHpStealth * gm.PlayerDeathLevel + playerAttack.basicHpStealth * gm.PlayerMagic / 100f;
            }
        }


                if (other.tag == "Walls")
        {
            anim.SetBool("End", true);
            TimeToDestroy = 0.2f;
        }


        if (other.tag == "Ground")
        {
            anim.SetBool("End", true);
            TimeToDestroy = 0.2f;
        }

    }
}





