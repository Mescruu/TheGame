using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

    [Tooltip("Type of dmg")]
    public DmgType[] dmgType;

    [Tooltip("Count of dmg")]
    public float dmg;
    [Tooltip("Percentage Distribution of all dmg's types for example: 0.1 = 10% ")]
    public float[] percentageDistribution;  //rozklad obrazen

    public Transform target;

    [Tooltip("Speed")]
    public float speed = 10;

    private Player_Controller player;
    private Destroy_obj destr;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Controller>();

        // Cache our start position, which is really the only thing we need
        // (in addition to our current position, and the target).
        destr = gameObject.GetComponent<Destroy_obj>();

        target.transform.position = player.transform.position;


    }
    // Use this for initialization
 
	
	// Update is called once per frame
	void Update ()
    {

        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

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

                if (destr != null)
                {
                    destr.TimeToDestroy = 0;
                }

            }
        }
        if (other.tag == "Shield")
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
        if (other.tag == "Ground")
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
        if (other.tag == "Wall")
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
