using UnityEngine;
using System.Collections;

using System; //Serializable.
using System.Runtime.Serialization.Formatters.Binary;//Serializes i deserializes obiektu lub cały wykres z połączonych obiektów w formacie binarnym.
using System.IO;//Do operacji na plikach (czytanie, pisanie do pliku).
using UnityEngine.SceneManagement; //działanie na scenach

public class Saveing : MonoBehaviour
{

    //odwołania
    private Player_Controller player;
    public Game_Master gameMaster;
    public itemDataBase idb;
    public itemDataBaseChest idbch;

    public bool notmenu;
    public int LastLevel;

    void Start() //pobierz komponenty, bądź załaduj menu
    {
        Debug.Log(notmenu);

        if (SceneManager.GetActiveScene().buildIndex > 1)
        {
            gameMaster = gameObject.GetComponent<Game_Master>();
            idb = gameObject.GetComponent<itemDataBase>();

        }
        else
        {
            Debug.Log("Ładuje menu");

            LoadMenu();
        }

        Debug.Log("Start skrypty - saveing");

    }

    void Update()
    {
        //Szybki zapis.
        if (Input.GetKeyDown(KeyCode.F5))
        {
            Save();
        }

        //Szybkie wczytanie.
        if (Input.GetKeyDown(KeyCode.F10))
        {
            Load();
        }
    }

