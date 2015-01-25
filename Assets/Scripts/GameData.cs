using UnityEngine;
using System.Collections;
namespace Game {
    public class GameData
    {

        public static int TotalScore = 0;
		public static float LifeAmout = 5.1f;
        public static float chargeLife = 6.0f;

        public static void resetGameData() {
            TotalScore = 0;
            LifeAmout = 5.1f;
            chargeLife = 6.0f;
        }
    }
}
