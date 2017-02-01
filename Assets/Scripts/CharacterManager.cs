using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour {

	public GameObject characterGo;
	private Rigidbody2D characterRb;
	private bool grounded;
	public float jumpForce;
	public float movementSpeed;
	public float rotationForce;
	public GameObject planet;

	// Use this for initialization
	void Start () {
		grounded = true;
		characterRb = characterGo.GetComponent<Rigidbody2D> ();
		//periphericRigidbody.centerOfMass = (planet.transform.position - this.transform.position) / 3;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		KeepUpright ();
		if (Input.GetButtonDown ("Jump")) {
			Jump ();
		}
		if (Input.GetButton ("Horizontal")) {
			MoveLeftRight (-(Mathf.Sign(Input.GetAxisRaw("Horizontal"))));
		}
		ApplyGravity ();
	}

	private void Jump() {
		if (grounded) {
			characterRb.AddRelativeForce(new Vector2 (0.0f, characterRb.velocity.y + jumpForce * Time.fixedDeltaTime), ForceMode2D.Force);
		}
	}

	private void MoveLeftRight(float direction) {
		
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

	private void ApplyGravity () {
		Vector2 v = this.transform.position - planet.transform.position;
		float magnitude = Mathf.Abs (v.x) + Mathf.Abs (v.y);
		characterRb.AddForce ((v / magnitude) * StaticData.gravity * Time.fixedDeltaTime);
	}

	private void KeepUpright() {
		Vector2 towardGround = planet.transform.position - characterGo.transform.position;
		RaycastHit2D hit = Physics2D.Raycast (characterGo.transform.position, towardGround);
		if (hit.collider != null) {
			float groundAngle = (hit.normal.x >= 0) ? 360.0f - Vector2.Angle (hit.normal, Vector2.up) : Vector2.Angle (hit.normal, Vector2.up);
			float charAngle = characterGo.transform.localEulerAngles.z;
			if (Mathf.Abs(groundAngle - charAngle) > 3.0f) {
				float direction = Mathf.Sign (groundAngle - charAngle);
				if ((groundAngle - charAngle) > 180.0f) {
					direction = -1.0f;
				} else if ((groundAngle - charAngle) > 180.0f) {
					direction = 1.0f;
				}
				Debug.Log ("Ground: " + groundAngle + " Character: " + charAngle + " Direction: " + direction);
				characterRb.angularVelocity = direction * rotationForce;	
			} else {
				characterRb.angularVelocity = characterRb.angularVelocity / 4;
			}
		}
	}
}
