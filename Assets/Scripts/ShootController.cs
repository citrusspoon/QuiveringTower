﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootController : MonoBehaviour {
	public GameObject arrowPrefab;
	private GameObject arrowClone;
	public Transform playerFace;
	public Transform firePoint;
	public float shootForce = 500.0f;
	public Quaternion rotation = Quaternion.identity;

	void Update () {
		if (Input.GetButtonDown ("Fire1")) {
			shootArrow ();
		}
	}

	void shootArrow () { 
		arrowClone = Instantiate (arrowPrefab, firePoint.position, rotation);	//	Create the arrow
		arrowClone.GetComponent<Rigidbody> ().AddForce (playerFace.forward * shootForce);	//	Add force to it
		destroyClone ();
	}

	private void destroyClone () {
		Destroy (arrowClone, 2);
	}
}