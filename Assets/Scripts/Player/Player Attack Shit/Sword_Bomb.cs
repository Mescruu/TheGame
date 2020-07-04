using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword_Bomb : MonoBehaviour {

    public Transform CheckPoint1;
    public Transform CheckPoint2;
    public bool CanRelease;
    public LayerMask layerMask;
    public float TimeToRelease=0.2f;
    public GameObject ElectBomb;
    // Use this for initialization
    void Start () {

        CanRelease = true;

        CanRelease = Physics2D.OverlapArea(CheckPoint1.position, CheckPoint2.position, layerMask);

    }

    // Update is called once per frame
    void Update () {


        if (!CanRelease)
        {
            if (TimeToRelease < 0)
            {
                Instantiate(ElectBomb, transform.position, transform.rotation);
                CanRelease = true;

            }
            else
            {
                TimeToRelease -= Time.deltaTime;
            }
        }
    }
}
