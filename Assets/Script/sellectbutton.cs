using UnityEngine;
using System.Collections;

public class sellectbutton : MonoBehaviour {
	[HideInInspector]
	public string go;
	public string mystr = "";
	[HideInInspector]
	public UI uig;
	[HideInInspector]
	public XmlSystem xml;
	public UnityEngine.UI.Text txt;
	public int hp = 0;
	GM gm;

	void Start(){
		uig = GameObject.Find ("Dialog").GetComponent<UI> ();
		xml = GameObject.Find ("xmlmanager").GetComponent<XmlSystem> ();
		gm = GameObject.Find ("GM").GetComponent<GM> ();
	}

	void cl(){
		if (hp <= gm.hp) {
			uig.sellect = false;
			if(hp > 0){
				gm.hp -= hp;
				gm.SendMessage("txtreturn");
			}
			if(mystr != "") xml.remembersellectstr = mystr;
			if (go == null) {
				uig.SendMessage ("conversation", uig.sc);

			} else if (go == "end"){
				uig.SendMessage ("conversation", "none");
			}else if(go == "back"){
				xml.currentstate = "back";
				uig.SendMessage ("conversation", uig.sc);
			}else{
				uig.sc = go;
				xml.cn = 0;
				uig.SendMessage ("conversation", go);
				//uig.backChk = true;
			}
			xml.SendMessage ("delbtt");
		}
	}
}
