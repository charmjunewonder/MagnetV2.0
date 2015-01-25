using UnityEngine;
using System.Collections;

public class ForceFieldController : MonoBehaviour {
	public GameObject sign;
	public GameObject forceFieldObject;
	public float quantityOfCharge = 30.0f;
	public Renderer selfRenderer;
	// Use this for initialization
	void Start () {
	}

	public void startToDie(){
		StartCoroutine (dieInSeconds ());
	}

	public void startToSpin(){
		StopCoroutine ("startSpinning");
		StartCoroutine ("startSpinning");
	}

	IEnumerator startSpinning(){
		while (true) {
			forceFieldObject.transform.Rotate(0, 0, -5);
			yield return new WaitForSeconds(0.05f);
		}
	}
	
	IEnumerator dieInSeconds(){
		yield return new WaitForSeconds (10);
		for (int i = 0; i < 10; i++) {
			yield return new WaitForSeconds(0.1f);
			selfRenderer.enabled = !selfRenderer.enabled;
		}
		gameObject.SetActive(false);
	}

	public void SetSign(int s){
		quantityOfCharge = s * 30;
		sign.GetComponent<SignController> ().SetSign (s);
	}

	// Update is called once per frame
	void FixedUpdate () {
		GameObject[] magnets = GameObject.FindGameObjectsWithTag("Magnet");
		GameObject strongest = null;
		float max = float.MinValue;
		for (int i = 0; i < magnets.Length; i++) {
			Vector3 direction = magnets[i].transform.position - transform.position;
			float targetCharge = magnets[i].GetComponent<MagnetController>().quantityOfCharge;
			if (direction.magnitude < 1f && targetCharge * quantityOfCharge < 0) return;
			float distance = Vector3.Distance(magnets[i].transform.position, transform.position);
			float force = targetCharge * quantityOfCharge / (distance * distance);
			direction.Normalize();
			Vector2 direction2d = new Vector2(direction.x,direction.y);
			magnets[i].rigidbody2D.AddForce(direction2d * force);
		}
	}
}
