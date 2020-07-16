using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

    //Typ obrażeń
    [Tooltip("Type of dmg")]
    public DmgType[] dmgType;

    //Ilość obrażeń
    [Tooltip("Count of dmg")]
    public float dmg;

    //rozklad obrazen
    [Tooltip("Percentage Distribution of all dmg's types for example: 0.1 = 10% ")]
    public float[] percentageDistribution; 

    //W co ma trafić
    public Transform target;

    //Prędkość pocisku
    [Tooltip("Speed")]
    public float speed = 10;

    //Obiekt gracza
    private Player_Controller player;

    //Komponent destrukcji
    private Destroy_obj destr;

    void Start()
    {
        //dołączenie obiektu gracza
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Controller>();

        //dołączenie komponentu destrukcji
        destr = gameObject.GetComponent<Destroy_obj>();

        //Zapisanie pozycji gdzie należy wysłać "pocisk"
        target.transform.position = player.transform.position;
    }

	void Update ()
    {
        //Poruszanie się w stronę początkowego punktu
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (!player.dead)
            {
                Vector3 point; //nowy punkt określający miejsce trafienia
                point = other.gameObject.GetComponent<Collider2D>().bounds.ClosestPoint(transform.position);
                
                player.getDmg(dmg, dmgType, percentageDistribution, point, transform);

                if (destr != null) //jeżeli komponent został dołączony zniszcz obiekt
                {
                    destr.TimeToDestroy = 0;
                }

            }
        }
        if (other.tag == "Shield") // w przypadku jeżeli gracz używa tarczy obrażenia otrzymuje ona
        {
            if (!player.dead)
            {
                Vector3 point;
                point = other.gameObject.GetComponent<Collider2D>().bounds.ClosestPoint(transform.position);

                other.GetComponent<Shield>().getDmg(dmg, dmgType, percentageDistribution, point, transform);

                if (destr != null)
                {
                    destr.TimeToDestroy = 0;
                }
            }
        }
        //jeżeli trafi w ziemie, bądź ściane
        if (other.tag == "Ground" || other.tag == "Wall") 
        {
            if (destr != null)
            {
                destr.TimeToDestroy = 0;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
