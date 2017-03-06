﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RulesManager : MonoBehaviour {

	public int playerScore = 0;

	public static RulesManager manager = null;
	public bool playerCanShoot = true;
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
		playerCanShoot = false;
	}

	void FixedUpdate(){
		// If a block has been downed this turn check the tower state
		if(blockDownThisTurn && !isTowerMoving(GameObject.Find("Block Tower"))){
			nextTurn();
		};
	}

	public bool isTowerMoving(GameObject tower){
		foreach(Rigidbody rigidbody in tower.GetComponentsInChildren<Rigidbody>()){
			if(!rigidbody.IsSleeping()) {
				print(rigidbody.gameObject.transform.parent.name.ToString() + " Not Sleeping");
				return true;
			}
		}
		return false;
	}

	public void nextTurn(){
		print("Next Turn");
		blockDownThisTurn = false;
		playerCanShoot = true;
	}
}