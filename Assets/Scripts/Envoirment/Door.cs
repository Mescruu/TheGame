using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Door : MonoBehaviour {


	public int DoorID; //Id drzwi
	public int LevelToLoad; //który level ma zostać załadowany

    //dane podpowiedzi
    public GameObject desk;
    public Text text;
	public bool loadLev;
	
    //Czas po którym podpowiedź zniknie
    private float ExitTimeCD=1f;
    private float ExitTime;
    private bool Exit;

    private Player_Controller player;
    private KeyMenager keyMenager;
    private Game_Master gm;
    void Start()
	{
        //Początkowe wartości
        desk.SetActive(false);
        ExitTime = ExitTimeCD;
        Exit = false;

        //Dołaczenie komponentów
        gm = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<Game_Master>();
        keyMenager = GameObject.Find("KeyMenager").GetComponent<KeyMenager>();
        player = GameObject.Find("Player").GetComponent<Player_Controller>();
    }
    void Update()
    {
        if(Exit)//Zniknięcie podpowiedzi po danym czasie
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
        if (col.gameObject.tag == "Player") //Atywowwanie podpowiedzi
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
			if(Input.GetKeyDown(keyMenager.keys["Action"]) || loadLev) //PRzejście do nastepnego poziomu
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

	void OnTriggerExit2D(Collider2D col) //Gracz opuścił kolizję 
	{
        if (col.gameObject.tag == "Player")
        {
            ExitTime = ExitTimeCD;
            Exit = true;
		}
	}
}
