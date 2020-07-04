using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DialogueManager : MonoBehaviour {

    private Queue<string> sentences;
    private KeyMenager keyMenager;

    public Image PicFrame;
    private Text DialogueText;

    public Text PlayerDialogueText;
    public Text OtherDialogueText;
    [Tooltip("0.1f is slow and ok")]
    public float TypingDelay=0.01f;
    private Animator anim;
    public GameObject AnimChild;
    private bool TimeToEnd;
    private float timeCd=1f;
    private float time = 1f;
    public int tap = 0;

    public GameObject BackPicInputText;
    public Text textToRead;
    public bool player_in_trigger;
    private bool makingConversation;
    private Game_Master gm;
    public AudioSource audioSource;
    public AudioClip[] speaking;
    public bool typing;
    private string sentence;
    private int Counter;
    private Dialogue[] dialogue;
    // Use this for initialization
    void Start() {
        makingConversation = false;
        sentences = new Queue<string>();
        keyMenager = GameObject.Find("KeyMenager").GetComponent<KeyMenager>();
        gm = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<Game_Master>();
        anim = AnimChild.GetComponent<Animator>();
        time = timeCd;
        audioSource = AnimChild.GetComponent<AudioSource>();
    }
    void Update()
    {
        gm.makingConversation = makingConversation;
        if (AnimChild.activeSelf==true && makingConversation)
        {
            BackPicInputText.SetActive(true);
            if (sentences.Count == 0)
            {
                textToRead.text = "[" + keyMenager.keys["Action"] + "] To end conversation";

            }
            else
            {
                textToRead.text = "[" + keyMenager.keys["Action"] + "] To skip";

            }
        }
        if (Input.GetKeyDown(keyMenager.keys["Action"]) && !TimeToEnd)
        {
            tap++;
            Debug.Log(tap);
            if (tap > 1)
            {
                if (typing)
                {
                    DialogueText.text = sentence;
                    StopAllCoroutines();
                    typing = false;
                }
                else
                {
                    PlayerDialogueText.text = " ";
                    OtherDialogueText.text = " ";
                    DisplayNextSentence();
                }
                
            }
        }
       
        if (TimeToEnd)
        {
    
            audioSource.Pause();
            if (!player_in_trigger && makingConversation)
            {
                BackPicInputText.GetComponent<Animator>().SetBool("disappear", true);
            }
            else
            {
                textToRead.text = "[" + keyMenager.keys["Action"] + "] To Talk";
            }
            if (time > 0)
            {
                time -= Time.deltaTime;

            }
            else
            {
                Debug.Log("AnIMVECHIL SET ACTIVE FALSE");
                makingConversation = false;
                AnimChild.SetActive(false);
                TimeToEnd = false;
            }
        }
    }

    public void StartDialogue(Dialogue[] Dialogue, int i)
    {
        Counter = i;
        dialogue = Dialogue;
        makingConversation = true;
        PlayerDialogueText.text = " ";
        OtherDialogueText.text = " ";
        Debug.Log("Starting conversation with" + dialogue[Counter].name);
        time = timeCd;
        TimeToEnd = false;
        PicFrame.sprite = dialogue[i].Portrait;
        if (dialogue[Counter].name == "Player")
        {
            DialogueText = PlayerDialogueText;
        }
        else
        {
            DialogueText = OtherDialogueText;
        }
        Debug.Log("SET ACTIVE ANIMCHILD");
        AnimChild.SetActive(true);
        Debug.Log("SET ACTIVE ANIMCHILD AAfter");

        Debug.Log(" sentences.Clear();");

        sentences.Clear();
        Debug.Log("foreach Counter= " + Counter);

        Debug.Log("sentences.Count="+ sentences.Count);

        foreach (string sentence in dialogue[Counter].sentences)
        {
            sentences.Enqueue(sentence);
        }

        Debug.Log("sentences.Count=" + sentences.Count);

        DisplayNextSentence();
        AnimChild.SetActive(true);

    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            TimeToEnd = true;

            if(Counter == dialogue.Length-1)
            {
                EndDialogue();
                return;
            }
            else
            {
                Counter++;
                Debug.Log("Next dialogue");
                StopAllCoroutines();
                sentence = null;
                StartDialogue(dialogue, Counter);
                return;
            }
        }
        sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
        Debug.Log(sentence);
    }
    IEnumerator TypeSentence(string sentence)
    {
        
        int i = 0;

        DialogueText.text = " ";
        foreach(char letter in sentence.ToCharArray())
        {
            i++;
            if(letter!=' ')
            {
                audioSource.Pause();
                audioSource.clip = speaking[(int)Random.Range(0, speaking.Length)];
                audioSource.Play();
            }
            else
            {
                audioSource.Pause();
            }

            if (i == sentence.Length)
            {
                typing = false;
            }
            else
            {
                typing = true;
            }

            DialogueText.text += letter;

            yield return new WaitForSeconds(TypingDelay);
            
        }
    }
    public void EndDialogue()
    {
        Debug.Log("end of conversation");
        anim.SetBool("disappear", true);
    }
}
