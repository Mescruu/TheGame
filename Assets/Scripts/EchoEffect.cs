using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EchoEffect : MonoBehaviour {

    private float DelayEcho;
    public float DelayEchoCD=0.01f; //Opóźnienie za obiektem właściwym

    public GameObject echo;
    public bool MakeAnEcho;
    private Rigidbody2D rgb2d;
    public float echoBeginning=150f;
    public float EchoTime = 1f; // Jak długo echo trwa

    // Skrypt służący do tworzenia echa/klonu obiektu za nim, na zasadzie smugi
    void Start () {
       DelayEchoCD = DelayEcho;
       MakeAnEcho =false;
       rgb2d = gameObject.GetComponent<Rigidbody2D>();
	}
	
	void Update () {
        if (MakeAnEcho)
        {
            if (Mathf.Abs(rgb2d.velocity.x) > echoBeginning || Mathf.Abs(rgb2d.velocity.y) > echoBeginning)
            {
                if (DelayEcho > 0)
                {
                    DelayEcho -= Time.deltaTime;
                }
                else
                {
                    GameObject createdEcho = Instantiate(echo, transform.position, transform.rotation);
                    createdEcho.transform.localScale = gameObject.transform.localScale;
                    createdEcho.GetComponent<SpriteRenderer>().sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
                    createdEcho.GetComponent<SpriteRenderer>().sortingOrder = gameObject.GetComponent<SpriteRenderer>().sortingOrder - 1;
                    Destroy(createdEcho, 1f);
                    DelayEcho = DelayEchoCD;
                }
            }
        }
		
	}
}
