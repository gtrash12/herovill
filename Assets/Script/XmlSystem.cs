using UnityEngine;
using System.Collections;
using System.Xml;
using System.IO;
using System.Collections.Generic;

public class XmlSystem : MonoBehaviour {
	public XmlNodeList Node1;
	public XmlNodeList Node2;
	public UnityAdsHelper admanager;
	public AdmobInstitial admobmanager;
	XmlNodeList remembernode;
	XmlNode itnodelist;
	XmlNode itemNode;
	public string remembersellectstr;
	public string currentsay = "";
	XmlDocument xmldoc;
	XmlDocument itemxmldoc;
	public UI ui;
	public int cn = 0;
	int rcn = 0;
	public UnityEngine.UI.Button button;
	public UnityEngine.UI.Button itembtn;
	public ArrayList libbtt = new ArrayList();
	public UnityEngine.UI.Button hpicon;
	[HideInInspector]
	public int gold;
	[HideInInspector]
	public string itname;
	//[HideInInspector]
	public iteminfocontainer selit;
	public GM gm;
	[HideInInspector]
	bool needchk = false;
	public List<string> conlist = new List<string>();
	public GameObject hpbox;
	public string currentstate = "nomal";
	List<int> conum = new List<int>();

	void Awake () {
		selit = GetComponent<iteminfocontainer> ();
		TextAsset textAsset = (TextAsset)Resources.Load("conversation"); 
		xmldoc = new XmlDocument();  // 객체선언
		xmldoc.LoadXml(textAsset.text);  // 텍스트 불러옴
		textAsset = (TextAsset)Resources.Load("item");
		itemxmldoc = new XmlDocument ();
		itemxmldoc.LoadXml (textAsset.text);
		/*
		Node1 = xmldoc.SelectNodes("/DialogueSet/tutorial");  // 노드선택
		Node2 = xmldoc.SelectNodes("/DialogueSet/tutorial/say");

		foreach(XmlNode Node in Node1){ //검색
			foreach(XmlNode node in Node2){
				Debug.Log(node.Attributes.GetNamedItem("name").Value + " : " + node.InnerText);
				currentsay = node.Attributes.GetNamedItem("name").Value + " : " + node.InnerText;
			}
		}
		*/
	}

	public void itemSet(string itn){
		//itemNode = itemxmldoc.SelectNodes ("/item/" + itn);
	}

