using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour {
	public enum ArrowType {Push,Pull,Dissolve};

	public Rigidbody arrow;
	public ArrowType type; 
	private Quaternion lastOrientation;





	private Ray movementRay;
	private RaycastHit raycastResult;

	void Start ()
	{
		movementRay = new Ray ();
		lastOrientation = transform.rotation; // Store initial orientation of the arrow
	}

	void OnCollisionEnter (Collision col) {	//	When the arrow hits something
		ArrowStick (col);
	}

	void ArrowStick (Collision col) {
		arrow.isKinematic = true;	//	Stop the arrow
		arrow.detectCollisions = false;
		transform.parent = col.transform;	//	Make the arrow a child of the gameobject it hits

	}

	void FixedUpdate(){
		if (arrow.velocity.magnitude>0.1) {		// If the arrow is flying, orient it with the velocity
			//TODO Optimize this script by moving the GetComp.
			transform.rotation = Quaternion.LookRotation (arrow.velocity) * Quaternion.Euler (Vector3.right * 90);
			lastOrientation = transform.rotation;


			// Test for possible hits in the next frame
			if (Physics.Raycast(movementRay,out raycastResult,GetComponent<Rigidbody> ().velocity.magnitude)){

				//Arrow effect
				switch (type) {
				case ArrowType.Push:
					raycastResult.rigidbody.AddForceAtPosition (arrow.velocity * 10, raycastResult.point, ForceMode.Impulse);
					break;
				case ArrowType.Pull:
					raycastResult.rigidbody.AddForceAtPosition (arrow.velocity * -10, raycastResult.point, ForceMode.Impulse);
					break;
				default:
					break;
				}



				//arrow.velocity = Vector3.zero;
				arrow.isKinematic = true;
				transform.position = raycastResult.point;
				transform.parent = raycastResult.transform;
			}

		} else {
			transform.rotation = lastOrientation;	// If the arrow stops retain last known orientation
		}
	}


	void Update(){
		// Setup ray for raycasting
		movementRay.origin = transform.position;
		movementRay.direction = arrow.velocity;
	}
}
