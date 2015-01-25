using UnityEngine;
using System.Collections;

public class ForceFieldController : MonoBehaviour {

	public float quantityOfCharge = 50.0f;

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
			if (direction.magnitude < 1f) return;
			float distance = Vector3.Distance(magnets[i].transform.position, transform.position);
			float force = -1.0f * magnets[i].GetComponent<MagnetController>().quantityOfCharge * quantityOfCharge / (distance * distance);
			direction.Normalize();
			Vector2 direction2d = new Vector2(direction.x,direction.y);
			magnets[i].rigidbody2D.AddForce(direction2d * force);
		}
	}
}
