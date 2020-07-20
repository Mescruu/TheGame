using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUp : MonoBehaviour {


    public float TimeToDestroy; //czas po którym efekt znika
   
    private ParticleSystem particle;
    private Rigidbody2D rgb2d;
    private Transform Player;
    public float moveSpeed;
    public AudioClip LevelSound;
    public Transform target;
    void Start()
    {
        Player = GameObject.Find("Player").GetComponent<Transform>();
        particle = gameObject.GetComponentInChildren<ParticleSystem>();
        Player.GetComponent<Player_Controller>().audioPlayer.PlayOnce(LevelSound, 0.5f);
        rgb2d = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed); //obiekt leci w stronę gracza

        TimeToDestroy -= Time.deltaTime;
        if (TimeToDestroy <= 0)
        {
            DestroyObj();
        }
    }
    void DestroyObj() //zniszczenie obiektu i rzeczy z nim związanych
    {
        if (particle != null)
        {
            float timetoDestroy = particle.startLifetime;
            particle.enableEmission = false;
            Destroy(gameObject, timetoDestroy);
            if (rgb2d != null)
            {
                rgb2d.isKinematic = true;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
