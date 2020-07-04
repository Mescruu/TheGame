using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Exp_Slider : MonoBehaviour
{

    public Slider slider1;

    public Image LvlPic;
    public Color32 LvlColor;
    public Color32 LvlChangeColor;

    public Slider slider2;

	private float NeededExp;

    private float nextlevel;
    private float percentlvl;

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
        gm = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<Game_Master>();
        resetSlider = false;
        delayLvl = 0;
        LvlPic.color = LvlColor;
    }
	
	// Update is called once per frame
	void Update ()
    {
        nextlevel = gm.nextLevel;

		LevelText1.text = ("" + gm.PlayerLevel);
		LevelText2.text = ("" + gm.PlayerLevel);


		YourScore.text = ("" + Mathf.Round((int)gm.exp));
		NeededText.text = ("" + (int)Mathf.Round(nextlevel - gm.exp));
		NeededPointsText.text = ("" + Mathf.Round((int)nextlevel));

        percentlvl = Mathf.Round(((gm.exp * 100f) / nextlevel));

        YourScorePercent.text = percentlvl.ToString("0") + "%";

        //slider HUD
      
        slider1.maxValue = 100f;
        
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

        //slider UI	
        slider2.maxValue =100f;
		
		slider2.value=Mathf.MoveTowards(slider2.value, percentlvl, 100f);

	}
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
