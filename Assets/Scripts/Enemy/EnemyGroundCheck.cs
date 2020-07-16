using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGroundCheck : MonoBehaviour {

    private Enemy enemy;
    // Use this for initialization
    void Start () {
	    enemy = gameObject.GetComponentInParent<Enemy>(); //przyłaczenie skryptu Enemy
    }

    void OnTriggerEnter2D(Collider2D other) //Sprawdzenie czy obiekt styka się z ziemią
    {
        if (other.tag == "Ground")
        {
            enemy.Grounded = true;
        }
    }
    void OnTriggerStay2D(Collider2D other) //Sorawdzenie czy obiekt wciąż styka się z ziemią
    {
        if (other.tag == "Ground")
        {
            enemy.Grounded = true;
        }
    }
    void OnTriggerExit2D(Collider2D other) //sprawdzenie czy obiekt nie dotyka już ziemi
    {
        if (other.tag == "Ground")
        {
            enemy.Grounded = false;
        }
    }
}
