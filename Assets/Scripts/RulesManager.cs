using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RulesManager : MonoBehaviour {

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
	
	public virtual void blockFallen(BlockController block){
		if (!blockDownThisTurn){
			GameController.controller.activePlayer.score += 50;
			blockDownThisTurn = true;
		} else {
			GameController.controller.activePlayer.score -= 10;
		}
		GameController.controller.playerShouldShoot = false;
	}

	public virtual void blockHit(BlockController block){
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
		GameController.controller.playerShouldShoot = true;

		// Reset all blocks kinematics
		foreach (BlockController block in GameObject.FindObjectsOfType<BlockController>()){
			block.nextTurn();
		}

		GameController.controller.nextPlayer();
	}
}
