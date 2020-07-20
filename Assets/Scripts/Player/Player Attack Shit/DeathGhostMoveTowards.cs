using UnityEngine;
using System.Collections;

public class DeathGhostMoveTowards : MonoBehaviour {

    private GameObject playerGameObj;

	//wartości
	public float moveSpeed;
	public float normalMoveSpeed;
	public float highSpeed;
	public bool playerInRange;

	//komponenty
	private Player_Controller player;
	private PlayerAttack playerAtck;
	public Transform playerPoint;
	public LayerMask playerLayer;
	public float PlayerRange; //pole w którym jest gracz blisko
	private Animator anim;
    private Rigidbody2D rgb2;

	void Start () {
		//komponenty
        playerGameObj = GameObject.FindGameObjectWithTag("Player");
        player = playerGameObj.GetComponent<Player_Controller>();
        playerAtck = playerGameObj.GetComponent<PlayerAttack>();
        playerPoint = playerAtck.DeathMaskPoint;
        rgb2 = gameObject.GetComponent<Rigidbody2D>();
		anim = gameObject.GetComponent<Animator> ();
	}
	
	void Update ()
	{
		//poruszanie się duszka
		transform.position = Vector3.MoveTowards (transform.position, playerPoint.transform.position, moveSpeed); 
		playerInRange = Physics2D.OverlapCircle (transform.position, PlayerRange, playerLayer);
		
		//odwracanie obiektu
		if(transform.position.x<playerPoint.position.x)
				transform.localScale = new Vector3(1, 1, 1);

        if (transform.position.x > playerPoint.position.x)
				transform.localScale = new Vector3(-1, 1, 1);	

        if (transform.position.x == playerPoint.position.x)
        {
            if(player.facingRight)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }

        if (playerInRange) {
			moveSpeed = normalMoveSpeed;
		} else {
			moveSpeed = highSpeed;
		}
	
		if (playerAtck.DeathMode == false)  //zniszczenie obiektu po zakończeniu działania czaru
		{
			Destroy (gameObject);
		}
		if (playerAtck.DeathModeTime <= 0.5f) 
		{
			anim.SetBool ("Dead", true);
		}
	}
}
