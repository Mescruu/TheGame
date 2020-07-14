using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EchoEffect : MonoBehaviour {

    private float DelayEcho;
    public float DelayEchoCD=0.01f;

    public GameObject echo;
    public bool MakeAnEcho;
    private Rigidbody2D rgb2d;
    public float echoBeginning=150f;

    public float EchoTime = 1f; // How long echo exist

    // Skrypt służący do tworzenia echa/klonu obiektu
    void Start () {
       DelayEchoCD = DelayEcho;
       MakeAnEcho =false;
       rgb2d = gameObject.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
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
