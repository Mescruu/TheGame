using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class takeDmgEnemy : MonoBehaviour
{

    public DmgType[] dmgType;
    public float dmg;
    public float[] percentageDistribution;  //rozklad obrazen
    private Player_Controller player;
    private Game_Master gm;

    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<Game_Master>();

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Controller>();
        if (!player.facingRight)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    // Use this for initialization

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Enemy Atack");
        if (other.tag == "Enemy")
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (!enemy.Dead)
            {




                Vector3 point;

                point = other.gameObject.GetComponent<Collider2D>().bounds.ClosestPoint(transform.position);

                for (int i = 0; i < dmgType.Length; i++)
                {
                    if (dmgType[i] == DmgType.Meele)
                    {
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
                    if (dmgType[i] == DmgType.Fire)
                    {
                        percentageDistribution[i] += 0.02f * gm.PlayerMagic + 0.2f * (gm.PlayerFireLevel - 1);
                    }
                    if (dmgType[i] == DmgType.Elect)
                    {
                        percentageDistribution[i] += 0.02f * gm.PlayerMagic + 0.2f * (gm.PlayerElectLevel - 1);
                    }
                    if (dmgType[i] == DmgType.Chaos)
                    {
                        percentageDistribution[i] += 0.02f * gm.PlayerMagic + 0.2f * (gm.PlayerDeathLevel - 1);
                    }
                    if (dmgType[i] == DmgType.Ankh)
                    {
                        percentageDistribution[i] += 0.02f * gm.PlayerMagic + 0.2f * (gm.PlayerWhiteLevel - 1);
                    }
                }

                enemy.getDmg(dmg, dmgType, percentageDistribution);



            }

        }
        // Update is called once per frame

    }
}

