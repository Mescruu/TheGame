using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectorForPlayer : MonoBehaviour {

    //Różnie traktująca gracza plaforma by gracz
    public PlatformEffector2D effector2D;
    private Player_Controller player;

    [Tooltip("The half height of the player in transform scale")]
    public float HalfPlayer;
	void Start ()
    {
    player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Controller>();
    }

    void Update () {
        if(player.transform.position.y- HalfPlayer < transform.position.y)
        {
            effector2D.surfaceArc = 0;
        }
        else
        {
            effector2D.surfaceArc = 180;
        }
    }
}
