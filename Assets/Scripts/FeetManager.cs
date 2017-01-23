using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeetManager : MonoBehaviour {

	public GameObject character;
	private CharacterManager cm;

	// Use this for initialization
	void Start () {
		cm = character.GetComponent<CharacterManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D col) {
		cm.OnFeetTriggerEnter (col);
	}

	void OnTriggerExit2D(Collider2D col) {
		cm.OnFeetTriggerExit (col);
	}
}
