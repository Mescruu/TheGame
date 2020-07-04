using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDisappear : MonoBehaviour {

    //"Disappear when parent sprite renderer's alpha is equal zero"

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(gameObject.GetComponentInParent<SpriteRenderer>().color.a==0)
        {
            transform.GetComponent<ParticleSystem>().enableEmission = false;
        }
	}
}
