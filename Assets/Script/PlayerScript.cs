using UnityEngine;
using System.Collections;
 
public class PlayerScript : MonoBehaviour{
	public bool MoveChk = false;
	CharacterController CC;
	public RaycastHit hit;
	float Distancedir;
	float Distancey;
	[HideInInspector]
	public Animator anim;
	public GameObject pointImg;
	[HideInInspector]
	public bool UIChk;


	void Start () {
		CC = GetComponent<CharacterController>();
		anim = transform.Find ("Cha").GetComponent<Animator> ();
	}

	void Update(){
		if (UIChk == false) {
			if (Input.GetMouseButton (0)) {
				Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hit, Mathf.Infinity, 1 << 8);
				if(hit.point.x > transform.position.x){
					anim.SetBool ("RightChk", true);
					pointImg.transform.position = new Vector3(hit.point.x - 0.2f, hit.point.y + 0.5f,hit.point.z);
				}else{
					anim.SetBool ("RightChk", false);
					pointImg.transform.position = new Vector3(hit.point.x + 0.2f, hit.point.y + 0.5f,hit.point.z);
				}
				if (!MoveChk)
					MoveChk = true;
			}
			if (MoveChk) {
				Distancedir = Vector3.Distance (hit.point, transform.localPosition);
				Distancey = Mathf.Abs (hit.point.y - transform.localPosition.y);
				if (Distancedir > Distancey + 0.01f) {   
					CC.Move ((hit.point - transform.position).normalized * 10.0f * Time.deltaTime);
					CC.Move (new Vector3 (0, -5f, 0));
					if (!anim.GetBool ("walkChk"))
						anim.SetBool ("walkChk", true);
				} else {
					anim.SetBool ("walkChk", false);
					MoveChk = false;
				}
			}
		}
	}
}