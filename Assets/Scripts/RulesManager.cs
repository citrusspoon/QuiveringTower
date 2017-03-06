using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RulesManager : MonoBehaviour {

	public int playerScore = 0;

	public static RulesManager manager = null;

	private bool blockDownThisTurn = false;
	// Use this for initialization
	void Start () {
		if (manager == null){
			RulesManager.manager = this;
		} else {
			Destroy(this);
		}
	}
	
	public void blockFallen(){
		if (!blockDownThisTurn){
			playerScore += 50;
			blockDownThisTurn = true;
		} else {
			playerScore -= 10;
		}
		print (playerScore);
	}
}
