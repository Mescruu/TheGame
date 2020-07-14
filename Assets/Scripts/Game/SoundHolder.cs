using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SoundHolder : MonoBehaviour
{
    public AudioSource myAudio;
    public int IdSong;
    public bool isloading;
    public bool changeSong;
    public int count_of_clips;
    public AudioClip[] playlist;
    public AudioData audioData;
    public float smoothTime;
    private int count;
    public float volume;
    private GameObject gm;
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        myAudio = gameObject.GetComponent<AudioSource>();
    }
    void Start()
    {
        myAudio.clip = playlist[IdSong]; //ustawienie utworu
        myAudio.Play();
        isloading = false;
        myAudio.volume = 0;

        myAudio.volume = myAudio.volume + (Time.deltaTime / (smoothTime / 4f + 1)); //zgłośnienie muzyki przy starcie
    }
    void Update()
	{
       

        if (isloading) // w przypadku gdy jest ładowana gra, podczas zmiany utworów następuje zciszanie itd
        {
            gm = GameObject.FindGameObjectWithTag("GameMaster");
            if (gm == null)
            {
                myAudio.loop = true;
            }
            else
            {
                myAudio.loop = false;
            }
            if (changeSong)
            {

                if (myAudio.volume > 0)
                {                                                   //czas na podglosnienie
                    myAudio.volume = myAudio.volume - (Time.deltaTime / (smoothTime+ 1));

                }
                else
                {
                    myAudio.Stop();
                    myAudio.clip = playlist[IdSong];
                    myAudio.Play();
                    changeSong = false;
                    myAudio.volume = 0f;
                }


            }
            else
            {
                if (myAudio.volume < volume)
                {                                                   //czas na podglosnienie
                    myAudio.volume = myAudio.volume + (Time.deltaTime / (smoothTime + 1));
                }
                else
                {
                    myAudio.volume = volume;

                }
            }
        }
        else
        {
            myAudio.volume = volume;

        }
    }
}