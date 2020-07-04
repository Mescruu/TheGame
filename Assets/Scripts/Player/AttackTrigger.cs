using UnityEngine;
using System.Collections;

public class AttackTrigger : MonoBehaviour
{

    public float basicDmg;
    public float dmg;

    private Game_Master gm;

    public float Wsp;
    public float DeathModeWsp;

    public DmgType[] DmgTypeSword;  //typy obrazen
    public DmgType[] DmgTypeSwordAndDeath;  //typy obrazen

    public float[] percentageDistribution;  //rozklad obrazen
    public float[] percentageDistributionWitchDeath;  //rozklad obrazen
    public bool DeathMode;


    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<Game_Master>();

        DeathMode = false;

        SetBasicDmg();
       
        Wsp = 1;
    }
    void Update()
    {
        if (DeathMode)
        {
            Wsp = DeathModeWsp;
        }
        else
        {
            Wsp = 1;
        }
    }
    public void SetBasicDmg()
    {
        dmg = (gm.PlayerAttack * basicDmg * 0.1f + basicDmg);
        dmg += gm.PlayerSwordLevel * dmg;
        dmg = dmg * Wsp;

    }
    public void SetDmg(float Dmg)
    {
        dmg = (gm.PlayerAttack * Dmg * 0.1f + Dmg);
        dmg += gm.PlayerSwordLevel * dmg;
        dmg = dmg * Wsp;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (!enemy.Dead)
            {
                if (!DeathMode)
                {
                    enemy.getDmg(dmg, DmgTypeSword, percentageDistribution);
                }
                else
                {
                    enemy.getDmg(dmg, DmgTypeSwordAndDeath, percentageDistributionWitchDeath);
                }

                Vector3 point;

                point = other.gameObject.GetComponent<Collider2D>().bounds.ClosestPoint(transform.position);
                if(transform.position.x>enemy.transform.position.x)
                {
                    Instantiate(enemy.blood, point, enemy.transform.rotation);
                }
                else
                {
                    Instantiate(enemy.bloodBack, point, enemy.transform.rotation);
                }

            }
        }
    }
}