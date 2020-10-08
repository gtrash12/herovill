using UnityEngine;
using System.Collections;

public class Action : MonoBehaviour {
	GameObject Dia;
	public string spot = "library";
	public string phase = "";
	GM gm;

	// Use this for initialization
	void Start () {
		Dia = GameObject.Find("Dialog");
		gm = GameObject.Find ("GM").GetComponent<GM> ();
		if (spot == "실비아") {
			if(gm.day <= 4 || gm.day == 9){
				gameObject.SetActive(false);
			}
		}
		if (spot == "area") {
			if(gm.day <= 5){
				gameObject.SetActive(false);
			}
		}
	}

	void OnTriggerEnter(Collider other){
		if (other.transform.position.z <= transform.position.z) {
			string sct = phase + spot;
			gm.currentspot = this;
			Dia.SetActive (true);
			other.GetComponent<PlayerScript> ().UIChk = true;
			Dia.GetComponent<UI> ().sc = sct;
			Dia.GetComponent<UI> ().osc = sct;
			Dia.GetComponent<UI> ().ima.gameObject.SetActive (true);
			Dia.SendMessage ("conversation", sct);
			other.GetComponent<PlayerScript> ().hit.point = other.transform.position;
			other.GetComponent<PlayerScript> ().anim.SetBool ("walkChk", false);
			other.GetComponent<PlayerScript> ().pointImg.transform.position = new Vector3 (
			other.transform.position.x, other.transform.position.y - 1.5f, other.transform.position.z);
		}
	}
}
