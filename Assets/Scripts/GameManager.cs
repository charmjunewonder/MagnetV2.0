using UnityEngine;
using System.Collections;
namespace Game {
	public enum GameState{
		Start = 0,
		Game,
		End,
	}


    public class GameManager : MonoBehaviour
    {
		private static bool isStart = true;
		public static GameState gameState;
        public TextMesh scoreTex;
        public Blur cameraBlur;

        public float reduceSpeed = 0.005f;

		public Material lifeBar;

		public UIController uicontroller;

        private bool isEnding = false;

        // Use this for initialization
        void Start()
        {
            GameData.resetGameData();

            isEnding = false;
			if (isStart) {
				gameState = GameState.Start;
			} else {
				gameState = GameState.Game;
				StartGame();
			}
        }

        // Update is called once per frame
        void Update()
        {
			switch (gameState) {
			case GameState.Start:
				if(Input.touchCount != 0 && Input.touches[0].tapCount >= 1 || Input.GetKeyDown(KeyCode.Space)){
					StartGame();
				}
				break;
			case GameState.Game:
				GameData.chargeLife -= reduceSpeed * Time.deltaTime;
				Mathf.Clamp(GameData.chargeLife, 1.0f, 10.0f);
				break;
			case GameState.End:
				if((Input.touchCount != 0 && Input.touches[0].tapCount == 2) || Input.GetKeyDown(KeyCode.Space))
					RestartGame();
				break;
			default: 
				break;
			}
            
        }


        void FixedUpdate() {
            scoreTex.text = GameData.TotalScore.ToString();
        }

		public void StartGame(){
			uicontroller.HideStartUI ();
			cameraBlur.blurSize = 0.0f;
			gameState = GameState.Game;
			isStart = false;
			StartCoroutine (lifeDecrease ());
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
			uicontroller.ShowEndUI ();
			gameState = GameState.End;
        }

		IEnumerator lifeDecrease(){
			lifeBar.SetFloat("_Amount", 5.1f);
			yield return new WaitForSeconds(1.0f);
			float speed = 0.005f;
			int count = 0;
			while (true) {
				if(count % 100 == 0) speed += 0.005f;
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
