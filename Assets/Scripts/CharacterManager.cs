using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour {

	public GameObject centerCharacter;
	public GameObject periphericCharacter;
	private Rigidbody2D centerRigidbody;
	private Rigidbody2D periphericRigidbody;
	private bool grounded;
	public float jumpForce;
	public float movementSpeed;
	public GameObject planet;

	// Use this for initialization
	void Start () {
		grounded = true;
		centerRigidbody = centerCharacter.GetComponent<Rigidbody2D> ();
		periphericRigidbody = periphericCharacter.GetComponent<Rigidbody2D> ();
		periphericRigidbody.centerOfMass = (planet.transform.position - this.transform.position) / 3;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (Input.GetButton ("Jump")) {
			Jump ();
		}
		if (Input.GetButton ("Horizontal")) {
			MoveLeftRight (-(Mathf.Sign(Input.GetAxisRaw("Horizontal"))));
		}
	}

	public void Jump() {
		if (grounded) {
			periphericRigidbody.AddRelativeForce(new Vector2 (0.0f, periphericRigidbody.velocity.y + jumpForce));
		}
	}

	public void MoveLeftRight(float direction) {
		//if (centerRigidbody.velocity.magnitude < movementSpeed) {
			centerRigidbody.angularVelocity = direction * movementSpeed;
		//}
	}

	public void OnFeetTriggerEnter(Collider2D col) {
		if (col.gameObject.tag == "Planet") {
			grounded = true;
		}
	}

	public void OnFeetTriggerExit(Collider2D col) {
		if (col.gameObject.tag == "Planet") {
			grounded = false;
		}
	}
}
