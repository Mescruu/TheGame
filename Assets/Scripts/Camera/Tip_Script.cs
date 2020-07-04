using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Tip_Script : MonoBehaviour {

	public string textonTip;
	private string nameOfTip;
	public int numberOfTip;
	public bool tipVisited;
	private float time=5f;
	public bool addEnter;


    public GameObject TipButton;
    public GameObject TipDesk;
    public bool cantip;

    // Use this for initialization
    void Start () 
	{

		nameOfTip = "TipNummber" + numberOfTip;
        //cantip = wczytanie czy tip byl uzyty;
        TipButton.SetActive(false);

    }

    // Update is called once per frame
    void Update () 
	{

		if (tipVisited&&cantip) 
				{
            TipButton.SetActive(true);
            cantip = false;
                }



    }
	void OnTriggerEnter2D(Collider2D col) 
	{
		if (col.gameObject.tag == "Player") 
		{
            tipVisited = true;

                if (textonTip.Length >= 20 && !addEnter)
                {
                    textonTip = textonTip.Insert(textonTip.Length / 2, "\n");
                    addEnter = true;
                }

          //      tipText.text = textonTip;


          
			
			
		}
	}
	void OnTriggerExit2D(Collider2D col) 
	{
		if (col.gameObject.tag == "Player") 
		{
		
			tipVisited=false;
           

		}
	}
    public void Open()
    {


            TipButton.SetActive(false);
            TipDesk.SetActive(true);
            //zapisac ze tip odwiedzony

    }
    public void Exit()
    {

        TipDesk.SetActive(false);

    }
}
