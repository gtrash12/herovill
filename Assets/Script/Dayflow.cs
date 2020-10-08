using UnityEngine;
using System.Collections;

public class Dayflow : MonoBehaviour {
	float spd = 5.0f;
	void Update () {
		transform.Rotate (0, spd * Time.deltaTime, 0);
		if (transform.rotation.y <= 0.45f && transform.rotation.y >= -0.45f) {
			transform.Rotate (0, spd * Time.deltaTime, 0);
		} else {
			transform.Rotate (0, 3*spd * Time.deltaTime, 0);
		}
	}
}
