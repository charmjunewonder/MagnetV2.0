using UnityEngine;
using System.Collections;
namespace Game {
    public class GameManager : MonoBehaviour
    {
        public TextMesh scoreTex;
        // Use this for initialization
        void Start()
        {
            GameData.TotalScore = 0;
        }

        // Update is called once per frame
        void Update()
        {

        }
        void FixedUpdate() {
            scoreTex.text = GameData.TotalScore.ToString();
        }
    }

}
