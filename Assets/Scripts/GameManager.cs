using UnityEngine;
using System.Collections;
namespace Game {
    public class GameManager : MonoBehaviour
    {
        public TextMesh scoreTex;


        public float reduceSpeed = 0.01f;

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
			while (true) {
				GameData.LifeAmout -= 0.5f;
				lifeBar.SetFloat("_Amount", GameData.LifeAmout);
				yield return new WaitForSeconds(1.0f);
			}
		}
    }

}
