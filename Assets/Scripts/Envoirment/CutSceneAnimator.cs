using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneAnimator : MonoBehaviour {

    public bool setActive;
    public GameObject Obj;

	// Use this for initialization
	
	// Update is called once per frame
	void Update () {
        if (setActive)
        {
            Obj.SetActive(true);

        }
        else
        {
            Obj.SetActive(false);
        }
	}
}
