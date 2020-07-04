using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Door : MonoBehaviour {


	public int DoorID;
	public int LevelToLoad;
	private Game_Master gm;
	public GameObject desk;
    public Text text;
	public bool loadLev;
	private Player_Controller player;
    private KeyMenager keyMenager;
    private float ExitTimeCD=1f;
    private float ExitTime;
    private bool Exit;
	void Start()
	{
		gm = GameObject.FindGameObjectWithTag ("GameMaster").GetComponent<Game_Master> ();
        desk.SetActive(false);
        keyMenager = GameObject.Find("KeyMenager").GetComponent<KeyMenager>();
        player = GameObject.Find("Player").GetComponent<Player_Controller>();
        ExitTime = ExitTimeCD;
        Exit = false;
    }
    void Update()
    {
        if(Exit)
        {
            desk.GetComponent<Animator>().SetBool("disappear", true);

            ExitTime -= Time.deltaTime;
            if (ExitTime < 0)
            {
                desk.SetActive(false);
                Exit = false;
            }
        }
        else
        {
            ExitTime = 0;
        }

    }
    void OnTriggerEnter2D(Collider2D col)
	{

        if (col.gameObject.tag == "Player")
        {

            desk.SetActive(true);
            text.text = "[" +keyMenager.keys["Action"] + "] To enter";
            Exit = false;
			}


	}

	void OnTriggerStay2D(Collider2D col)
	{

        if (col.gameObject.tag=="Player" && player.curHP>0)
			    {
				    if(Input.GetKeyDown(keyMenager.keys["Action"]) || loadLev)
				    {
                if(player.curHP>0)
                {
                    gm.DoorID = DoorID;
                    gm.SceneIdToLoad = LevelToLoad;
                    gm.saving = true;
                    gm.loadScene = true;
                    desk.SetActive(true);
                }
            }
        }
	}

	void OnTriggerExit2D(Collider2D col)
	{

        if (col.gameObject.tag == "Player")
        {
            ExitTime = ExitTimeCD;
            Exit = true;
		}


	}


}
