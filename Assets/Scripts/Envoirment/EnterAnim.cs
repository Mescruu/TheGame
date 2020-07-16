using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterAnim : MonoBehaviour {

    //Zmienne dotyczace cząsteczek animacji itd
    private Animation anim;
    public AnimationClip animationClip;
    public AudioClip[] clip;
    private AudioSource audioSource;
    public ParticleSystem particle;
    private ParticleSystem instantiateParticle;
    public bool ParticleFollow;

	void Start () {
        ParticleFollow = false;
        anim = gameObject.GetComponent<Animation>();
            if (anim != null)
            {
                anim.clip = animationClip;
            }
        audioSource = gameObject.GetComponent<AudioSource>();
        }

    void OnTriggerEnter2D(Collider2D col)
    {

        if (col.gameObject.tag == "Player")
        {
            if (anim != null)
            {
                anim.Play();
            }
            Debug.Log("PlayerEnter");
            if (audioSource != null)
            {
                audioSource.clip=clip[(int)Random.Range(0, clip.Length)];
                audioSource.Play();
            }
            if (particle != null)
            {
                if (ParticleFollow)
                {
                    instantiateParticle = Instantiate(particle, col.gameObject.transform);
                }
                else
                {
                    instantiateParticle = Instantiate(particle, col.transform.position, col.transform.rotation);
                    float timetoDestroy = instantiateParticle.startLifetime;
                    Destroy(instantiateParticle, timetoDestroy);
                }
            }
        }
    }
    void OnTriggerStay2D(Collider2D col)
    {
        if (ParticleFollow)
        {
            if (col.gameObject.tag == "Player")
            {
                Debug.Log("PlayerEnter to animation trigger");
                if (Mathf.Abs(col.gameObject.GetComponent<Rigidbody2D>().velocity.x) < 0.1f && Mathf.Abs(col.gameObject.GetComponent<Rigidbody2D>().velocity.y) < 0.1f)
                {
                    Debug.Log("Gracz sie nie poruszza");
                    if (audioSource != null)
                    {
                        audioSource.Stop();
                    }
                    if (particle != null)
                    {
                        instantiateParticle.enableEmission = false;
                    }
                }
                else
                {
                    Debug.Log("Gracz sie poruszza");
                    if (audioSource != null)
                    {
                        if (!audioSource.isPlaying)
                        {
                            audioSource.clip = clip[(int)Random.Range(0, clip.Length)];
                            audioSource.Play();
                        }
                    }
                    if (particle != null)
                    {
                        instantiateParticle.enableEmission = true;
                    }
                }
            }
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (ParticleFollow)
        {
            if (col.gameObject.tag == "Player")
            {

                Debug.Log("Player Exit");
                if (audioSource != null)
                {
                    audioSource.clip = clip[(int)Random.Range(0, clip.Length)];
                    audioSource.Play();
                }
                if (particle != null)
                {
                    float timetoDestroy = instantiateParticle.startLifetime;
                    instantiateParticle.enableEmission = false;
                    Destroy(instantiateParticle, timetoDestroy);
                }
            }
        }
    }
}
