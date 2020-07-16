using UnityEngine;
using System.Collections;

public class Falling_Platform : MonoBehaviour {


	private Rigidbody2D rb2d;
	public float destroyTime; //czas po którym obiekt zniknie
	public float fallDelay; //czas po jakim platforma opadnie

	void Start()
	{
		rb2d = GetComponent<Rigidbody2D> ();
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.collider.CompareTag ("Player")) 
		{
		StartCoroutine(Fall());
		}
	}

	IEnumerator Fall() //sekwencja wykonywane klatka po klatce gry
	{
		yield return new WaitForSeconds (fallDelay);
		rb2d.isKinematic = false;
		Destroy (gameObject, destroyTime);
		yield return 0;
	}
}
