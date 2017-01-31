﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour {
	public enum ArrowType {Push,Pull,Dissolve};

	public Rigidbody arrow;
	public ArrowType type; 
	private Quaternion lastOrientation;

	private Ray movementRay;
	private RaycastHit raycastResult;
	private Transform arrowWillStick = null;

	void Start ()
	{
		movementRay = new Ray ();
		lastOrientation = transform.rotation; // Store initial orientation of the arrow
	}

	void FixedUpdate ()
	{

		movementRay.origin = transform.position;
		movementRay.direction = arrow.velocity;


		if (arrowWillStick != null) {
				//Stop the arrow
				arrow.isKinematic = true;

				// Stick to the surface shot
				transform.parent = arrowWillStick;
		}

		if (arrow.velocity.magnitude > 0.1) {		// If the arrow is flying, orient it with the velocity
			//TODO Optimize this script by moving the GetComp.
			transform.rotation = Quaternion.LookRotation (arrow.velocity) * Quaternion.Euler (Vector3.right * 90);
			lastOrientation = transform.rotation;

			//Debug.Break();
			// Test for possible hits in the next frame
			if (Physics.Raycast (movementRay, out raycastResult, GetComponent<Rigidbody> ().velocity.magnitude * 0.02f)) {

				if (raycastResult.rigidbody != null) {
					//Arrow effects
					switch (type) {
					case ArrowType.Push:
						raycastResult.rigidbody.AddForceAtPosition (arrow.velocity * 10, raycastResult.point, ForceMode.Impulse);
						break;
					case ArrowType.Pull:
						raycastResult.rigidbody.AddForceAtPosition (arrow.velocity * -10, raycastResult.point, ForceMode.Impulse);
						break;
					case ArrowType.Dissolve:
						Destroy (raycastResult.rigidbody.gameObject);
						break;
					default:
						break;
					}
				}

				arrowWillStick = raycastResult.transform;
					
				//Stop the arrow
				arrow.isKinematic = true;

				// Stick to the surface shot
				transform.position = raycastResult.point;
				GetComponent<Rigidbody> ().MovePosition (raycastResult.point);
				transform.SetParent(raycastResult.transform);
			}

		} else {
			transform.rotation = lastOrientation;	// If the arrow stops retain last known orientation
		}

	}

}
