using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadow : MonoBehaviour {

    public float maxdistance; //Maksymalny dystans przy którym cień istnieje
    public float theDistance; //Dystans od obiektu

    private RaycastHit2D hit; //Promień sprawdzający
    private Vector2 down; //Wektor w dół

    //Komponenty
    public SpriteRenderer renderer;
    public SpriteRenderer ParentRenderer;
    public LayerMask mask; //Warstwa na której działa cień

    private Color color;
    private Color colorUnvisible; //kolor przezroczysty = brak cienia
    public GameObject shadow;
    public float ShadowOffset; //Odstęp od punktu objektu, który rzuca cień 

    public bool OwnShadow; //Czy posiada inny niż wygenerowany z obiektu cień

    // Use this for initialization
    void Start () {

    //Wstępne ustawienia cienia
        down = transform.TransformDirection(Vector2.down);
        renderer = shadow.GetComponent<SpriteRenderer>();
        ParentRenderer = gameObject.GetComponentInParent<SpriteRenderer>();
        color = new Color(0, 0, 0, 0.35f);
        colorUnvisible = new Color(0, 0, 0, 0);
    }

    // Update is called once per frame
    void Update () {

        if(!OwnShadow)
        {
            renderer.sprite = ParentRenderer.sprite;
        }

        hit = Physics2D.Raycast(transform.position, down, maxdistance,mask); //prawda/fałsz jeżeli uda się znaleźć miejsce na którym może być cień

        if(hit)
        {
            theDistance = hit.distance;

            shadow.transform.position = new Vector3(transform.position.x, transform.position.y - theDistance + ShadowOffset, 0.0f);

            renderer.color = color;
        }
        else
        {
            renderer.color = colorUnvisible; 
        }

        if(ParentRenderer.color.a<=0.2f|| ParentRenderer.isVisible==false)
        {
            renderer.color = colorUnvisible;
        }
    }
}
