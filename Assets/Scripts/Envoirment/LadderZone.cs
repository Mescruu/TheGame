using UnityEngine;
using System.Collections;

public class LadderZone : MonoBehaviour {
	private Player_Controller player;
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Controller>();
	}
	void OnTriggerEnter2D(Collider2D col) { //gracz wchodzi na drabine
		
		if (col.CompareTag ("Player")) {
			Debug.Log("LadderOn");
            if (player.grounded)
            {
                player.onLadder = false;
            }
            else
            {
                player.onLadder = true;
            }
        }
	}
    void OnTriggerStay2D(Collider2D col) //gracz jest w miejscu gdzie jest drabina
    {
        if (col.CompareTag("Player"))
        {
            Debug.Log("LadderOn");
            if(player.grounded)
            {
                player.onLadder = false;
            }
            else
            {
                player.onLadder = true;
            }
        }
    }
    void OnTriggerExit2D(Collider2D col) { //gracz zszedł z drabiny
		if (col.CompareTag ("Player")) {
			Debug.Log("LadderOff");
			player.onLadder = false;
		}
	}
}
