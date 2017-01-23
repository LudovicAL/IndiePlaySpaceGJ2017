using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetRotator : MonoBehaviour {

	public float gravity = 9.81f;
	private Quaternion initialRotation;
	private StaticData.AvailableGameStates gameState;
	public GameObject scriptsBucket;
	public GameObject periphericCharacter;
    public float rotationSpeed;
	private Rigidbody2D planetRigidbody;

	// Use this for initialization
	void Start () {
		scriptsBucket.GetComponent<GameStatesManager> ().MenuGameState.AddListener(OnMenu);
		scriptsBucket.GetComponent<GameStatesManager> ().StartingGameState.AddListener(OnStarting);
		scriptsBucket.GetComponent<GameStatesManager> ().PlayingGameState.AddListener(OnPlaying);
		scriptsBucket.GetComponent<GameStatesManager> ().PausedGameState.AddListener(OnPausing);
		SetCanvasState (scriptsBucket.GetComponent<GameStatesManager> ().gameState);
		initialRotation = this.transform.rotation;
		planetRigidbody = this.GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (gameState == StaticData.AvailableGameStates.Playing) {
			planetRigidbody.angularVelocity = rotationSpeed;
			Vector2 v = new Vector2 (this.transform.position.x - periphericCharacter.transform.position.x, this.transform.position.y - periphericCharacter.transform.position.y);
			float magnitude = Mathf.Abs (v.x) + Mathf.Abs (v.y);
			Physics2D.gravity = new Vector2 ((v.x / magnitude) * gravity, (v.y / magnitude) * gravity);
		}
	}

	//Reinitialize the world
	public void Reinitialize() {
		this.transform.rotation = initialRotation;
	}

	protected void OnMenu() {
		SetCanvasState (StaticData.AvailableGameStates.Menu);
	}

	protected void OnStarting() {
		SetCanvasState (StaticData.AvailableGameStates.Starting);
	}

	protected void OnPlaying() {
		SetCanvasState (StaticData.AvailableGameStates.Playing);
	}

	protected void OnPausing() {
		SetCanvasState (StaticData.AvailableGameStates.Paused);
	}

	public void SetCanvasState(StaticData.AvailableGameStates state) {
		gameState = state;
	}
}
