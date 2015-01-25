using UnityEngine;
using System.Collections;
using Game;

public class ChargeController : MonoBehaviour {

	public int score = 1;
    public float life;
    private float dangerLife = 1.0f;
    private bool danger;
    private SpriteRenderer render;
    public AudioClip scoring;
	// Use this for initialization
	void Start () {
        life = GameData.chargeLife;
        danger = false;
        render = GetComponentInChildren<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
//        life -= Time.deltaTime;
//        if (life < 0) {
//            gameObject.SetActive(false);
//        }
//        if (life < dangerLife && !danger) {
//            danger = true;
//            StartCoroutine(flashInSeconds());
//        }
	}
    public void ResetLife() {
        life = GameData.chargeLife;
        danger = false;
    }

    IEnumerator flashInSeconds() {
        for (int i = 0; i < 10; i++) {
            yield return new WaitForSeconds(0.1f);
            render.enabled = !render.enabled;
        }
    }
	public void startToDie(){
		StartCoroutine (dieInSeconds ());
	}
	IEnumerator dieInSeconds(){
		float seconds = Random.Range (2.5f, 5.0f);
		yield return new WaitForSeconds (seconds);
		for (int i = 0; i < 10; i++) {
			yield return new WaitForSeconds(0.1f);
			render.enabled = !render.enabled;
		}
		gameObject.SetActive(false);
	}
	void OnTriggerEnter2D(Collider2D other) {
		GameData.TotalScore += score;
		GameData.LifeAmout += 0.2f;
        AudioSource.PlayClipAtPoint(scoring,transform.position);
        gameObject.SetActive(false);
    }
}
