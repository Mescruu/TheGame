using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneAnimator : MonoBehaviour {

    public bool setActive;
    public GameObject Obj;
    //skrypt odpowiadjący za aktywację i dezaktywację obiektu
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
