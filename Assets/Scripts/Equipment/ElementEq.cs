using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/**
 * Skrypt odpowiedzialny za obsługę przeciągania elementu ekwipunku. 
 * 
 * @author Hubert Paluch.
 * MViRe - na potrzeby kursu UNITY3D v5.
 * mvire.com 
 */
public class ElementEq : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

	/** Obiekt transform rodzica.*/
	private Transform rodzicEq;
	/** Obiekt transform bieżącego elementu.*/
	private Transform trans;

	/** Typ elementu.*/
	public EqElementType elementTyp = EqElementType.Main_Weapon;
    public int Count;
    public int Id;
    public GameObject CountLook;
    public Text text;

    public int attack;
    public int armor;
    public int magic;
    public int agility;
    public double value;

    public WeaponType weaponType= WeaponType.None;

    private Game_Master gm;
    private bool show;
    public bool onDrag;
    public Transform prevParent;

    public RectTransform ItemShadow;

    // Use this for initialization
    void Start ()
    {
        onDrag = false;
		trans = GetComponent<Transform> ();
       
        prevParent = trans.parent;
        rodzicEq = trans.parent;
        ItemShadow.anchoredPosition = new Vector3(0, 0);
        gm = GameObject.Find("gameMaster").GetComponent<Game_Master>();

        ItemShadow.anchoredPosition = new Vector2(0, 0);

        if (elementTyp == EqElementType.Second_Weapon)
        {
            if (Count > 1)
            {
                CountLook.SetActive(true);
                text.text = Count.ToString();
            }
            if (Count == 1)
            {
                CountLook.SetActive(false);
            }
          
        }
        else
        {
            CountLook.SetActive(false);
        }

    }

    void Update()
    {
        if(elementTyp== EqElementType.Second_Weapon)
        {
            if (Count > 1)
            {
                CountLook.SetActive(true);
                text.text = Count.ToString();
            }
            if (Count == 1)
            {
                CountLook.SetActive(false);
            }
            if (Count < 1)
            {
                Destroy(gameObject, 0);
            }
        }
    }
    public void setPrevParent()
    {
        prevParent = transform.parent;
        ItemShadow.anchoredPosition = new Vector2(0, 0);

    }
    /**
	 * Metoda wywoływana w chwili rozpoczęcia przeciągania.
	 */
    public void OnBeginDrag(PointerEventData eventData)
    {
		//Debug.Log ("OnBeginDrag");

		//Na czas przeciągania obiektu zmieniam rodzica
		//aby nasz element nie zmieniał pozycji w ekwipunku (rodzicem będzie płutno).

		trans.SetParent(rodzicEq.parent);
		//Włączamy wykrywanie kursora myszy popbrzez wyłaczenie blokady promienia.
		GetComponent<CanvasGroup>().blocksRaycasts = false;

        gm.PlayerAttack = attack;
        gm.PlayerArmor = armor;
        gm.PlayerMagic = magic;
        gm.PlayerSpeed = agility;
        show = gm.ShowWeaponStats();
        ItemShadow.anchoredPosition = new Vector2(5, -10);

    }

    /**
	 * Metoda wywoływana w czasie przeciągania elementu.
	 */
    public void OnDrag(PointerEventData eventData){
		//Debug.Log ("OnDrag");		
		//Aktualizujemy pozycję elementu o aktualną pozycję kursora.
		trans.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData){
        GetComponent<CanvasGroup>().blocksRaycasts = true;

        Debug.Log ("OnEndDrag");
        //Ponownie przypisujemy rodzica ekwipunku do elementu
        //spowoduje to jego ustawienie/posortowanie w ekwipunku.
        trans.SetParent(rodzicEq);


        //Wyłączamy wykrywanie kursora myszy popbrzez właczenie blokady promienia.
        Debug.Log("GetComponent<CanvasGroup>().blocksRaycasts = true;");
        if(show)
        {
            gm.CloseWeaponStats();
        }
        else
        {
            gm.RefreshEqStats();
        }
        ItemShadow.anchoredPosition = new Vector2(0, 0);


        prevParent = trans.parent;
    }

    public Transform getRodzicEq()
    {
		return rodzicEq;
	}
	public void setRodzic(Transform trans)
    {
		rodzicEq = trans;
        gameObject.transform.parent = trans;
        prevParent = transform.parent;    //noze z tego  korzystaja, bo one po przeniesieniu (OnEndDrag) nie zmieniaja rodzica chyba, ze jest to 1 noz.

    }
}
