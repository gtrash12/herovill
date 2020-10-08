using UnityEngine;
using System.Collections;

public class infoscript : MonoBehaviour {
	public UnityEngine.UI.Text txt;
	public GameObject canvas;
	// Use this for initialization
	void Start () {
		canvas = GameObject.Find ("Canvas");
	}
	
	// Update is called once per frame
	void Update () {
		//transform.localPosition = Input.mousePosition-canvas.transform.position;
		//GetComponent<RectTransform> ().sizeDelta = new Vector2 (txt.GetComponent<RectTransform> ().sizeDelta.x + 5f, txt.GetComponent<RectTransform> ().sizeDelta.y + 10f);
	}
}
