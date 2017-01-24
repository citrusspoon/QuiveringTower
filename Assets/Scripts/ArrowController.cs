using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour {
	public Rigidbody arrow;

	private Quaternion lastOrientation;

	void OnCollisionEnter (Collision col) {	//	When the arrow hits something
		ArrowStick (col);
	}

	void ArrowStick (Collision col) {
		arrow.isKinematic = true;	//	Stop the arrow
		arrow.detectCollisions = false;
		transform.parent = col.transform;	//	Make the arrow a child of the gameobject it hits

	}

	void FixedUpdate(){
		if (!arrow.isKinematic) {
			transform.rotation = Quaternion.LookRotation (GetComponent<Rigidbody> ().velocity) * Quaternion.Euler (Vector3.right * 90);
			lastOrientation = transform.rotation;
		} else {
			transform.rotation = lastOrientation;
		}
	}
}
