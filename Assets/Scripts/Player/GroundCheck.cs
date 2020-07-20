using UnityEngine;
using System.Collections;

public class GroundCheck : MonoBehaviour {

	// Use this for initialization

	private Player_Controller player; //obiekt gracza
    public AudioSource groundSound; //dźwięk poruszania się
    public GameObject Dust; //kurz
    public Transform DustPos; //pozycja nóg

    void Start()
	{
		player = gameObject.GetComponentInParent<Player_Controller> (); //czym jest gracz
	}

	void OnTriggerEnter2D(Collider2D col)
	{	
		if (col.gameObject.tag == "Ground")  //jeżeli jest to ziemia utworz cząstki kurzu i zagraj dźwięk
		{
			groundSound.Play();
			player.grounded = true;
			Instantiate(Dust, DustPos.position, transform.rotation);
         }
	}

	void OnTriggerStay2D(Collider2D col) //gracz jest na ziemi (nie w powietrzu)
	{
		if (col.gameObject.tag == "Ground") 
		{
			player.grounded = true;
		}
	}

	void OnTriggerExit2D(Collider2D col) //gracz wyskoczył - nie jest juz na ziemi
	{
        if (col.gameObject.tag == "Ground")
        {
            player.grounded = false;
            groundSound.Play();
        }
    }
}   
