using UnityEngine;
using System.Collections;

public class itemDataBase : MonoBehaviour 
{

    public GameObject[] Main_Weapons;
    public int[] Main_WeaponCount;

    public GameObject[] Secon_Weapons;
    public int[] Secon_WeaponCount;

    public Transform WeaponSlots;


    public GameObject[] Tasks;
    public int[] TasksCount;

    public GameObject[] Amulets;
    public int[] AmuletsCount;

    public Transform RestSlos;


    public GameObject[] Boots;
    public int[] BootsCount;

    public GameObject[] Legs;
    public int[] LegsCount;

    public GameObject[] Armors;
    public int[] ArmorsCount;

    public Transform DefendSlots;

    public int AmuletSlotItemID;
    public Transform AmuletSlot;


    public int Main_WeaponSlotItemID;
    public Transform MainMain_Weapon;


    public int SecondWeaponSlotItemID;
    public Transform MainSecondWeaponSlot;
    public int SecondWeaponSlotItemCount;

    public int ArmorSlotItemID;
    public Transform MainArmorSlot;


    public int LegsSlotItemID;
    public Transform MainLegsSlot;


    public int BootsSlotItemID;
    public Transform MainBootsSlot;

    public void Update()
    {
        if (MainSecondWeaponSlot.childCount>0)
        {
            SecondWeaponSlotItemCount = MainSecondWeaponSlot.transform.GetChild(0).GetComponent<ElementEq>().Count;
        }
        else
        {
            SecondWeaponSlotItemCount = 0;
        }
    }

    public void Instantiate()
    {

        //Load main eq
        for(int i=0;i<AmuletsCount.Length;i++)
        {
            if(AmuletsCount[i]>0)
            {
                for(int j=0;j<AmuletsCount[i];j++)
                {
                    Instantiate(Amulets[i], RestSlos);
                }
            }
        }
        for (int i = 0; i < Tasks.Length; i++)
        {
            if (TasksCount[i] > 0)
            {
                for (int j = 0; j < TasksCount[i]; j++)
                {
                    Instantiate(Tasks[i], RestSlos);
                }
            }
        }


        for (int i = 0; i < Main_Weapons.Length; i++)
        {
            if (Main_WeaponCount[i] > 0)
            {
                for (int j = 0; j < Main_WeaponCount[i]; j++)
                {
                    Instantiate(Main_Weapons[i], WeaponSlots);
                }
            }
        }
        for (int i = 0; i < Secon_Weapons.Length; i++)
        {
            if (Secon_WeaponCount[i] > 0)
            {
                
                    GameObject effect = Instantiate(Secon_Weapons[i], WeaponSlots) as GameObject;
                    effect.GetComponent<ElementEq>().Count = Secon_WeaponCount[i];
            }
        }
        for (int i = 0; i < Armors.Length; i++)
        {
            if (ArmorsCount[i] > 0)
            {
                for (int j = 0; j < ArmorsCount[i]; j++)
                {
                    Instantiate(Armors[i], DefendSlots);
                }
            }
        }
        for (int i = 0; i < Legs.Length; i++)
        {
            if (LegsCount[i] > 0)
            {
                for (int j = 0; j < LegsCount[i]; j++)
                {
                    Instantiate(Legs[i], DefendSlots);
                }
            }
        }
        for (int i = 0; i < Boots.Length; i++)
        {
            if (BootsCount[i] > 0)
            {
                for (int j = 0; j < BootsCount[i]; j++)
                {
                    Instantiate(Boots[i], DefendSlots);
                }
            }
        }


        if (AmuletSlotItemID>=0)
        {
            Instantiate(Amulets[AmuletSlotItemID], AmuletSlot);
        }
        if (Main_WeaponSlotItemID >= 0)
        {
            Instantiate(Main_Weapons[Main_WeaponSlotItemID], MainMain_Weapon);
        }
        if (SecondWeaponSlotItemID >= 0)
        {
            Instantiate(Secon_Weapons[SecondWeaponSlotItemID], MainSecondWeaponSlot);
            Debug.Log("uaktualnianie ilosci nozy");
            MainSecondWeaponSlot.GetChild(0).GetComponent<ElementEq>().Count = SecondWeaponSlotItemCount;
        }

        if (ArmorSlotItemID >= 0)
        {
            Instantiate(Armors[ArmorSlotItemID], MainArmorSlot);
        }
        if (LegsSlotItemID >= 0)
        {
            Instantiate(Legs[LegsSlotItemID], MainLegsSlot);
        }
        if (BootsSlotItemID >= 0)
        {
            Instantiate(Boots[BootsSlotItemID], MainBootsSlot);
        }


        //load rest;

    }

