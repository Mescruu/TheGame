using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

/**
 * Skrypt odpowiedzialny za obsługę odebrania upuszczonego obiektu. 
 * 
 * @author Hubert Paluch.
 * MViRe - na potrzeby kursu UNITY3D v5.
 * mvire.com 
 */
public class DropPlace : MonoBehaviour, IDropHandler
{

    public EqElementType elementTyp;

    /** Obiekt ekwipunku.*/
    public GameObject Rest_Eq;
    public GameObject Weapon_Eq;
    public GameObject Defend_Eq;
    public GameObject Chest_Eq;


    public bool ChestPlace;


    public AudioClip audioClip;
    private AudioSource audioSource;
    /** Maksymalna ilość elementów w obiekcie. */
    public int maksElement;

    /** Obiekt transform bieżącego elementu.*/
    private Transform trans;
    private itemDataBase itd;
    public itemDataBaseChest itdch;  //Nalezy uzupelnic reczznie zeby nie probowac pobieraac tego elementu kiedy to nie jest potrzebne;


    private ElementEq child;
    private Game_Master gm;
    private CanvasEkwipunek eq;

    // Use this for initialization
    void Start()
    {
        trans = GetComponent<Transform>();
        itd = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<itemDataBase>();

        if (gameObject.name == "Chest")
        {
            itdch = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<itemDataBaseChest>();
        }

        gm = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<Game_Master>();
        eq = GameObject.Find("Equipment").GetComponent<CanvasEkwipunek>();

        child = gameObject.GetComponentInChildren<ElementEq>();
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.clip = audioClip;
    }

