using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGroundCheck : MonoBehaviour {

    private Enemy enemy;
    // Use this for initialization
    void Start () {

	    enemy = gameObject.GetComponentInParent<Enemy>();

    }

    // Update is called once per frame
    void Update () {
		
	}
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Ground")
        {
            enemy.Grounded = true;
        }
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Ground")
        {
            enemy.Grounded = true;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Ground")
        {
            enemy.Grounded = false;
        }
    }
}
