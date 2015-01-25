using UnityEngine;
using System.Collections;

public class ForceFieldController : MonoBehaviour {
	public GameObject sign;
	public GameObject forceFieldObject;
	public float quantityOfCharge = 30.0f;
	public Renderer selfRenderer;
	GameObject forceFieldClone;
	// Use this for initialization
	void Start () {
	}


	public void startToDie(){
		StartCoroutine (dieInSeconds ());
	}

	public void startToDiffuse(){
		StopCoroutine ("diffuse");
		StartCoroutine ("diffuse");
	}


	IEnumerator dieInSeconds(){
		yield return new WaitForSeconds (10);
//		for (int i = 0; i < 10; i++) {
//			yield return new WaitForSeconds(0.1f);
//			selfRenderer.enabled = !selfRenderer.enabled;
//		}
		gameObject.SetActive(false);
		forceFieldClone.SetActive (false);
		Destroy (forceFieldClone);

	}

	IEnumerator diffuse(){
		while (true) {
			forceFieldClone = Instantiate(forceFieldObject) as GameObject;
			forceFieldClone.SetActive(true);
			forceFieldClone.transform.parent = transform;
			forceFieldClone.transform.position = transform.position;
			forceFieldClone.transform.localScale = Vector3.one * 0.1f;
			StartCoroutine(disappearInSeconds(forceFieldClone));
			yield return new WaitForSeconds(1.05f);
		}
	}

	IEnumerator disappearInSeconds(GameObject forceFieldClone){
		for (int i = 0; i < 20; i++) {
			yield return new WaitForSeconds(0.05f);
			Color c = forceFieldClone.GetComponent<SpriteRenderer>().color;
			forceFieldClone.transform.localScale *= 1.1f;
			c.a -= 0.05f;
			forceFieldClone.GetComponent<SpriteRenderer>().color = c;
		}
		forceFieldClone.SetActive (false);
		Destroy (forceFieldClone);
	}

	public void SetSign(int s){
		quantityOfCharge = s * 30;
		sign.GetComponent<SignController> ().SetSign (s);
	}

	void FixedUpdate () {
		GameObject[] magnets = GameObject.FindGameObjectsWithTag("Magnet");
		GameObject strongest = null;
		float max = float.MinValue;
		for (int i = 0; i < magnets.Length; i++) {
			Vector3 direction = magnets[i].transform.position - transform.position;

//			if(direction.magnitude < 2.5f){
//
//			}

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
