using UnityEngine;
using System.Collections;

public class daysellectbutton : MonoBehaviour {
	public int myday;
	public UnityEngine.UI.Text txt;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void click(){
		//Application.LoadLevel ("Day" + myday);
		GameObject.Find ("MainGM").GetComponent<MainGM> ().sellectedday = myday;
		GameObject.Find ("MainGM").SendMessage ("gamestart");
	}
}
