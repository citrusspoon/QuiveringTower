using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour {

	public bool isTouchingGround = false;
	
	// Keep reference to base blocks so other blocks can rest on them
	[SerializeField] private bool isBaseBlock = false;
	public void OnCollisionStay(Collision collision){
		// If the object we are touching is tagged as ground this block should also be grounded
		if (!isBaseBlock && collision.gameObject.tag == "Ground" && !isTouchingGround){
			isTouchingGround = true;
			gameObject.tag = "Ground";		
			RulesManager.manager.blockFallen();
			}
	}

}
