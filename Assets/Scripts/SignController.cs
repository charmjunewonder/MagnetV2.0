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
		if (s > 0) {
			targetSprite = positveSign;
			s = 1;
		}
		if (s < 0) {
			targetSprite = negativeSign;
			s = -1;
		}
		if (s != sign) {
			sign = s;
			GetComponent<SpriteRenderer> ().sprite = targetSprite;
		}
	}

	// Update is called once per frame
	void Update () {
	
	}
}