   //Zapisanie stanu gry do pliku
    public void Save()
    {
        gameMaster.saving = true; //gra jest zapisywana

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Controller>();
        idb = gameObject.GetComponent<itemDataBase>();

        Debug.Log("Saveing");

        //Posłuży do przesyłania danych do pliku.
        //Unity ma własną konfigurację w tym wyspecyfikowane miejsce, w którym składuje takie pliki.
        //Pod Windows jest to z reguły miejsce w katalogu użytkownika.
        FileStream plik = File.Create(Application.persistentDataPath + "/Record.data");

        // Obiekt zawierający informacjie o stanie naszego gracza.

        GameData gmdata = new GameData();

        gmdata.SceneId = Application.loadedLevel; //Pobieram id poziomu
        //zapisanie odpowiednich danych o graczu
        gmdata.exp = gameMaster.exp;
        gmdata.PDPoint = gameMaster.PDPoint;
        gmdata.PDSkillPoint = gameMaster.SkillPDPoint;

        gmdata.PlayerLevel = gameMaster.PlayerLevel;
        gmdata.PlayerDeathLevel = gameMaster.PlayerDeathLevel;
        gmdata.PlayerFireLevel = gameMaster.PlayerFireLevel;
        gmdata.PlayerElectLevel = gameMaster.PlayerElectLevel;
        gmdata.PlayerWhiteLevel = gameMaster.PlayerWhiteLevel;
        gmdata.PlayerSwordLevel = gameMaster.PlayerSwordLevel;


        gmdata.PlayerDeathManaLevel = gameMaster.PlayerDeathManaLevel;
        gmdata.PlayerFireManaLevel = gameMaster.PlayerFireManaLevel;
        gmdata.PlayerElectManaLevel = gameMaster.PlayerElectManaLevel;
        gmdata.PlayerWhiteManaLevel = gameMaster.PlayerWhiteManaLevel;
        gmdata.PlayerSwordStaminaLevel = gameMaster.PlayerSwordStaminaLevel;

        gmdata.PlayerAttack = gameMaster.PlayerAttack;
        gmdata.PlayerArmor = gameMaster.PlayerArmor;
        gmdata.PlayerMagic = gameMaster.PlayerMagic;
        gmdata.PlayerSpeed = gameMaster.PlayerSpeed;

        gmdata.PlayerStatsAttack = gameMaster.PlayerStatsAttack;
        gmdata.PlayerStatsMagic = gameMaster.PlayerStatsMagic;
        gmdata.PlayerStatsAgility = gameMaster.PlayerStatsAgility;

        gmdata.FireUsedMana = gameMaster.FireUsedMana;
        gmdata.ElectUsedMana = gameMaster.ElectUsedMana;
        gmdata.AnkhUsedMana = gameMaster.AnkhUsedMana;
        gmdata.ChaosUsedMana = gameMaster.ChaosUsedMana;
        gmdata.SwordUsedStamina = gameMaster.SwordUsedStamina;

        //zapisanie dokąd gracz przeszedł i jego danych
        gmdata.DoorID = gameMaster.DoorID;
        gmdata.LevelId = gameMaster.LevelId;
        gmdata.Element = gameMaster.Element;

        gmdata.gold = gameMaster.gold;
        gmdata.mana_potion = gameMaster.mana_potion;
        gmdata.hp_potion = gameMaster.hp_potion;
        gmdata.runes = gameMaster.runes;

        gmdata.Hp = player.curHP;
        gmdata.Mana = player.curMana;
        gmdata.Stamina = player.curStamina;

        //zapisanie notatek gracza
        for (int i = 0; i < gameMaster.note.Length; i++)
        {
            gmdata.notes[i] = gameMaster.note[i];
        }

        for (int i = 0; i < gmdata.AmuletsCount.Length; i++)
        {
            gmdata.AmuletsCount[i] = idb.AmuletsCount[i];
        }
        for (int i = 0; i < gmdata.Main_WeaponCount.Length; i++)
        {
            gmdata.Main_WeaponCount[i] = idb.Main_WeaponCount[i];
        }
        for (int i = 0; i < gmdata.Secon_WeaponCount.Length; i++)
        {
            gmdata.Secon_WeaponCount[i] = idb.Secon_WeaponCount[i];
        }
        for (int i = 0; i < gmdata.TasksCount.Length; i++)
        {
            gmdata.TasksCount[i] = idb.TasksCount[i];
        }
        for (int i = 0; i < gmdata.ArmorsCount.Length; i++)
        {
            gmdata.ArmorsCount[i] = idb.ArmorsCount[i];
        }
        for (int i = 0; i < gmdata.BootsCount.Length; i++)
        {
            gmdata.BootsCount[i] = idb.BootsCount[i];
        }
        for (int i = 0; i < gmdata.LegsCount.Length; i++)
        {
            gmdata.LegsCount[i] = idb.LegsCount[i];
        }

        //zapisanie jego przedmiotów
        gmdata.AmuletSlotItemID = idb.AmuletSlotItemID;
        gmdata.Main_WeaponSlotItemID = idb.Main_WeaponSlotItemID;
        gmdata.SecondWeaponSlotItemID = idb.SecondWeaponSlotItemID;
        gmdata.SecondWeaponSlotItemCount = idb.SecondWeaponSlotItemCount;

        gmdata.ArmorSlotItemID = idb.ArmorSlotItemID;
        gmdata.LegsSlotItemID = idb.LegsSlotItemID;
        gmdata.BootsSlotItemID = idb.BootsSlotItemID;

        //Posłuży do zapisywania danych do pliku.
        BinaryFormatter bf = new BinaryFormatter();

        //Serializujemy/zapisujemy dane do pliku
        bf.Serialize(plik, gmdata);
        plik.Close();//Zamykamy plik (kończymy operacje na pliku).

        Debug.Log("Saveing - save all");

        gameMaster.saving = false;
    }

    //Odczytywanie danych z pliku
    public void Load()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Controller>();
        gameMaster = gameObject.GetComponent<Game_Master>();
        idb = gameObject.GetComponent<itemDataBase>();

        Debug.Log("Loading..");

