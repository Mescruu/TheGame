using UnityEngine;
using System.Collections;
using UnityEngine.UI; //można używać UI w skryptach 

	
public class H_U_D : MonoBehaviour {

    public Slider sliderhp;
    public Image HpPic;

    public Color32 HpColor;
    public Color32 HpChangeColor;


    public Slider slidermana;
    public Image MpPic;

    public Color32 MpColor;
    public Color32 MpChangeColor;

    //public Sprite[] StaminaSprites;     //[] bo wiecej obiektow w jednym
    public Image Stamina;

    public Text textHP;
    public Text textMANA;


    public Image StaminaUI;

	private Player_Controller player;

    private float percenthealth;
    private float percentmana;
    private string frictionthealth;
    private string frictiontmana;

    public float maxPicRad;

    private float delayHp;
    private float delayMp;



    void Start()
	{

		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player_Controller> ();
        delayMp = 0f;
        delayHp = 0f;
        HpPic.color = HpColor;
        MpPic.color = MpColor;

    }

    void Update () {

        percenthealth = (100f*player.curHP)/player.maxHP;
        percentmana = (100f*player.curMana)/player.MaxMana;

        slidermana.maxValue = 100;

        slidermana.value = Mathf.MoveTowards(slidermana.value, percentmana, 3f);

        frictionthealth = percenthealth.ToString();
    


        sliderhp.maxValue = 100;

        sliderhp.value = Mathf.MoveTowards(sliderhp.value, percenthealth, 3f);

        frictiontmana = percentmana.ToString();

   

        // StaminaUI.sprite = StaminaSprites[player.curStamina];
        Stamina.fillAmount = player.curSTM/100f * maxPicRad;

        if (delayHp > 0)
        {
            delayHp -= Time.deltaTime;

            HpPic.color = HpChangeColor;

        }
        else
        {
            HpPic.color = HpColor;

        }

        if (delayMp > 0)
        {
            delayMp -= Time.deltaTime;
            MpPic.color = MpChangeColor;


        }
        else
        {
            MpPic.color = MpColor;

        }
    }
    public void onValueChangeHP()
    {
        Debug.Log(sliderhp.value);
        delayHp = 1;
    }
    public void onValueChangeMp()
    {
        Debug.Log(sliderhp.value);
        delayMp = 1;
    }



}