using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GM : MonoBehaviour {
	public List<GameObject> itemlist = new List<GameObject>();
	public int gold = 0;
	public int hp = 2;
	public int pow = 0;
	//[HideInInspector]
	public int day = 1;
	[HideInInspector]
	public int enemyhp;
	[HideInInspector]
	public int enemymaxhp = 20;
	[HideInInspector]
	public string weak;
	[HideInInspector]
	public string hard;
	public UnityEngine.UI.Text daytext;
	public UnityEngine.UI.Text goldtext;
	public UnityEngine.UI.Text hptext;
	public UnityEngine.UI.Text powtext;
	public GameObject infobox;
	public GameObject scrollrange;
	public GameObject itemslot;
	public GameObject uicanvas;
	public Animator invenanim;
	public Action currentspot;
	public UnityEngine.UI.Image ehpbar;
	public UnityEngine.UI.Text ehpt;
	public AudioClip atk1;
	public AudioClip atk2;
	// Use this for initialization
	void Start () {
		enemyhp = enemymaxhp;
		txtreturn ();
		daytext.text = "Day - " + day;
	}
	void Awake(){
		day = PlayerPrefs.GetInt ("currentday");
		hp = PlayerPrefs.GetInt ("dayhp");
		gold = PlayerPrefs.GetInt ("daygold");
	}
	// Update is called once per frame
	void Update () {
	
	}
	void txtreturn(){
		hptext.text = hp.ToString();
		goldtext.text = gold.ToString();
		powtext.text = pow.ToString();
	}
	void get(iteminfocontainer data){
		var it = Instantiate (itemslot, Vector3.zero, Quaternion.identity) as GameObject;
		it.transform.SetParent(scrollrange.transform);
		it.transform.SetAsFirstSibling ();
		it.GetComponent<RectTransform>().localScale = uicanvas.transform.localScale*0.05f + new Vector3(0.95f,0.95f,0.95f);
		it.GetComponent<RectTransform>().localPosition = new Vector3(itemlist.Count * it.GetComponent<RectTransform>().sizeDelta.x, 0, 0);
		it.transform.FindChild("Image").GetComponent<UnityEngine.UI.Image>().sprite = Resources.Load<Sprite>("item/"+data.itname);
		it.GetComponent<itemslotscript> ().itinfo = data.itinfo;
		it.GetComponent<itemslotscript>().itname = data.itname;
		it.GetComponent<itemslotscript> ().type = data.type;
		it.GetComponent<itemslotscript>().pow = data.pow;
		it.GetComponent<itemslotscript>().element = data.element;
		itemlist.Add (it);
		scrollrange.GetComponent<RectTransform> ().sizeDelta = new Vector2 (itemlist.Count * it.GetComponent<RectTransform> ().sizeDelta.x, scrollrange.GetComponent<RectTransform> ().sizeDelta.y);
		invenanim.SetBool ("invenchk", true);
	}
	void invenclick(){
		invenanim.SetBool ("invenchk", !invenanim.GetBool ("invenchk"));
	}
	void delete(int index){
		Destroy (itemlist[index]);
		itemlist.RemoveAt (index);
		for (var i = index; i < itemlist.Count; i++) {
			itemlist[i].transform.localPosition = new Vector3(i*itemlist[i].GetComponent<RectTransform>().sizeDelta.x,0,0);
		}
		scrollrange.GetComponent<RectTransform> ().sizeDelta = new Vector2 (itemlist.Count * 50, scrollrange.GetComponent<RectTransform> ().sizeDelta.y);
	}
	void attack(){
		itemlist [0].GetComponent<Animator> ().SetBool ("atkChk", true);
	}
	IEnumerator ready(){
		ehpt.text = enemyhp + " / " + enemymaxhp;
		ehpbar.fillAmount = (float) enemyhp / enemymaxhp;
		yield return new WaitForSeconds (1);
		if (enemyhp <= 0) {
			UI ui = GameObject.Find ("Dialog").GetComponent<UI> ();
			ui.xml.hpbox.SetActive(false);
			ui.sc = ui.sc + "win";
			ui.xml.cn = 0;
			ui.sellect = false;
			ui.SendMessage ("conversation", ui.sc);

		} else {
			if (itemlist.Count > 0) {
				GetComponent<AudioSource>().clip = atk1;
				GetComponent<AudioSource>().Play();
				attack ();
			} else {
				UI ui = GameObject.Find ("Dialog").GetComponent<UI> ();
				ui.sc = ui.sc + "fail";
				ui.xml.hpbox.SetActive(false);
				ui.xml.cn = 0;
				ui.sellect = false;
				ui.SendMessage ("conversation", ui.sc);
				//Application.LoadLevel("fail");
			}
		}
	}
}
