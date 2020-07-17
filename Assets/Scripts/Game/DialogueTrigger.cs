using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DialogueTrigger : MonoBehaviour
{

    [Tooltip("use when player have to go inside some area")]
    public bool OnEnter;

    [Tooltip("use when player have to click something to interact")]
    public bool OnClick;

    [Tooltip("use in animation")]
    public bool StartDialogue;
    private bool activeOnce;

    public Dialogue[] dialogue; //dialogi

    public GameObject BackPicInputText;
    public Text textToRead;

    private bool ExitDesk;
    public float ExitTimeCD;
    private float ExitTime;

    private KeyMenager keyMenager;
    private DialogueManager dialogueManager;

    void Start()
    {
        //poczatkowe wartosci
        activeOnce = true;
        StartDialogue = false;
        ExitTime = ExitTimeCD;

        //dołaczenie komponentów
        keyMenager = GameObject.Find("KeyMenager").GetComponent<KeyMenager>();
        dialogueManager = FindObjectOfType<DialogueManager>().GetComponent<DialogueManager>();
    }
    public void Update()
    {
        if (StartDialogue && activeOnce) //rozpoczęcie dialogu
        {
            dialogueManager.StartDialogue(dialogue,0); //wyslanie dialogów
            StartDialogue = false;
            activeOnce = false;
        }
        if (ExitDesk)
        {
            BackPicInputText.GetComponent<Animator>().SetBool("disappear", true);

            ExitTime -= Time.deltaTime;
            if (ExitTime < 0)
            {
                BackPicInputText.SetActive(false);
                ExitDesk = false;
            }
        }
        else
        {
            ExitTime = 0;
        }
    }
    public void TriggerDialogue() //wejscie w dialog
    {
        dialogueManager.StartDialogue(dialogue, 0);
    }
    void OnTriggerEnter2D(Collider2D col) //wejscie w trigger (zalezy czy należy kliknąć żeby porozmawiać, czy wystarczy wejść)
    {
        if (col.CompareTag("Player"))
        {
            if(OnEnter)
            {
                dialogueManager.StartDialogue(dialogue, 0);
            }
            if(OnClick)
            {
                BackPicInputText.SetActive(true);
                textToRead.text = "[" + keyMenager.keys["Action"] + "] To Talk";
                ExitDesk = false;
            }
        }
    }

    void OnTriggerStay2D(Collider2D col) //jezeli jest gracz w kolizji
    {
        if(OnClick)
        {
            if (col.gameObject.tag == "Player")
            {
                if (Input.GetKeyDown(keyMenager.keys["Action"]))
                {
                    FindObjectOfType<DialogueManager>().player_in_trigger = true;

                    Debug.Log("Player-Input-Action");

                    if (FindObjectOfType<DialogueManager>().AnimChild.active == false)
                    {
                        dialogueManager.tap = 0;
                        dialogueManager.StartDialogue(dialogue, 0);
                    }
                }
                BackPicInputText.SetActive(true);
            }
        }
    }

    void OnTriggerExit2D(Collider2D col) //jezeli wyjdzie z niej
    {
        if (col.gameObject.tag == "Player")
        {
            dialogueManager.player_in_trigger = false;
            if (OnClick)
            {
                ExitTime = ExitTimeCD;
                ExitDesk = true;
            }
        }
    }
}
    