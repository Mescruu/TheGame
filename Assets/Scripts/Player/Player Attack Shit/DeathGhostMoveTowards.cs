using UnityEngine;
using System.Collections;

public class DeathGhostMoveTowards : MonoBehaviour {

    private GameObject playerGameObj;

    private Player_Controller player;
	private PlayerAttack playerAtck;
	public Transform playerPoint;
	public float moveSpeed;
	public float normalMoveSpeed;
	public float highSpeed;
	public bool playerInRange;
	public LayerMask playerLayer;
	public float PlayerRange;
	private Animator anim;
    private Rigidbody2D rgb2;

	// Use this for initialization
	void Start () {


        playerGameObj = GameObject.FindGameObjectWithTag("Player");
        player = playerGameObj.GetComponent<Player_Controller>();
        playerAtck = playerGameObj.GetComponent<PlayerAttack>();
        playerPoint = playerAtck.DeathMaskPoint;

        rgb2 = gameObject.GetComponent<Rigidbody2D>();

		anim = gameObject.GetComponent<Animator> ();


	}
	
	// Update is called once per frame
	void Update ()
		
	{

		transform.position = Vector3.MoveTowards (transform.position, playerPoint.transform.position, moveSpeed);
		
		playerInRange = Physics2D.OverlapCircle (transform.position, PlayerRange, playerLayer);

			
			if(transform.position.x<playerPoint.position.x)
			{
				transform.localScale = new Vector3(1, 1, 1);
			}
        if (transform.position.x > playerPoint.position.x)
             {
				transform.localScale = new Vector3(-1, 1, 1);	
              }
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
	
		if (playerAtck.DeathMode == false) 
		{


			Destroy (gameObject);

		}
		if (playerAtck.DeathModeTime <= 0.5f) 
		{


			anim.SetBool ("Dead", true);


		}
}
}
