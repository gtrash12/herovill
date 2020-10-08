using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Mainstart : MonoBehaviour {
	int[] dayhp = new int[9];
	int[] daygold = new int[9];
	void Awake(){
		dayhp [0] = 4;
		daygold [0] = 0;
		dayhp [1] = 12;
		daygold [1] = 100;
		dayhp [2] = 11;
		daygold [2] = 0;
		dayhp [3] = 12;
		daygold [3] = 0;
		dayhp [4] = 12;
		daygold [4] = 0;
		dayhp [5] = 30;
		daygold [5] = 20;
		dayhp [6] = 70;
		daygold [6] = 0;
		dayhp [7] = 51;
		daygold [7] = 0;
		dayhp [8] = 33;
		daygold [8] = 0;
	}
	void load(){
		PlayerPrefs.SetInt("dayhp",dayhp[GameObject.Find ("MainGM").GetComponent<MainGM> ().sellectedday-1]);
		PlayerPrefs.SetInt("daygold",daygold[GameObject.Find ("MainGM").GetComponent<MainGM> ().sellectedday-1]);
		PlayerPrefs.SetInt ("currentday", GameObject.Find ("MainGM").GetComponent<MainGM> ().sellectedday);
		Application.LoadLevel ("Day");
	}
}
