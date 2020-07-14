using UnityEngine;
using System.Collections;

public class Destroy_obj : MonoBehaviour {

	public float TimeToDestroy; //Czas do zniszczenia obiektu
	public GameObject ObjectToApper; //Jaki obiekt ma się pojawić po zniszczeniu
	public bool shouldAppear;   //Czy taki obiekt wgl powinien się pojawić

	private Camera_Following cam; //Kamera 
	public bool shouldShakeCamera; //Trzęsienie się kamery
	public float ShakeDur; //Czas trzęsienia
	public float ShakePow; //Moc trzęsienia
	public bool shouldShakeCameraOnStart; //Czy powinna się trząść kamera na poczatku

    private AudioSource audioSource; //Dźwięk
    public AudioClip audioClip;

    private ParticleSystem particle;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rgb2d;
    private Transform Player;

    private float dist; //Dystans od gracza
    private float distCam; //Dystans od kamery
    public GameObject Shadow; //Cień

    void Start ()
    {
        //Dołączenie do obiektu odpowiednich komponentów
        Player = GameObject.Find("Player").GetComponent<Transform>();
        rgb2d = gameObject.GetComponent<Rigidbody2D>();
        audioSource = gameObject.GetComponent<AudioSource>();
        cam = GameObject.Find("Main Camera").GetComponent<Camera_Following>();

        if (audioClip!=null)
        {
            audioSource.clip = audioClip;
            audioSource.Play();
        }

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

        //Zniszczenie obiektu z danym efektem
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

            if (shouldAppear) //jeżeli coś ma się pojawić to w tym momencie
			{
			Instantiate (ObjectToApper, transform.position, transform.rotation);
            shouldAppear = false;
			}

            DestroyObj();
        }
    }

    //Niszczenie obiektu i rzeczy z nich stricte powiązanych
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
