using UnityEngine;
using System.Collections;

public class replaybttscript : MonoBehaviour {
	public XmlSystem gm;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void load(){
		gm.showad ();
		Application.LoadLevel ("Day");
	}
}
