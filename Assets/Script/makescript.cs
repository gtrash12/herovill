using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class makescript : MonoBehaviour,IPointerDownHandler,IPointerUpHandler,IPointerExitHandler {
	public string itinfo;
	public string itname;
	string infot;
	public List<string> conlist = new List<string>();
	public UnityEngine.UI.Image img;
	public int gold = 10;
	GameObject infobox;
	UnityEngine.UI.Text infotxt;
	// Use this for initialization
	void Start () {
		infobox = GameObject.Find ("GM").GetComponent<GM>().infobox;
		infotxt = infobox.transform.FindChild ("Text").GetComponent<UnityEngine.UI.Text> ();
	}
	public void OnPointerDown(PointerEventData data){
		infobox.SetActive (true);
		infot = "<size=" + 25 * GameObject.Find ("Canvas").transform.localScale.x + "> <color=#ffffffff>" + itname.Replace("_"," ") + "</color> </size> \n" + itinfo + "\n<color=#ff0000> < 필요재료 ></color>";
		for(int i = 0 ; i < conlist.Count; i ++) {
			infot = infot + "\n -" + conlist[i].Replace("_"," ");
		}
		infot = infot + "\n<color=#faed7d> -" + gold +"골드</color>";
		infotxt.text = infot;
		StartCoroutine(resize());
	}
	public void OnPointerUp(PointerEventData data){
		infobox.SetActive(false);
	}
	public void OnPointerExit(PointerEventData data){
		infobox.SetActive(false);
	}
	IEnumerator resize(){
		yield return new WaitForEndOfFrame ();
		infobox.GetComponent<RectTransform> ().sizeDelta = new Vector2 (infotxt.GetComponent<RectTransform> ().sizeDelta.x * infotxt.transform.localScale.x +20, infotxt.GetComponent<RectTransform> ().sizeDelta.y* infotxt.transform.localScale.y + 20f);
	}
	void buy(){
		XmlSystem xml = GameObject.Find ("xmlmanager").GetComponent<XmlSystem> ();
		xml.gold = gold;
		xml.itname = itname;
		xml.cn = 0;
		UI ui = transform.parent.GetComponent<UI> ();
		ui.sc = ui.osc + "makeChk";
		xml.selit.itname = itname;
		xml.selit.itinfo = itinfo;
		xml.conlist = conlist;
		ui.SendMessage ("conversation", ui.sc);
	}
}