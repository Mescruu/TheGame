using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour {

  
    private ParticleSystem particleSystem;
    public GameObject particleObj;
    public Color32 color;
    public Transform transform;

    //Tworzenie cząstek na podstawie ustawionego koloru
	void Start () {
        particleSystem = particleObj.GetComponent<ParticleSystem>();
        particleSystem.startColor = color;
    }
}
