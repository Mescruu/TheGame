using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillUI : MonoBehaviour {

    public GameObject SkillUIDesk;

    public bool fire;
    public bool chaos;
    public bool sword;
    public bool ankh;
    public bool elect;

    public GameObject FireElement;
    public GameObject ChaosElement;
    public GameObject SwordElement;
    public GameObject AnkhElement;
    public GameObject ElectElement;

    public AudioClip ElectSound;
    public AudioClip FireSound;
    public AudioClip SwordSound;
    public AudioClip AnkhSound;
    public AudioClip ChaosSound;
    public AudioSource audioSource;

    public GameObject[] FireLevelButtons;
    public GameObject[] ChaosLevelButtons;
    public GameObject[] SwordLevelButtons;
    public GameObject[] AnkhLevelButtons;
    public GameObject[] ElectLevelButtons;

    public Button[] FireShowButtons;
    public Button[] ChaosShowButtons;
    public Button[] SwordShowButtons;
    public Button[] AnkhShowButtons;
    public Button[] ElectShowButtons;

    public Color FireColor;
    public Color ChaosColor;
    public Color SwordColor;
    public Color AnkhColor;
    public Color ElectColor;

    public Text[] FireNeededMana;
    public Text[] ChaosNeededMana;
    public Text[] SwordNeededStamina;
    public Text[] AnkhNeededMana;
    public Text[] ElectNeededMana;

    public Image Bar;
    public Slider slider;
    public GameObject SkillShit;

    public float HowManyManaHaveToUse;
    public Text percentTxt;
    public Text howManyLeftTxt;
    public Text howManyDoyouNeedTxt;
    public Text HowManyDoYouUsed;

    public GameObject addAttackButton;
    public GameObject addMagicButton;
    public GameObject addAgilityButton;

    
    private float nextlevel;
    private Game_Master gm;
    private KeyMenager keyMenager;

    public Text AttackStatsText;
    public Text MagicStatsText;
    public Text AgilityStatsText;

    public Text PlayerPDPoints;
    public Text PlayerPDSkillPoints;
    public Text PlayerLevel;
    public Text SkillLevel;

    private float TimeToDisappear;
    private float TimeToDisappearCD=1f;
    private bool Disappear;
    // Use this for initialization
    void Start () {


        SkillUIDesk.SetActive(false);
        fire = false;
        chaos = false;
        sword = false;
        ankh = false;
        elect = false;
        SkillShit.SetActive(false);

        keyMenager = GameObject.Find("KeyMenager").GetComponent<KeyMenager>();

        Disappear = true;


        gm = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<Game_Master>();

        CheckPics();
    }
    void CheckPics()
    {
    
        for (int i = 0; i < FireNeededMana.Length; i++)
        {
            if (gm.PlayerFireLevel > i)
            {
                FireShowButtons[i].interactable = true;
          //      Debug.Log("eneblabled");
            }
            else
            {
              //  Debug.Log("disabled");
                FireShowButtons[i].interactable = false;

            }
            if (gm.PlayerDeathLevel > i)
            {
                ChaosShowButtons[i].interactable = true;
            }
            else
            {
                ChaosShowButtons[i].interactable = false;

            }
            if (gm.PlayerSwordLevel > i)
            {
                SwordShowButtons[i].interactable = true;
            }
            else
            {
                SwordShowButtons[i].interactable = false;

            }
            if (gm.PlayerWhiteLevel > i)
            {
                AnkhShowButtons[i].interactable = true;
            }
            else
            {
                AnkhShowButtons[i].interactable = false;

            }
            if (gm.PlayerElectLevel > i)
            {
                ElectShowButtons[i].interactable = true;
            }
            else
            {
                ElectShowButtons[i].interactable = false;

            }
        }

    }
    void Update()
    {
      
        CheckPics();
            
        if (fire)
        {
            FireElement.SetActive(true);
            SkillLevel.text = gm.PlayerFireManaLevel.ToString();
        }
        else
        {
            FireElement.SetActive(false);

        }
        if (ankh)
        {
            AnkhElement.SetActive(true);
            SkillLevel.text = gm.PlayerWhiteManaLevel.ToString();

        }
        else
        {
            AnkhElement.SetActive(false);

        }
        if (chaos)
        {
            ChaosElement.SetActive(true);
            SkillLevel.text = gm.PlayerDeathManaLevel.ToString();

        }
        else
        {
            ChaosElement.SetActive(false);

        }
        if (elect)
        {
            ElectElement.SetActive(true);
            SkillLevel.text = gm.PlayerElectManaLevel.ToString();

        }
        else
        {
            ElectElement.SetActive(false);

        }
        if (sword)
        {
            SwordElement.SetActive(true);
            SkillLevel.text = gm.PlayerSwordStaminaLevel.ToString();

        }
        else
        {
            SwordElement.SetActive(false);
        }
        if (gm.SwordUsedStamina > HowManyManaHaveToUse)
        {
            gm.MakeAnEvent(0, gm.SwordUsedStamina - 1000);
            gm.SwordUsedStamina = 0;
        }
        if (gm.FireUsedMana > HowManyManaHaveToUse)
        {
            gm.MakeAnEvent(1, gm.FireUsedMana - 1000);
            gm.FireUsedMana = 0;

        }
        if (gm.ChaosUsedMana > HowManyManaHaveToUse)
        {
            gm.MakeAnEvent(2, gm.ChaosUsedMana - 1000);
            gm.ChaosUsedMana = 0;

        }
        if (gm.AnkhUsedMana > HowManyManaHaveToUse)
        {
            gm.MakeAnEvent(3, gm.AnkhUsedMana - 1000);
            gm.AnkhUsedMana = 0;
        }
        if (gm.ElectUsedMana > HowManyManaHaveToUse)
        {
            gm.MakeAnEvent(4,gm.ElectUsedMana-1000);
        }

        PlayerLevel.text = gm.PlayerLevel.ToString();
        PlayerPDPoints.text = gm.PDPoint.ToString();
        PlayerPDSkillPoints.text = gm.SkillPDPoint.ToString();


        nextlevel = gm.nextLevel;

        if (!fire && !ankh && !elect && !sword && !chaos)
        {
            SkillShit.SetActive(false);

        }

        if (fire || ankh || elect || sword || chaos)
        {
            SkillShit.SetActive(true);
        }

        if (SkillUIDesk.activeSelf == false)
        {
            if (Input.GetKeyDown(keyMenager.keys["Experience"])&&gm.active_menu==0)
            {
                Open();
               
            }

        }
        if(SkillUIDesk.activeSelf == true)
        {
            if (Input.GetKeyDown(keyMenager.keys["Pause"]))
            {
                Close();
            }
        }

        if (gm.SkillPDPoint > 0)
        {
           for(int i=0;i<FireLevelButtons.Length;i++)
            {

               
                if (gm.PlayerFireLevel == i)
                {
                    FireLevelButtons[i].GetComponent<Animator>().SetBool("disappear", false);
                }
                else
                {
                    FireLevelButtons[i].GetComponent<Animator>().SetBool("disappear", true);
                }

                if (gm.PlayerDeathLevel == i)
                {
                    ChaosLevelButtons[i].GetComponent<Animator>().SetBool("disappear", false);
                }
                else
                {
                    ChaosLevelButtons[i].GetComponent<Animator>().SetBool("disappear", true);
                }
                if (gm.PlayerElectLevel == i)
                {
                    ElectLevelButtons[i].GetComponent<Animator>().SetBool("disappear", false);
                }
                else
                {
                    ElectLevelButtons[i].GetComponent<Animator>().SetBool("disappear", true);
                }
                if (gm.PlayerSwordLevel == i)
                {
                    SwordLevelButtons[i].GetComponent<Animator>().SetBool("disappear", false);
                }
                else
                {
                    SwordLevelButtons[i].GetComponent<Animator>().SetBool("disappear", true);
                }
                if (gm.PlayerWhiteLevel == i)
                {
                    AnkhLevelButtons[i].GetComponent<Animator>().SetBool("disappear", false);
                }
                else
                {
                    AnkhLevelButtons[i].GetComponent<Animator>().SetBool("disappear", true);
                }

            }

        }
        else
        {
            for (int i = 0; i < FireNeededMana.Length; i++)
            {
                ElectLevelButtons[i].GetComponent<Animator>().SetBool("disappear", true);
                AnkhLevelButtons[i].GetComponent<Animator>().SetBool("disappear", true);
                SwordLevelButtons[i].GetComponent<Animator>().SetBool("disappear", true);
                ChaosLevelButtons[i].GetComponent<Animator>().SetBool("disappear", true);
                FireLevelButtons[i].GetComponent<Animator>().SetBool("disappear", true);
            }
        }

        if(gm.PDPoint>0)
        {
            addAgilityButton.GetComponent<Animator>().SetBool("disappear", false);
            addMagicButton.GetComponent<Animator>().SetBool("disappear", false);
            addAttackButton.GetComponent<Animator>().SetBool("disappear", false);

            addAgilityButton.SetActive(true);
            addAttackButton.SetActive(true);
            addMagicButton.SetActive(true);
      
        }
        else
        {
            if (SkillUIDesk.activeSelf)
            {
                addAgilityButton.GetComponent<Animator>().SetBool("disappear", true);
                addMagicButton.GetComponent<Animator>().SetBool("disappear", true);
                addAttackButton.GetComponent<Animator>().SetBool("disappear", true);
            }
           
        }

        AgilityStatsText.text = gm.PlayerStatsAgility.ToString();
        AttackStatsText.text = gm.PlayerStatsAttack.ToString();
        MagicStatsText.text = gm.PlayerStatsMagic.ToString();

     


    }
    public void Open()
    {
  

        for (int i = 0; i < FireNeededMana.Length; i++)
        {

            FireNeededMana[i].text = gm.NeededManaFire[i].ToString("0");
            ChaosNeededMana[i].text =  gm.NeededManaChaos[i].ToString("0");
            AnkhNeededMana[i].text = gm.NeededManaAnkh[i].ToString("0");
            ElectNeededMana[i].text =  gm.NeededManaElect[i].ToString("0");
        }
        SwordNeededStamina[0].text = gm.JumpSpecialAttackCost.ToString("0");
        SwordNeededStamina[1].text =  gm.JumpDownSpecialAttackCost.ToString("0");
        SwordNeededStamina[2].text = gm.RunSpecialAttackCost.ToString("0");

        SkillUIDesk.SetActive(true);
        gm.active_menu = 2;
    }
    public void Close()
    {
        SkillUIDesk.SetActive(false);
        fire = false;
        chaos = false;
        sword = false;
        ankh = false;
        elect = false;
        SkillShit.SetActive(false);
        gm.RefreshEqStats();
        gm.ChangeToZero = true;


    }
    // Update is called once per frame

    public void Back()
    {
     


        howManyDoyouNeedTxt.text = " ";
        howManyLeftTxt.text = " ";
        HowManyDoYouUsed.text = " ";
        fire = false;
        chaos = false;
        sword = false;
        ankh = false;
        elect = false;
        SkillShit.SetActive(false);
    }

    public void AddAttack()
    {
        gm.PDPoint -= 1;
        gm.PlayerStatsAttack += 1;

        if (gm.PDPoint <= 0)
        {
            addAgilityButton.GetComponent<Animator>().SetBool("disappear", true);
            addMagicButton.GetComponent<Animator>().SetBool("disappear", true);
            addAttackButton.GetComponent<Animator>().SetBool("disappear", true);
        }
    }
    public void AddMagic()
    {
        gm.PDPoint -= 1;
        gm.PlayerStatsMagic += 1;
        if (gm.PDPoint <= 0)
        {
            addAgilityButton.GetComponent<Animator>().SetBool("disappear", true);
            addMagicButton.GetComponent<Animator>().SetBool("disappear", true);
            addAttackButton.GetComponent<Animator>().SetBool("disappear", true);
        }
        Open();
    }
    public void AddAgility()
    {
        gm.PDPoint -= 1;
        gm.PlayerStatsAgility += 1;
        if (gm.PDPoint <= 0)
        {
            addAgilityButton.GetComponent<Animator>().SetBool("disappear", true);
            addMagicButton.GetComponent<Animator>().SetBool("disappear", true);
            addAttackButton.GetComponent<Animator>().SetBool("disappear", true);
        }
        Open();

    }

    public void AddSkillLevel()
    {

        CheckPics();


        if (fire)
        {
            gm.PlayerFireLevel += 1;
            gm.SkillPDPoint -= 1;
            audioSource.clip = FireSound;
        }
        if(chaos)
        {
            gm.PlayerDeathLevel += 1;
            gm.SkillPDPoint -= 1;
            audioSource.clip = ChaosSound;

        }
        if (sword)
        {
            gm.PlayerSwordLevel += 1;
            gm.SkillPDPoint -= 1;
            audioSource.clip = SwordSound;

        }
        if (ankh)
        {
            gm.PlayerWhiteLevel += 1;
            gm.SkillPDPoint -= 1;
            audioSource.clip = AnkhSound;

        }
        if (elect)
        {
            gm.PlayerElectLevel += 1;
            gm.SkillPDPoint -= 1;
            audioSource.clip = ElectSound;

        }
        audioSource.Play();
    }
    
    public void Fire()
    {
        CheckPics();


        for (int i = 0; i < FireShowButtons.Length; i++)
        {
            FireShowButtons[i].interactable = false;

        }



        Bar.color = FireColor;
        slider.value = 100f * gm.FireUsedMana / HowManyManaHaveToUse;
        percentTxt.text = (100f * gm.FireUsedMana / HowManyManaHaveToUse).ToString("0") + "%";

        howManyDoyouNeedTxt.text =HowManyManaHaveToUse.ToString("0");
        howManyLeftTxt.text = (HowManyManaHaveToUse - gm.FireUsedMana).ToString("0");
        HowManyDoYouUsed.text = gm.FireUsedMana.ToString("0");

        fire = true;
        chaos = false;
        sword = false;
        ankh = false;
        elect = false;
    }
    public void Chaos()
    {
        for (int i = 0; i < ChaosShowButtons.Length; i++)
        {
            ChaosShowButtons[i].interactable = false;

        }
        CheckPics();


        Bar.color = ChaosColor;
        slider.value = 100f * gm.ChaosUsedMana / HowManyManaHaveToUse;
        percentTxt.text = (100f * gm.ChaosUsedMana / HowManyManaHaveToUse).ToString("0") + "%";

        howManyDoyouNeedTxt.text = HowManyManaHaveToUse.ToString("0");
        howManyLeftTxt.text =  (HowManyManaHaveToUse - gm.ChaosUsedMana).ToString("0");
        HowManyDoYouUsed.text = gm.ChaosUsedMana.ToString("0");
        fire = false;
        chaos = true;
        sword = false;
        ankh = false;
        elect = false;
    }
    public void Sword()
    {
        for (int i = 0; i < SwordShowButtons.Length; i++)
        {
            SwordShowButtons[i].interactable = false;
        }

        CheckPics();


        Bar.color = SwordColor;
        slider.value = 100f * gm.SwordUsedStamina / HowManyManaHaveToUse;
        percentTxt.text = (100f * gm.SwordUsedStamina / HowManyManaHaveToUse).ToString("0") + "%";

        howManyDoyouNeedTxt.text =  HowManyManaHaveToUse.ToString("0");
        howManyLeftTxt.text = (HowManyManaHaveToUse - gm.SwordUsedStamina).ToString("0");
        HowManyDoYouUsed.text =  gm.SwordUsedStamina.ToString("0");
        fire = false;
        chaos = false;
        sword = true;
        ankh = false;
        elect = false;
    }
    public void Ankh()
    {
        for (int i = 0; i < AnkhLevelButtons.Length; i++)
        {
            AnkhShowButtons[i].interactable = false;
        }
        CheckPics();

        Bar.color = AnkhColor;
        slider.value = 100f * gm.AnkhUsedMana / HowManyManaHaveToUse;
        percentTxt.text = (100f * gm.AnkhUsedMana / HowManyManaHaveToUse).ToString("0") + "%";

        howManyDoyouNeedTxt.text =  HowManyManaHaveToUse.ToString("0");
        howManyLeftTxt.text =(HowManyManaHaveToUse - gm.AnkhUsedMana).ToString("0");
        HowManyDoYouUsed.text =  gm.AnkhUsedMana.ToString("0");
        fire = false;
        chaos = false;
        sword = false;
        ankh = true;
        elect = false;
    }
    public void Elect()
    {
        for(int i=0;i<ElectLevelButtons.Length;i++)
        {
            ElectShowButtons[i].interactable = false;
        }

        CheckPics();

        Bar.color = ElectColor;
        slider.value = 100f * gm.ElectUsedMana / HowManyManaHaveToUse;
        percentTxt.text= (100f * gm.ElectUsedMana / HowManyManaHaveToUse).ToString("0") + "%";
        howManyDoyouNeedTxt.text =  HowManyManaHaveToUse.ToString("0");
        howManyLeftTxt.text = (HowManyManaHaveToUse - gm.ElectUsedMana).ToString("0");
        HowManyDoYouUsed.text = gm.ElectUsedMana.ToString("0");
        fire = false;
        chaos = false;
        sword = false;
        ankh = false;
        elect = true;
    }
}
