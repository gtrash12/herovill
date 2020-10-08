using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class itemslotscript : MonoBehaviour,IPointerDownHandler,IPointerUpHandler,IPointerExitHandler {
	public string itinfo;
	public string itname;
	public string type;
	public int pow;
	public string element;
	public UnityEngine.UI.Image img;
	public int gold = 10;
	GameObject infobox;
	GM gm;
	UnityEngine.UI.Text infotxt;
	// Use this for initialization
	void Start () {
		infobox = GameObject.Find ("GM").GetComponent<GM>().infobox;
		infotxt = infobox.transform.FindChild ("Text").GetComponent<UnityEngine.UI.Text> ();
		gm = GameObject.Find ("GM").GetComponent<GM> ();
	}
	public void OnPointerDown(PointerEventData data){
		infobox.SetActive (true);
		infotxt.text = "<size=" + 25 * GameObject.Find ("Canvas").transform.localScale.x + "> <color=#ffffffff>" + itname.Replace("_"," ") + "</color> </size> \n" + itinfo;
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
	void delete(){
		gm.GetComponent<AudioSource> ().clip = gm.atk2;
			gm.GetComponent<AudioSource>().Play();
		if (gm.weak == "풀") {
			if (gm.weak == element) {
				gm.enemyhp -= pow*2 + gm.pow;
			}
		} else if (gm.weak == element) {
			gm.enemyhp -= pow*2 + gm.pow;
		} else if (gm.hard == element) {
			gm.enemyhp -= System.Convert.ToInt32(pow*0.5f) + gm.pow;
		} else {
			gm.enemyhp -= pow + gm.pow;
		}
		gm.ehpbar.GetComponentInParent<Animator> ().Play ("hit");
		GameObject.Find ("npcface").GetComponent<Animator> ().Play ("hit");
		GameObject.Find ("Dialog").GetComponent<Animator> ().Play ("diahit");
		gm.StartCoroutine("ready");
		gm.SendMessage("delete", System.Convert.ToInt32(0));
	}
}