	public void showad(){
		if(admanager.isActiveAndEnabled){
			if(PlayerPrefs.GetInt("adn") == 1){
				if(admanager.ShowTestAds() == false){
					if (admobmanager.interstitial.IsLoaded ()) {
						admobmanager.interstitial.Show ();
					}else{
						PlayerPrefs.SetInt("adn",2);
					}
					//admanager.ShowTestAds();
					//Debug.Log(false);
				}
			}else{
				if (admobmanager.interstitial.IsLoaded ()) {
					admobmanager.interstitial.Show ();
					PlayerPrefs.SetInt("adn",1);
				}else{
					admanager.ShowTestAds();
				}
			}
		}
	}
	public IEnumerator outputdia(string scene){
		Node1 = xmldoc.SelectNodes ("/DialogueSet/day"+gm.day+"/"+scene+"/say");
		conum.Clear ();
		if (currentstate == "back") {
			Node1 = remembernode;
			cn =rcn;
			ui.sc = remembernode[rcn].ParentNode.Name;
			currentstate = "nomal";
			yield return new WaitForChangedResult();
		}
		if(cn >= Node1.Count){
			if(currentstate == "fail")Application.LoadLevel("Main");
			if(currentstate == "win"){
				if(PlayerPrefs.GetInt("clearday") == gm.day){
					PlayerPrefs.SetInt("clearday",gm.day +1);
					PlayerPrefs.Save();
				}
				Application.LoadLevel("Main");
			}
			if(currentstate == "ending"){
				PlayerPrefs.SetInt("clearday",10);
				PlayerPrefs.Save();
				Application.LoadLevel("end");
			}
			currentsay = null;
			cn = 0;
			yield break;
		}
		if (Node1 [cn].Attributes.GetNamedItem ("ad") != null) {
			showad();
		}
		if (Node1[cn].Attributes.GetNamedItem ("state") != null)
			currentstate = Node1[cn].Attributes.GetNamedItem ("state").Value;
		if (Node1[cn].Attributes.GetNamedItem ("remember") != null){
			remembernode = Node1;
			rcn = cn;
		}
		if (Node1[cn].Attributes.GetNamedItem ("back") != null){
			Node1 = remembernode;
			cn = rcn;
		}
		if (Node1[cn].Attributes.GetNamedItem("con") != null) {
			needchk = true;
			string uigoto = ui.sc + Node1[cn].SelectSingleNode("con").SelectSingleNode("go").InnerText;
			if(Node1[cn].SelectSingleNode("con").SelectSingleNode("go").InnerText == "end"){
				uigoto = "end";
			}
			Node2 = Node1[cn].Clone().SelectSingleNode("con").SelectNodes("ele");
			if(conlist.Count >0){
				foreach(var con in conlist){
					XmlNode it = Node2[0].Clone();
					it.InnerText = con;
					Node2[0].ParentNode.AppendChild(it);
				}
				conlist.Clear();
			}
			for(int i2 = 0; i2 < Node2.Count; i2 ++){
				if(Node2[i2].InnerText == "gold"){
					if(gm.gold < gold){
						ui.sc = uigoto;
						cn = 0;
						Node1 = xmldoc.SelectNodes ("/DialogueSet/day"+gm.day+"/"+ui.sc+"/say");
						needchk = false;
						break;
					}
					//yield break;
				}else if(Node2[i2].Attributes.GetNamedItem("gold") != null){
					if(gm.gold < System.Convert.ToInt32(Node2[i2].InnerText)){
						ui.sc = uigoto;
						cn = 0;
						Node1 = xmldoc.SelectNodes ("/DialogueSet/day"+gm.day+"/"+ui.sc+"/say");
						needchk = false;
						break;
					}
				}else{
					if(gm.itemlist.Count == 0){
						ui.sc = uigoto;
						cn = 0;
						Node1 = xmldoc.SelectNodes ("/DialogueSet/day"+gm.day+"/"+ui.sc+"/say");
						needchk = false;
						break;
					}
					for(var i = gm.itemlist.Count-1; i >= 0; i --){
						if(Node2[i2].InnerText == gm.itemlist[i].GetComponent<itemslotscript>().itname && conum.IndexOf(i) == -1){
							conum.Add(i);
							break;
						}
						if( i <= 0){
							ui.sc = uigoto;
							cn = 0;
							Node1 = xmldoc.SelectNodes ("/DialogueSet/day"+gm.day+"/"+ui.sc+"/say");
							needchk = false;
							Debug.Log("DFDF");
							break;
						}
					}
				}
			}
		}
		////////////임시
		if(cn >= Node1.Count){
			if(currentstate == "fail")Application.LoadLevel("fail");
			if(currentstate == "win")Application.LoadLevel("win");
			currentsay = null;
			cn = 0;
			yield break;
		}
		//////////////////////
		if (needchk == true) {
			for(int i2 = 0; i2 < Node2.Count; i2 ++){
				if(Node2[i2].InnerText == "gold"){
					gm.gold -= gold;
					gm.SendMessage("txtreturn");
				}else if(Node2[i2].Attributes.GetNamedItem("gold") != null){
					gm.gold -= System.Convert.ToInt32(Node2[i2].InnerText);
					gm.SendMessage("txtreturn");
				}else if(Node2[i2].Attributes.GetNamedItem("stay") != null) {

				}else{//if (gm.itemlist.IndexOf(Node3[i2].InnerText) == -1){
					for(var i = gm.itemlist.Count-1; i >= 0; i --){
						if(Node2[i2].InnerText == gm.itemlist[i].GetComponent<itemslotscript>().itname){
							gm.SendMessage("delete",i);
							break;
						}
					}
				}
			}
				needchk = false;
		}
		if (Node1 [cn].Attributes.GetNamedItem ("name") == null) {
			currentsay = Node1 [cn].FirstChild.InnerText;
		} else {
			currentsay = "<size=" + 30 * ui.transform.parent.localScale.x + "> <color=#ffffffff> < " + Node1 [cn].Attributes.GetNamedItem ("name").Value + " > </color> </size> \n" + Node1 [cn].FirstChild.InnerText;
		}
		currentsay = currentsay.Replace("/gold","<color=#faed7d>" + gold.ToString() + "</color>");
		currentsay = currentsay.Replace("/name","<color=#ffffff>" + itname + "</color>");
		ui.textsize.text = currentsay;
		currentsay = currentsay.Replace("_"," ");
		ui.textsize.text = currentsay;
		if (Node1[cn].Attributes.GetNamedItem("name") == null ||(Node1[cn].Attributes.GetNamedItem("name")!= null &&Resources.Load("npc/"+Node1[cn].Attributes.GetNamedItem("name").Value) == null)) {
			ui.ima.gameObject.SetActive (false);
		} else {
			ui.ima.gameObject.SetActive (true);
			ui.ima.sprite = Resources.Load<Sprite>("npc/"+Node1[cn].Attributes.GetNamedItem("name").Value);
		}
		yield return new WaitForEndOfFrame();
		ui.GetComponent<RectTransform> ().sizeDelta = new Vector2 (320f, ui.textsize.rectTransform.sizeDelta.y*ui.textsize.transform.localScale.y+ 20);
		//yield return new WaitForChangedResult();
		if (Node1 [cn].Attributes.GetNamedItem ("type") != null) {
			if (Node1 [cn].Attributes.GetNamedItem ("type").Value == "sellect") {
				Node2 = Node1[cn].SelectNodes("sellect");
				yield return new WaitForChangedResult ();
				float totalwidth = 0;
				int selline = 0;
				ui.bsc = ui.sc;
				ui.selcn = cn;
				for (int i = 0; i < Node2.Count; i ++) {
					while(Node2[i].SelectSingleNode("con") != null){
						bool slneedchk = true;
						for(int i2 = 0; i2 < Node2[i].SelectSingleNode("con").ChildNodes.Count; i2 ++){
							if(gm.itemlist.Count == 0){ 
								slneedchk = false; 
								break;
						}else{
							for(int i3 = gm.itemlist.Count-1; i3 >= 0; i3 --){
									if(Node2[i].SelectSingleNode("con").ChildNodes[i2].InnerText == gm.itemlist[i3].GetComponent<itemslotscript>().itname && conum.IndexOf(i3) == -1){
										conum.Add(i3);
									break;
								}
								if( i3 <= 0){
									slneedchk = false;
									break;
								}
							}
						}
						}
						conum.Clear();
						if(!slneedchk){
							i ++;
							if(i >= Node2.Count) break;
						}else{
							break;
						}
					}
					if(i >= Node2.Count) break;
					UnityEngine.UI.Button selnode;
					if (Node2 [i].Attributes.GetNamedItem ("hp") == null) {
						selnode = Instantiate (button, Vector3.zero, Quaternion.identity) as UnityEngine.UI.Button;
					} else {
						selnode = Instantiate (hpicon, Vector3.zero, Quaternion.identity) as UnityEngine.UI.Button;
						selnode.transform.FindChild ("hpimg").transform.FindChild ("hpt").GetComponent<UnityEngine.UI.Text> ().text = Node2 [i].Attributes.GetNamedItem ("hp").Value;
						selnode.GetComponent<sellectbutton> ().hp = System.Convert.ToInt32 (Node2 [i].Attributes.GetNamedItem ("hp").Value);
					}
					selnode.transform.SetParent (ui.transform);
					selnode.GetComponent<sellectbutton> ().txt.text = Node2 [i].FirstChild.InnerText;
					yield return new WaitForChangedResult ();
					selnode.GetComponent<RectTransform> ().localScale = (ui.transform.parent.localScale * 0.05f + new Vector3 (0.95f, 0.95f, 0.95f));
					//selnode.transform.localScale = Vector3.one * Screen.height/540;
					selnode.GetComponent<RectTransform> ().sizeDelta = new Vector2 (selnode.GetComponent<sellectbutton> ().txt.rectTransform.sizeDelta.x * selnode.GetComponent<sellectbutton> ().txt.transform.localScale.x + 20
				                                                                , selnode.GetComponent<sellectbutton> ().txt.rectTransform.sizeDelta.y * selnode.GetComponent<sellectbutton> ().txt.transform.localScale.x + 15);
					if (selnode.GetComponent<RectTransform> ().sizeDelta.x < 50)
						selnode.GetComponent<RectTransform> ().sizeDelta = new Vector2 (50f, selnode.GetComponent<RectTransform> ().sizeDelta.y);
					totalwidth += selnode.GetComponent<RectTransform> ().sizeDelta.x * selnode.transform.localScale.x;
					if(totalwidth > ui.GetComponent<RectTransform> ().sizeDelta.x){
						totalwidth = selnode.GetComponent<RectTransform> ().sizeDelta.x * selnode.transform.localScale.x;
						selline ++;
					}
					selnode.GetComponent<RectTransform> ().localPosition = new Vector3 (totalwidth - selnode.GetComponent<RectTransform> ().sizeDelta.x * selnode.transform.localScale.x, -ui.GetComponent<RectTransform> ().sizeDelta.y - selnode.GetComponent<RectTransform> ().sizeDelta.y * selline * selnode.transform.localScale.y, 0);
						//selnode.GetComponent<RectTransform> ().localPosition = new Vector3 (ui.GetComponent<RectTransform> ().sizeDelta.x / 2f, -ui.GetComponent<RectTransform> ().sizeDelta.y - selnode.GetComponent<RectTransform> ().sizeDelta.y * i * selnode.transform.localScale.x, 0);
					//Debug.Log(transform.parent.name);
					if(Node2[i].Attributes.GetNamedItem ("remove") != null)selnode.GetComponent<sellectbutton> ().mystr = Node2[i].InnerText;
					if (Node2 [i].Attributes.GetNamedItem ("go") == null) {
						selnode.GetComponent<sellectbutton> ().go = null;
					} else if (Node2 [i].Attributes.GetNamedItem ("go").Value == "end"){
						selnode.GetComponent<sellectbutton> ().go = "end";
					}else if(Node2 [i].Attributes.GetNamedItem ("go").Value == "back"){
						selnode.GetComponent<sellectbutton> ().go = "back";
					}else if(Node2 [i].Attributes.GetNamedItem ("osc") == null){
						selnode.GetComponent<sellectbutton> ().go = ui.sc + Node2 [i].Attributes.GetNamedItem ("go").Value;
					}else{
						selnode.GetComponent<sellectbutton> ().go = ui.osc + Node2 [i].Attributes.GetNamedItem ("go").Value;
					}
					libbtt.Add (selnode);
				}
				ui.sellect = true;
			}
			if (Node1 [cn].Attributes.GetNamedItem ("type").Value == "sell") {
				Node2 = Node1[cn].SelectNodes("sell");
				if(Node2.Count > 0)itnodelist = Node2 [0].ParentNode;
				for (int i = 0; i < Node2.Count; i ++) {
					itemNode = itemxmldoc.SelectSingleNode ("/item/" + Node2 [i].InnerText);
					yield return new WaitForChangedResult ();
					var sellnode = Instantiate (itembtn, Vector3.zero, Quaternion.identity) as UnityEngine.UI.Button;
					sellnode.gameObject.AddComponent<sell>();
					sellnode.transform.SetParent (ui.transform);
					sellnode.GetComponent<RectTransform> ().localScale = ui.transform.parent.localScale * 0.05f + new Vector3 (0.95f, 0.95f, 0.95f);
					sellnode.GetComponent<RectTransform> ().localPosition = new Vector3 ((i - 6*System.Convert.ToInt32(i/6)) * sellnode.GetComponent<RectTransform> ().sizeDelta.x
					                                                                     , -ui.GetComponent<RectTransform> ().sizeDelta.y - System.Convert.ToInt32(i/6)*sellnode.GetComponent<RectTransform> ().sizeDelta.y, 0);
					sellnode.transform.FindChild ("Image").GetComponent<UnityEngine.UI.Image> ().sprite = Resources.Load<Sprite> ("item/"+Node2 [i].InnerText);
					sellnode.GetComponent<sell> ().itinfo = itemNode.FirstChild.InnerText;
					sellnode.GetComponent<sell> ().itname = itemNode.Name;
					sellnode.GetComponent<sell> ().gold = System.Convert.ToInt32 (Node2 [i].Attributes.GetNamedItem ("cost").Value);
					libbtt.Add (sellnode);
				}
			}
			if (Node1 [cn].Attributes.GetNamedItem ("type").Value == "make") {
				Node2 = Node1[cn].SelectNodes("make");
				itnodelist = Node2 [0].ParentNode;
				for (int i = 0; i < Node2.Count; i ++) {
					itemNode = itemxmldoc.SelectSingleNode ("/item/" + Node2 [i].Attributes.GetNamedItem("name").Value);
					yield return new WaitForChangedResult ();
					var sellnode = Instantiate (itembtn, Vector3.zero, Quaternion.identity) as UnityEngine.UI.Button;
					sellnode.gameObject.AddComponent<makescript>();
					sellnode.transform.SetParent (ui.transform);
					sellnode.GetComponent<RectTransform> ().localScale = ui.transform.parent.localScale * 0.05f + new Vector3 (0.95f, 0.95f, 0.95f);
					sellnode.GetComponent<RectTransform> ().localPosition = new Vector3 ((i - 6*System.Convert.ToInt32(i/6)) * sellnode.GetComponent<RectTransform> ().sizeDelta.x
					                                                                     , -ui.GetComponent<RectTransform> ().sizeDelta.y - System.Convert.ToInt32(i/6)*sellnode.GetComponent<RectTransform> ().sizeDelta.y, 0);
					sellnode.transform.FindChild ("Image").GetComponent<UnityEngine.UI.Image> ().sprite = Resources.Load<Sprite> ("item/"+Node2 [i].Attributes.GetNamedItem("name").Value);
					sellnode.GetComponent<makescript> ().itinfo = itemNode.FirstChild.InnerText;
					sellnode.GetComponent<makescript> ().itname = itemNode.Name;
					sellnode.GetComponent<makescript> ().gold = System.Convert.ToInt32 (Node2 [i].Attributes.GetNamedItem ("cost").Value);
					XmlNodeList Node3 = Node2[i].SelectNodes("con");
					foreach(XmlNode node in Node3){
						sellnode.GetComponent<makescript> ().conlist.Add(node.InnerText);
					}
					libbtt.Add (sellnode);
				}
			}
			if (Node1 [cn].Attributes.GetNamedItem ("type").Value == "buy") {
				itemNode = itemxmldoc.SelectSingleNode ("/item/"+selit.itname);
				yield return new WaitForChangedResult();
				itset ();
				gm.SendMessage ("get", selit);
				Node2 = itnodelist.SelectNodes ("sell");
				foreach (XmlNode node in Node2) {
					if (node.InnerText == selit.itname) {
						node.ParentNode.RemoveChild (node);
					}
				}
			}
			if (Node1 [cn].Attributes.GetNamedItem ("type").Value == "give") {
				Node2 = Node1[cn].SelectNodes("give");
				foreach(XmlNode node in Node2){
					if(node.Attributes.GetNamedItem("gold") != null){
						gm.gold += System.Convert.ToInt32(node.InnerText);
						gm.SendMessage("txtreturn");
						currentsay = currentsay.Replace("/ggold","<color=#faed7d>" + node.InnerText + "골드</color>");
						ui.textsize.text = currentsay;
					}else if(node.Attributes.GetNamedItem("pow") != null){
						gm.pow += System.Convert.ToInt32(node.InnerText);
						gm.SendMessage("txtreturn");
						currentsay = currentsay.Replace("/gpow","<color=#ffffff>" + node.InnerText + "</color>");
						ui.textsize.text = currentsay;
					}else{
						itemNode = itemxmldoc.SelectSingleNode ("/item/" + node.InnerText);
						selit.itname = node.InnerText;
						itset ();
						currentsay = currentsay.Replace("/gname","<color=#ffffff>" + selit.itname + "</color>");
						ui.textsize.text = currentsay;
						gm.SendMessage ("get", selit);
					}
				}
			}
			if (Node1 [cn].Attributes.GetNamedItem ("type").Value == "battle") {
				ui.sellect = true;
				gm.enemymaxhp = System.Convert.ToInt32(Node1[cn].Attributes.GetNamedItem("hp").Value);
				gm.enemyhp = gm.enemymaxhp;
				gm.weak = Node1[cn].Attributes.GetNamedItem("weak").Value;
				gm.hard = Node1[cn].Attributes.GetNamedItem("hard").Value;
				hpbox.SetActive(true);
				gm.invenanim.SetBool("invenchk",true);
				gm.SendMessage("ready");

			}
		}
		if (Node1[cn].SelectSingleNode ("go") != null) {
			if(Node1[cn].SelectSingleNode("go").InnerText == "back"){
				ui.backChk = true;
			}else{
				ui.sc = ui.osc + Node1 [cn].SelectSingleNode("go").InnerText;
				cn = 0;
			yield break;
			}
		}
		if (Node1 [cn].Attributes.GetNamedItem ("phase") != null) {
			gm.currentspot.phase = Node1[cn].Attributes.GetNamedItem ("phase").Value;
		}
		if (Node1[cn].Attributes.GetNamedItem ("remove") != null){
			removesellect(remembersellectstr);
		}
		cn ++;
		yield break;
	}

	void itset(){
		selit.itname = itemNode.Name;
		selit.itinfo = itemNode.InnerText;
		selit.type = itemNode.Attributes.GetNamedItem ("type").Value;
		selit.element = itemNode.Attributes.GetNamedItem ("element").Value;
		selit.pow = System.Convert.ToInt32 (itemNode.Attributes.GetNamedItem ("pow").Value);
	}
	void removesellect(string str){
		foreach (XmlNode node in remembernode[rcn].SelectNodes("sellect")) {
			if (node.InnerText == str) {
				node.ParentNode.RemoveChild (node);
				return;
			}
		}
	}
	void delbtt(){
		foreach (UnityEngine.UI.Button node in libbtt) {
			Destroy(node.gameObject);
		}
		libbtt.Clear ();
	}
}
