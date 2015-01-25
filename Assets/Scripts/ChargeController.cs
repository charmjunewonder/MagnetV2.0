using UnityEngine;
using System.Collections;
using Game;

public class ChargeController : MonoBehaviour {

	public int score = 1;
	// Use this for initialization
	void Start () {
        StartCoroutine(dieInSeconds());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator dieInSeconds(){
		yield return new WaitForSeconds (5);
		gameObject.SetActive(false);
	}
	void OnTriggerEnter2D(Collider2D other) {
		GameData.TotalScore += score;
        gameObject.SetActive(false);
    }
}
