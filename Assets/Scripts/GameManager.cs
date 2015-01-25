﻿using UnityEngine;
using System.Collections;
namespace Game {
    public class GameManager : MonoBehaviour
    {
        public TextMesh scoreTex;


        public float reduceSpeed = 0.005f;

		public Material lifeBar;

        // Use this for initialization
        void Start()
        {
            GameData.TotalScore = 0;
			StartCoroutine (lifeDecrease ());

        }

        // Update is called once per frame
        void Update()
        {
            GameData.chargeLife -= reduceSpeed * Time.deltaTime;
            Mathf.Clamp(GameData.chargeLife, 1.0f, 10.0f);
        }


        void FixedUpdate() {
            scoreTex.text = GameData.TotalScore.ToString();
        }

		IEnumerator lifeDecrease(){
			lifeBar.SetFloat("_Amount", 5.1f);
			yield return new WaitForSeconds(1.0f);
			float speed = 0.1f;
			int count = 0;
			while (true) {
				if(count % 10 == 0) speed += 0.1f;
				GameData.LifeAmout -= speed;
				GameData.LifeAmout = Mathf.Clamp(GameData.LifeAmout, -5.1f, 5.1f);
				lifeBar.SetFloat("_Amount", GameData.LifeAmout);
				yield return new WaitForSeconds(1.0f);
				count++;
			}
		}
    }

}