    public void checkQuantity()
    {
        Debug.Log("checkQuantity");
        for (int i = 0; i < Main_Weapons.Length; i++)
        {
            Main_WeaponCount[i] = 0;
            AmuletsCount[i] = 0;
            ArmorsCount[i] = 0;
            LegsCount[i] = 0;
            BootsCount[i] = 0;
            Secon_WeaponCount[i] = 0;
            TasksCount[i] = 0;

        }
      //  Debug.Log("0 na kazdy ilosc przedmiotow");



        if (AmuletSlot.childCount > 0)
        {
            AmuletSlotItemID = AmuletSlot.GetChild(0).GetComponent<ElementEq>().Id;
            AmuletSlot.GetChild(0).GetComponent<ElementEq>().setPrevParent();
        }
        else
        {
            AmuletSlotItemID = -1;
        }

        if (MainMain_Weapon.childCount > 0)
        {
            Main_WeaponSlotItemID = MainMain_Weapon.GetChild(0).GetComponent<ElementEq>().Id;
            MainMain_Weapon.GetChild(0).GetComponent<ElementEq>().setPrevParent();
        }
        else
        {
            Main_WeaponSlotItemID = -1;
        }

        if (MainSecondWeaponSlot.childCount > 0)
        {
            SecondWeaponSlotItemID = MainSecondWeaponSlot.GetChild(0).GetComponent<ElementEq>().Id;
            MainSecondWeaponSlot.GetChild(0).GetComponent<ElementEq>().setPrevParent();
        }
        else
        {
            SecondWeaponSlotItemID = -1;
        }

        if (MainArmorSlot.childCount > 0)
        {
            ArmorSlotItemID = MainArmorSlot.GetChild(0).GetComponent<ElementEq>().Id;
            MainArmorSlot.GetChild(0).GetComponent<ElementEq>().setPrevParent();
        }
        else
        {
            ArmorSlotItemID = -1;
        }

        if (MainLegsSlot.childCount > 0)
        {
            LegsSlotItemID = MainLegsSlot.GetChild(0).GetComponent<ElementEq>().Id;
            MainLegsSlot.GetChild(0).GetComponent<ElementEq>().setPrevParent();
        }
        else
        {
            LegsSlotItemID = -1;
        }

        if (MainBootsSlot.childCount > 0)
        {
            BootsSlotItemID = MainBootsSlot.GetChild(0).GetComponent<ElementEq>().Id;
            MainBootsSlot.GetChild(0).GetComponent<ElementEq>().setPrevParent();
        }
        else
        {
            BootsSlotItemID = -1;
        }
       // Debug.Log("=1 lub id na uzywaany przedmiot");

        if (WeaponSlots.childCount > 0)
        {
            //Debug.Log("(WeaponSlots.childCount > 0");

            for (int i = 0; i < WeaponSlots.childCount; i++)
            {
                WeaponSlots.GetChild(i).GetComponent<CanvasGroup>().blocksRaycasts = true;
                WeaponSlots.GetChild(i).GetComponent<ElementEq>().setPrevParent();

               // Debug.Log("petla glowna przechodzi juz " + i);
               // Debug.Log("nazwa przedmiotu " + WeaponSlots.GetChild(i).name);
                for (int j = 0; j < Main_Weapons.Length; j++)
                {
                //    Debug.Log("to jest pobrane z bazy danych "+Main_Weapons[j].GetComponent<ElementEq>().Id  + "==" + WeaponSlots.GetChild(i).GetComponent<ElementEq>().Id +" to jest pobrane z plecaka");
                //    Debug.Log("to jest pobrane z bazy danych " + Main_Weapons[j].GetComponent<ElementEq>().elementTyp.ToString() + "==" + WeaponSlots.GetChild(i).GetComponent<ElementEq>().elementTyp.ToString() + " to jest pobrane z plecaka");

                    if (WeaponSlots.GetChild(i).GetComponent<ElementEq>().Id == Main_Weapons[j].GetComponent<ElementEq>().Id && WeaponSlots.GetChild(i).GetComponent<ElementEq>().elementTyp == EqElementType.Main_Weapon)
                    {
                        Main_WeaponCount[j] += WeaponSlots.GetChild(i).GetComponent<ElementEq>().Count;
                   //     Debug.Log("zgadza sie " + WeaponSlots.GetChild(i).name);

                    }
                }
                for (int j = 0; j < Secon_Weapons.Length; j++)
                {
                    if (WeaponSlots.GetChild(i).GetComponent<ElementEq>().Id == Secon_Weapons[j].GetComponent<ElementEq>().Id && WeaponSlots.GetChild(i).GetComponent<ElementEq>().elementTyp == EqElementType.Second_Weapon)
                    {
                        Secon_WeaponCount[j] += WeaponSlots.GetChild(i).GetComponent<ElementEq>().Count;
                   //     Debug.Log("zgadza sie " + WeaponSlots.GetChild(i).name);
                    }
                }

            }
        }
        if (RestSlos.childCount > 0)
        {
            // Debug.Log("RestSlos.childCount > 0");

            for (int i = 0; i < RestSlos.childCount; i++)
            {
                RestSlos.GetChild(i).GetComponent<CanvasGroup>().blocksRaycasts = true;
                RestSlos.GetChild(i).GetComponent<ElementEq>().setPrevParent();


                for (int j = 0; j < Amulets.Length; j++)
                {
                    if (RestSlos.GetChild(i).GetComponent<ElementEq>().Id == Amulets[j].GetComponent<ElementEq>().Id && RestSlos.GetChild(i).GetComponent<ElementEq>().elementTyp == EqElementType.Amulet)
                    {
                        AmuletsCount[j] += RestSlos.GetChild(i).GetComponent<ElementEq>().Count;
                //        Debug.Log("zgadza sie " + RestSlos.GetChild(i).name);

                    }
                }
                for (int j = 0; j < Tasks.Length; j++)
                {
                    if (RestSlos.GetChild(i).GetComponent<ElementEq>().Id == Tasks[j].GetComponent<ElementEq>().Id && RestSlos.GetChild(i).GetComponent<ElementEq>().elementTyp == EqElementType.Task_Item)
                    {
                        TasksCount[j] += RestSlos.GetChild(i).GetComponent<ElementEq>().Count;
                 //       Debug.Log("zgadza sie " + RestSlos.GetChild(i).name);

                    }
                }
            }
        }
        if (DefendSlots.childCount > 0)
        {
          //  Debug.Log("DefendSlots.childCount > 0");

            for (int i = 0; i < DefendSlots.childCount; i++)
            {
                DefendSlots.GetChild(i).GetComponent<CanvasGroup>().blocksRaycasts = true;
                DefendSlots.GetChild(i).GetComponent<ElementEq>().setPrevParent();


                for (int j = 0; j < Armors.Length; j++)
                {
                    if (DefendSlots.GetChild(i).GetComponent<ElementEq>().Id == Armors[j].GetComponent<ElementEq>().Id && DefendSlots.GetChild(i).GetComponent<ElementEq>().elementTyp == EqElementType.Armor)
                    {
                        ArmorsCount[j] += DefendSlots.GetChild(i).GetComponent<ElementEq>().Count;
                    }
                }
                for (int j = 0; j < Legs.Length; j++)
                {
                    if (DefendSlots.GetChild(i).GetComponent<ElementEq>().Id == Legs[j].GetComponent<ElementEq>().Id && DefendSlots.GetChild(i).GetComponent<ElementEq>().elementTyp == EqElementType.Legs)
                    {
                        LegsCount[j] += DefendSlots.GetChild(i).GetComponent<ElementEq>().Count;
                    }
                }
                for (int j = 0; j < Boots.Length; j++)
                {
                    if (DefendSlots.GetChild(i).GetComponent<ElementEq>().Id == Boots[j].GetComponent<ElementEq>().Id && DefendSlots.GetChild(i).GetComponent<ElementEq>().elementTyp == EqElementType.Boots)
                    {
                        BootsCount[j] += DefendSlots.GetChild(i).GetComponent<ElementEq>().Count;
                    }
                }
            }
        }
    }
    

}


