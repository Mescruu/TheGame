using UnityEngine;
using System.Collections;

public class PowerUIScript : MonoBehaviour {


	private Animator anim;

	//POWER UI
	static public int powerLevel;
	
	public bool setActivePowerUI;
	private float setActiveTimePowerUI;
	public float setActiveTimePowerUICD = 0.7f;

    public Sprite[] PowerSprite;     //[] bo wiecej obiektow w jednym
    public GameObject ShowPowerSprite;
    private PlayerAttack playerAttack;

	private Game_Master gm;
    private KeyMenager keyMenager;

    private CameraProfile camProfile;
    private float chAberration;
    // Use this for initialization
    void Start () {

		powerLevel = 0;

		setActiveTimePowerUI = setActiveTimePowerUICD;

		setActivePowerUI = false;

		gm = GameObject.FindGameObjectWithTag ("GameMaster").GetComponent<Game_Master> ();

		playerAttack = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerAttack> ();

        keyMenager = GameObject.Find("KeyMenager").GetComponent<KeyMenager>();

        anim = gameObject.GetComponent<Animator> ();
        ShowPowerSprite.SetActive(false);

        camProfile = gameObject.GetComponent<CameraProfile>();

    }

    // Update is called once per frame
    void Update () {

		if (Input.GetKeyDown(keyMenager.keys["PowerMagic"]) && playerAttack.countOfRune>0) 
		{

			setActiveTimePowerUI = setActiveTimePowerUICD;


			setActivePowerUI = true;
			powerLevel = powerLevel +1;
            gm.StopTimePowerChoose = true;
            chAberration = camProfile.ChAberrationIntensity;
            if (!camProfile.audioSource.isPlaying)
            {
                camProfile.audioSource.clip = camProfile.ChAberrationaudioClip;
                camProfile.audioSource.Play();
            }
        

        }

		if (setActivePowerUI) 
		{
            ShowPowerSprite.SetActive(true);

            setActiveTimePowerUI -= Time.deltaTime;
			if(setActiveTimePowerUI<=0)
			{
				gm.StopTimePowerChoose = false;			
					powerLevel=0;

                ShowPowerSprite.SetActive(false);
                camProfile.ChangeChAberrationAtRuntimeToPreviousSetting();

                setActivePowerUI = false;
			}else
            {
                chAberration +=Time.deltaTime/3;
                camProfile.audioSource.volume = chAberration;

                if (chAberration <= 1)
                {
                    camProfile.ChangeChAberrationAtRuntime(chAberration,true);

                }
                else
                {
                    camProfile.ChangeChAberrationAtRuntime(1,true);

                }
            }

        }
        else
        {
            chAberration = 0;
            ShowPowerSprite.GetComponent<SpriteRenderer>().sprite = PowerSprite[0];
            powerLevel = 0;
            gm.StopTimePowerChoose = false;
            ShowPowerSprite.SetActive(false);
        }


        if (Input.GetKeyDown(keyMenager.keys["ReleaseMagic"]))
		{
			powerLevel=0;
			setActivePowerUI=false;
			gm.StopTimePowerChoose = false;
            camProfile.ChangeChAberrationAtRuntimeToPreviousSetting();
        }


        if (powerLevel > 3) 
		{
			powerLevel = 1;
		}

        ShowPowerSprite.GetComponent<SpriteRenderer>().sprite = PowerSprite[powerLevel];
        gm.powerLevel = powerLevel;

	}
}
