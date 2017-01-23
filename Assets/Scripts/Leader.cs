using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leader {
	
	public int iterationNumber;
	public string playerName;
	public Time date;
	public float distance;
	public float score;

	public Leader(int iterationNumber, string playerName, Time date, float distance, float score) {
		this.iterationNumber = iterationNumber;
		this.playerName = playerName;
		this.date = date;
		this.distance = distance;
		this.score = score;
	}
}
