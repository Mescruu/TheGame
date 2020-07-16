using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PdBullet : MonoBehaviour {


    private GameObject player;
    public float moveSpeed;

    public LayerMask playerLayer; //warstaw gracza
    private Animator anim;
    public ParticleSystem particle;
    private Game_Master gm;
    public int expPoints; //ilosc expa

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        gm = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<Game_Master>();
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, moveSpeed* Time.deltaTime); //poruszanie się kuli exp
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player") //kula dotarła do gracza
        {
            gm.exp += expPoints;
            Instantiate(particle, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
