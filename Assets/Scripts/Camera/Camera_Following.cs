using UnityEngine;
using System.Collections;

public class Camera_Following : MonoBehaviour {

	private Vector2 velocity; //prędkość kamery

    //zmienne określające jak "gładko" ma poruszać się kamera
	public float smoothTimeY;
	public float smoothTimeX;

    //Punkt do którego dąży kamera i jej margines
	public Transform Point;
    public float offset;
    public float offsetSpeed; //szybkość poruszania się kamery przy marginesie


	public bool bounds; //blokada kamery w określonych niżej ramach
    public Vector3 minCameraPos;
	public Vector3 maxCameraPos;

    //zmienne dot. trzęsienia się kamery
	public float shakeTimer;
	public float shakeAmount;

    private KeyMenager keyMenager;
    private GameObject player;
    void Start()
    {
        //dodanie odpowiednich obiektów
        keyMenager = GameObject.Find("KeyMenager").GetComponent<KeyMenager>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void FixedUpdate () {

            // Mathf.SmoothDamp Wartość jest wygładzana przez funkcję przypominającą sprężynę, która nigdy nie zostanie przekroczona.
            float posX = Mathf.SmoothDamp (transform.position.x, Point.position.x, ref velocity.x, smoothTimeX);
			float posY = Mathf.SmoothDamp (transform.position.y+0.5f, Point.transform.position.y+0.5f, ref velocity.y, smoothTimeY);
        
            //Ustawienie odpowiedniej pozycji kamery, po wygładzeniu
			transform.position = new Vector3 (posX, posY, transform.position.z);

            //ustawienie blokady 
			if (bounds)
            {    //Mathf.Clamp Zaciska podaną wartość między podanymi wartościami minimalnymi i maksymalnymi.
            transform.position = new Vector3(Mathf.Clamp (transform.position.x, minCameraPos.x, maxCameraPos.x),Mathf.Clamp(transform.position.y, minCameraPos.y, maxCameraPos.y),Mathf.Clamp(transform.position.z, minCameraPos.z, maxCameraPos.z));
			}

		}

	void Update()
	{
        //Trzęsienie się kamery
		if (shakeTimer >= 0) 
		{
        //Random.insideUnitCircle Zwraca losowy punkt wewnątrz okręgu o promieniu 1 (tylko do odczytu).
        Vector2 ShakePos = Random.insideUnitCircle * shakeAmount;
		transform.position= new Vector3(transform.position.x + ShakePos.x, transform.position.y+ShakePos.y, transform.position.z);
		shakeTimer-=Time.deltaTime;
		}

        //Ustawienie poruszania się kamery podczas wciskania klawiszy i poza limitem itd..
        if (Input.GetKey(keyMenager.keys["SpecialAttack"]))
        {
            //Debug.Log("Special Attack kamera w dol");
            if (Point.position.y > player.transform.position.y)
            {
                Point.position = new Vector3(Point.position.x, Point.position.y - Time.deltaTime*offsetSpeed*2);
            }
            else
            {
                Point.position = new Vector3(Point.position.x, player.transform.position.y);
            }
        }
        else
        {
            if (Input.GetKey(keyMenager.keys["Jump"]))
            {
                if (Point.position.y < player.transform.position.y + offset + offset)
                {
                    Point.position = new Vector3(Point.position.x, Point.position.y + Time.deltaTime * offsetSpeed/2);
                }
                else
                {
                    Point.position = new Vector3(Point.position.x, player.transform.position.y + offset + offset);
                }
            }
            else
            {
                if (Input.GetKey(keyMenager.keys["Right"]))
                {
                    if (Point.position.x < player.transform.position.x + offset)
                    {
                        Point.position = new Vector3(Point.position.x + Time.deltaTime * offsetSpeed*2, Point.position.y);
                    }
                    else
                    {
                        Point.position = new Vector3(player.transform.position.x + offset, Point.position.y);
                    }
                }
                else
                {
                    if (Input.GetKey(keyMenager.keys["Left"]))
                    {
                        if (Point.position.x > player.transform.position.x - offset)
                        {
                            Point.position = new Vector3(Point.position.x - Time.deltaTime * offsetSpeed * 2, Point.position.y);
                        }
                        else
                        {
                            Point.position = new Vector3(player.transform.position.x - offset, Point.position.y);
                        }
                    }
                    else
                    {
                        if(Point.position.y!=player.transform.position.y+offset || Point.position.x != player.transform.position.x)
                        {
                            Point.position = Vector3.MoveTowards(Point.position, new Vector3(player.transform.position.x, player.transform.position.y + offset), offsetSpeed *2 * Time.deltaTime);
                        }
                        else
                        {
                            Point.position = new Vector3(player.transform.position.x, player.transform.position.y+offset);
                        }
                    }
                }
            }
        }
    }

    //Funkcja do ustawiania zmiennych przez inne obiekty
	public void ShakeCamera(float shakePwr, float shakeDur)
	{
		shakeAmount = shakePwr;
		shakeTimer = shakeDur;

	}

	public void SetMinCamPosition()
	{
		minCameraPos = gameObject.transform.position;
	}
	public void SetMaxCamPosition()
	{
		maxCameraPos = gameObject.transform.position;

	}
}
