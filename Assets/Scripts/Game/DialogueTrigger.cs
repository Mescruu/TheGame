using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; //działanie na scenach

public class DialogueTrigger : MonoBehaviour
{
    public int DialogueID=0;
    [Tooltip("use when player have to go inside some area")]
    public bool OnEnter;

    [Tooltip("use when player have to click something to interact")]
    public bool OnClick;

    [Tooltip("use when player can only once read that dialogue")]
    public bool OnlyOnce;

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
            if (OnEnter)
            {
                if (OnlyOnce) //jezeli ma się to odpalić tylko raz
                {
                    Debug.Log("Dialog trigger name:"+ gameObject.GetInstanceID());
                    if(PlayerPrefs.HasKey("Dialogue" + SceneManager.GetActiveScene().buildIndex + DialogueID) ==false)//jeżeli nie gracz nie odbył tej rozmowy
                    {
                        BackPicInputText.SetActive(true);
                        ExitDesk = false;

                        dialogueManager.StartDialogue(dialogue, 0);
                        dialogueManager.counterOfSentences = 0; //wyzerowanie liczonych sentencji
                        PlayerPrefs.SetString("Dialogue" + SceneManager.GetActiveScene().buildIndex+DialogueID, "visited"); //zaznacz, że rozmowa się odbyła
                    }
                }
                else
                {
                    BackPicInputText.SetActive(true);
                    ExitDesk = false;

                    dialogueManager.StartDialogue(dialogue, 0);
                    dialogueManager.counterOfSentences = 0; //wyzerowanie liczonych sentencji
                }
            }
            if (OnClick)
            {
                BackPicInputText.SetActive(true);
                ExitDesk = false;

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
            textToRead.text = "[" + keyMenager.keys["Action"] + "] To Talk";

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
                        dialogueManager.counterOfSentences = 1; //wyzerowanie liczonych sentencji
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
    