using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDisappear : MonoBehaviour {

    //Cząsteczki znikają, gdy alfa komponentu renderującego  rysunek rodzica jest równy zero

    // Update is called once per frame
    void Update ()
    {
        if(gameObject.GetComponentInParent<SpriteRenderer>().color.a==0)
        {
            transform.GetComponent<ParticleSystem>().enableEmission = false;
        }
	}
}
