using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class takeDamage : MonoBehaviour
{

    public DmgType[] dmgType;
    public float dmg;
    public float[] percentageDistribution;  //rozklad obrazen



    private Player_Controller player;

    // Use this for initialization
    void Start()
    {

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Controller>();

    }
    void OnTriggerEnter2D(Collider2D other)
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
    // Update is called once per frame





}
