using UnityEngine;
using System.Collections;

public class Paralaxing_Script : MonoBehaviour {


	public Transform[] backgrounds;  //array list of all the back and foregrounds to be parallaxed
	private float[] paralaxScales;  //propose for the cameras movement to move this backgrounds
	public float smoothing = 1f;			//smooth to paralax

	private Transform cam;				//reference to main camera transform
	private Vector3 previousCamPos;    //the position of the camera in the previouse frame


	//is called before Start(). 
	void Awake()
	{
		//set up camera the reference
		cam = Camera.main.transform;

	}

	// Use this for initialization

	void Start () 
	{
	//the previouse frame had the current frame's camera position
		previousCamPos = cam.position;

		//asiging coresponigng parallaxScales
		paralaxScales = new float[backgrounds.Length]; //dlugosc tla

		for (int i = 0; i < backgrounds.Length; i++) 
				{

			paralaxScales[i] = backgrounds[i].position.z*1;

				}
	}
	
	// Update is called once per frame
	void Update () 
	{

		//for each background
		for (int i = 0; i< backgrounds.Length; i++) {
			//the parallax is the opposite of the camera movement because the previous frame multiplied by the scale
			float parallax = (previousCamPos.x - cam.position.x) * paralaxScales[i];

			//set a target x position which is the current position plus the parallax
			float backgroundTargetPosX = backgrounds[i].position.x + parallax;

			//create a target position which is the background's current position with it's target x position
			Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, backgrounds[i].position.y, backgrounds[i].position.z);
			//fade between current position and the target position using lerp
			backgrounds[i].position = Vector3.Lerp(backgrounds[i].position,backgroundTargetPos, smoothing * Time.deltaTime);
				}
		//set the previousCamPos to the camera's position at the end of the frame
		previousCamPos = cam.position;
	
	}
}
