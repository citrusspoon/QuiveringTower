using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour {

	public bool isTouchingGround = false;
	public bool isInTower = true;
	public void OnCollisionStay(Collision collision){
		// If a block is within the tower it cannot be grounded
		// If the object we are touching is tagged as ground this block should also be grounded
		if (!isInTower && collision.gameObject.tag == "Ground" && !isTouchingGround){
			isTouchingGround = true;
			gameObject.tag = "Ground";		
			RulesManager.manager.blockFallen();
			}
	}

	public void OnTriggerExit(){
		isInTower = false;

	}

}
