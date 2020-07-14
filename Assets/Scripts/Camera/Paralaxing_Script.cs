using UnityEngine;
using System.Collections;

public class Paralaxing_Script : MonoBehaviour {


	public Transform[] backgrounds;  //tablica wszystkich tylnych i pierwszych planów, które mają być branę pod uwagę paralaxy
	private float[] paralaxScales;  //Czynnik określający ruch kamery
	public float smoothing = 1f;			//""Gładkość paralaxy"

	private Transform cam;				//Odniesienie do kamery
	private Vector3 previousCamPos;    //Pozycja poprzedniej pozycji kamery


	//Wywoływane przed Start(). 
	void Awake()
	{
		//Ustawienie kamery
		cam = Camera.main.transform;
	}

	void Start () 
	{
		// poprzednia klatka =  bieżącej pozycji kamery klatki
		previousCamPos = cam.position;

		// przypisywanie odpowiednich skal paralaksy
		paralaxScales = new float[backgrounds.Length]; //dlugosc tla

		for (int i = 0; i < backgrounds.Length; i++) 
			paralaxScales[i] = backgrounds[i].position.z*1;
	}
	
	void Update () 
	{
		// dla każdego tła
		for (int i = 0; i< backgrounds.Length; i++) {
			// paralaksa jest przeciwieństwem ruchu kamery, ponieważ poprzednia wartość pozycji jest pomnożona przez skalę
			float parallax = (previousCamPos.x - cam.position.x) * paralaxScales[i];

			// ustawia docelową pozycję x, która jest bieżącą pozycją plus wartość paralaksy
			float backgroundTargetPosX = backgrounds[i].position.x + parallax;

			// utwórz pozycję docelową, która jest bieżącą pozycją tła z jego docelową pozycją x
			Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, backgrounds[i].position.y, backgrounds[i].position.z);

			// przenikanie między bieżącą pozycją a pozycją docelową za pomocą lerp  //Vector3.Lerp - Wartość interpolowana, równa a + (b - a) * t.
			backgrounds[i].position = Vector3.Lerp(backgrounds[i].position,backgroundTargetPos, smoothing * Time.deltaTime);
		}

		// ustaw previousCamPos (poprzednią wartość pozycji) w pozycji kamery na końcu klatki
		previousCamPos = cam.position;
	}
}