    /**
	 * Metoda wykrywająca zdarzenie upuszczenia obiektu.
	 */
    public void OnDrop(PointerEventData eventData)
    {
        ElementEq d = eventData.pointerDrag.GetComponent<ElementEq>();
        if (d != null)
        {
            if(d.prevParent!=transform)
            {
                if (d.elementTyp == EqElementType.Second_Weapon)
                {
                    if (d.prevParent !=Chest_Eq.transform && trans == Chest_Eq.transform)
                    {
                        Debug.Log("Pomiedzy nie skrzynia a skrzynia");
                        eq.Transfer(d,gameObject);
                    }
                    else
                    {
                        if (d.prevParent == Chest_Eq.transform && trans != Chest_Eq.transform)
                        {
                            Debug.Log("Pomiedz skrzynia a nie skrzynia");
                            eq.Transfer(d, gameObject);

                        }
                        else
                        {
                            Debug.Log("Pomiedz plecakiem a slotem");
                            Transfer(d);
                        }
                    }
                }
                else
                {
                    Debug.Log("Albo nie noze, albo tych nozy jest 1 sztuka");

                    Transfer(d);
                }
            }
        }
    }
    public void TransferKnives(ElementEq d, int amountToTransfer)
    {
        if (d != null)
        {
            Debug.Log("TransferKnives");
            Debug.Log("Amount = " + amountToTransfer);

            if (elementTyp == EqElementType.Second_Weapon)
            {

                Debug.Log(trans.childCount);
                //Sprawdzamy czy slot jest pusty.
                if (trans.childCount < maksElement)
                {
                    Debug.Log("Slot jest wolny, wiec usuwam z przedmiotu przezucanego ilosc, i tworze nowy przedmiot w dsecond wweapon slot.");


                    d.Count-=amountToTransfer;
                    GameObject knives = Instantiate(itd.Secon_Weapons[d.Id], transform) as GameObject;
                    knives.GetComponent<ElementEq>().Count =amountToTransfer;
                }
                else
                {//Slot nie jest pusty i osiągnięto maksymalną ilość elementów.

                    //Pobieram obecny element slotu.
                    Transform elem = transform.GetChild(0);
                    ElementEq element = gameObject.GetComponentInChildren<ElementEq>();

                    //Obecny element slotu przerzucam do ekwipunku poprzez ustawienie rodzica.
                    if (d.prevParent ==  Weapon_Eq.transform)
                    {
                            
                        if (element.Id == d.Id && element.elementTyp == EqElementType.Second_Weapon)
                        {
                            if (d.Count - amountToTransfer <= 0)
                            {
                                Destroy(d.gameObject);
                                element.Count += amountToTransfer;
                            }
                            else
                            {
                                d.Count -= amountToTransfer;
                                element.Count += amountToTransfer;
                            }
                        }
                        else
                        {
                            if (d.Count - amountToTransfer < 0)
                            {
                                if (d.prevParent.GetComponent<DropPlace>().maksElement > d.prevParent.childCount)
                                {
                                    Debug.Log("Jest miejscce na dodatkowy przedmiot ktory wroci");
                                    d.Count -= amountToTransfer;
                                    GameObject knives = Instantiate(itd.Secon_Weapons[d.Id], transform) as GameObject;
                                    knives.GetComponent<ElementEq>().Count = amountToTransfer;

                                    bool sameknife = false;
                                    for (int i = 0; i < d.prevParent.childCount; i++)
                                    {
                                        if (d.prevParent.GetChild(i).GetComponent<ElementEq>().Id == element.Id && d.prevParent.GetChild(i).GetComponent<ElementEq>().elementTyp == EqElementType.Second_Weapon)
                                        {
                                            d.prevParent.GetChild(i).GetComponent<ElementEq>().Count += elem.GetComponent<ElementEq>().Count;
                                            Debug.Log("zgadza sie " + d.prevParent.GetChild(i).name);
                                            Destroy(elem.gameObject);
                                            sameknife = true;
                                        }
                                    }
                                    if (!sameknife)
                                    {
                                        elem.SetParent(d.prevParent.transform);
                                        Debug.Log("Nie sa takie same noze");
                                    }
                                    else
                                    {
                                        Debug.Log("zgadza sie ");
                                    }
                                }
                                else
                                {
                                    Debug.Log("Nie ma miejsca");
                                }
                            }
                            else
                            {
                                elem.SetParent(d.prevParent);
                                d.setRodzic(trans);
                            }

                        }

                    }
                    else
                    {

                        if (element.Id == d.Id && element.elementTyp == EqElementType.Second_Weapon)
                        {
                            if (d.Count - amountToTransfer <= 0)
                            {
                                Destroy(d.gameObject);
                                element.Count += amountToTransfer;
                            }
                            else
                            {
                                d.Count -= amountToTransfer;
                                element.Count += amountToTransfer;
                            }
                        }
                        else
                        {
                            if (d.Count - amountToTransfer > 0)
                            {
                                Debug.Log("d.prevParent = " + d.prevParent.ToString());
                                if (d.prevParent.GetComponent<DropPlace>().maksElement > d.prevParent.childCount)
                                {
                                    d.Count -= amountToTransfer;
                                    GameObject knives = Instantiate(itd.Secon_Weapons[d.Id], transform) as GameObject;
                                    knives.GetComponent<ElementEq>().Count = amountToTransfer;


                                    bool sameknife = false;
                                    for (int i = 0; i < Chest_Eq.transform.childCount; i++)
                                    {
                                        if (Chest_Eq.transform.GetChild(i).GetComponent<ElementEq>().Id == element.Id && Chest_Eq.transform.GetChild(i).GetComponent<ElementEq>().elementTyp == EqElementType.Second_Weapon)
                                        {
                                            Chest_Eq.transform.GetChild(i).GetComponent<ElementEq>().Count += elem.GetComponent<ElementEq>().Count;
                                            Debug.Log("zgadza sie " + Chest_Eq.transform.GetChild(i).name);
                                            Destroy(elem.gameObject);
                                            sameknife = true;
                                        }
                                    }
                                    if (!sameknife)
                                    {
                                        elem.SetParent(Chest_Eq.transform);
                                    }
                                }
                                else
                                {
                                    Debug.Log("Nie ma miejsca");
                                }
                            }
                            else
                            {
                                Debug.Log("Calosc przezucam, calosc wraca");
                                d.setRodzic(trans);

                                bool sameknife = false;
                                for (int i = 0; i < Chest_Eq.transform.childCount; i++)
                                {
                                    if (Chest_Eq.transform.GetChild(i).GetComponent<ElementEq>().Id == element.Id && Chest_Eq.transform.GetChild(i).GetComponent<ElementEq>().elementTyp == EqElementType.Second_Weapon)
                                    {
                                        Chest_Eq.transform.GetChild(i).GetComponent<ElementEq>().Count += elem.GetComponent<ElementEq>().Count;
                                        Debug.Log("zgadza sie " + Chest_Eq.transform.GetChild(i).name);
                                        Destroy(elem.gameObject);
                                        sameknife = true;
                                    }
                                }
                                if (!sameknife)
                                {
                                    elem.SetParent(Chest_Eq.transform);
                                }

                            }
                        }
                    }
                }
                audioSource.Play();
            }
            else
            {

                if (elementTyp == EqElementType.Weapons)
                {
                    bool sameknife = false;
                    for (int i = 0; i < Weapon_Eq.transform.childCount; i++)
                    {
                        if (Weapon_Eq.transform.GetChild(i).GetComponent<ElementEq>().Id == d.Id && Weapon_Eq.transform.GetChild(i).GetComponent<ElementEq>().elementTyp == EqElementType.Second_Weapon)
                        {
                            sameknife = true; 
                            Weapon_Eq.transform.GetChild(i).GetComponent<ElementEq>().Count += d.Count;
                            Debug.Log("zgadza sie " + Weapon_Eq.transform.GetChild(i).name);
                            Destroy(d.gameObject);
                        }
                    }
                    if (!sameknife)
                    {
                        if (d.Count - amountToTransfer <= 0)
                        {
                            d.setRodzic(trans);
                        }else
                        {
                            d.Count -= amountToTransfer;
                            GameObject knives = Instantiate(itd.Secon_Weapons[d.Id], transform) as GameObject;
                            knives.GetComponent<ElementEq>().Count = amountToTransfer;
                        }
                    }
                }

                if (elementTyp == EqElementType.Chest)
                {
                    bool sameknife = false;
                    for (int i = 0; i < Chest_Eq.transform.childCount; i++)
                    {
                        if (Chest_Eq.transform.GetChild(i).GetComponent<ElementEq>().Id == d.Id && Chest_Eq.transform.GetChild(i).GetComponent<ElementEq>().elementTyp == EqElementType.Second_Weapon)
                        {
                            sameknife = true;
                            Chest_Eq.transform.GetChild(i).GetComponent<ElementEq>().Count += d.Count;
                            Debug.Log("zgadza sie " + Chest_Eq.transform.GetChild(i).name);
                            Destroy(d.gameObject);
                        }
                    }
                    if (!sameknife)
                    {
                        Debug.Log(" ");
                        if (d.Count - amountToTransfer <= 0)
                        {
                            d.setRodzic(trans);
                        }
                        else
                        {
                            d.Count -= amountToTransfer;
                            GameObject knives = Instantiate(itd.Secon_Weapons[d.Id], transform) as GameObject;
                            knives.GetComponent<ElementEq>().Count = amountToTransfer;
                        }
                    }
                }
                eq.PlayBag();
            }

        }

        if (d.Count <= 0)
        {
            Destroy(d.gameObject);

        }
        gm.RefreshEqStats();
        gm.RefreshEqStats();
        itd.checkQuantity();
        itdch.checkQuantity();

        Debug.Log("odswiezam, statystyki broni itd.");

    }
    public void Transfer(ElementEq d)
     {
        if (d != null)
        {

            Debug.Log("Transfer");

            if (elementTyp == d.elementTyp)
            {
                Debug.Log(trans.childCount);
                //Sprawdzamy czy slot jest pusty.
                if (trans.childCount < maksElement)
                {
                    d.setRodzic(trans);

                }
                else
                {//Slot nie jest pusty i osiągnięto maksymalną ilość elementów.

                   
                    //Pobieram obecny element slotu.
                    Transform elem = transform.GetChild(0);
                    ElementEq element = gameObject.GetComponentInChildren<ElementEq>();

                    if (element.elementTyp == EqElementType.Second_Weapon && element.Id == d.Id)
                    {
                        element.Count += d.Count;
                        Destroy(d.gameObject);
                    }
                    else
                    {
                        //Obecny element slotu przerzucam do ekwipunku poprzez ustawienie rodzica.
                        if (d.prevParent == Rest_Eq.transform || d.prevParent == Defend_Eq.transform || d.prevParent == Weapon_Eq.transform)
                        {
                            if (d.elementTyp == EqElementType.Armor || d.elementTyp == EqElementType.Legs || d.elementTyp == EqElementType.Boots)
                            {

                                elem.SetParent(Defend_Eq.transform);

                            }
                            if (d.elementTyp == EqElementType.Second_Weapon)
                            {
                                bool sameknife = false;
                                for (int i = 0; i < d.prevParent.childCount; i++)
                                {
                                    if (d.prevParent.GetChild(i).GetComponent<ElementEq>().Id == element.Id && d.prevParent.GetChild(i).GetComponent<ElementEq>().elementTyp == EqElementType.Second_Weapon)
                                    {
                                        d.prevParent.GetChild(i).GetComponent<ElementEq>().Count += elem.GetComponent<ElementEq>().Count;
                                        Debug.Log("zgadza sie " + d.prevParent.GetChild(i).name);
                                        Destroy(elem.gameObject);
                                        sameknife = true;
                                    }
                                }
                                if (!sameknife)
                                {
                                    elem.SetParent(d.prevParent.transform);
                                    Debug.Log("Nie sa takie same noze");
                                }
                                else
                                {
                                    Debug.Log("zgadza sie ");
                                }
                            }
                            if (d.elementTyp == EqElementType.Main_Weapon)
                            {
                                elem.SetParent(Weapon_Eq.transform);
                            }
                            if (d.elementTyp == EqElementType.Amulet || d.elementTyp == EqElementType.Task_Item)
                            {
                                elem.SetParent(Rest_Eq.transform);
                            }
                        }
                        else
                        {
                            elem.SetParent(Chest_Eq.transform);
                        }
                        element.setPrevParent();
                        //Umieszczam nowy element w slocie poprzez ustawienie rodzica.

                        d.setRodzic(trans);
                    }
                        

                }

                audioSource.Play();

            }
            else
            {

                if (elementTyp == EqElementType.Weapons || elementTyp == EqElementType.Rest || elementTyp == EqElementType.Defend)
                {//Jeżeli typy się nie zgadzają to sprawdź 
                 //czy czasem nie ekwipunek, bo tu można dodać wszystko.
                    if (d.elementTyp == EqElementType.Armor || d.elementTyp == EqElementType.Legs || d.elementTyp == EqElementType.Boots)
                    {
                     d.setRodzic(Defend_Eq.transform);
                    }
                    if (d.elementTyp == EqElementType.Main_Weapon)
                    {
                     d.setRodzic(Weapon_Eq.transform);
                    }
                    if (d.elementTyp == EqElementType.Second_Weapon)
                    {
                        bool sameknife = false;
                        for (int i = 0; i < Weapon_Eq.transform.childCount; i++)
                        {
                            if (Weapon_Eq.transform.GetChild(i).GetComponent<ElementEq>().Id == d.Id && Weapon_Eq.transform.GetChild(i).GetComponent<ElementEq>().elementTyp == EqElementType.Second_Weapon)
                            {
                                Weapon_Eq.transform.GetChild(i).GetComponent<ElementEq>().Count += d.GetComponent<ElementEq>().Count;
                                Debug.Log("zgadza sie " + Weapon_Eq.transform.GetChild(i).name);
                                Destroy(d.gameObject);
                                sameknife = true;
                            }
                        }
                        if (!sameknife)
                        {
                            d.setRodzic(Weapon_Eq.transform);
                            Debug.Log("Nie sa takie same noze");
                        }
                        else
                        {
                            Debug.Log("zgadza sie ");
                        }
                    }
                    if (d.elementTyp == EqElementType.Amulet || d.elementTyp == EqElementType.Task_Item)
                    {
                      d.setRodzic(Rest_Eq.transform);
                    }
                }
                if (elementTyp == EqElementType.Chest)
                {
                    if (d.elementTyp == EqElementType.Second_Weapon)
                    {
                      d.setRodzic(gameObject.transform);
                    }
                    else
                    {
                     d.setRodzic(trans);
                    }
                }
                eq.PlayBag();
            }

        }

        gm.RefreshEqStats();
        gm.RefreshEqStats();
        itd.checkQuantity();
        itdch.checkQuantity();

        Debug.Log("odswiezam, statystyki broni itd.");

}
    //fromEq==true  z ekwipunku do slotu, fromEq==false  z slotu do ekwipunku, //zamiana 
    void Set(ElementEq d, bool fromEq)
    {
        if (!fromEq)
        {
        }


    }

    void SetChest(ElementEq d, bool fromChest)
    {
        

    }


    void SetChestEqTransfer(ElementEq d, bool fromEq)
    {
        

        eq.PlayBag();



    }



}

