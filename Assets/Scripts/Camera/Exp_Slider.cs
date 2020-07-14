using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Exp_Slider : MonoBehaviour
{
    //Ustawienia pasków
    public Slider slider1;
    public Image LvlPic;
    public Color32 LvlColor;
    public Color32 LvlChangeColor;
    public Slider slider2;

    //Zmienne liczbowe dot levela
	private float NeededExp;
    private float nextlevel;
    private float percentlvl;

    //Zmienne tekstów
	public Text LevelText1;
	public Text LevelText2;
	public Text YourScore;
    public Text YourScorePercent;
    public Text NeededText;
	public Text NeededPointsText;

    private Game_Master gm;

    private bool resetSlider;
    private float delayLvl;

    // Use this for initialization
    void Start ()
    {
    //odnalezienie obiektu Game_Master i ustawienie poczatkowych wartości
        gm = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<Game_Master>();
        resetSlider = false;
        delayLvl = 0;
        LvlPic.color = LvlColor;
    }
	
	// Update is called once per frame
	void Update ()
    {
        nextlevel = gm.nextLevel;

        //Wypełnianie tekstów
		LevelText1.text = ("" + gm.PlayerLevel);
		LevelText2.text = ("" + gm.PlayerLevel);
		YourScore.text = ("" + Mathf.Round((int)gm.exp));
		NeededText.text = ("" + (int)Mathf.Round(nextlevel - gm.exp));
		NeededPointsText.text = ("" + Mathf.Round((int)nextlevel));

        //Oblaczanie procentowe osiągniętego exp do kolejnego poziomu
        percentlvl = Mathf.Round(((gm.exp * 100f) / nextlevel));
        YourScorePercent.text = percentlvl.ToString("0") + "%";

        //slider HUD
        slider1.maxValue = 100f;
        
        //Wypełnianie paska
        if (!resetSlider)
        {
            slider1.value = Mathf.MoveTowards(slider1.value, percentlvl, 1f);
        }
        if (delayLvl > 0)
        {
            delayLvl -= Time.deltaTime;
            LvlPic.color = LvlChangeColor;
        }
        else
        {
            LvlPic.color = LvlColor;
        }

        //Ustawienia slidera 
        slider2.maxValue =100f;
		slider2.value=Mathf.MoveTowards(slider2.value, percentlvl, 100f);

	}
    //Ustawienia podczas zdobycia levla
    public void NewLevel()
    {
        resetSlider = true;
        slider1.value = Mathf.MoveTowards(slider1.value, 0, 1000f);
        resetSlider = false;
    }
    public void onValueChange()
    {
        delayLvl = 1;
    }

}
