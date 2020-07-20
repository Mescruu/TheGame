using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvertMaterial : MonoBehaviour {

    public Material material1;
    public SpriteRenderer sprite;
    public bool invert;
    public float timetoinvert =0;

    //shadery i materiał umożliwiający zmiane koloru
    public Shader shader;
    public Material _mat;
    //czas trwania zmiany
    public float InvertTime=1f;
    public float InvertTimeCD=1f;
    //dodatkowe obiekty
    public GameObject Cape;
    public GameObject Particle;
    //Dźwięk
    public AudioClip EndSound;
    public AudioClip MiddleSound;
    //Odtwarzacz
    public AudioSource audioSource;
    public bool playOnce;
    public bool playOnceMiddle;

    void Start()    
    {
    playOnce = true;
    sprite = gameObject.GetComponent<SpriteRenderer>();
    shader = Shader.Find("Custom/2dInvert");
    invert = false;
    Particle.SetActive(false);
    }

    void Update()
    {
        if(InvertTime>0) //odwracanie
        {
            InvertTime -= Time.deltaTime;
        }
        else
        {
            invert = false;
        }
        if (invert)
        {
            if(playOnceMiddle)
            {
                audioSource.clip = MiddleSound;
                audioSource.loop = true;
                audioSource.Play();
                playOnceMiddle = false;
            }
            sprite.material = _mat;
            if (gameObject.name == "Player")
            {
                Cape.GetComponent<SpriteRenderer>().material = _mat;
                Particle.SetActive(true);
            }
            if (timetoinvert<1)
            {
                timetoinvert += Time.deltaTime*4;
                _mat.SetFloat("_Threshold", timetoinvert);
            }
        }
        else
        {
            if(playOnce)
            {
                audioSource.loop = false;
                audioSource.clip = EndSound;
                audioSource.Play();
                playOnce = false;
            }
            if (timetoinvert > 0)
            {
                timetoinvert -= Time.deltaTime * 4;
                _mat.SetFloat("_Threshold", timetoinvert);
            }
            else
            {
                timetoinvert = 0;
                sprite.material = material1;
                if(gameObject.name=="Player")
                {
                    Cape.GetComponent<SpriteRenderer>().material = material1;
                    Particle.SetActive(false);
                }
            }
        }
    }
}
