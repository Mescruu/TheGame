using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameMasterMainMenu : MonoBehaviour {

	public Text LevelText1_1;
	public Text LevelText1_2;
	public Text LevelText1_3;
	
	public Text LevelText2_1;

	public int levels;

	// Update is called once per frame
	void Update () {

		levels = PlayerPrefs.GetInt ("HighLeveL") - 1;

						if (levels >= 1) {
								LevelText1_1.text = ("Level 1");

						} else {
								LevelText1_1.text = ("BLOKED");
						}

						if (levels >= 2) {

								LevelText1_2.text = ("Level 2");
						} else {
								LevelText1_2.text = ("BLOKED");
						}
						if (levels >= 3) {

								LevelText1_3.text = ("Level 3");
						} else {
								LevelText1_3.text = ("BLOKED");
						}


						if (levels >= 4) {

								LevelText2_1.text = ("BOSS");
						} else {
								LevelText2_1.text = ("BLOKED");
						}
				
	}
}
