using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockTester : MonoBehaviour {

	public Vector3 forceToApply;
	public GameObject objectToMove;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.anyKey) {
			print ("Applying the force for test");
			objectToMove.GetComponent<Rigidbody> ().AddForce (forceToApply);
		}
	}
}
