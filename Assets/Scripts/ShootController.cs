using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootController : MonoBehaviour {
	public GameObject arrowPrefab;
	private GameObject arrowClone;
	public Transform firePoint;
	public float shootForce = 500.0f;

	void Update () {
		if (Input.GetButtonDown ("Fire1")) {
			arrowClone = Instantiate (arrowPrefab, firePoint.position, Quaternion.identity);
			arrowClone.GetComponent<Rigidbody> ().isKinematic = false;
			arrowClone.GetComponent<Rigidbody> ().AddForce (Vector3.forward * shootForce);
		}
	}
}
