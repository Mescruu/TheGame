using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class takeDamage : MonoBehaviour
{

    public DmgType[] dmgType; //Typ obrażeń
    public float dmg; //ilość obrażeń
    public float[] percentageDistribution;  //rozklad obrazen

    //Gracz
    private Player_Controller player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Controller>();
    }
    void OnTriggerEnter2D(Collider2D other) //Otrzymywanie obrażeń 
    {
        if (other.tag == "Player")
        {
            if (!player.dead)
            {
                Vector3 point;
                point = other.gameObject.GetComponent<Collider2D>().bounds.ClosestPoint(transform.position);
                player.getDmg(dmg, dmgType, percentageDistribution, point, transform);
            }
        }
        if (other.tag == "Shield")
        {
            if (!player.dead)
            {
                Vector3 point;
                point = other.gameObject.GetComponent<Collider2D>().bounds.ClosestPoint(transform.position);
                other.GetComponent<Shield>().getDmg(dmg, dmgType, percentageDistribution, point, transform);
            }
        }
    }
}
