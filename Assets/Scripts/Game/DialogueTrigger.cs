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

    public Dialogue[] dialogue;

    private KeyMenager keyMenager;
    public GameObject BackPicInputText;
    public Text textToRead;

    private bool ExitDesk;
    public float ExitTimeCD;
    private float ExitTime;
    private DialogueManager dialogueManager;

    void Start()
    {
        activeOnce = true;
        StartDialogue = false;
        keyMenager = GameObject.Find("KeyMenager").GetComponent<KeyMenager>();
        ExitTime = ExitTimeCD;
        dialogueManager = FindObjectOfType<DialogueManager>().GetComponent<DialogueManager>();
    }
    public void Update()
    {

        if (StartDialogue && activeOnce)
        {
            dialogueManager.StartDialogue(dialogue,0);
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
    public void TriggerDialogue()
    {
        dialogueManager.StartDialogue(dialogue, 0);

    }
    void OnTriggerEnter2D(Collider2D col)
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

    void OnTriggerStay2D(Collider2D col)
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
    void OnTriggerExit2D(Collider2D col)
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
    