        // File.Delete(Application.persistentDataPath + "/Record.data");
        //Zanim odczytamy dane upewnijmy się, że jest co czytać.
        //Sprawdzamy czy plik zapisu istnieje.
        if (File.Exists(Application.persistentDataPath + "/Record.data"))
        {
            Debug.Log("Posiada zz tyłu te dane - zwykłe");

            //Odczytujemy/pobieramy dane z pliku.
            FileStream file = File.Open(Application.persistentDataPath + "/Record.data", FileMode.Open);

            //Posłuży do odczytu danych do pliku.
            BinaryFormatter bf = new BinaryFormatter();

            // Deserializujemy dane z pliku i przekształcamy je na obiekt GraczData.


            GameData gmdata = (GameData)bf.Deserialize(file);

            file.Close(); //Plik odczytany zamykamy plik.          

            //Ustawiamy dane;
            player.SetData(gmdata.Hp, gmdata.Mana, gmdata.Stamina);//Ustawiamy zdrowie mane i stamine trzeba zrobic funkcje w PlayerController  dane zmienic na data powyzej i gracz zmienic na Player.

            //odczytywanie danych
            gameMaster.exp = gmdata.exp;
            gameMaster.PDPoint = gmdata.PDPoint;
            gameMaster.SkillPDPoint = gmdata.PDSkillPoint;

            gameMaster.PlayerLevel = gmdata.PlayerLevel;
            gameMaster.PlayerDeathLevel = gmdata.PlayerDeathLevel;
            gameMaster.PlayerFireLevel = gmdata.PlayerFireLevel;
            gameMaster.PlayerElectLevel = gmdata.PlayerElectLevel;
            gameMaster.PlayerWhiteLevel = gmdata.PlayerWhiteLevel;
            gameMaster.PlayerSwordLevel = gmdata.PlayerSwordLevel;

            gameMaster.PlayerDeathManaLevel = gmdata.PlayerDeathManaLevel;
            gameMaster.PlayerFireManaLevel = gmdata.PlayerFireManaLevel;
            gameMaster.PlayerElectManaLevel = gmdata.PlayerElectManaLevel;
            gameMaster.PlayerWhiteManaLevel = gmdata.PlayerWhiteManaLevel;
            gameMaster.PlayerSwordStaminaLevel = gmdata.PlayerSwordStaminaLevel;

            gameMaster.PlayerAttack = gmdata.PlayerAttack;
            gameMaster.PlayerArmor = gmdata.PlayerArmor;
            gameMaster.PlayerMagic = gmdata.PlayerMagic;
            gameMaster.PlayerSpeed = gmdata.PlayerSpeed;

            gameMaster.PlayerStatsAgility = gmdata.PlayerStatsAgility;
            gameMaster.PlayerStatsAttack = gmdata.PlayerStatsAttack;
            gameMaster.PlayerStatsMagic = gmdata.PlayerStatsMagic;



            gameMaster.FireUsedMana = gmdata.FireUsedMana;
            gameMaster.ElectUsedMana = gmdata.ElectUsedMana;
            gameMaster.AnkhUsedMana = gmdata.AnkhUsedMana;
            gameMaster.ChaosUsedMana = gmdata.ChaosUsedMana;
            gameMaster.SwordUsedStamina = gmdata.SwordUsedStamina;


            gameMaster.DoorID = gmdata.DoorID;
            gameMaster.Element = gmdata.Element;

            gameMaster.SetPlayerPlace();

            gameMaster.gold = gmdata.gold;
            gameMaster.mana_potion = gmdata.mana_potion;
            gameMaster.hp_potion = gmdata.hp_potion;
            gameMaster.runes = gmdata.runes;


            for (int i = 0; i < gmdata.notes.Length; i++)
            {
                gameMaster.note[i] = gmdata.notes[i];
            }


            for (int i = 0; i < gmdata.AmuletsCount.Length; i++)
            {
                idb.AmuletsCount[i] = gmdata.AmuletsCount[i];
            }
            for (int i = 0; i < gmdata.Main_WeaponCount.Length; i++)
            {
                idb.Main_WeaponCount[i] = gmdata.Main_WeaponCount[i];
            }
            for (int i = 0; i < gmdata.Secon_WeaponCount.Length; i++)
            {
                idb.Secon_WeaponCount[i] = gmdata.Secon_WeaponCount[i];
            }
            for (int i = 0; i < gmdata.TasksCount.Length; i++)
            {
                idb.TasksCount[i] = gmdata.TasksCount[i];
            }
            for (int i = 0; i < gmdata.ArmorsCount.Length; i++)
            {
                idb.ArmorsCount[i] = gmdata.ArmorsCount[i];
            }
            for (int i = 0; i < gmdata.BootsCount.Length; i++)
            {
                idb.BootsCount[i] = gmdata.BootsCount[i];
            }
            for (int i = 0; i < gmdata.LegsCount.Length; i++)
            {
                idb.LegsCount[i] = gmdata.LegsCount[i];
            }

            idb.AmuletSlotItemID = gmdata.AmuletSlotItemID;
            idb.Main_WeaponSlotItemID = gmdata.Main_WeaponSlotItemID;
            idb.SecondWeaponSlotItemID = gmdata.SecondWeaponSlotItemID;
            idb.SecondWeaponSlotItemCount = gmdata.SecondWeaponSlotItemCount;
            idb.ArmorSlotItemID = gmdata.ArmorSlotItemID;
            idb.LegsSlotItemID = gmdata.LegsSlotItemID;
            idb.BootsSlotItemID = gmdata.BootsSlotItemID;

            idb.Instantiate();

            Debug.Log("Zostaje zapisany id sceny: " + SceneManager.GetActiveScene().buildIndex);
            SaveLavelId(SceneManager.GetActiveScene().buildIndex, gmdata);
        }
    }


    public void LoadMenu()
    {

        Debug.Log("Loading menu..");

        //Zanim odczytamy dane upewnijmy się, że jest co czytać.
        //Sprawdzamy czy plik zapisu istnieje.
        if (File.Exists(Application.persistentDataPath + "/Record.data"))
        {
            Debug.Log("Posiada zz tyłu te dane - menu");

            //Odczytujemy/pobieramy dane z pliku.
            FileStream file = File.Open(Application.persistentDataPath + "/Record.data", FileMode.Open);

            //Posłuży do odczytu danych do pliku.
            BinaryFormatter bf = new BinaryFormatter();

            // Deserializujemy dane z pliku i przekształcamy je na obiekt GraczData.

            GameData gmdata = (GameData)bf.Deserialize(file);

            file.Close(); //Plik odczytany zamykamy plik.          

            //Ustawiamy dane;

            LastLevel = gmdata.SceneId;
        }
    }
    //Klasa zapisująca dane o skrzynie z poziomu

    public void ChestSave(int id)
    {
        gameMaster.saving = true;

        if (GameObject.FindGameObjectWithTag("chest") == null)
        { //jeżeli znajduje się taki przedmiot na mapie
            return;
        }
        ChestTrigger chest = GameObject.FindGameObjectWithTag("chest").GetComponent<ChestTrigger>();

        idbch = gameObject.GetComponent<itemDataBaseChest>();
        Debug.Log("Saveing..Chest");

        //Posłuży do przesyłania danych do pliku.

        FileStream plik = File.Create(Application.persistentDataPath + "/ChestRecord" + id + ".data");

        ChestData chdata = new ChestData();



        for (int i = 0; i < chdata.AmuletsCount.Length; i++)
        {
            chdata.AmuletsCount[i] = idbch.AmuletsCount[i];
        }
        for (int i = 0; i < chdata.Main_WeaponCount.Length; i++)
        {
            chdata.Main_WeaponCount[i] = idbch.Main_WeaponCount[i];
        }
        for (int i = 0; i < chdata.Secon_WeaponCount.Length; i++)
        {
            chdata.Secon_WeaponCount[i] = idbch.Secon_WeaponCount[i];
        }
        for (int i = 0; i < chdata.TasksCount.Length; i++)
        {
            chdata.TasksCount[i] = idbch.TasksCount[i];
        }
        for (int i = 0; i < chdata.ArmorsCount.Length; i++)
        {
            chdata.ArmorsCount[i] = idbch.ArmorsCount[i];
        }
        for (int i = 0; i < chdata.BootsCount.Length; i++)
        {
            chdata.BootsCount[i] = idbch.BootsCount[i];
        }
        for (int i = 0; i < chdata.LegsCount.Length; i++)
        {
            chdata.LegsCount[i] = idbch.LegsCount[i];
        }

        chdata.visited = chest.visited;

        //Posłuży do zapisywania danych do pliku.
        BinaryFormatter bf = new BinaryFormatter();

        //Serializujemy/zapisujemy dane do pliku
        bf.Serialize(plik, chdata);
        plik.Close();//Zamykamy plik (kończymy operacje na pliku).
        gameMaster.saving = false;
        Debug.Log("Saveing chest - save all");

    }
    public void ChestLoad(int id)
    {
        Debug.Log("Loading Chest.."); //ładowanie skrzyni

        idbch = gameObject.GetComponent<itemDataBaseChest>();

        if (GameObject.FindGameObjectWithTag("chest")==null){ //jeżeli znajduje się taki przedmiot na mapie
            return;
        }
        ChestTrigger chest = GameObject.FindGameObjectWithTag("chest").GetComponent<ChestTrigger>();


        //  File.Delete(Application.persistentDataPath + "/ChestRecord" + id + ".data");

        //Zanim odczytamy dane upewnijmy się, że jest co czytać.
        //Sprawdzamy czy plik zapisu istnieje.
        if (File.Exists(Application.persistentDataPath + "/ChestRecord" + id + ".data"))
        {

            //Odczytujemy/pobieramy dane z pliku.

            FileStream file = File.Open(Application.persistentDataPath + "/ChestRecord" + id + ".data", FileMode.Open);



            //Posłuży do odczytu danych do pliku.
            BinaryFormatter bf = new BinaryFormatter();

            // Deserializujemy dane z pliku i przekształcamy je na obiekt GraczData.

            ChestData chdata = (ChestData)bf.Deserialize(file);

            file.Close(); //Plik odczytany zamykamy plik.          

            //Ustawiamy dane;

            chest.visited = chdata.visited;
            for (int i = 0; i < chdata.AmuletsCount.Length; i++)
            {
                Debug.Log("zerowanie");
                idbch.AmuletsCount[i] = 0;
            }
            for (int i = 0; i < chdata.Main_WeaponCount.Length; i++)
            {
                idbch.Main_WeaponCount[i] = 0;
                Debug.Log("zerowanie");

            }
            for (int i = 0; i < chdata.Secon_WeaponCount.Length; i++)
            {
                idbch.Secon_WeaponCount[i] = 0;
                Debug.Log("zerowanie");

            }
            for (int i = 0; i < chdata.TasksCount.Length; i++)
            {
                idbch.TasksCount[i] = 0;
                Debug.Log("zerowanie");

            }
            for (int i = 0; i < chdata.ArmorsCount.Length; i++)
            {
                idbch.ArmorsCount[i] = 0;
                Debug.Log("zerowanie");

            }
            for (int i = 0; i < chdata.BootsCount.Length; i++)
            {
                idbch.BootsCount[i] = 0;
                Debug.Log("zerowanie");

            }
            for (int i = 0; i < chdata.LegsCount.Length; i++)
            {
                idbch.LegsCount[i] = 0;
                Debug.Log("zerowanie");

            }

            for (int i = 0; i < chdata.AmuletsCount.Length; i++)
            {
                Debug.Log(i);
                idbch.AmuletsCount[i] = chdata.AmuletsCount[i];
            }
            for (int i = 0; i < chdata.Main_WeaponCount.Length; i++)
            {
                idbch.Main_WeaponCount[i] = chdata.Main_WeaponCount[i];
            }
            for (int i = 0; i < chdata.Secon_WeaponCount.Length; i++)
            {
                idbch.Secon_WeaponCount[i] = chdata.Secon_WeaponCount[i];
            }
            for (int i = 0; i < chdata.TasksCount.Length; i++)
            {
                idbch.TasksCount[i] = chdata.TasksCount[i];
            }
            for (int i = 0; i < chdata.ArmorsCount.Length; i++)
            {
                idbch.ArmorsCount[i] = chdata.ArmorsCount[i];
            }
            for (int i = 0; i < chdata.BootsCount.Length; i++)
            {
                idbch.BootsCount[i] = chdata.BootsCount[i];
            }
            for (int i = 0; i < chdata.LegsCount.Length; i++)
            {
                idbch.LegsCount[i] = chdata.LegsCount[i];
            }
        }
        idbch.Instantiate();

        Debug.Log("Loading Chest END");
    }

    public void SaveLavelId(int id, GameData gmdata) //służy do zapisania id sceny do której się właśnie weszło
    {
        gameMaster.saving = true; //gra jest zapisywana

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Controller>();
        idb = gameObject.GetComponent<itemDataBase>();

        Debug.Log("Saveing level id");

        //Posłuży do przesyłania danych do pliku.
        //Unity ma własną konfigurację w tym wyspecyfikowane miejsce, w którym składuje takie pliki.
        //Pod Windows jest to z reguły miejsce w katalogu użytkownika.
        FileStream plik = File.Create(Application.persistentDataPath + "/Record.data");

        // Obiekt zawierający informacjie o stanie naszego gracza.

        gmdata.SceneId = Application.loadedLevel; //Pobieram id poziomu

        //Posłuży do zapisywania danych do pliku.
        BinaryFormatter bf = new BinaryFormatter();

        //Serializujemy/zapisujemy dane do pliku
        bf.Serialize(plik, gmdata);
        plik.Close();//Zamykamy plik (kończymy operacje na pliku).

        Debug.Log("Saveing - save level id = "+SceneManager.GetActiveScene().buildIndex);

        gameMaster.saving = false;
    }
}



