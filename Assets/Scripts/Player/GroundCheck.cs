using UnityEngine;
using System.Collections;

public class GroundCheck : MonoBehaviour {

	// Use this for initialization

	private Player_Controller player;
    public AudioSource groundSound;
    public GameObject Dust;
    public Transform DustPos;


    void Start()
	{
		player = gameObject.GetComponentInParent<Player_Controller> ();
	}

	void OnTriggerEnter2D(Collider2D col)
	{	
		if (col.gameObject.tag == "Ground") 
				{
            groundSound.Play();
            player.grounded = true;
            Instantiate(Dust, DustPos.position, transform.rotation);
                }
	}

	void OnTriggerStay2D(Collider2D col)
	{
		if (col.gameObject.tag == "Ground") 
		{
			player.grounded = true;
		}


	}

	void OnTriggerExit2D(Collider2D col)
	{
        if (col.gameObject.tag == "Ground")
        {
            player.grounded = false;
            groundSound.Play();

        }
    }
}   
