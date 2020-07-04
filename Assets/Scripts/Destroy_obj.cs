using UnityEngine;
using System.Collections;

public class Destroy_obj : MonoBehaviour {

	public float TimeToDestroy;
	public GameObject ObjectToApper;
	public bool shouldAppear;
	private Camera_Following cam;
	public bool shouldShakeCamera;
	public float ShakeDur;
	public float ShakePow;
	public bool shouldShakeCameraOnStart;
    private AudioSource audioSource;
    public AudioClip audioClip;
    // Use this for initialization
    private ParticleSystem particle;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rgb2d;
    private Transform Player;
    private float dist;
    private float distCam;
    public GameObject Shadow;
    void Start ()
    {
        Player = GameObject.Find("Player").GetComponent<Transform>();

        rgb2d = gameObject.GetComponent<Rigidbody2D>();

        audioSource = gameObject.GetComponent<AudioSource>();
        if(audioClip!=null)
        {
            audioSource.clip = audioClip;
            audioSource.Play();
        }
        cam = GameObject.Find("Main Camera").GetComponent<Camera_Following> ();

		if (shouldShakeCameraOnStart) 
			{
            cam.ShakeCamera(ShakePow, ShakeDur);
			}
        if(gameObject.GetComponentInChildren<ParticleSystem>()!=null)
        {
            particle = gameObject.GetComponentInChildren<ParticleSystem>();
            spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        }
        if (Shadow != null)
        {
            Instantiate(Shadow, transform);
        }
    }

        // Update is called once per frame
        void Update () {

        distCam = Vector3.Distance(cam.transform.position, transform.position);

        if (audioSource != null)
        {
            dist = Vector3.Distance(Player.position, transform.position);

            if (dist < 1000)
            {
                audioSource.volume = 1 - (dist / 1000);
                if (transform.position.x > cam.transform.position.x)
                {
                    audioSource.panStereo = (distCam / 1000);
                }
                else
                {
                    audioSource.panStereo = (distCam / -1000);
                }
            }
            else
            {
                audioSource.volume = 0;
            }

           
        }
        /*
        if (Shadow != null)
        {
            float startAlpha = 1;
            if (dist < 1000)
            {
                Shadow.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, startAlpha - (dist / 1000));
            
            }
            else
            {
                Shadow.GetComponent<SpriteRenderer>().color = new Color(1,1,1,0);
            }
        }
      */

		TimeToDestroy -= Time.deltaTime;
			if(TimeToDestroy<=0)
        {
            if (audioSource != null)
            {
                audioSource.Stop();
            }
			if(shouldShakeCamera)
			{
				cam.ShakeCamera(ShakePow,ShakeDur);
			}


            DestroyObj();


            if (shouldAppear)
			{
			Instantiate (ObjectToApper, transform.position, transform.rotation);
                shouldAppear = false;
			}   
		}
	}
    void DestroyObj()
    {
        if (particle != null)
        {

            float timetoDestroy = particle.startLifetime;
            particle.enableEmission = false;
            Destroy(gameObject, timetoDestroy);

            if(rgb2d!=null)
            {
                rgb2d.isKinematic = true;

            }
            if(gameObject.GetComponent<Collider2D>()!=null)
            {
                gameObject.GetComponent<Collider2D>().enabled = false;
            }
            if (gameObject.GetComponent<SpriteRenderer>() != null)
            {
                spriteRenderer.enabled = false;
            }
            if (audioSource != null)
            {
                audioSource.Stop();
            }
        }       
        else
        {
            Destroy(gameObject);
        }
    }
}
