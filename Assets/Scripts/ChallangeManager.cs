using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChallangeManager : RulesManager {

	/// Defines the rules of the challanges

	// Store a reference to the tower
	private GameObject tower; 
	[SerializeField] ChallangeUIController uiController;
	private GameController gameController;

	// Use this for initialization
	void Start () {
		// Setup tower
		// TODO Singleton pattern
		manager = this;
		tower = GameObject.FindGameObjectWithTag("Tower");

		gameController = GameController.controller;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public override void blockFallen(BlockController block){
		// Check if the player was required / allowed to remove the block from tower
		if (block.challangeType == BlockController.ChallangeType.DoNotRemove){
			gameController.pauseGame();
			uiController.challangeFail();
		}
	}

    public override void blockHit(BlockController block){
		// Check if the player was allowed to hit the block
		if (block.challangeType == BlockController.ChallangeType.DoNotShoot){
			gameController.pauseGame();
			uiController.challangeFail();
		}
	}

	void FixedUpdate(){
		// Check challange status
	}

	public void NextChallange (){
		// Challange success. Move to the next one
	}
}
