﻿using UnityEngine;
using System.Collections;

public class ChargeController : MonoBehaviour {

	public int score = 1;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator dieInSeconds(){
		yield return new WaitForSeconds (5);
		gameObject.SetActive(false);
	}
	void OnTriggerEnter2D(Collider2D other) {
		other.gameObject.GetComponent<MagnetController>().score += score;

        gameObject.SetActive(false);
    }
}
