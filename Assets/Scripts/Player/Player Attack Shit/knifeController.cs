﻿using UnityEngine;
using System.Collections;

public class knifeController : MonoBehaviour {

    public DmgType[] DmgType;  //typy obrazen
    public float[] percentageDistribution;  //rozklad obrazen

    public bool ShouldAppearObject; //tworzenie dodatkowego obiektu
	public GameObject ObjectToAppear;

    //prędkość obiektu
	public float speed;
	public float dmg = 20;

    //trzęsienie kamery
	public bool shouldShakeCamera;
	public float ShakePow;
	public float ShakeDur;

    private bool HasAMeeleDMG; //czy ma obrazenia zwykłe
    public bool Knife; //czy to nóż
    public bool ShouldAppearObjectOnEnemy; //czy pojawić jakiś dodatkowy obiekt na przeciwniku

    public GameObject ObjectToAppearOnEnemy;
    public Transform PointToAppearObj;

    //komponenty
    private AudioSource audioSource; //odtwarzacz dźwięku
    private Rigidbody2D rb2d; //fizyka obiektu
    private Player_Controller player;
    public Camera_Following cam;
    private Game_Master gm;

    void Start () {
		player = FindObjectOfType<Player_Controller> ();
        cam = GameObject.Find("Main Camera").GetComponent<Camera_Following>();
        gm = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<Game_Master>();
        HasAMeeleDMG = false;
        rb2d = gameObject.GetComponent<Rigidbody2D>();

       
        if(Knife) //jeżeli to jest nóż z ekwipunku
        {
            dmg = gm.Secon_Weapon_Slot.GetComponentInChildren<ElementEq>().attack; //pobranie wartości obrażeń z obiektu w ekwipunku
            dmg = gm.PlayerStatsAttack * dmg * 0.1f + dmg;   //mnożnik ataku
            HasAMeeleDMG = true;
        }
        else
        {
            for (int i = 0; i < DmgType.Length; i++)
            {
                if (DmgType[i] == global::DmgType.Meele)
                {
                    HasAMeeleDMG = true;
                }
            }
        }

        if (player.facingRight) //ustawienie skali obiektu
		{
			transform.localScale = new Vector3(1, 1, 1);
		}
		else
		{
			transform.localScale = new Vector3(-1, 1, 1);
            speed = -speed;
        }
    }

    void Update () {
      //poruszanie się obiektu
		GetComponent<Rigidbody2D> ().velocity = new Vector2 (speed, GetComponent<Rigidbody2D> ().velocity.y);
    }

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Enemy")  //zadanie obrażeń przeciwnikowi / niszczenie obiektu
		{
            Enemy enemy = other.GetComponent<Enemy>();
             if(!enemy.Dead)
             {
                 if (shouldShakeCamera)
                 {
                    cam.ShakeCamera(ShakePow, ShakeDur);
                 }
                DestroyObj();
                if (ShouldAppearObject)
                {
                        Instantiate(ObjectToAppear, transform.position, transform.rotation);
                }
                if (ShouldAppearObjectOnEnemy)
                {
                    Instantiate(ObjectToAppearOnEnemy, PointToAppearObj.position, transform.rotation);
                }
                enemy.getDmg(dmg, DmgType, percentageDistribution);

                if(HasAMeeleDMG)
                {
                    Vector3 point;
                    point = other.gameObject.GetComponent<Collider2D>().bounds.ClosestPoint(transform.position);
                    if (transform.position.x > enemy.transform.position.x)
                    {
                        Instantiate(enemy.blood, point, enemy.transform.rotation);
                    }
                    else
                    {
                        Instantiate(enemy.bloodBack, point, enemy.transform.rotation);
                        enemy.CheckYourBack();
                    }
                }
            }
         }


		if (other.tag == "FlyerEnemy") 
		{
			if(shouldShakeCamera)
			{
				cam.ShakeCamera(ShakePow,ShakeDur);
			}
            DestroyObj();
            if (ShouldAppearObject)
			{	
				Instantiate (ObjectToAppear, transform.position, transform.rotation);
			}
		}

        //niszczenie obiektu
		if (other.tag == "Walls") 
		{
			if(shouldShakeCamera)
			{
				cam.ShakeCamera(ShakePow,ShakeDur);
			}
            DestroyObj();
            if (ShouldAppearObject)
			{
				Instantiate (ObjectToAppear, transform.position, transform.rotation);
			}
		}

		if (other.tag == "Ground") 
		{
			if(shouldShakeCamera)
			{
				cam.ShakeCamera(ShakePow,ShakeDur);
			}
			if(ShouldAppearObject)
			{
				Instantiate (ObjectToAppear, transform.position, transform.rotation);
				
			}
            DestroyObj();
		}
		
	}
    void DestroyObj() //niszczenie obiektu
    {
        if(gameObject.GetComponentInChildren<ParticleSystem>()!=null)
        {
            float timetoDestroy = GetComponentInChildren<ParticleSystem>().startLifetime;
            gameObject.GetComponentInChildren<ParticleSystem>().enableEmission=false;
            Destroy(gameObject, timetoDestroy);
            rb2d.isKinematic=true;
            gameObject.GetComponent<Collider2D>().enabled = false;
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            if(gameObject.GetComponent<AudioSource>()!=null)
            {
                gameObject.GetComponent<AudioSource>().Stop();
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}