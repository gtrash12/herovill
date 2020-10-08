/*
using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Icon : MonoBehaviour,IPointerUpHandler,IPointerDownHandler {
	public GameObject MyCha;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (MyCha != null) {
			transform.Find ("HpBar").GetComponent<UnityEngine.UI.Image> ().fillAmount = (float)MyCha.GetComponent<PlayerScript> ()._hp / MyCha.GetComponent<PlayerScript> ()._hpMax;
		}else{
			transform.Find ("HpBar").GetComponent<UnityEngine.UI.Image> ().fillAmount = 0;
		}
	}
	public void OnPointerUp(PointerEventData data){
		if (MyCha != null) {
			if (data.pointerCurrentRaycast.gameObject.CompareTag ("Button")) {
				MyCha.SendMessage ("useSkill", MyCha.GetComponent<PlayerScript> ().skill);
			}
		}
	}

	public void OnPointerDown(PointerEventData data){
		if (MyCha != null) {
			GameObject.Find ("MC").GetComponent<CameraScript> ().currentSellect = MyCha;
		}
	}
}
*/