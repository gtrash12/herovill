using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class UI : MonoBehaviour,IPointerClickHandler {
	public PlayerScript Cha;
	public XmlSystem xml;
	public string sc = "tutorial";
	public UnityEngine.UI.Image ima;
	public Sprite npcface;
	[HideInInspector]
	public bool backChk = false;
	[HideInInspector]
	public bool sellect = false;
	[HideInInspector]
	public int selcn = 0;
	[HideInInspector]
	public string bsc;
	[HideInInspector]
	public string osc;
	[HideInInspector]
	public string imgname;
	public UnityEngine.UI.Text textsize;

	void Start(){
		textsize.text = "시작";
		StartCoroutine (conversation (sc));
	}

	void activeChk(){
		gameObject.SetActive (false);
		Cha.UIChk = false;	
		ima.gameObject.SetActive(false);
	}
	void enableChk(){
		this.enabled = true;
	}
	public void OnPointerClick(PointerEventData data){
		//gameObject.SetActive (false);

		if(!sellect) StartCoroutine (conversation (sc));
	}
	public IEnumerator conversation(string scene){
		if(xml.libbtt.Count > 0) xml.SendMessage ("delbtt");

		/*
		if (npcface != null) {
			ima.sprite = npcface;
		} else {
			ima.gameObject.SetActive(false);
		}
		*/
		yield return StartCoroutine(xml.outputdia(scene));
		if(xml.currentsay == null){
			if(backChk == false){
			GetComponent<Animator>().Play("diaexit");
			this.enabled = false;
			yield break;
			}else{
				sc = bsc;
				xml.cn = selcn;
				backChk = false;
				StartCoroutine (conversation (sc));
				yield return new WaitForEndOfFrame();
			}
		}
		//textsize.text = xml.currentsay;
		//yield return new WaitForEndOfFrame();
		//GetComponent<RectTransform> ().sizeDelta = new Vector2 (320f, textsize.rectTransform.sizeDelta.y);
	}
}
