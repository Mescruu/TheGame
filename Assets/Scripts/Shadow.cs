using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadow : MonoBehaviour {

    public float maxdistance;
    public float theDistance;
    private RaycastHit2D hit;
    private Vector2 down;
    public SpriteRenderer renderer;
    public SpriteRenderer ParentRenderer;
    public LayerMask mask;
    private Color color;
    private Color colorUnvisible;
    public GameObject shadow;
    public float ShadowOffset;
    public bool OwnShadow;

    // Use this for initialization
    void Start () {
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

        hit = Physics2D.Raycast(transform.position, down, maxdistance,mask);

        if(hit)
        {
            theDistance = hit.distance;

            shadow.transform.position = new Vector3(transform.position.x, transform.position.y - theDistance + ShadowOffset, 0.0f);

        //   Debug.Log(theDistance + "    " + hit.collider.gameObject.name);
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
