using UnityEngine;
using System.Collections;

public class MainGM : MonoBehaviour {
	public int clearday = 1;
	public int sellectedday;
	public GameObject sellectbar;
	public GameObject button;
	public Animator uianim;
	public Animator cameraanim;
	public Admob admob;
	public UnityEngine.UI.Image bg;
	public Sprite clearbg;
	// Use this for initialization
	void Awake(){
		//if (PlayerPrefs.GetInt ("clearday") < 5)PlayerPrefs.SetInt ("clearday", 9);
		if (PlayerPrefs.GetInt ("clearday") < 1) {
			PlayerPrefs.SetInt("clearday",1);
		}
		clearday = PlayerPrefs.GetInt ("clearday");
		if (admob.isActiveAndEnabled) admob.Show ();
	}
	void Start () {
		if (PlayerPrefs.GetInt ("clearday") >= 10) {
			bg.sprite = clearbg;
		}
		for (int i = clearday - 1; i >= 0; i--) {
			if(i < 9){
			var btn = Instantiate(button, Vector3.zero, Quaternion.identity) as GameObject;
			btn.SetActive(true);
			btn.transform.SetParent(sellectbar.transform);
			btn.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -i * btn.GetComponent<RectTransform>().sizeDelta.y);
			btn.transform.localScale = Vector3.one;
			btn.GetComponent<daysellectbutton>().myday = i + 1;
			btn.GetComponent<daysellectbutton>().txt.text = "Day - " + btn.GetComponent<daysellectbutton>().myday;
			btn.transform.localRotation = new Quaternion( 0, 0, 0, 1);
			}
		}
		sellectbar.GetComponent<RectTransform> ().sizeDelta = new Vector3 (sellectbar.GetComponent<RectTransform> ().sizeDelta.x, clearday * button.GetComponent<RectTransform> ().sizeDelta.y);
	}
	void gamestart(){
		uianim.SetBool ("start", true);
		cameraanim.SetBool ("start", true);
		if (admob.isActiveAndEnabled) admob.Hide ();
	}
}
