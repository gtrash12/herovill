using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class sell : MonoBehaviour,IPointerDownHandler,IPointerUpHandler,IPointerExitHandler {
	public string itinfo;
	public string itname;
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
		infotxt.text = "<size=" + 25 * GameObject.Find ("Canvas").transform.localScale.x + "> <color=#ffffffff>" + itname.Replace("_"," ") + "</color> </size> \n" + itinfo +"\n\n가격\n<color=#faed7d>"+gold+"gold</color>";
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
		ui.sc = ui.osc + "buyChk";
		xml.selit.itname = itname;
		xml.selit.itinfo = itinfo;
		ui.SendMessage ("conversation", ui.sc);
	}
}
