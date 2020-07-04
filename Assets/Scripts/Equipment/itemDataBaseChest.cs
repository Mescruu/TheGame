using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemDataBaseChest : MonoBehaviour
{
    public Transform Chest;


    public GameObject[] Main_Weapons;
    public int[] Main_WeaponCount;

    public GameObject[] Secon_Weapons;
    public int[] Secon_WeaponCount;


    public GameObject[] Tasks;
    public int[] TasksCount;

    public GameObject[] Amulets;
    public int[] AmuletsCount;

    public GameObject[] Boots;
    public int[] BootsCount;

    public GameObject[] Legs;
    public int[] LegsCount;

    public GameObject[] Armors;
    public int[] ArmorsCount;





    public void Instantiate()
    {

        //Load main eq
        for (int i = 0; i < AmuletsCount.Length; i++)
        {
            if (AmuletsCount[i] > 0)
            {
                for (int j = 0; j < AmuletsCount[i]; j++)
                {
                    Instantiate(Amulets[i], Chest);
                }
            }
        }

        for (int i = 0; i < Tasks.Length; i++)
        {
            if (TasksCount[i] > 0)
            {
                for (int j = 0; j < TasksCount[i]; j++)
                {
                    Instantiate(Tasks[i], Chest);
                }
            }
        }


        for (int i = 0; i < Main_Weapons.Length; i++)
        {
            if (Main_WeaponCount[i] > 0)
            {
                for (int j = 0; j < Main_WeaponCount[i]; j++)
                {
                    Instantiate(Main_Weapons[i], Chest);
                }
            }
        }

        for (int i = 0; i < Secon_Weapons.Length; i++)
        {
            if (Secon_WeaponCount[i] > 0)
            {

                GameObject effect = Instantiate(Secon_Weapons[i], Chest) as GameObject;
                effect.GetComponent<ElementEq>().Count = Secon_WeaponCount[i];
            }
        }

        for (int i = 0; i < Armors.Length; i++)
        {
            if (ArmorsCount[i] > 0)
            {
                for (int j = 0; j < ArmorsCount[i]; j++)
                {
                    Instantiate(Armors[i], Chest);
                }
            }
        }

        for (int i = 0; i < Legs.Length; i++)
        {
            if (LegsCount[i] > 0)
            {
                for (int j = 0; j < LegsCount[i]; j++)
                {
                    Instantiate(Legs[i], Chest);
                }
            }
        }

        for (int i = 0; i < Boots.Length; i++)
        {
            if (BootsCount[i] > 0)
            {
                for (int j = 0; j < BootsCount[i]; j++)
                {
                    Instantiate(Boots[i], Chest);
                }
            }
        }

    }

    public void checkQuantity()
    {
        for (int i = 0; i < Main_Weapons.Length; i++)
        {
            Main_WeaponCount[i] = 0;
            AmuletsCount[i] = 0;
            ArmorsCount[i] = 0;
            LegsCount[i] = 0;
            BootsCount[i] = 0;
            Secon_WeaponCount[i] = 0;
            TasksCount[i] = 0;

           // Debug.Log("zeruje skrzynie " + i);
        }
        if (Chest.childCount > 0)
        {
            for (int i = 0; i < Chest.childCount; i++)
            {
                Chest.GetChild(i).GetComponent<CanvasGroup>().blocksRaycasts = true;
                Chest.GetChild(i).GetComponent<ElementEq>().setPrevParent();


                for (int j = 0; j < Main_Weapons.Length; j++)
                {
                    if (Chest.GetChild(i).GetComponent<ElementEq>().elementTyp == EqElementType.Main_Weapon&& Chest.GetChild(i).GetComponent<ElementEq>().Id == Main_Weapons[j].GetComponent<ElementEq>().Id)
                    {
                   //     Debug.Log(Chest.GetChild(i).GetComponent<ElementEq>().elementTyp.ToString() + "NA PEWNO TAKI SAM TYP"+ EqElementType.Main_Weapon.ToString());

                    //    Debug.Log("NA PEWNO TAKI SAM TYP");
                        Main_WeaponCount[j] += Chest.GetChild(i).GetComponent<ElementEq>().Count;
                    //    Debug.Log("dODAAJE " + Chest.GetChild(i).GetComponent<ElementEq>().Count +" DO MIECZA Z " + Chest.GetChild(i).name);

                    }
                    if (Chest.GetChild(i).GetComponent<ElementEq>().Id == Secon_Weapons[j].GetComponent<ElementEq>().Id && Chest.GetChild(i).GetComponent<ElementEq>().elementTyp == EqElementType.Second_Weapon)
                     {
                    Secon_WeaponCount[j] += Chest.GetChild(i).GetComponent<ElementEq>().Count;
                   //     Debug.Log("dODAAJE " + Chest.GetChild(i).GetComponent<ElementEq>().Count + " DO SECON WEAPON Z " + Chest.GetChild(i).name);
                    }


                    if (Chest.GetChild(i).GetComponent<ElementEq>().Id == Amulets[j].GetComponent<ElementEq>().Id && Chest.GetChild(i).GetComponent<ElementEq>().elementTyp == EqElementType.Amulet)
                    {
                        AmuletsCount[j] += Chest.GetChild(i).GetComponent<ElementEq>().Count;
                   //     Debug.Log("dODAAJE " + Chest.GetChild(i).GetComponent<ElementEq>().Count + " DO AMULETU Z " + Chest.GetChild(i).name);

                    }


                    if (Chest.GetChild(i).GetComponent<ElementEq>().Id == Armors[j].GetComponent<ElementEq>().Id && Chest.GetChild(i).GetComponent<ElementEq>().elementTyp == EqElementType.Armor)
                    {
                        ArmorsCount[j] += Chest.GetChild(i).GetComponent<ElementEq>().Count;
                   //     Debug.Log("dODAAJE " + Chest.GetChild(i).GetComponent<ElementEq>().Count + " DO PANCERZ Z " + Chest.GetChild(i).name);

                    }


                    if (Chest.GetChild(i).GetComponent<ElementEq>().Id == Tasks[j].GetComponent<ElementEq>().Id && Chest.GetChild(i).GetComponent<ElementEq>().elementTyp == EqElementType.Task_Item)
                    {
                        TasksCount[j] += Chest.GetChild(i).GetComponent<ElementEq>().Count;
                   //     Debug.Log("dODAAJE " + Chest.GetChild(i).GetComponent<ElementEq>().Count + " DO TASK Z " + Chest.GetChild(i).name);

                    }


                    if (Chest.GetChild(i).GetComponent<ElementEq>().Id == Legs[j].GetComponent<ElementEq>().Id && Chest.GetChild(i).GetComponent<ElementEq>().elementTyp == EqElementType.Legs)
                    {
                        LegsCount[j] += Chest.GetChild(i).GetComponent<ElementEq>().Count;
                    //    Debug.Log("dODAAJE " + Chest.GetChild(i).GetComponent<ElementEq>().Count + " DO NOGAWIC Z " + Chest.GetChild(i).name);

                    }


                    if (Chest.GetChild(i).GetComponent<ElementEq>().Id == Boots[j].GetComponent<ElementEq>().Id && Chest.GetChild(i).GetComponent<ElementEq>().elementTyp == EqElementType.Boots)
                    {
                        BootsCount[j] += Chest.GetChild(i).GetComponent<ElementEq>().Count;
                 //       Debug.Log("dODAAJE " + Chest.GetChild(i).GetComponent<ElementEq>().Count + " DO BUTOW Z " + Chest.GetChild(i).name);

                    }
                }
            }
        }
    }

}


