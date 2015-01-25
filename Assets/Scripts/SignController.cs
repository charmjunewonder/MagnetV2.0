using UnityEngine;
using System.Collections;

public class SignController : MonoBehaviour {
	public Sprite positveSign;
	public Sprite negativeSign;
	public float sign;
	// Use this for initialization
	void Start () {
		sign = 0;
	}

	public void ChangeSign(float s){
		Sprite targetSprite = null;

		this.transform.localScale = new Vector3 (1, 1, 1) * Mathf.Abs (s) * 0.5f;
		if (s > 0) {
			targetSprite = positveSign;
			s = 1;
		}
		if (s < 0) {
			targetSprite = negativeSign;
			s = -1;
		}
//		Debug.Log (s + " " + sign);
		if (s != sign) {
//			Debug.Log ("change");

			sign = s;
			GetComponent<SpriteRenderer> ().sprite = targetSprite;
		}
	}

	public void SetSign(float s){
		if (s > 0) {
			GetComponent<SpriteRenderer> ().sprite = positveSign;
		}
		if (s < 0) {
			GetComponent<SpriteRenderer> ().sprite = negativeSign;
		}
	}

	// Update is called once per frame
	void Update () {
	
	}
}
