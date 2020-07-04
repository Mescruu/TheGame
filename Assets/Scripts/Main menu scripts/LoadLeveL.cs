using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoadLeveL : MonoBehaviour {
	
	private MainMenu gmm;

	public GameObject LoadLevelUI;


	public int levels;

	void Start()

	{
		gmm = GameObject.FindGameObjectWithTag ("MainMenu").GetComponent<MainMenu> ();

}

	void OnGUI()
	{

	}
	void Update()
	
	{

		levels = PlayerPrefs.GetInt ("HighLeveL");

	}
	public void Return()
	{

		Application.LoadLevel (0);


	}
	
	public void Level1Stg1(){



		if (levels >= 2)
				{

			Application.LoadLevel (2);
		}
	
	
	}




	public void Level2Stg1(){

		if (levels >= 3)
		{
			
			Application.LoadLevel (3);
		}
		
	}
	
	public void Level3Stg1(){

		
		if (levels >= 4)
		{
			
			Application.LoadLevel (4);
		}
		
	}
	public void Level1Stg2(){
		
		
if (levels >= 5)
				{

			Application.LoadLevel (5);
		}
		
	}
	public void Level2Stg2(){
		
		
		if (levels >= 6)
		{
			
			Application.LoadLevel (6);
		}
		
	}

	
	public void Level3Stg2()
	{
	
	
		if (levels >= 7)
		{
			
			Application.LoadLevel (7);
		}	
	
}

	public void Level1Stg3(){
		
		
		if (levels >= 8)
		{
			
			Application.LoadLevel (8);
		}
		
	}
	public void Level2Stg3(){
		
		
		if (levels >= 9)
		{
			
			Application.LoadLevel (9);
		}
		
	}
	
	
	public void Level3Stg3()
	{
		
		
		if (levels >= 10)
		{
			
			Application.LoadLevel (10);
		}
		
	}

}

