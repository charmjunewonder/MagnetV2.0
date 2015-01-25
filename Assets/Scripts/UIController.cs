﻿using UnityEngine;
using System.Collections;
using Game;

public class UIController : MonoBehaviour {

	public GameObject[] thumbs;
	public GameObject[] sliders;
	public UILabel gameoverLal;
	public UILabel scoreLal;
	public UILabel restartLal;
	public UILabel highscoreLal;

	private string highscoreKey = "HighScore";

	// Use this for initialization
	void Start () {
		foreach (GameObject slider in sliders) {
			slider.GetComponentInChildren<UISlider>().value = Random.Range(0.0f,1.0f);		
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void disableButtons(){
		foreach(GameObject thumb in thumbs){
			thumb.GetComponent<BoxCollider>().enabled = false;
		}
	}

	public void ShowEndUI(){
		disableButtons ();
		gameoverLal.enabled = true;
		scoreLal.enabled = true;
		restartLal.enabled = true;
		highscoreLal.enabled = true;
		scoreLal.text = string.Format ("Score : {0}",GameData.TotalScore);
		int highscore = 0;
		if (PlayerPrefs.HasKey(highscoreKey)) {
			highscore = PlayerPrefs.GetInt(highscoreKey);
		}
		highscore = Mathf.Max (highscore, GameData.TotalScore);
		PlayerPrefs.SetInt(highscoreKey, highscore);
		highscoreLal.text = string.Format("HighScore:{0}",highscore);
	}
}
