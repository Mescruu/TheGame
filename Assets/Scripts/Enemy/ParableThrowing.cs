using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class ParableThrowing : MonoBehaviour
{
    [Tooltip("Type of dmg")]
    public DmgType[] dmgType;

    [Tooltip("Count of dmg")]
    public float dmg;
    [Tooltip("Percentage Distribution of all dmg's types for example: 0.1 = 10% ")]
    public float[] percentageDistribution;  //rozklad obrazen


    [Tooltip("Position we want to hit")]
    public Vector3 targetPos;

    [Tooltip("Horizontal speed, in units/sec")]
    public float speed = 10;

    [Tooltip("How high the arc should be, in units")]
    public float arcHeight = 1;

    Vector3 startPos;
    private Player_Controller player;
    private Destroy_obj destr;

    public float minRange;
    public float middleRange;
    public float farRange;




    void Start()
    {
        targetPos = new Vector3(GameObject.FindGameObjectWithTag("Player").transform.position.x, GameObject.FindGameObjectWithTag("Player").transform.position.y, GameObject.FindGameObjectWithTag("Player").transform.position.z);
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Controller>();

        // Cache our start position, which is really the only thing we need
        // (in addition to our current position, and the target).
        startPos = transform.position;
        destr = gameObject.GetComponent<Destroy_obj>();

        if(Mathf.Abs(player.transform.position.x)> farRange)
        {
            arcHeight = Mathf.Abs(player.transform.position.x / 2f) * Mathf.Tan(Mathf.PI / 4f);
        }
        if (Mathf.Abs(player.transform.position.x) <= farRange && Mathf.Abs(player.transform.position.x) > middleRange)
        {
            arcHeight = Mathf.Abs(player.transform.position.x / 2f) * Mathf.Tan(Mathf.PI / 4f)/2f;

        }
        if (Mathf.Abs(player.transform.position.x)>=0&& Mathf.Abs(player.transform.position.x)<=middleRange)
        {
            arcHeight = 1;
        }
    }

    void Update()
    {
        // Compute the next position, with arc added in
        float x0 = startPos.x;
        float x1 = targetPos.x;
        float dist = x1 - x0;
        if(x1 - x0>=0)
        {
            float nextX = Mathf.MoveTowards(transform.position.x, x1 + 1000f, speed * Time.deltaTime);
            float baseY = Mathf.Lerp(startPos.y, targetPos.y, (nextX - x0) / dist);
            float arc = arcHeight * (nextX - x0) * (nextX - x1) / (-0.25f * dist * dist);
            Vector3 nextPos = new Vector3(nextX, baseY + arc, transform.position.z);
            transform.rotation = LookAt2D(nextPos - transform.position);
            transform.position = nextPos;

        }
        else
        {
            float nextX = Mathf.MoveTowards(transform.position.x, x1 - 1000f, speed * Time.deltaTime);
            float baseY = Mathf.Lerp(startPos.y, targetPos.y, (nextX - x0) / dist);
            float arc = arcHeight * (nextX - x0) * (nextX - x1) / (-0.25f * dist * dist);
            Vector3 nextPos = new Vector3(nextX, baseY + arc, transform.position.z);
            transform.rotation = LookAt2D(nextPos - transform.position);
            transform.position = nextPos;
        }

        // Rotate to face the next position, and then move there


        // Do something when we reach the target
   

    }

    void Arrived()
    {
        Destroy(gameObject);
    }

    /// 
    /// This is a 2D version of Quaternion.LookAt; it returns a quaternion
    /// that makes the local +X axis point in the given forward direction.
    /// 
    /// forward direction
    /// Quaternion that rotates +X to align with forward
    static Quaternion LookAt2D(Vector2 forward)
    {
        return Quaternion.Euler(0, 0, Mathf.Atan2(forward.y, forward.x) * Mathf.Rad2Deg);
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

                if (destr!=null)
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