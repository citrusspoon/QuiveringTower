using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour {

public enum ChallengeType{Normal, Goal, DoNotRemove, DoNotShoot}
public ChallengeType challengeType;


	public bool isTouchingGround = false;
	public bool isInTower = true;
	private int frozenTurns = 0;
	public void OnCollisionStay(Collision collision){
		// If a block is within the tower it cannot be grounded
		// If the object we are touching is tagged as ground this block should also be grounded
		if (!isInTower && collision.gameObject.tag == "Ground" && !isTouchingGround){
			isTouchingGround = true;
			gameObject.tag = "Ground";		
			RulesManager.manager.blockFallen(this);
			}
	}

	public void OnTriggerExit(){
		isInTower = false;
	}

	public void setFreezeTurns(int turns){
		frozenTurns = turns;
		if (frozenTurns > 0){
			GetComponent<Renderer>().material.color = Color.cyan;
			GetComponent<Rigidbody>().isKinematic  =true;
		} else {			
			GetComponent<Renderer>().material.color = Color.white;
			GetComponent<Rigidbody>().isKinematic  =false;
		}
	}

	public void nextTurn(){
		setFreezeTurns(frozenTurns - 1);
	}

	public void blockHit(){
		RulesManager.manager.blockHit(this);
	}



}
