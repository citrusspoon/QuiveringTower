using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveArrowController : MonoBehaviour {
	public Rigidbody arrow;

	void OnCollisionEnter (Collision col) {	//	When the arrow hits something
		ArrowStick (col);
	}

	void ArrowStick (Collision col) {
		arrow.isKinematic = true;	//	Stop the arrow
	
		transform.parent = col.transform;	//	Make the arrow a child of the gameobject it hits
        Destroy(col.gameObject);

	}
}
