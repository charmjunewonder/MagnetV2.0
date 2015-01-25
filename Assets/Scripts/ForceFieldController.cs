using UnityEngine;
using System.Collections;

public class ForceFieldController : MonoBehaviour {

	public float quantityOfCharge = 30.0f;

	// Use this for initialization
	void Start () {
	
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
