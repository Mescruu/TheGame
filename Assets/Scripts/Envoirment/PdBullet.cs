using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PdBullet : MonoBehaviour {


    private GameObject player;
    public float moveSpeed;

    public LayerMask playerLayer;
    private Animator anim;
    public ParticleSystem particle;
    private Game_Master gm;
    public int expPoints;
    // Use this for initialization
    void Start()
    {


        player = GameObject.FindGameObjectWithTag("Player");
        gm = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<Game_Master>();




    }

    // Update is called once per frame
    void Update()

    {
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, moveSpeed* Time.deltaTime);
    }
    void OnTriggerEnter2D(Collider2D col)
    {


        if (col.gameObject.tag == "Player")
        {
            gm.exp += expPoints;
            Instantiate(particle, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
