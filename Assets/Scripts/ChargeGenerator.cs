﻿using UnityEngine;
using System.Collections;

public class ChargeGenerator : MonoBehaviour {
	public GameObject charge;
	public ObjectPool objectPool;
	// Use this for initialization
	void Start () {
		StartCoroutine(createCharge());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator createCharge(){
		while(true){
			Vector3 pos = getRandomPosition();
			if(pos.x > 10){
				yield return new WaitForSeconds(3f);
				continue;
			}
			GameObject chargeClone = objectPool.GetObjectFromPool();
			chargeClone.transform.position = pos;
			chargeClone.SetActive(true);
//			chargeClone.GetComponent<ChargeController>().score = Random.Range (-8, 8);

			yield return new WaitForSeconds(3f);
		}
	}

	float Distance2D(Vector3 a, Vector3 b){
		float xd = a.x - b.x;
		float zd = a.y - b.y;
		return Mathf.Sqrt(xd*xd + zd*zd);
	}

	Vector3 getRandomPosition(){
		Vector3 pos;
		GameObject[] magnets = GameObject.FindGameObjectsWithTag("Magnet");
		GameObject[] charges = GameObject.FindGameObjectsWithTag("Charge");
		for(int i = 0; i < 30; i++){
			bool isGood = true;
			pos = new Vector3 (Random.Range (-4.1f, 4.1f), Random.Range (-2.9f, 2.9f), 0);

			for(int j = 0; j < magnets.Length; j++){
//				Debug.Log(Distance2D(pos, magnets[j].transform.position));
				if(Distance2D(pos, magnets[j].transform.position) < 0.43f){
//					Debug.Log("magnets" + Distance2D(pos, magnets[j].transform.position));
					isGood &= false;
				}
			}

			for(int j = 0; j < charges.Length; j++){
				if(charges[j].activeSelf == false) continue;
				if(Distance2D(pos, charges[j].transform.position) < 0.23f){
//					Debug.Log("charges" + Distance2D(pos, charges[j].transform.position));
					isGood &= false;
				}
			}

			if(isGood){
				// Debug.Log("space" + Distance2D(pos, spaceShip.transform.position));
				// for(int j = 0; j < 4; j++){
				// Debug.Log("item" + Distance2D(pos, specialItems[j].transform.position));
				// }
				return pos;
			}
		}
		Debug.Log ("damn");
		return new Vector3(50, 50, 0);
	}


}
