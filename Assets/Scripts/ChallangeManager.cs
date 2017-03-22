﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChallangeManager : RulesManager {

	/// Defines the rules of the challanges

	// Store a reference to the tower
	private GameObject tower; 
	[SerializeField] ChallangeUIController uiController;
	private GameController gameController;
	private bool blockRemoved = false;

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
		} else if (block.challangeType == BlockController.ChallangeType.Goal){
			block.challangeType = BlockController.ChallangeType.Normal;
		}

		// A block was blockRemoved
		blockRemoved = true;
	}

    public override void blockHit(BlockController block){
		// Check if the player was allowed to hit the block
		if (block.challangeType == BlockController.ChallangeType.DoNotShoot){
			gameController.pauseGame();
			uiController.challangeFail();
		}
	}

	void FixedUpdate(){
		// Wait until tower stops moving
		if (blockRemoved && !isTowerMoving(tower) ){
			// Check challange status now
			if (isChallangeComplete()){
				uiController.challangeSuccess();
			} else {

			}
		}
	}

	public void NextChallange (){
		// Challange success. Move to the next one
		uiController.challangeSuccess();
	}

	private bool isChallangeComplete(){
		// Check each block in the tower
		// If there are any blocks marked as Goal return false
		foreach(BlockController block in tower.GetComponentsInChildren<BlockController>()){
			if (block.challangeType == BlockController.ChallangeType.Goal) {
				return false;
			}
		}
	return true;
	}
}
