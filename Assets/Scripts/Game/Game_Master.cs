using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Game_Master : MonoBehaviour {



    public static Game_Master instance = null;
    public int LevelId;
    
    public float MinY;
    public int difficultLevel;

    //Player stats
    public int exp;
    public int PDPoint;
    public int SkillPDPoint;
    public float nextLevel;
    private Exp_Slider exp_Slider;

    public int PlayerLevel;
    public int PlayerDeathLevel;
    public int PlayerFireLevel;
    public int PlayerElectLevel;
    public int PlayerWhiteLevel;
    public int PlayerSwordLevel;

    public int PlayerDeathManaLevel;
    public int PlayerFireManaLevel;
    public int PlayerElectManaLevel;
    public int PlayerWhiteManaLevel;
    public int PlayerSwordStaminaLevel;


    public int PlayerAttack = 0;
    public int PlayerArmor = 0;
    public int PlayerMagic = 0;
    public int PlayerSpeed = 0;

    public int PlayerStatsAttack = 0;
    public int PlayerStatsAgility = 0;
    public int PlayerStatsMagic = 0;

    public int powerLevel;

    //Player Elements
    public int Element;
    public float[] NeededManaFire;
    public float[] NeededManaElect;
    public float[] NeededManaAnkh;
    public float[] NeededManaChaos;

    public  float[] NeededManaFireBasic;
    public  float[] NeededManaElectBasic;
    public  float[] NeededManaAnkhBasic;
    public  float[] NeededManaChaosBasic;

    public float FireUsedMana;
    public float ElectUsedMana;
    public float AnkhUsedMana;
    public float ChaosUsedMana;
    public float SwordUsedStamina;

    public bool Player_Burn;
    public bool Player_Poisoned;
    public bool Player_Defend;
    public bool Player_DeathMode;
    public Transform EffectSlot;
    public GameObject[] Effect;
    //1 fire, 2 elect, 3 chaos, 4 Ankh 
    //bosses
    public int ForestBoss;

    //basiceq

    public int gold = 0;
    public int runes = 0;

    public int mana_potion = 0;
    public int hp_potion = 0;
    //Used Knives
    public int knifeCount;
    public int knifeId;
    public GameObject Secon_Weapon_Slot;

    //Notatki
    public int[] note;

    //informatory 
    
    public GameObject YouGetBoard;
    public Text YouGet;
    public float timeToShow;
    //czas i menu rozne

    public bool CanMove;

    public bool CanUseMenu;

    public bool StopTimePowerChoose;

    public float TimePowerChoose;
    public float TimePowerChooseBasic = 0.5f;
    //0 = zadne, 1 = pause_menu, 2=skill menu, 3=eq menu, 4=chest menu, 5=trade menu, -1=end/start

    public int active_menu = 0;
    public bool ChangeToZero;
    public bool Chest_Open;

    //Dead Start
    public float StartTime = 2.5f;

    public float deadTime = 2.5f;

    public GameObject ScreenImage;
    public GameObject DeadScreenImage;
    public float TimeToFade = 20f;
    //skrypty


    private Player_Controller player;
    // private PlayerAttack playerAttack;
    private CanvasEkwipunek eq;

    private SoundHolder soundHolder;
    private Saveing save;
  


    //koszty ruchu

    public float BasicBackDashCost;
    public float BasicJumpSpecialAttackCost;
    public float BasicJumpDownSpecialAttackCost;
    public float BasicRunSpecialAttackCost;
    public float BasicAttackCost;
    public float BasicAttackRunCost;
    public float BasicJumpCost;

    public float BackDashCost;
    public float JumpSpecialAttackCost;
    public float JumpDownSpecialAttackCost;
    public float RunSpecialAttackCost;
    public float AttackCost;
    public float AttackRunCost;
    public float JumpCost;

    // drzwi 
    public Transform[] Enters;
    public int DoorID;

    public bool loadScene;
    public int SceneIdToLoad;

    public bool saving;
    public bool dead;
    public bool awake;
    private LoadTargetScript loadScript;
    private KeyMenager keyMenager;
    public itemDataBaseChest itemDataBaseChest;
    private PlayerAttack playerAtckScript;
    public GameObject NewLevel;
    public GameObject NewLevelFire;
    public GameObject NewLevelElect;
    public GameObject NewLevelChaos;
    public GameObject NewLevelAnkh;
    public GameObject NewLevelSword;


    private bool NewLevelInstantiate;
    private float NewLevelTimer;
    public Transform PlayerNewLevelTransform;
    public itemDataBase itd;

    public bool PlayerFighting;
    public GameObject Enemies;
    public Settings settings;
    public bool makingConversation;
    public DmgTxtController dmgTxtController;
    private WeaponType weaponType;
    public int usedWeaponType; //used waepon type: 0=swword, 1=spear, 2 = a
    private bool fade;
    //static public GameObject powerUI;
   
    void Start()
    {

        PlayerFighting = false;
        ChangeToZero = true;
        YouGetBoard.SetActive(false);
        YouGet.text = " ";
        for (int i = 0; i < note.Length; i++) note[i] = 0;
        LevelId = Application.loadedLevel;
        save = gameObject.GetComponent<Saveing>();
        DeadScreenImage.SetActive(false);

        NewLevelInstantiate = false;
     

        // data = gameObject.GetComponent<itemDataBase>();
        awake = true;
     
        soundHolder = GameObject.Find("SoundHolder").GetComponent<SoundHolder>();
        keyMenager = GameObject.Find("KeyMenager").GetComponent<KeyMenager>();

        soundHolder.isloading = true;

        eq = GameObject.Find("Equipment").GetComponent<CanvasEkwipunek>();

        saving = false;
        loadScene = false;

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Controller>();
        playerAtckScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAttack>();

        loadScript = gameObject.GetComponent<LoadTargetScript>();


        itd = gameObject.GetComponent<itemDataBase>();
        Debug.Log("itd nie jest null");

        save.Load();
        itemDataBaseChest = gameObject.GetComponent<itemDataBaseChest>();

        if (itemDataBaseChest != null)
        {
            Debug.Log("itemDataBaseChest nie jest null");
            save.ChestLoad(LevelId);
        }

        RefreshEqStats();

        soundHolder.isloading = false;

        if (PlayerPrefs.HasKey("difficultLevel"))
        {
            difficultLevel = PlayerPrefs.GetInt("difficultLevel");
        }
        settings.load();


        exp_Slider = GameObject.Find("SkillUI").GetComponent<Exp_Slider>();



    }
    void Update()
    {
        TimePowerChoose = 0.4f + (TimePowerChooseBasic*difficultLevel / 10f);
        if (settings.gameObject.activeSelf)
        {
            difficultLevel = settings.qualityindex;
        }
        
        if (PlayerLevel == 0)
        {
            nextLevel = 50f;
        }
        if (PlayerLevel == 1)
        {
            nextLevel = 75f;
        }
        if (PlayerLevel > 1)
        {
            nextLevel = PlayerLevel * PlayerLevel * PlayerLevel * 10f - PlayerLevel * PlayerLevel * 20f + 100f;
        }

        if (nextLevel>0)
        {
            if (nextLevel <= exp)
            {
                exp_Slider.NewLevel();
                PDPoint += 1;
                if (PlayerLevel % 10 == 0)
                {
                    SkillPDPoint += 1;
                }

                if (NewLevelTimer >= 1)
                {
                    NewLevelInstantiate = true;
                }
                PlayerLevel += 1;
            }
        }

        if (NewLevelInstantiate)
        {
            NewLevelTimer = 0;
            GameObject particle = Instantiate(NewLevel, PlayerNewLevelTransform.position, transform.rotation);
            particle.GetComponent<LevelUp>().target = PlayerNewLevelTransform;
            NewLevelInstantiate = false;
        }
       else
        {
            if (NewLevelTimer < 1)
            {
                NewLevelTimer += Time.deltaTime;
            }
            else
            {
                NewLevelTimer = 1f;
            }
        }



        if (Secon_Weapon_Slot.transform.childCount>0)
        {
            ElementEq Second_Weapon = Secon_Weapon_Slot.GetComponentInChildren<ElementEq>();
            knifeId = Second_Weapon.Id;
            knifeCount = Second_Weapon.Count;
        }
        else
        {
            knifeCount = 0;
        }
       

        //wczytywanie i zapisywanie
        if (awake)
        {
            GameStart();
        }
        if (dead)
        {
            GameEnd();
        }
        if (saving)
        {
            if (gameObject.GetComponent<itemDataBaseChest>() != null)
            {
                itemDataBaseChest = gameObject.GetComponent<itemDataBaseChest>();
                save.ChestSave(LevelId);
                Debug.Log("saveing chest");
            }
            save.Save();
        }
        if (loadScene == true && saving == false)
        {

            player.blockMove = true;
            ScreenImage.SetActive(true);
            ScreenImage.GetComponent<Animator>().SetBool("FadeOut", true);
            fade = true;
        }
        if (fade)
        {
            Cursor.lockState = CursorLockMode.Locked; //Zablokowanie kursora myszy.
            Cursor.visible = false;//Ukrycie kursora.
            TimeToFade -= Time.deltaTime;
            if (TimeToFade <= 0)
            {                               //50 line
                loadScript.LoadScreenNum(SceneIdToLoad, Application.loadedLevel);
            }
        }
        if (ChangeToZero)
        {
            active_menu = 0;
            ChangeToZero = false;
        }

            if (PlayerSpeed == 0) 
            {
                BackDashCost = BasicBackDashCost;
                JumpDownSpecialAttackCost = BasicJumpDownSpecialAttackCost;
                JumpSpecialAttackCost = BasicJumpSpecialAttackCost;
                RunSpecialAttackCost = BasicRunSpecialAttackCost;
                AttackCost = BasicAttackCost;
                AttackRunCost = BasicAttackRunCost;
                JumpCost = BasicJumpCost;

            }
            else
             {
            BackDashCost = BasicBackDashCost - (5 * PlayerSpeed);
            JumpDownSpecialAttackCost = BasicJumpDownSpecialAttackCost - (2 * (PlayerSpeed + PlayerSwordStaminaLevel));
            JumpSpecialAttackCost = BasicJumpSpecialAttackCost - (2 * (PlayerSpeed + PlayerSwordStaminaLevel));
            RunSpecialAttackCost = BasicRunSpecialAttackCost - (2 * (PlayerSpeed + PlayerSwordStaminaLevel));

            AttackCost = BasicAttackCost - (1 * PlayerSpeed+PlayerSwordStaminaLevel); 
            AttackRunCost = BasicAttackRunCost - (2 * (PlayerSpeed + PlayerSwordStaminaLevel)); 
            JumpCost = BasicJumpCost - (5 * PlayerSpeed); 

            }

     

        if (BackDashCost <= 5) 
            {
                BackDashCost=5f;
            }
            if (JumpDownSpecialAttackCost <= 10) 
            {
                JumpDownSpecialAttackCost=10;
            }
            if (JumpSpecialAttackCost <= 15) 
            {
                JumpSpecialAttackCost= 15;
            }
            if (RunSpecialAttackCost <= 20) 
            {
                RunSpecialAttackCost= 20;
            }

            if (AttackCost <= 1)
            {
            AttackCost = 1;
            }
            if (AttackRunCost <= 5)
            {
            AttackRunCost = 5f;
            }              
            if(JumpCost <= 5)
            {
            JumpCost = 5f;
            }
        /*
                if (playerAttack.DeathMode)
                {
                    BackDashCost = 0;
                    JumpDownSpecialAttackCost = 0;
                    JumpSpecialAttackCost = 0;
                    RunSpecialAttackCost = 0;

                }

        */

    
        if (active_menu == 0)
        {
            Time.timeScale = 1;
            player.blockMove = true;
            Cursor.lockState = CursorLockMode.Locked; //Zablokowanie kursora myszy.
            Cursor.visible = false;//Ukrycie kursora.
            if (StopTimePowerChoose)
            {
                Time.timeScale = TimePowerChoose;
            }
        }
        else
        {

            Time.timeScale = 0;
            //player.CanMove = false;
            Cursor.lockState = CursorLockMode.None;//Odblokowanie kursora myszy.
            Cursor.visible = true;//Pokazanie kursora.


        }

        if(timeToShow<1f)
        {
            YouGetBoard.GetComponent<Animator>().SetBool("disappear", true);
        }
        if (timeToShow > 0)
        {
            timeToShow -= Time.deltaTime;
            YouGetBoard.SetActive(true);
        }
        else
        {
            YouGetBoard.SetActive(false);
            YouGet.text = " ";
        }

    }
    public void AddHp()
    {
        player.curHP += 100 + 100 * difficultLevel / 10f ;
    }
    public void AddMana()
    {
        player.curMana += 100 + 100 * difficultLevel / 10f ;
    }
    public void Info(string Text)
    {
        float time = 10f;
    
        if (time > 0)
        {
            time -= Time.deltaTime;
            YouGet.text = Text;
            YouGetBoard.SetActive(true);
        }
        else
        {
            YouGetBoard.SetActive(false);
        }


    }
    public void RefreshEqStats()
    {
        PlayerAttack = PlayerStatsAttack;
        PlayerArmor = 0;
        PlayerMagic = PlayerStatsMagic;
        PlayerSpeed = PlayerStatsAgility;
        if (itd.AmuletSlot.GetChildCount() > 0)
        {
            PlayerAttack += itd.AmuletSlot.GetComponentInChildren<ElementEq>().attack;
            PlayerArmor += itd.AmuletSlot.GetComponentInChildren<ElementEq>().armor;
            PlayerMagic += itd.AmuletSlot.GetComponentInChildren<ElementEq>().magic;
            PlayerSpeed += itd.AmuletSlot.GetComponentInChildren<ElementEq>().agility;
        }
        if(itd.MainMain_Weapon.GetChildCount()>0)
        {
            PlayerAttack += itd.MainMain_Weapon.GetComponentInChildren<ElementEq>().attack;
            PlayerArmor += itd.MainMain_Weapon.GetComponentInChildren<ElementEq>().armor;
            PlayerMagic += itd.MainMain_Weapon.GetComponentInChildren<ElementEq>().magic;
            PlayerSpeed += itd.MainMain_Weapon.GetComponentInChildren<ElementEq>().agility;

            //change animator Layer
            if (itd.MainMain_Weapon.GetComponentInChildren<ElementEq>().weaponType == WeaponType.Sword)
            {
             ChangePlayerAnimatorLayer(0);
                usedWeaponType = 0;
            }
            if (itd.MainMain_Weapon.GetComponentInChildren<ElementEq>().weaponType == WeaponType.Spear)
            {
                ChangePlayerAnimatorLayer(1);
                usedWeaponType = 1;
            }
            if (itd.MainMain_Weapon.GetComponentInChildren<ElementEq>().weaponType == WeaponType.Axe)
            {
                ChangePlayerAnimatorLayer(2);
                usedWeaponType = 2;
            }
        }
        else
        {
            ChangePlayerAnimatorLayer(0);
        }
        if (itd.MainArmorSlot.GetChildCount() > 0)
        {
            PlayerAttack += itd.MainArmorSlot.GetComponentInChildren<ElementEq>().attack;
            PlayerArmor += itd.MainArmorSlot.GetComponentInChildren<ElementEq>().armor;
            PlayerMagic += itd.MainArmorSlot.GetComponentInChildren<ElementEq>().magic;
            PlayerSpeed += itd.MainArmorSlot.GetComponentInChildren<ElementEq>().agility;
        }
        if (itd.MainLegsSlot.GetChildCount() > 0)
        {
            PlayerAttack += itd.MainLegsSlot.GetComponentInChildren<ElementEq>().attack;
            PlayerArmor += itd.MainLegsSlot.GetComponentInChildren<ElementEq>().armor;
            PlayerMagic += itd.MainLegsSlot.GetComponentInChildren<ElementEq>().magic;
            PlayerSpeed += itd.MainLegsSlot.GetComponentInChildren<ElementEq>().agility;
        }
        if (itd.MainBootsSlot.GetChildCount() > 0)
        {
            PlayerAttack += itd.MainBootsSlot.GetComponentInChildren<ElementEq>().attack;
            PlayerArmor += itd.MainBootsSlot.GetComponentInChildren<ElementEq>().armor;
            PlayerMagic += itd.MainBootsSlot.GetComponentInChildren<ElementEq>().magic;
            PlayerSpeed += itd.MainBootsSlot.GetComponentInChildren<ElementEq>().agility;
        }
        
        eq.StatsText.text = "S  t a  t s";

        if (PlayerMagic <= 0)
        {
            for (int i = 0; i < NeededManaFire.Length; i++)
            {
                NeededManaFire = NeededManaFireBasic;
                NeededManaAnkh = NeededManaAnkhBasic;
                NeededManaChaos = NeededManaChaosBasic;
                NeededManaElect = NeededManaElectBasic;
            }
        }
        else
        {
            for (int i = 0; i < NeededManaFire.Length; i++)
            {
                NeededManaFire[i] = NeededManaFireBasic[i] - (NeededManaFireBasic[i] * (PlayerMagic+PlayerFireManaLevel) / 100);
                NeededManaAnkh[i] = NeededManaAnkhBasic[i] - (NeededManaAnkhBasic[i] * (PlayerMagic + PlayerWhiteManaLevel) / 100);
                NeededManaChaos[i] = NeededManaChaosBasic[i] - (NeededManaChaosBasic[i] * (PlayerMagic + PlayerDeathManaLevel) / 100);
                NeededManaElect[i] = NeededManaElectBasic[i] - (NeededManaElectBasic[i] * (PlayerMagic + PlayerElectManaLevel) / 100);

            }
        }

        player.SetNumbers(PlayerSpeed,PlayerMagic, PlayerArmor);

    }
    public bool ShowWeaponStats()
    {
        eq.StatsText.text = "item stats";
        if (gameObject.GetComponent<itemDataBaseChest>() != null && Chest_Open)
        {
            if (eq.StatsUIChest.active == true)
            {
                return false;
            }
            else
            {
                eq.StatsUIChest.SetActive(true);
                return true;
            }
        }
        else
        {
            if (eq.StatsUI.active == true)
            {
                return false;
            }
            else
            {
                eq.StatsUI.SetActive(true);
                return true;
            }
        }
      
    }
    public void CloseWeaponStats()
    {
        eq.StatsUI.SetActive(false);
        RefreshEqStats();
        eq.StatsText.text = "stats";

        if (gameObject.GetComponent<itemDataBaseChest>() != null)
        {
            eq.StatsUIChest.SetActive(false);
        }

        }
    public void SetPlayerPlace()
    {
        for (int i = 0; i < Enters.Length; i++)
        {
            if (Enters[i].name == DoorID.ToString())
            {
                player.transform.position = Enters[i].position;
            }
        }

    }
    public void GameStart()
    {
        ScreenImage.SetActive(true);

        StartTime -= Time.deltaTime;



        if (StartTime <= 0)
        {
            active_menu = 0;

            ScreenImage.SetActive(false);

          //  player.CanMove = true;
            awake = false;
        }

    }
    public void GameEnd()
    {
      //  player.CanMove = false;

        deadTime -= Time.deltaTime;
            Time.timeScale = 0.5f;

            if (deadTime < 0)
            {

                DeadScreenImage.SetActive(true);
    
                if (Input.GetKeyDown("e"))
                {
                    Application.LoadLevel(Application.loadedLevel);
                }
            }
      
    }
    public  void SetChestBool(bool open)
    {
       Chest_Open=open;
  
    }
    public void ShowInfo(float timeToShow, string txt)
    {
        YouGet.text = txt;
        this.timeToShow = timeToShow;
    }
    public void ThrowKnife()
    {
        ElementEq Second_Weapon = Secon_Weapon_Slot.GetComponentInChildren<ElementEq>();
        Second_Weapon.Count -= 1 ;
        for(int i=0;i<itd.Secon_WeaponCount.Length;i++)
        {
            if(itd.SecondWeaponSlotItemID==itd.Secon_Weapons[i].GetComponent<ElementEq>().Id)
            {
                itd.Secon_WeaponCount[i] -= 1;
            }
        }
    }
    public void AddKnife(GameObject Knife)
    {
        ElementEq Second_Weapon = Secon_Weapon_Slot.GetComponentInChildren<ElementEq>();
        bool add = false;

        if (Secon_Weapon_Slot.transform.childCount != 0)
        {
            if (Second_Weapon.Id == Knife.GetComponent<ElementEq>().Id)
            {
                Secon_Weapon_Slot.transform.GetChild(0).GetComponent<ElementEq>().Count += Knife.GetComponent<ElementEq>().Count;
                add = true;
                Debug.Log("added to pocket when there is some knives in that type");
            }
        }

    

        if (eq.Weapon_CatUI.transform.childCount > 0 && add == false)
        {

            for (int i = 0; i < eq.Weapon_CatUI.transform.childCount; i++)
            {

                if (eq.Weapon_CatUI.transform.GetChild(i).GetComponent<ElementEq>().Id == Knife.GetComponent<ElementEq>().Id)
                {
                    eq.Weapon_CatUI.transform.GetChild(i).GetComponent<ElementEq>().Count += Knife.GetComponent<ElementEq>().Count;
                    for (int j = 0; j < itd.Secon_WeaponCount.Length; j++)
                    {
                        if (Knife.GetComponent<ElementEq>().Id == itd.Secon_Weapons[j].GetComponent<ElementEq>().Id)
                        {
                            itd.Secon_WeaponCount[j] += Knife.GetComponent<ElementEq>().Count;
                            add = true;
                            Debug.Log("added to backpack when there is some knives");

                        }
                    }
                }

            }
          
        }

        if (Secon_Weapon_Slot.transform.childCount == 0 && add == false)
        {
            Instantiate(Knife, Secon_Weapon_Slot.transform);
            itd.SecondWeaponSlotItemID = Knife.GetComponent<ElementEq>().Id;

            itd.SecondWeaponSlotItemCount = Knife.GetComponent<ElementEq>().Count;
            add = true;
            Debug.Log("added to pocket");

        }

        if (eq.Weapon_CatUI.transform.childCount == 0 && add == false)
        {
            for (int j = 0; j < itd.Secon_WeaponCount.Length; j++)
            {
                if (Knife.GetComponent<ElementEq>().Id == itd.Secon_Weapons[j].GetComponent<ElementEq>().Id)
                {
                    itd.Secon_WeaponCount[j] += Knife.GetComponent<ElementEq>().Count;
                    add = true;
                    Instantiate(Knife, eq.Weapon_CatUI.transform);
                    Debug.Log("added to backpack");

                }
            }
        }
        if(add==false)
        {
           
                ShowInfo(3, "there is no space");
       
        }

        RefreshEqStats();

    }
    public void MakeAnEvent(int element,float points)  //0 sword, 1 fire, 2 chaos, 3 ankh, 4 elect
    {
    if(element==0)
        {
            PlayerSwordStaminaLevel += 1;
          //  GameObject particle = Instantiate(NewLevelSword, PlayerNewLevelTransform.position, transform.rotation);
          //  particle.GetComponent<LevelUp>().target = PlayerNewLevelTransform;

            SwordUsedStamina = points;
        }
    else
        {
            if (element==1)
            {
            PlayerFireManaLevel += 1;
                GameObject particle = Instantiate(NewLevelFire, PlayerNewLevelTransform.position, transform.rotation);
                particle.GetComponent<LevelUp>().target = PlayerNewLevelTransform;

                FireUsedMana = points;

            }
            if (element == 4)
            {
            PlayerElectManaLevel += 1;
                GameObject particle = Instantiate(NewLevelElect, PlayerNewLevelTransform.position, transform.rotation);
                particle.GetComponent<LevelUp>().target = PlayerNewLevelTransform;

                ElectUsedMana = points;
              }
            if (element == 2)
            {
            PlayerDeathManaLevel += 1;
                GameObject particle = Instantiate(NewLevelChaos, PlayerNewLevelTransform.position, transform.rotation);
                particle.GetComponent<LevelUp>().target = PlayerNewLevelTransform;

                ChaosUsedMana = points;
             }
            if (element == 3)
            {
            PlayerWhiteManaLevel += 1;
                GameObject particle = Instantiate(NewLevelAnkh, PlayerNewLevelTransform.position, transform.rotation);
                particle.GetComponent<LevelUp>().target = PlayerNewLevelTransform;

                AnkhUsedMana = points;
            }
        }

    }

    public void CheckFighting()
    {
        bool checkPlayerFight = false;

        for(int i=0;i<Enemies.transform.childCount;i++)
        {
            if (Enemies.gameObject.transform.GetChild(i).GetComponent<Enemy>().noticePlayer)
            {
                checkPlayerFight = true;
            }
        }

        if(checkPlayerFight==true)
        {
            PlayerFighting=true;
        }
        else
        {
            PlayerFighting = false;
        }
    }
    public void AddEffect(int effectNumber)  //0 fire, 1 poisoned, 2 deaathmode, 3 shiield, 4 sword stun
    {
        Instantiate(Effect[effectNumber], EffectSlot);
    }
    public void ChangePlayerAnimatorLayer(int LayerNumber) //0 sword, 1 spear, 2 axe
    {
        if (LayerNumber == 0)
        {
            player.GetComponent<Animator>().SetLayerWeight(0, 1);
            player.GetComponent<Animator>().SetLayerWeight(1, 0);
            player.GetComponent<Animator>().SetLayerWeight(2, 0);

        }
        if (LayerNumber == 1)
        {
            player.GetComponent<Animator>().SetLayerWeight(0, 0);
            player.GetComponent<Animator>().SetLayerWeight(1, 1);
            player.GetComponent<Animator>().SetLayerWeight(2, 0);

        }
        if (LayerNumber == 2)
        {
            player.GetComponent<Animator>().SetLayerWeight(0, 0);
            player.GetComponent<Animator>().SetLayerWeight(1, 0);
            player.GetComponent<Animator>().SetLayerWeight(2, 1);
        }

    }

}
