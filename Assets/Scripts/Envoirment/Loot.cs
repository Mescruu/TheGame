using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class Loot : MonoBehaviour {

    //Zmienne określające loot
    public bool RandomLoot;

	private int countOfRune; //Runy
    public int MinRunes;
    public int MaxRunes;
	public bool Rune;

	public bool Money; //Złoto
    public int MoneyMin;
    public int MoneyMax;
    public int gold;

    public bool Knife; //Noże
    public int MinKnife;
    public int MaxKnife;
    public GameObject knifeObj;

    public int countOfKnife;
    public float timeToDestroy;

    public SpriteRenderer sprite; //Rysunek przedstawiający "loot"
	public Collider2D col;

	public float speed; //szybkość z którą wyskakuje loot

    //Obiekty
	private PlayerAttack attack;
	private Player_Controller player;
	private Game_Master gm;
    public bool Enter;
    private ParticleSystem particle;
    private AudioSource audio;

    void Start () {
        if (RandomLoot) //Losowy loot
        {
            int randomNumber;
            Knife = false;
            Rune = false;
            Money = false;
            randomNumber = Random.Range(1, 2);

            if (randomNumber == 1)
                Money = true;

            if (randomNumber == 2)
                Rune = true;
            
            if (randomNumber == 3)
                Knife = true;
            
            RandomLoot = false;
        }

        Enter = false;

        countOfRune = Random.Range (MinRunes,MaxRunes);

		if (Money) 
            gold = Random.Range (MoneyMin,MoneyMax);
	
		if(knifeObj!=null)
        {
            countOfKnife = Random.Range(MinKnife, MaxKnife);
            Knife = true;
        }

        //Dołaczenie komponentów
        particle = gameObject.GetComponentInChildren<ParticleSystem>(); //cząsteczki
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Controller>();
        audio = gameObject.GetComponent<AudioSource>();
        sprite = gameObject.GetComponent<SpriteRenderer>();
        col = gameObject.GetComponent<Collider2D>();
		GetComponent<Rigidbody2D> ().velocity = new Vector2 (GetComponent<Rigidbody2D> ().velocity.x, speed);
		gm = GameObject.FindGameObjectWithTag ("GameMaster").GetComponent<Game_Master> ();
		attack = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerAttack> ();
		
	}
	
	// Update is called once per frame
	void Update () {
        if(Enter) //jezeli gracz wejdzie w loot
        {
            particle.enableEmission = false;
            sprite.enabled = false;
            col.enabled = false;
            gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            gameObject.layer = 13;
            timeToDestroy -= Time.deltaTime;
            if (timeToDestroy <= 0)
            {
                Destroy(gameObject, 0);
            }
        }
	}
	void OnTriggerEnter2D(Collider2D col) {
	    if (col.gameObject.tag == "Player") 
	    {
         Enter = true;
         audio.Play(); //dźwięk zgarnięcia lootu

            //wyswietlenie informacji i.. 
            if (Knife) //dodanie do ekwipunku noży
			{
                knifeObj.GetComponent<ElementEq>().Count = countOfKnife;
                gm.ShowInfo(4f, "you get " + knifeObj.name +" :" + countOfKnife);
                gm.AddKnife(knifeObj);
            }
			if(Rune) //dodanie do ekwipunku run
            {
                gm.ShowInfo(4f, "you get rune: " + countOfRune);
                gm.runes += countOfRune;
            }

            if (Money) //dodanie do ekwipunku złota
            {
                gm.ShowInfo(4f, "you get gold: " + gold);
                gm.gold += gold;
            }
	    }
    }
}
