using UnityEngine;
using System.Collections;
namespace Game {
    public class GameManager : MonoBehaviour
    {
        public TextMesh scoreTex;
        public Blur cameraBlur;

        public float reduceSpeed = 0.005f;

		public Material lifeBar;

        public UILabel gameoverLal;
        public UILabel highscoreLal;
        public UILabel restartLal;

        public bool isEnding = false;
        public bool isEnded = false;

        private string highscoreKey = "HighScore";

        // Use this for initialization
        void Start()
        {
            GameData.resetGameData();
			StartCoroutine (lifeDecrease ());
            isEnding = false;
            isEnded = false;
        }

        // Update is called once per frame
        void Update()
        {
            GameData.chargeLife -= reduceSpeed * Time.deltaTime;
            Mathf.Clamp(GameData.chargeLife, 1.0f, 10.0f);
            if (isEnded)
            {
				if((Input.touchCount != 0 && Input.touches[0].tapCount == 2) || Input.GetKeyDown(KeyCode.Space))
                RestartGame();
            }
        }


        void FixedUpdate() {
            scoreTex.text = GameData.TotalScore.ToString();
        }

        public void RestartGame() {
            Application.LoadLevel("Gameplay");
        }

        public void EndGame() {
            StartCoroutine(EndingGame());
        }

        IEnumerator EndingGame() {
			cameraBlur.enabled = true;
            for (int i = 0; i < 5; i++) {
                yield return new WaitForSeconds(0.1f);
                cameraBlur.blurSize += 1.0f;
            }
            gameoverLal.enabled = true;
            restartLal.enabled = true;
            highscoreLal.enabled = true;
            int highscore = 0;
            if (PlayerPrefs.HasKey(highscoreKey)) {
                highscore = PlayerPrefs.GetInt(highscoreKey);
            }
            PlayerPrefs.SetInt(highscoreKey, Mathf.Max(highscore, GameData.TotalScore));
            highscoreLal.text = string.Format("HighScore:{0}",highscore);
            isEnded = true;
        }

		IEnumerator lifeDecrease(){
			lifeBar.SetFloat("_Amount", 5.1f);
			yield return new WaitForSeconds(1.0f);
			float speed = 0.01f;
			int count = 0;
			while (true) {
				if(count % 100 == 0) speed += 0.01f;
				GameData.LifeAmout -= speed;
                if (GameData.LifeAmout <= -5.1f && !isEnding) {
                    isEnding = true;
                    EndGame();
                }
				GameData.LifeAmout = Mathf.Clamp(GameData.LifeAmout, -5.1f, 5.1f);
				lifeBar.SetFloat("_Amount", GameData.LifeAmout);
				yield return new WaitForSeconds(0.1f);
				count++;
			}
		}
    }

}
