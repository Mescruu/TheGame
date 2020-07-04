using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvertMaterial : MonoBehaviour {

    public Material material1;
    public SpriteRenderer sprite;
    public bool invert;
    public float timetoinvert =0;

    public Shader shader;
    public Material _mat;
    public float InvertTime=1f;
    public float InvertTimeCD=1f;
    public GameObject Cape;
    public GameObject Particle;
    public AudioClip EndSound;

    public AudioClip MiddleSound;
    public AudioSource audioSource;
    public bool playOnce;
    public bool playOnceMiddle;

    // Use this for initialization
    void Start()    
{
        playOnce = true;
    sprite = gameObject.GetComponent<SpriteRenderer>();
     shader = Shader.Find("Custom/2dInvert");
        invert = false;
        Particle.SetActive(false);
    

    }

    // Update is called once per frame
    void Update()
{
        if(InvertTime>0)
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
