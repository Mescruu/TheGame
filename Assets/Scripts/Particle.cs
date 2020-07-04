using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour {

  
    private ParticleSystem particleSystem;
    public GameObject particleObj;
    public Color32 color;
    public Transform transform;
	// Use this for initialization
	void Start () {
        particleSystem = particleObj.GetComponent<ParticleSystem>();
        particleSystem.startColor = color;
    }

    // Update is called once per frame
    void Update () {
		
	}

 
}
