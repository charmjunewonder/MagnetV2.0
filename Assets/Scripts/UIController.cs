using UnityEngine;
using System.Collections;

public class UIController : MonoBehaviour {

	public GameObject[] thumbs;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void disableButtons(){
		foreach(GameObject thumb in thumbs){
			thumb.GetComponent<BoxCollider>().enabled = false;
		}
	}
}