//Klasa odpowiadająca za dane gry do zapisu i wczytania
[Serializable]
public class GameData //żeby można było przesłać obiekt tej klasy
{
    //GameMaster
    public int SceneId;

    public int exp;
    public int PDPoint;
    public int PDSkillPoint;


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

    public float FireUsedMana;
    public float ElectUsedMana;
    public float AnkhUsedMana;
    public float ChaosUsedMana;
    public float SwordUsedStamina;


    public int PlayerStatsAttack;
    public int PlayerStatsAgility;
    public int PlayerStatsMagic;

    public int PlayerAttack;
    public int PlayerArmor;
    public int PlayerMagic;
    public int PlayerSpeed;

    public int Element;

    public int DoorID;
    public int LevelId;

    //EQ
    public int gold = 0;
    public int mana_potion = 0;
    public int hp_potion = 0;
    public int runes = 0;
    //Player
    public float Hp;//Punkty zdrowia.
    public float Mana;//Punkty zdrowia.
    public float Stamina;//Punkty zdrowia.



    public int AmuletSlotItemID;
    public int Main_WeaponSlotItemID;
    public int SecondWeaponSlotItemID;
    public int SecondWeaponSlotItemCount;
    public int ArmorSlotItemID;
    public int LegsSlotItemID;
    public int BootsSlotItemID;

    public int[] Main_WeaponCount = new int[2];
    public int[] Secon_WeaponCount = new int[2];
    public int[] TasksCount = new int[2];
    public int[] AmuletsCount = new int[2];
    public int[] BootsCount = new int[2];
    public int[] LegsCount = new int[2];
    public int[] ArmorsCount = new int[2];

    public int[] notes = new int[9];
}

//dane skrzyni
[Serializable]
class ChestData
{
    public bool visited;
    public int[] Main_WeaponCount = new int[2];
    public int[] Secon_WeaponCount = new int[2];
    public int[] TasksCount = new int[2];
    public int[] AmuletsCount = new int[2];
    public int[] BootsCount = new int[2];
    public int[] LegsCount = new int[2];
    public int[] ArmorsCount = new int[2];
        //na podstaiwe: kursu UNITY3D v5. mvire.com 
}

 